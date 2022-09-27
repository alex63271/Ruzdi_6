using Microsoft.Extensions.DependencyInjection;
using Ruzdi_6.Commands;
using Ruzdi_6.Model.Applicant_Classes;
using Ruzdi_6.Model.Other_Classes;
using Ruzdi_6.Services;
using Ruzdi_DB.Context;
using Ruzdi_DB.Entityes;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using UploadNotification;

namespace Ruzdi_6.ViewModel
{
    public class VM_For_Win_UP1 : ViewModel
    {
        public VM_For_Win_UP1(DB_Ruzdi db, IWindowService service)
        {
            SourceComboRegion = App.Region_list;

            #region генерируем NotificationId
            App.NotificationId = Guid.NewGuid().ToString(); // генеируем NotificationId 

            #endregion

            #region Объект-пустышка, который сформируется через привязку
            UP1 = new PledgeNotificationToNotary
            {
                NotificationId = App.NotificationId,
                version = 2.3M,
                NotificationData = new NotificationData
                {
                    version = 2.3M,
                    FormUP1 = new FormUP1
                    {
                        CreationReferenceNumber = "",
                        NotificationApplicant = new NotificationApplicant
                        {
                            Role = 2,
                            Organization = new ApplicantOrganization
                            {
                                NameFull = "",
                                UINN = "",
                                URN = "",
                                Email = ""
                            },
                            Attorney = new ApplicantAttorney
                            {
                                Name = new ApplicantAttorneyName
                                {
                                    First = "",
                                    Last = "",
                                    Middle = ""
                                },
                                BirthDate = DateTime.Now,
                                Authority = "",
                                PersonDocument = new ApplicantAttorneyPersonDocument
                                {
                                    Code = 21,
                                    Name = "Паспорт",
                                    SeriesNumber = ""
                                },
                                PersonAddress = new ApplicantAttorneyPersonAddress
                                {
                                    AddressRF = new ApplicantAttorneyPersonAddressAddressRF
                                    {
                                        RegionCode = "",
                                        Region = "",
                                        City = ""
                                    }
                                }
                            }
                        }
                    }
                }
            };
            #endregion

            #region Конструкция чтения хранилища сертификатов и сохранения их перечня в коллекции
            using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                List = new ArrayList();
                ListThumbprint = new ArrayList();
                store.Open(OpenFlags.ReadOnly);
                // Проходим по всем сертификатам 
                foreach (X509Certificate2 cert in store.Certificates)
                {
                    if (cert.NotAfter > DateTime.Now) // выбираем сертификаты с действующим сроком
                    {
                        string zap = ",";
                        string otvet = cert.SubjectName.Name + zap; //добавляем запятую в конец чтобы искался последний элемент в строке
                        if (otvet.Contains("CN=") && otvet.Contains("SN=") && otvet.Contains("G="))  //отсеиваем сертификаты без нужных атрибутов
                        {
                            if (otvet.Contains("ОГРН="))//если есть ОГРН. значит юр лицо
                            {
                                string s = "CN=";
                                string CN = otvet.Substring(otvet.IndexOf(s) + s.Length, otvet.IndexOf(zap, otvet.IndexOf(s)) - (otvet.IndexOf(s) + s.Length));
                                s = "SN=";
                                string SN = otvet.Substring(otvet.IndexOf(s) + s.Length, otvet.IndexOf(zap, otvet.IndexOf(s)) - (otvet.IndexOf(s) + s.Length));
                                s = "G=";
                                string G = otvet.Substring(otvet.IndexOf(s) + s.Length, otvet.IndexOf(zap, otvet.IndexOf(s)) - (otvet.IndexOf(s) + s.Length));
                                string stroka = CN + ", " + SN + " " + G;    //создаем строку для записи её в лист
                                List.Add(stroka);   //записываем строку в лист для отображения в интерфейсе
                                ListThumbprint.Add(cert.Thumbprint);  // лист2 для программного выбора сертификата(содержит отпечатки сертификатов)
                            }
                            else //если физ. лицо
                            {
                                string CN = otvet.Substring(otvet.IndexOf("CN=") + "CN=".Length, otvet.IndexOf(zap, otvet.IndexOf("CN=")) - (otvet.IndexOf("CN=") + "CN=".Length));
                                List.Add(CN);   //записываем строку в лист для отображения в интерфейсе
                                ListThumbprint.Add(cert.Thumbprint);  // лист2 для программного выбора сертификата(содержит отпечатки сертификатов)
                            }
                        }
                    }
                }
            }
            #endregion

