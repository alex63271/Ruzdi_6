using GetNotification;
using Microsoft.Win32;
using Ruzdi_6.Commands;
using Ruzdi_6.Model;
using Ruzdi_6.Model.Applicant_Classes;
using Ruzdi_6.Model.Other_Classes;
using Ruzdi_6.Model.Pledgee_Classes;
using Ruzdi_6.Model.Pledgor_Classes;
using Ruzdi_6.Model.Property_Classes;
using Ruzdi_6.Model.RegistrationCertificate;
using Ruzdi_6.Model.RegistrationRejectMessage;
using Ruzdi_6.Services;
using Ruzdi_DB.Entityes;
using Ruzdi_Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Ruzdi_6.ViewModel
{
    public class VM_ForGlavnaya : ViewModel
    {
        //IRepository<Notification> NotificationRepository
        public VM_ForGlavnaya(IRepository<Notification> NotificationRepository)
        {
            if (App.DesignMode)
            {
                DataGridCollection = new CollectionViewSource
                {
                    Source = new ObservableCollection<ModelForDatagrid>
                    {
                        new ModelForDatagrid
                        {
                            TypeNotification = "Возникновение",
                            FIO = "Иванов Иван Иваныч",
                            DataRegistartion = DateTime.Now.ToString(),
                            NumberNotification = "2022-005-123456-123",
                            Status = "Зарегистрировано",
                            NumberContract = "05-456"
                        }
                    },
                    IsLiveFilteringRequested = true
                };
                //SourceDatagrid = new ObservableCollection<ModelForDatagrid>
                //{
                //    new ModelForDatagrid
                //    {
                //        TypeNotification = "Возникновение",
                //        FIO = "Иванов Иван Иваныч",
                //        DataRegistartion = DateTime.Now.ToString(),
                //        NumberNotification = "2022-005-123456-123",
                //        Status = "Зарегистрировано",
                //        NumberContract = "05-456"
                //    }
                //};
                #region Команды
                OpenUZ1Command = new RelayCommand(OnOpenUZ1CommandExecute);
                RenewStatus = new RelayCommand(OnRenewStatusCommandExecute, CanRenewStatusCommandExecute);
                OpenUP1Command = new RelayCommand(OnOpenUP1CommandExecute, CanOpenUP1CommandExecute);
                /*
                CopyPackageidCommand = new RelayCommand(OnCopyPackageidCommandExecute, CanCopyPackageidCommandExecute);
                SaveMessageCommand = new RelayCommand(OnSaveMessageCommandExecute, CanSaveMessageCommandExecute);
                ViewNotificationCommand = new RelayCommand(OnViewNotificationCommandExecute);
                */
                OpenSettings = new RelayCommand(OnOpenSettingsCommandExecute);
                #endregion
            }
            else
            {
                //_NotificationRepository = NotificationRepository;
                SourceDatagrid = NotificationRepository.GetAll();
                /*
                Query query = new SelectForUpdateDataGrid();
                SourceDatagrid = query.GetCollectionForDataGrid();
                */

                DataGridCollection = new CollectionViewSource 
                {
                    Source = SourceDatagrid
                };
               // DataGridCollection.GroupDescriptions.Add(new PropertyGroupDescription("Pledgors"));

                 //OnPropertyChanged(nameof(SourceDatagridForFilter));
                #region Команды
                OpenUZ1Command = new RelayCommand(OnOpenUZ1CommandExecute);
                RenewStatus = new RelayCommand(OnRenewStatusCommandExecute, CanRenewStatusCommandExecute);
                OpenUP1Command = new RelayCommand(OnOpenUP1CommandExecute, CanOpenUP1CommandExecute);
                /*
                CopyPackageidCommand = new RelayCommand(OnCopyPackageidCommandExecute, CanCopyPackageidCommandExecute);
                SaveMessageCommand = new RelayCommand(OnSaveMessageCommandExecute, CanSaveMessageCommandExecute);
                ViewNotificationCommand = new RelayCommand(OnViewNotificationCommandExecute);
                */
                OpenSettings = new RelayCommand(OnOpenSettingsCommandExecute);
                #endregion

                DataGridCollection.Filter += OnFIOfilter;
                DataGridCollection.Filter += OnContractfilter;

            }
        }

       // private IRepository<Notification> _NotificationRepository;

       /* public void UpdateCollectionDataGrid()
        {
            /*
            Query query = new SelectForUpdateDataGrid();

            SourceDatagrid = query.GetCollectionForDataGrid();
            //SourceDatagrid.CollectionChanged(0);
            DataGridCollection.View.Refresh();


            /*DataGridCollection.Filter += OnFIOfilter;
            DataGridCollection.Filter += OnContractfilter;

        }*/


        #region Список, отображаемый в DataGrid
        private ObservableCollection<Notification> sourceDatagrid;
        public ObservableCollection<Notification> SourceDatagrid
        {
            get => sourceDatagrid;
            set => Set(ref sourceDatagrid, value);
        }
        #endregion

        #region Команды

        #region RenewStatus - команда обновления статусов
        /// <summary>
        /// команда обновления статусов
        /// </summary>
        public ICommand RenewStatus { get; }
        public bool CanRenewStatusCommandExecute(object p)
        {
            return true;
        }
        public async void OnRenewStatusCommandExecute(object p)
        {
            /*
            App.packageid_list.Clear(); // очищаем список(в случае повторного нажатия кнопки "обновить")
            Query query = new SelectForGetPackageInwork();
            query.GetPackageInwork();   // метод записывает в Check.packageid_list перечень пакетов из БД, по которым надо делать запрос статусов


            foreach (string packageid in App.packageid_list)    //перебираем коллецию
            {//в каждой итерации цикла делаем запрос к серверу, если статус == RESULT, то записываем файл в БД
             // создаем объект-запрос к серверу
                ruzdiGetNotificationPackageStateService_v1_1PortTypeClient request = new ruzdiGetNotificationPackageStateService_v1_1PortTypeClient("ruzdiGetNotificationPackageStateService_v1_1HttpSoap11Endpoint");
                getNotificationPackageStateRequest Package = new getNotificationPackageStateRequest(packageid); // объект - тело запроса к серверу, в конструктор передаём  packageid

                getNotificationPackageStateResponse response = await request.getNotificationPackageStateAsync(Package); // делаем запрос и записываем ответ в response

                //Task<getNotificationPackageStateResponse> Zapros = Task.Run(() => request.getNotificationPackageStateAsync(Package));

                //MessageBox.Show(Zapros.Result.pledgeNotificationStatePackage.packageStateCode.message);

                //Zapros.Result.pledgeNotificationStatePackage.packageStateCode.message();
                Query updatequery = new UpdateForStatusResult(); // объект для выполнения sql-запроса update в БД
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
                        Query insertquery = new InsertForSaveResult();
                        insertquery.InsertResult(mystr, pathgzip); //метод сохраняет в БД guid уведомления и zip-архив в base64
                    }
                    //распаковка zip-архива
                    ZipFile.ExtractToDirectory("Temp/" + pathgzip + ".zip", "Temp/");
                    //удаление zip-архива
                    File.Delete("Temp/" + pathgzip + ".zip");
                    //десериализовать xml, затем ее удалить
                    using (FileStream fs = new FileStream("Temp/" + pathgzip + ".xml", FileMode.OpenOrCreate))
                    {
                        try     //выполняем десериализацию св-ва о регистрации
                        {
                            XmlSerializer formatter = new XmlSerializer(typeof(RegistrationCertificate));
                            RegistrationCertificate xml = (RegistrationCertificate)formatter.Deserialize(fs);
                            updatequery.UpdateForResult(xml, packageid); //метод устанавливает в БД статус "зарегистрировано", номер уведомления и дата его регистрации                            
                        }
                        catch (InvalidOperationException)   //если выпало исключение - значит это св-во об отказе, десериализируем его
                        {
                            fs.Position = 0;
                            XmlSerializer formatter = new XmlSerializer(typeof(RegistrationRejectMessage));
                            RegistrationRejectMessage xml = (RegistrationRejectMessage)formatter.Deserialize(fs);
                            updatequery.SetUpdate(new SetStatusResultReject()); //изменяю объект для выполнения другрого запроса update в БД
                            updatequery.UpdateForResultReject(packageid); //метод устанавливает в БД статус "Отказ в регистрации"                            
                        }
                    }
                    File.Delete("Temp/" + pathgzip + ".xml");
                    File.Delete("Temp/" + pathgzip + ".xml.sig");
                }
                else    //если статус != "RESULT" то устанавлиаем в БД его значение
                {
                    if (response.pledgeNotificationStatePackage.packageStateCode.code != "0")
                    {
                        MessageBox.Show(response.pledgeNotificationStatePackage.packageStateCode.message + " " + packageid);
                    }
                    else
                    {
                        GetNotification.StateType otvet = (GetNotification.StateType)response.pledgeNotificationStatePackage.pledgeNotificationStateList[0].Item;


                        MessageBox.Show(otvet.message);
                        updatequery.SetUpdate(new SetStatus()); //изменяю объект для выполнения другрого запоса update в БД
                        updatequery.UpdateStatus(response, packageid);  //метод записывает в БД статсы inwork, incontrol и прочие, а также packageid  
                    }
                }
            }
            /*query.SetSelect(new UpdateDatagrid());
            DataGridCollection.Source = query.GetCollectionForDataGrid(); // обновление коллекции
            DataGridCollection.View.Refresh();
            UpdateCollectionDataGrid();*/
        }
        #endregion

        #region OpenUZ1Command - команда открытия окна UZ1
        /// <summary>
        /// команда открытия окна UZ1
        /// </summary>
        public ICommand OpenUZ1Command { get; }
        public bool CanOpenUZ1CommandExecute(object p)
        {
            return true;
        }
        public void OnOpenUZ1CommandExecute(object p)
        {
            /*
            Query query = new SelectForRegions();
            query.GetRegionsInCheck(); //метод записывает из БД регионы в список класса App

            
            App.SetNull_Subsidiary_VM_UZ1(); // очистка дочерних VM
            App.SetFlag_False();
            VMWindowForUZ1.Get_VW_UZ1().IsCheckedPledgor = true;
            VMWindowForUZ1.Get_VW_UZ1().CurrentContentVM = VM_Pledgor.Get_VM_Pledgor_UZ1();
            */
            IWindowService service = new ServiceWindow();
            service.ShowWindowDialog("Create_UZ1");
        }
        #endregion

        #region OpenUP1Command - команда открытия окна UP1
        /// <summary>
        /// команда открытия окна UP1
        /// </summary>
        public ICommand OpenUP1Command { get; }
        public bool CanOpenUP1CommandExecute(object p)
        {
            return true;
        }
        public void OnOpenUP1CommandExecute(object p)
        {
            /*
            Query query = new SelectForRegions();
            query.GetRegionsInCheck(); //метод записывает из БД регионы в список класса App
            
            VM_For_Win_UP1.SetNull_VM_UP1();
            VM_For_Win_UP1.Get_VM_UP1().IsView = false;
            */
            IWindowService service = new ServiceWindow();
            service.ShowWindowDialog("Create_UP1");

        }
        #endregion       

        #region CopyPackageidCommand - команда копирования рег. номера пакета
        /// <summary>
        /// команда копирования рег. номера пакета
        /// </summary>
        public ICommand CopyPackageidCommand { get; }
        /*
        public bool CanCopyPackageidCommandExecute(object p)
        {
            /*
            Query query = new SelectForCheckExistRegId();
            return query.CheckExistRegistrationID(SelectedIndex);
            
        }
       
        public void OnCopyPackageidCommandExecute(object p)
        {
            /*
            Query Query = new SelectForPackageIDFromDataGrid();
            Query.GetPackageID(SelectedIndex);
            
        }
        */
        #endregion

        #region SaveMessageCommand - команда сохранения св-ва о регистрации
        /*
        /// <summary>
        /// команда сохранения св-ва о регистрации
        /// </summary>
        public ICommand SaveMessageCommand { get; }
        public bool CanSaveMessageCommandExecute(object p)
        {
            Query query = new SelectForGetNotificationIDFromDataGrid();
            query.GetNotificationIDInCheck(SelectedIndex);  //записываем в App.NotificationId номер уведомления

            query.SetSelect(new CheckExistZip_Base64());
            return query.CheckExistZirResultRegistration(App.NotificationId); // используя номер уведомления выясняем есть ли в БД результат регистрации
        }
        public void OnSaveMessageCommandExecute(object p)
        {
            Query query = new SelectForGetNotificationIDFromDataGrid();

            query.GetNotificationIDInCheck(SelectedIndex); // метод запишет Notification в статическую переменную класса Check
            query.SetSelect(new SaveArrayByteFromBase64FromTableRegistrationCertificate());
            query.SaveByteArrayFromTableRegistrationCertificate();//метод запишет массив байтов в статическую переменную класса Check

            SaveFileDialog SaveFile = new SaveFileDialog
            {
                FileName = App.NotificationId + ".zip"
            };
            if ((bool)SaveFile.ShowDialog())
            {
                using (FileStream fstream = new FileStream(SaveFile.FileName, FileMode.OpenOrCreate))
                {
                    fstream.Write(App.array, 0, App.array.Length);
                }
                MessageBox.Show("Архив сохранен!", "Ok!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        */
        #endregion

        #region ViewNotificationCommand - команда просмотра уведомления
        /*
        /// <summary>
        /// команда команда просмотра уведомления
        /// </summary>
        public ICommand ViewNotificationCommand { get; }

        public void OnViewNotificationCommandExecute(object p)
        {

            Query query = new SelectForRegions();
            query.GetRegionsInCheck(); //метод записывает из БД регионы в список класса App

            #region записываем в App.NotificationId значение NotificationId из datagrid
            query.SetSelect(new GetPackageidFromDataGrid());
            query.GetNotificationIDInCheck(SelectedIndex);//записываем в App.NotificationId значение NotificationId из datagrid 
            #endregion

            #region Получаем из БД архив по значению App.NotificationId и записываем его в массив байтов
            query.SetSelect(new SaveArrayByteFromBase64FromTableNotification());
            query.SaveByteArrayFromZipArchive();    // читаем из БД архив по значению App.NotificationId и записываем его в массив байтов 
            #endregion

            #region создаем архив на основе  массива байтов
            using (FileStream fstream = new FileStream("Temp/" + App.NotificationId + ".zip", FileMode.OpenOrCreate))
            {
                // запись массива байтов в поток файла
                fstream.Write(App.array, 0, App.array.Length);  //читаем массив байтив и записываем его в файл архива
            }
            #endregion

            #region распаковка zip-архива   
            ZipFile.ExtractToDirectory("Temp/" + App.NotificationId + ".zip", "Temp/");  //распаковка zip-архива   
            #endregion

            PledgeNotificationToNotary xml; // объект, который будет хранить результат десериализации

            #region выполняем десериализацию уведомления
            using (FileStream fs = new FileStream("Temp/" + App.NotificationId + ".xml", FileMode.OpenOrCreate))
            {
                //выполняем десериализацию уведомления 
                XmlSerializer Serializer = new XmlSerializer(typeof(PledgeNotificationToNotary));
                xml = (PledgeNotificationToNotary)Serializer.Deserialize(fs);
            }
            #endregion

            #region Удаляем временные файлы в папке Temp
            File.Delete("Temp/" + App.NotificationId + ".xml");
            File.Delete("Temp/" + App.NotificationId + ".xml.sig");
            File.Delete("Temp/" + App.NotificationId + ".zip");  //удаление zip-архива
            #endregion

            if (xml.NotificationData.FormUZ1 != null) // определяем тип уведомления
            {
                App.SetNull_Subsidiary_VM_UZ1(); // очищаем дочерние VM, затем их заполняем

                #region Заполняем коллекцию имущества
                foreach (PersonalProperty Property in xml.NotificationData.FormUZ1.PersonalProperties)
                {
                    if (Property.VehicleProperty is null)
                    {
                        VM_Property.GetVM_Property_UZ1().PersonalProperty.Add(Property.OtherProperty);
                    }
                    else
                    {
                        VM_Property.GetVM_Property_UZ1().PersonalProperty.Add(Property.VehicleProperty);
                    }

                }
                #endregion

                #region Заполняем коллекцию залогодателей
                foreach (Pledgor PledgorItem in xml.NotificationData.FormUZ1.Pledgors)
                {
                    if (PledgorItem.PrivatePerson is null)
                    {
                        VM_Pledgor.Get_VM_Pledgor_UZ1().Pledgors.Add(PledgorItem.Organization);
                    }
                    else
                    {
                        VM_Pledgor.Get_VM_Pledgor_UZ1().Pledgors.Add(PledgorItem.PrivatePerson);
                    }
                }
                #endregion

                #region Заполняем коллекцию залогодержателей
                foreach (Pledgee PledgeeItem in xml.NotificationData.FormUZ1.Pledgee)
                {
                    if (PledgeeItem.Organization is null)
                    {
                        VM_Pledgee.GetVM_Pledgee_UZ1().Pledgee.Add(PledgeeItem.PrivatePerson);
                    }
                    else
                    {
                        VM_Pledgee.GetVM_Pledgee_UZ1().Pledgee.Add(PledgeeItem.Organization);
                    }

                }
                #endregion

                #region Заполнение договора залога
                VM_Contract.GetVM_Contract_UZ1().Contract = xml.NotificationData.FormUZ1.PledgeContract;
                #endregion

                #region Заполняем заявителя
                if (xml.NotificationData.FormUZ1.NotificationApplicant.Organization is null)
                {
                    VM_Applicant.Get_VM_Applicant().DisplayApplicant = new NotificationApplicant
                    {
                        PrivatePerson = new ApplicantPrivatePerson()
                    };
                    VM_Applicant.Get_VM_Applicant().DisplayApplicant = xml.NotificationData.FormUZ1.NotificationApplicant.PrivatePerson;
                }
                else
                {
                    VM_Applicant.Get_VM_Applicant().DisplayApplicant = xml.NotificationData.FormUZ1.NotificationApplicant;
                }
                #endregion

                VMWindowForUZ1.Get_VW_UZ1().IsCheckedPledgor = true;
                VMWindowForUZ1.Get_VW_UZ1().CurrentContentVM = VM_Pledgor.Get_VM_Pledgor_UZ1();
                App.SetFlag_True();

                IWindowService service = new ServiceWindow();
                service.ShowWindowDialog("Create_UZ1");

            }
            else //если это уведомление об исключении
            {
                VM_For_Win_UP1.SetNull_VM_UP1();
                VM_For_Win_UP1.Get_VM_UP1().UP1.NotificationData.FormUP1 = xml.NotificationData.FormUP1;
                VM_For_Win_UP1.Get_VM_UP1().IsView = true;

                IWindowService service = new ServiceWindow();
                service.ShowWindowDialog("Create_UP1");

            }
        }
        */
        #endregion

        #region OpenSettings - команда открытия окна настроек
        /// <summary>
        /// команда открытия окна настроек
        /// </summary>
        public ICommand OpenSettings { get; }

        public void OnOpenSettingsCommandExecute(object p)
        {
            IWindowService service = new ServiceWindow();
            service.ShowWindowDialog("Settings");
        }
        #endregion

        #endregion

        #region  Свойство для дизайнера
        /*private static VM_ForGlavnaya vM_Glavnaya_ForDesiner;
        public static VM_ForGlavnaya VM_Glavnaya_ForDesiner
        {
            get
            {
                if (vM_Glavnaya_ForDesiner == null)
                {
                    vM_Glavnaya_ForDesiner = new VM_ForGlavnaya();
                }
                return vM_Glavnaya_ForDesiner;
            }
        }*/
        #endregion

        #region Поля и методы Singletone

         private static VM_ForGlavnaya VM_Glavnaya;
        /* public static VM_ForGlavnaya Get_VM_ForGlavnaya()
         {
             if (VM_Glavnaya == null)
             {
                 VM_Glavnaya = new VM_ForGlavnaya();
             }
             return VM_Glavnaya;
         }*/

        /// <summary>
        /// Метод для сброса экземпляра VW_UZ1
        /// </summary>
        public static void SetNull_VM_ForGlavnaya()
        {
            VM_Glavnaya = null;
        }
        #endregion                        

        #region SelectedIndex - индекс выбранного элемента в Datagrid
        private int selectedIndex;
        public int SelectedIndex
        {
            get => selectedIndex;
            set => Set(ref selectedIndex, value);
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
            if (Notification.Pledgors.Any(s=> (s.Person?.First + s.Person?.Last + s.Person?.Middle).Contains(FIO_FilterText, StringComparison.OrdinalIgnoreCase)))
            {
                return;
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
            //if (Notification.PledgeContract.IndexOf(NumberContract_FilterText, StringComparison.OrdinalIgnoreCase) >= 0)
            //{
            //    return;
            //}
            e.Accepted = false;
        }
        #endregion

        #region Список, отображаемый в DataGrid
        private CollectionViewSource DataGridCollection;

        public ICollectionView SourceDatagridForFilter
        {
            get => DataGridCollection?.View;
        }
        #endregion
    }
}
