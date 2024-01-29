using GetNotification;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using Ruzdi_6.Commands;
using Ruzdi_6.Model.Applicant_Classes;
using Ruzdi_6.Model.Other_Classes;
using Ruzdi_6.Model.Pledgee_Classes;
using Ruzdi_6.Model.Property_Classes;
using Ruzdi_6.Model.RegistrationCertificate;
using Ruzdi_6.Model.RegistrationRejectMessage;
using Ruzdi_6.Services;
using Ruzdi_DB.Context;
using Ruzdi_DB.Entityes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Ruzdi_6.ViewModel
{
    public class VM_ForGlavnaya : ViewModel
    {
        public VM_ForGlavnaya(IWindowService service)
        {

            this.db = App.Host.Services.GetRequiredService<DB_Ruzdi>();
            serviceWindow = service;

            SourceDatagrid = new ObservableCollection<Notification>(db.Notifications.Include(p => p.Pledgors).Include(c => c.PledgeContract));

            DataGridCollection = new CollectionViewSource
            {
                Source = SourceDatagrid
            };

            #region Команды
            OpenUZ1Command = new RelayCommand(OnOpenUZ1CommandExecute);
            RenewStatus = new RelayCommand(OnRenewStatusCommandExecute, CanRenewStatusCommandExecute);
            OpenUP1Command = new RelayCommand(OnOpenUP1CommandExecute, CanOpenUP1CommandExecute);

            CopyPackageidCommand = new RelayCommand(OnCopyPackageidCommandExecute, CanCopyPackageidCommandExecute);

            SaveMessageCommand = new RelayCommand(OnSaveMessageCommandExecuteAsync, CanSaveMessageCommandExecute);
            ViewNotificationCommand = new RelayCommand(OnViewNotificationCommandExecute);

            OpenSettings = new RelayCommand(OnOpenSettingsCommandExecute);

            #endregion

            DataGridCollection.Filter += OnFIOfilter;
            DataGridCollection.Filter += OnContractfilter;


        }


        private readonly DB_Ruzdi db;

        private readonly IWindowService serviceWindow;

        #region Команды

        #region RenewStatus - команда обновления статусов
        /// <summary>
        /// команда обновления статусов
        /// </summary>
        public ICommand RenewStatus { get; }

        public bool CanRenewStatusCommandExecute(object p) => true;

        public async void OnRenewStatusCommandExecute(object p)
        {
            App.packageid_list.Clear(); // очищаем список(в случае повторного нажатия кнопки "обновить")

            var Notifications_inwork = db.Notifications.Where(s => s.Status != "FAULT")
                .Where(s => s.Status != "RESULT (зарегистрировано)")
                .Where(s => s.Status != "RESULT (отказ в регистрации)");

            foreach (var notification in Notifications_inwork)
            {
                App.packageid_list.Add(notification.Packageid);
            }

            foreach (string packageid in App.packageid_list)    //перебираем коллецию
            {//в каждой итерации цикла делаем запрос к серверу, если статус == RESULT, то записываем файл в БД
             // создаем объект-запрос к серверу
                using (ruzdiGetNotificationPackageStateService_v1_1PortTypeClient request = new ruzdiGetNotificationPackageStateService_v1_1PortTypeClient(ruzdiGetNotificationPackageStateService_v1_1PortTypeClient.EndpointConfiguration.ruzdiGetNotificationPackageStateService_v1_1HttpSoap11Endpoint))
                {
                    getNotificationPackageStateRequest Object_for_request = new getNotificationPackageStateRequest(packageid); // объект - тело запроса к серверу, в конструктор передаём  packageid

                    getNotificationPackageStateResponse response = await request.getNotificationPackageStateAsync(Object_for_request); // делаем запрос и записываем ответ в response

                    Notification not;

                    if (response.packageState.ToString() == "RESULT")
                    {
                        documentAndSignatureType Doc = (documentAndSignatureType)response.pledgeNotificationStatePackage.pledgeNotificationStateList[0].Item; //достаём из ответа св-во о регистрации в виде массива байтов                        
                        string pathgzip = response.pledgeNotificationStatePackage.pledgeNotificationStateList[0].notificationId;    //достаём из ответа notificationId, он нужен для формирования имени файла
                                                                                                                                    //сохраняем массив байтов как zip архив
                        using (FileStream fstream = new FileStream("Temp/" + pathgzip + ".zip", FileMode.OpenOrCreate))
                        {
                            // запись массива байтов в поток файла
                            fstream.Write(Doc.Value, 0, Doc.Value.Length);
                            string mystr = Convert.ToBase64String(Doc.Value);
                            not = db.Notifications.FirstOrDefault(p => p.Packageid == packageid);
                            Ruzdi_DB.Entityes.RegistrationCertificate registrationCertificate = new Ruzdi_DB.Entityes.RegistrationCertificate
                            {
                                documentAndSignature = mystr
                            };
                            //registrationCertificate.Id = registrationCertificate.GetHashCode().ToString();
                            not.registrationCertificate = registrationCertificate;
                        }
                        //распаковка zip-архива
                        ZipFile.ExtractToDirectory("Temp/" + pathgzip + ".zip", "Temp/", true);
                        //удаление zip-архива
                        File.Delete("Temp/" + pathgzip + ".zip");
                        //десериализовать xml, затем ее удалить
                        using (FileStream fs = new FileStream("Temp/" + pathgzip + ".xml", FileMode.OpenOrCreate))
                        {
                            try     //выполняем десериализацию св-ва о регистрации
                            {
                                XmlSerializer formatter = new XmlSerializer(typeof(Ruzdi_6.Model.RegistrationCertificate.RegistrationCertificate));
                                Ruzdi_6.Model.RegistrationCertificate.RegistrationCertificate xml = (Ruzdi_6.Model.RegistrationCertificate.RegistrationCertificate)formatter.Deserialize(fs);
                                not.Status = "RESULT (зарегистрировано)";
                                not.NumberNotification = xml.RegistrationCertificateData.NotificationReferenceNumber;
                                not.DataTime = xml.RegistrationCertificateData.RegistrationTime;
                            }
                            catch (InvalidOperationException)   //если выпало исключение - значит это св-во об отказе, десериализируем его
                            {
                                fs.Position = 0;
                                XmlSerializer formatter = new XmlSerializer(typeof(RegistrationRejectMessage));
                                RegistrationRejectMessage xml = (RegistrationRejectMessage)formatter.Deserialize(fs);
                                not.Status = "RESULT (отказ в регистрации)";
                            }
                        }
                        File.Delete("Temp/" + pathgzip + ".xml");
                        File.Delete("Temp/" + pathgzip + ".xml.sig");
                        db.Notifications.Update(not);
                        await db.SaveChangesAsync();
                    }
                    else    //если статус != "RESULT" то устанавлиаем в БД его значение
                    {
                        not = db.Notifications.Include(p => p.Pledgors).Include(c => c.PledgeContract).FirstOrDefault(p => p.Packageid == packageid);

                        GetNotification.StateType stateType = (GetNotification.StateType)response.pledgeNotificationStatePackage.pledgeNotificationStateList[0].Item;
                        if (stateType.code != "0")
                        {
                            not.Error = stateType.message;
                            not.Status = response.packageState.ToString();
                        }
                        else
                        {
                            not.Status = response.packageState.ToString();
                        }
                        db.Notifications.Update(not);
                        db.SaveChanges();
                    }
                    SourceDatagrid[SourceDatagrid.IndexOf(SourceDatagrid.FirstOrDefault(n => n.Packageid == not.Packageid))] = not;//находим объект в коллекции, вычисляем его индекс и заменяем на новый объект
                }
            }
            SourceDatagridForFilter.Refresh();
        }

        #endregion

        #region OpenUZ1Command - команда открытия окна UZ1
        /// <summary>
        /// команда открытия окна UZ1
        /// </summary>
        public ICommand OpenUZ1Command { get; }
        public bool CanOpenUZ1CommandExecute(object p) => true;

        public void OnOpenUZ1CommandExecute(object p)
        {
            serviceWindow.ShowWindowDialog("Create_UZ1");
        }
        #endregion

        #region OpenUP1Command - команда открытия окна UP1
        /// <summary>
        /// команда открытия окна UP1
        /// </summary>
        public ICommand OpenUP1Command { get; }
        public bool CanOpenUP1CommandExecute(object p) => true;

        public void OnOpenUP1CommandExecute(object p)
        {
            serviceWindow.ShowWindowDialog("Create_UP1");
        }
        #endregion       

        #region CopyPackageidCommand - команда копирования рег. номера пакета
        /// <summary>
        /// команда копирования рег. номера пакета
        /// </summary>
        public ICommand CopyPackageidCommand { get; }

        public bool CanCopyPackageidCommandExecute(object p) => !string.IsNullOrEmpty(SelectedItem.Packageid);

        public void OnCopyPackageidCommandExecute(object p)
        {
            Clipboard.SetText(SelectedItem.Packageid.ToString());
        }

        #endregion

        #region SaveMessageCommand - команда сохранения св-ва о регистрации

        /// <summary>
        /// команда сохранения св-ва о регистрации
        /// </summary>
        public ICommand SaveMessageCommand { get; }

        public bool CanSaveMessageCommandExecute(object p) => App.Host.Services.GetRequiredService<DB_Ruzdi>().Notifications
            .Include(r => r.registrationCertificate)
            .FirstOrDefault(n => n.Id == SelectedItem.Id).registrationCertificate != null;

        public async void OnSaveMessageCommandExecuteAsync(object p)
        {

            Notification notification = await db.Notifications
                .Include(r => r.registrationCertificate)
                .FirstOrDefaultAsync(r => r.Id == SelectedItem.Id);

            string base64_registration = notification.registrationCertificate.documentAndSignature;

            App.array = Convert.FromBase64String(base64_registration);

            SaveFileDialog SaveFile = new SaveFileDialog
            {
                FileName = SelectedItem.Id + ".zip"
            };
            if ((bool)SaveFile.ShowDialog())
            {
                using (FileStream fstream = new FileStream(SaveFile.FileName, FileMode.OpenOrCreate))
                {
                    await fstream.WriteAsync(App.array, 0, App.array.Length);
                }
                MessageBox.Show("Архив сохранен!", "Ok!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        #endregion

        #region ViewNotificationCommand - команда просмотра уведомления

        /// <summary>
        /// команда команда просмотра уведомления
        /// </summary>
        public ICommand ViewNotificationCommand { get; }

        public async void OnViewNotificationCommandExecute(object p)
        {

            #region Получаем из БД архив по значению App.NotificationId и записываем его в массив байтов
            Notification t = await db.Notifications.FirstOrDefaultAsync(a => a.Id == SelectedItem.Id);
            string base64 = t.ZipArchive;
            App.array = Convert.FromBase64String(base64);// читаем из БД архив и записываем его в массив байтов
            #endregion

            #region создаем архив на основе  массива байтов
            using (FileStream fstream = new FileStream("Temp/" + SelectedItem.Id + ".zip", FileMode.OpenOrCreate))
            {
                // запись массива байтов в поток файла
                await fstream.WriteAsync(App.array, 0, App.array.Length);  //читаем массив байтив и записываем его в файл архива
            }
            #endregion

            #region распаковка zip-архива   
            ZipFile.ExtractToDirectory("Temp/" + SelectedItem.Id + ".zip", "Temp/", true);  //распаковка zip-архива   
            #endregion

            PledgeNotificationToNotary xml; // объект, который будет хранить результат десериализации

            #region выполняем десериализацию уведомления
            using (FileStream fs = new FileStream("Temp/" + SelectedItem.Id + ".xml", FileMode.OpenOrCreate))
            {
                //выполняем десериализацию уведомления 
                XmlSerializer Serializer = new XmlSerializer(typeof(PledgeNotificationToNotary));
                xml = (PledgeNotificationToNotary)Serializer.Deserialize(fs);
            }
            #endregion

            #region Удаляем временные файлы в папке Temp
            File.Delete("Temp/" + SelectedItem.Id + ".xml");
            File.Delete("Temp/" + SelectedItem.Id + ".xml.sig");
            File.Delete("Temp/" + SelectedItem.Id + ".zip");  //удаление zip-архива
            #endregion

            if (xml.NotificationData.FormUZ1 != null) // определяем тип уведомления
            {

                #region Заполняем коллекцию имущества
                foreach (PersonalProperty Property in xml.NotificationData.FormUZ1.PersonalProperties)
                {
                    if (Property.VehicleProperty is null)
                    {
                        VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Property>().PersonalProperty.Add(Property.OtherProperty);
                    }
                    else
                    {
                        VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Property>().PersonalProperty.Add(Property.VehicleProperty);
                    }

                }
                #endregion

                #region Заполняем коллекцию залогодателей
                foreach (Ruzdi_6.Model.Pledgor_Classes.Pledgor PledgorItem in xml.NotificationData.FormUZ1.Pledgors)
                {
                    if (PledgorItem.PrivatePerson is null)
                    {
                        VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgor>().Pledgors.Add(PledgorItem.Organization);
                    }
                    else
                    {
                        VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgor>().Pledgors.Add(PledgorItem.PrivatePerson);
                    }
                }
                #endregion

                #region Заполняем коллекцию залогодержателей
                foreach (Pledgee PledgeeItem in xml.NotificationData.FormUZ1.Pledgee)
                {
                    if (PledgeeItem.Organization is null)
                    {
                        VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgee>().Pledgee.Add(PledgeeItem.PrivatePerson);
                    }
                    else
                    {
                        VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgee>().Pledgee.Add(PledgeeItem.Organization);
                    }

                }
                #endregion

                #region Заполнение договора залога
                VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Contract>().Contract = xml.NotificationData.FormUZ1.PledgeContract;
                #endregion

                #region Заполняем заявителя
                if (xml.NotificationData.FormUZ1.NotificationApplicant.Organization is null)
                {
                    VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Applicant>().DisplayApplicant = new NotificationApplicant
                    {
                        PrivatePerson = new ApplicantPrivatePerson()
                    };
                    VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Applicant>().DisplayApplicant = xml.NotificationData.FormUZ1.NotificationApplicant.PrivatePerson;
                }
                else
                {
                    VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Applicant>().DisplayApplicant = xml.NotificationData.FormUZ1.NotificationApplicant;
                }



                #endregion

                #region Получение из БД отпечатка, поиск по нему серта на компе, внесение в список данных о серте
                VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Applicant>().ListCert.Clear();

                Notification Notification = await db.Notifications.FirstOrDefaultAsync(a => a.Id == SelectedItem.Id);
                VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Applicant>().ListCert.Add(Notification.CertInfo);
                //using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
                //{
                //    store.Open(OpenFlags.ReadOnly);


                //    X509Certificate2 cert = store.Certificates.FirstOrDefault(c => c.Thumbprint == Notification.CertInfo);
                //    string otvet = cert.SubjectName.Name;
                //    string zap = ",";
                //    if (otvet.Contains("ОГРН="))//если есть ОГРН. значит юр лицо
                //    {
                //        string s = "CN=";
                //        string CN = otvet.Substring(otvet.IndexOf(s) + s.Length, otvet.IndexOf(zap, otvet.IndexOf(s)) - (otvet.IndexOf(s) + s.Length));
                //        s = "SN=";
                //        string SN = otvet.Substring(otvet.IndexOf(s) + s.Length, otvet.IndexOf(zap, otvet.IndexOf(s)) - (otvet.IndexOf(s) + s.Length));
                //        s = "G=";
                //        string G = otvet.Substring(otvet.IndexOf(s) + s.Length, otvet.IndexOf(zap, otvet.IndexOf(s)) - (otvet.IndexOf(s) + s.Length));
                //        string stroka = CN + ", " + SN + " " + G;    //создаем строку для записи её в лист
                //        VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Applicant>().ListCert.Add(stroka);
                //    }
                //    else //если физ. лицо
                //    {
                //        string CN = otvet.Substring(otvet.IndexOf("CN=") + "CN=".Length, otvet.IndexOf(zap, otvet.IndexOf("CN=")) - (otvet.IndexOf("CN=") + "CN=".Length));
                //        VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Applicant>().ListCert.Add(CN);
                //    }
                //}
                #endregion


                VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VMWindowForUZ1>().IsCheckedPledgor = true;
                VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VMWindowForUZ1>().CurrentContentVM = VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgor>();
                App.SetFlag_True();

                serviceWindow.ShowWindowDialog("Create_UZ1");

            }
            else //если это уведомление об исключении
            {
                VM_Locator.scopeUP1.ServiceProvider.GetRequiredService<VM_For_Win_UP1>().UP1.NotificationData.FormUP1 = xml.NotificationData.FormUP1;

                VM_Locator.scopeUP1.ServiceProvider.GetRequiredService<VM_For_Win_UP1>().IsView = true;


                #region Получение из БД отпечатка, поиск сертфиката, чтение его и формирование списка на отображение
                VM_Locator.scopeUP1.ServiceProvider.GetRequiredService<VM_For_Win_UP1>().ListCert.Clear();
                using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
                {
                    store.Open(OpenFlags.ReadOnly);
                    Notification Notification = await db.Notifications.FirstOrDefaultAsync(a => a.Id == SelectedItem.Id);

                    X509Certificate2 cert = store.Certificates.FirstOrDefault(c => c.Thumbprint == Notification.CertInfo);
                    string otvet = cert.SubjectName.Name;
                    string zap = ",";
                    if (otvet.Contains("ОГРН="))//если есть ОГРН. значит юр лицо
                    {
                        string s = "CN=";
                        string CN = otvet.Substring(otvet.IndexOf(s) + s.Length, otvet.IndexOf(zap, otvet.IndexOf(s)) - (otvet.IndexOf(s) + s.Length));
                        s = "SN=";
                        string SN = otvet.Substring(otvet.IndexOf(s) + s.Length, otvet.IndexOf(zap, otvet.IndexOf(s)) - (otvet.IndexOf(s) + s.Length));
                        s = "G=";
                        string G = otvet.Substring(otvet.IndexOf(s) + s.Length, otvet.IndexOf(zap, otvet.IndexOf(s)) - (otvet.IndexOf(s) + s.Length));
                        string stroka = CN + ", " + SN + " " + G;    //создаем строку для записи её в лист
                        VM_Locator.scopeUP1.ServiceProvider.GetRequiredService<VM_For_Win_UP1>().ListCert.Add(stroka);
                    }
                    else //если физ. лицо
                    {
                        string CN = otvet.Substring(otvet.IndexOf("CN=") + "CN=".Length, otvet.IndexOf(zap, otvet.IndexOf("CN=")) - (otvet.IndexOf("CN=") + "CN=".Length));
                        VM_Locator.scopeUP1.ServiceProvider.GetRequiredService<VM_For_Win_UP1>().ListCert.Add(CN);
                    }
                    #endregion
                }

                serviceWindow.ShowWindowDialog("Create_UP1");
            }
        }

        #endregion

        #region OpenSettings - команда открытия окна настроек
        /// <summary>
        /// команда открытия окна настроек
        /// </summary>
        public ICommand OpenSettings { get; }

        public void OnOpenSettingsCommandExecute(object p) => serviceWindow.ShowWindowDialog("Settings");
        #endregion




        #endregion

        #region SelectedItem - объект, выбранный в Datagrid
        private Notification selectedItem;

        public Notification SelectedItem
        {
            get => selectedItem;
            set => Set(ref selectedItem, value);
        }
        #endregion

        #region Поля, св-ва, методы фильтров
        #region NumberContract_FilterText - Свойство фильтра по номеру договора
        private string numberContract_FilterText;
        public string NumberContract_FilterText
        {
            get => numberContract_FilterText;
            set
            {
                if (!Set(ref numberContract_FilterText, value)) return;
                DataGridCollection.View.Refresh();
            }
        }
        #endregion

        #region FIO_FilterText - Свойство фильтра по ФИО
        private string fIO_FilterText;
        public string FIO_FilterText
        {
            get => fIO_FilterText;
            set
            {
                if (!Set(ref fIO_FilterText, value)) return;
                DataGridCollection.View.Refresh();
            }
        }
        #endregion

        private void OnFIOfilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Notification Notification))
            {
                e.Accepted = false;
                return;
            }
            if (string.IsNullOrWhiteSpace(FIO_FilterText))
            {
                return;
            }
            if (Notification.Pledgors is null)
            {
                e.Accepted = false;
                return;
            }


            foreach (var pledgor in Notification.Pledgors)
            {
                if (pledgor is Persons person)
                {
                    if ((person.First + person.Last + person.Middle).Contains(FIO_FilterText, StringComparison.OrdinalIgnoreCase))
                        return;
                }
                else if (pledgor is Organizations organization)
                {
                    if ((organization.NameFull + organization.INN + organization.OGRN).Contains(FIO_FilterText, StringComparison.OrdinalIgnoreCase))
                        return;
                }
            }
            e.Accepted = false;
        }

        private void OnContractfilter(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Notification Notification))
            {
                e.Accepted = false;
                return;
            }
            if (string.IsNullOrWhiteSpace(NumberContract_FilterText))
            {
                return;
            }
            if (Notification.PledgeContract is null)
            {
                e.Accepted = false;
                return;
            }
            if (Notification.PledgeContract.Number.IndexOf(NumberContract_FilterText, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return;
            }
            e.Accepted = false;
        }
        #endregion

        #region SourceDatagrid - Список-источник для DataGrid

        public ObservableCollection<Notification> SourceDatagrid { get; set; }

        #endregion

        #region SourceDatagridForFilter - Список, отображаемый в DataGrid после фильтрации (обёртка над SourceDatagrid)
        private CollectionViewSource DataGridCollection;

        public ICollectionView SourceDatagridForFilter => DataGridCollection?.View;

        #endregion

    }
}