            SendNotification_UP1 = new RelayCommand(OnSendNotification_UP1CommandExecute, CanSendNotification_UP1CommandExecute);

            this.vM_ForGlavnaya = App.Host.Services.GetRequiredService<VM_ForGlavnaya>();
            serviceWindow = service;
            this.db = db;

        }

        #region Поля для хранения экземпляров из DI
        private readonly VM_ForGlavnaya vM_ForGlavnaya;
        private readonly IWindowService serviceWindow;
        private readonly DB_Ruzdi db;
        #endregion

        Notification Notification; //экземпляр Entity который создастся в команде

        public bool IsView { get; set; } //флаг просмотра/создания уведомления

        public List<string> SourceComboRegion { get; set; }

        #region UP1 Объект уведомление об исключении залога
        private PledgeNotificationToNotary uP1;

        public PledgeNotificationToNotary UP1
        {
            get => uP1;
            set => Set(ref uP1, value);
        }
        #endregion

        #region List - Список сертфикатов
        public ArrayList List { get; set; }
        #endregion

        #region ListThumbprint - Список, хранящий отпечатки сертификатов
        public ArrayList ListThumbprint { get; set; }
        #endregion

        #region SelectedCertInCombobox - индекс выбранного из списка сертифката
        public int SelectedCertInCombobox { get; set; }
        #endregion

