using Ruzdi_6.Commands;
using Ruzdi_6.Model.Applicant_Classes;
using Ruzdi_6.Model.Other_Classes;
using Ruzdi_6.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Ruzdi_6.ViewModel
{
    public class VM_For_Win_UP1 : ViewModel
    {
        public VM_For_Win_UP1()
        {
            if (App.DesignMode)
            {
                SourceComboRegion = App.Region_list;

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
                                    NameFull = "222",
                                    UINN = "222",
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
                /*
                SendNotification_UP1 = new RelayCommand(OnSendNotification_UP1CommandExecute, CanSendNotification_UP1CommandExecute);
                */
            }
            else
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
                /*
                SendNotification_UP1 = new RelayCommand(OnSendNotification_UP1CommandExecute, CanSendNotification_UP1CommandExecute);
                */
            }
        }

        #region Поля и методы Singletone
        private static VM_For_Win_UP1 VM_UP1;


        public static VM_For_Win_UP1 Get_VM_UP1()
        {
            if (VM_UP1 == null)
            {
                VM_UP1 = new VM_For_Win_UP1();
            }
            return VM_UP1;
        }

        /// <summary>
        /// Метод для сброса экземпляра VW_UZ1
        /// </summary>
        public static void SetNull_VM_UP1()
        {
            VM_UP1 = null;
        }
        #endregion

        #region Св-во для дизайнера
        private static VM_For_Win_UP1 vM_UP1_ForDesiner;


        public static VM_For_Win_UP1 VM_UP1_ForDesiner
        {
            get
            {
                if (vM_UP1_ForDesiner == null)
                {
                    vM_UP1_ForDesiner = new VM_For_Win_UP1();
                }
                return vM_UP1_ForDesiner;
            }
        }

        #endregion

        public bool IsView { get; set; }

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
        /*
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
            Query query = new SelectForSaveAttorneyUP1();

            #region создание временных папок для хранения уведомления и подписи
            string TempNotificationId = "Temp/" + App.NotificationId;    //папка для хранения уведомления и подписи
            string TempNotificationIdXml = TempNotificationId + "/" + App.NotificationId + ".xml"; //путь до имени xml файла включительно      
            Directory.CreateDirectory(TempNotificationId);  // создаем папку внутри Temp куда потом сохраним уведомление 
            #endregion

            #region Заполняем код региона заявителя

            if (!(UP1.NotificationData.FormUP1.NotificationApplicant.Attorney is null) && String.IsNullOrEmpty(UP1.NotificationData.FormUP1.NotificationApplicant.Attorney.PersonAddress.AddressRF.RegionCode))
            {
                query.SetSelect(new GetCodeRegion());
                UP1.NotificationData.FormUP1.NotificationApplicant.Attorney.PersonAddress.AddressRF.RegionCode = query.GetCodeRegion(UP1.NotificationData.FormUP1.NotificationApplicant.Attorney.PersonAddress.AddressRF.Region);
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
                pledgeNotificationPackage = new pledgeNotificationPackageType     //использую инициализатор, т.к. нет перегруженного конструктора
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

            //далее записываем в БД информацию о сформированном пакете
            query.SetSelect(new SaveInfoUP1());
            query.SaveInfoPackageUP1(UP1.NotificationData.FormUP1.CreationReferenceNumber);

            //Запись сфорированного архива в БД
            using (FileStream fstream = new FileStream(compressedFile, FileMode.OpenOrCreate))
            {
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                string mystr = Convert.ToBase64String(array);
                query.SetUpdate(new SaveArchiveNotificationAndSig());
                query.SaveArchiveNitificationAndSig(mystr);
            }

            // удаляем созданные файлы, т.к. более не нужны

            #region удаляем созданные файлы, т.к. более не нужны
            DirectoryInfo dirInfo = new DirectoryInfo(TempNotificationId);
            dirInfo.Delete(true);
            File.Delete(compressedFile);
            #endregion

            // создаем объект, метод которого может отправить запрос к серверу
            ruzdiUploadNotificationPackageService_v1_1PortTypeClient request = new ruzdiUploadNotificationPackageService_v1_1PortTypeClient("ruzdiUploadNotificationPackageService_v1_1HttpSoap11Endpoint");

            //создание объекта, который может хранить ответ от сервера и записываем в него полученный ответ с помощью метода uploadNotificationPackageAsync
            uploadNotificationPackageResponse response = await request.uploadNotificationPackageAsync(package); // отправляем пакет 

            if (response.packageStateCode.code != "0")
            {
                MessageBox.Show($"код ошибки - {response.packageStateCode.code}");
                MessageBox.Show($"код ошибки - {response.packageStateCode.message}");
            }
            query.SetUpdate(new SaveRegistrationID());
            query.SaveRegistrationID(response.registrationId);

            MessageBox.Show($"Пакет отправлен успешно, рег № пакета -  {response.registrationId}");

            #region Обновление коллекции главного окна
            /* query.SetSelect(new UpdateDatagrid());
             VM_ForGlavnaya.Get_VM_ForGlavnaya().DataGridCollection.Source = query.GetCollectionForDataGrid(); // обновление коллекции 
             VM_ForGlavnaya.Get_VM_ForGlavnaya().DataGridCollection.View.Refresh()
            VM_ForGlavnaya.Get_VM_ForGlavnaya().UpdateCollectionDataGrid();
            #endregion

            //ниже цикл поиска открытого окна в коллекции

            IWindowService service = new ServiceWindow();
            service.CloseWindowDialog("UP1");
        }
        */
        #endregion

    }
}