        #region Signxml Метод подписания уведомления
        bool Signxml()
        {
            string csp = "\"C:/Program Files/Crypto Pro/CSP/csptest.exe\"";
            string putxml = App.Temp + App.NotificationId + "/" + App.NotificationId;
            Process Process = Process.Start(new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/C  " + csp + " -sfsign -sign -detached -add -in " + putxml + ".xml -out " + putxml + ".xml.sig -my " + ListThumbprint[SelectedCertInCombobox].ToString(),
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            });
            Process.WaitForExit();  //ждем окончания процесса подписания(на случай если в компе нет ключа)
            string message = Process.StandardError.ReadToEnd();
            if (string.IsNullOrEmpty(message))
            {
                return true;
            }
            else
            {
                MessageBox.Show(message, "Ошибка подписания");
                return false;
            }
        }
        #endregion

        #region Команда отправки уведомления UP1

        public ICommand SendNotification_UP1 { get; }

        public bool CanSendNotification_UP1CommandExecute(object p)
        {
            //должна быть логика проверки, что валидация успешна
            return !IsView
                && UP1.NotificationData.FormUP1.NotificationApplicant.Organization.IsValid
                && UP1.NotificationData.FormUP1.NotificationApplicant.Attorney.IsValid
                && UP1.NotificationData.FormUP1.NotificationApplicant.Attorney.PersonAddress.AddressRF.IsValid
                && UP1.NotificationData.FormUP1.IsValid;
        }

        public async void OnSendNotification_UP1CommandExecute(object p)
        {

            #region создание временных папок для хранения уведомления и подписи
            string TempNotificationId = "Temp/" + App.NotificationId;    //папка для хранения уведомления и подписи
            string TempNotificationIdXml = TempNotificationId + "/" + App.NotificationId + ".xml"; //путь до имени xml файла включительно      
            Directory.CreateDirectory(TempNotificationId);  // создаем папку внутри Temp куда потом сохраним уведомление 
            #endregion

            #region Заполняем код региона заявителя

            if (!(UP1.NotificationData.FormUP1.NotificationApplicant.Attorney is null) && String.IsNullOrEmpty(UP1.NotificationData.FormUP1.NotificationApplicant.Attorney.PersonAddress.AddressRF.RegionCode))
            {
                foreach (var region in db.Regions)
                {
                    if (region.Region == UP1.NotificationData.FormUP1.NotificationApplicant.Attorney.PersonAddress.AddressRF.Region)
                    {
                        UP1.NotificationData.FormUP1.NotificationApplicant.Attorney.PersonAddress.AddressRF.RegionCode = region.CodeRegion;
                    }
                }
            }

            #endregion

            #region сериализация xml
            using (FileStream fs = new FileStream(TempNotificationIdXml, FileMode.OpenOrCreate))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(PledgeNotificationToNotary));
                formatter.Serialize(fs, UP1);
            }
            #endregion

            #region Подписание xml
            if (!Signxml()) //запускаем метод подписания уведомления
            {
                return; //если подписание не прошло - прерываем обработчик события нажатия кнопки
            }
            #endregion

            #region архивирование уведомления и подписи

            string sourceFile = TempNotificationId + "/"; // исходная папка(архивируем все, что в ней есть)
            string compressedFile = "Temp/" + App.NotificationId + ".zip"; // сжатый файл       
            ZipFile.CreateFromDirectory(sourceFile, compressedFile, CompressionLevel.Optimal, false);   // создание архива 
            #endregion

            App.guidp = Convert.ToString(Guid.NewGuid()); //генерируем гуид пкета

            #region Преобразование архива в массив байтов
            using (FileStream fstr = File.OpenRead(compressedFile))
            {//создаем поток из созданного ранее архива
                // преобразуем строку в байты
                byte[] array = new byte[fstr.Length]; //создаем массив байтов длинною в поток архива
                                                      //читаем поток файла
                fstr.Read(array, 0, array.Length); //записываем в массив байты из архива
                App.array = array;          //использую переменную статического класса чтобы вывести значение за using
            }
            #endregion

            #region формирование объекта-пакет
            uploadNotificationPackageRequest package = new uploadNotificationPackageRequest
            {
                pledgeNotificationPackage = new pledgeNotificationPackageType
                {
                    packageId = App.guidp, //присваиваем пакету гуид
                    senderType = (senderTypeType)0, // присваиваем занчение senderType(0)
                    uip = "000000000000000000000TEST", //прописываем УИП
                    pledgeNotificationList = new pledgeNotificationListElementType[1]
                    {
                        new pledgeNotificationListElementType
                        {
                            notificationId = App.NotificationId, // прописываем гуид уведомления
                            documentAndSignature = App.array      //передаем массив байтов в тег documentAndSignature
                        }
                    }
                }
            };
            #endregion

            //Запись сфорированного архива в БД
            using (FileStream fstream = new FileStream(compressedFile, FileMode.OpenOrCreate))
            {
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                string mystr = Convert.ToBase64String(array);
                Notification = new Notification()
                {
                    Id = App.NotificationId,
                    NumberNotification = UP1.NotificationData.FormUP1.CreationReferenceNumber,
                    Packageguid = App.guidp,
                    TypeNotification = "Исключение",
                    ZipArchive = mystr,
                    Status = "Первичная отправка",
                    ThumbprintCert = ListThumbprint[SelectedCertInCombobox].ToString()
                };

                db.Add(Notification);
                db.SaveChanges();
                vM_ForGlavnaya.SourceDatagrid.Add(Notification);

            }

            #region удаляем созданные файлы, т.к. более не нужны
            DirectoryInfo dirInfo = new DirectoryInfo(TempNotificationId);
            dirInfo.Delete(true);
            File.Delete(compressedFile);
            #endregion

            // создаем объект, метод которого может отправить запрос к серверу
            ruzdiUploadNotificationPackageService_v1_1PortTypeClient request = new ruzdiUploadNotificationPackageService_v1_1PortTypeClient(ruzdiUploadNotificationPackageService_v1_1PortTypeClient.EndpointConfiguration.ruzdiUploadNotificationPackageService_v1_1HttpSoap11Endpoint);

            //создание объекта, который может хранить ответ от сервера и записываем в него полученный ответ с помощью метода uploadNotificationPackageAsync
            uploadNotificationPackageResponse response = await request.uploadNotificationPackageAsync(package); // отправляем пакет 

            #region Сохраняем в БД ответный guid
            Notification.Packageid = response.registrationId;
            db.Update(Notification);
            db.SaveChanges(); 
            #endregion

            MessageBox.Show($"Пакет отправлен успешно, рег № пакета -  {response.registrationId}");

            serviceWindow.CloseWindowDialog("UP1");
        }

        #endregion
    }
}
