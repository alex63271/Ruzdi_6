using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ruzdi_6.Commands;
using Ruzdi_6.Model.Applicant_Classes;
using Ruzdi_6.Model.Other_Classes;
using Ruzdi_6.Model.Pledgee_Classes;
using Ruzdi_6.Model.Pledgor_Classes;
using Ruzdi_6.Model.Property_Classes;
using Ruzdi_6.Services;
using Ruzdi_DB.Context;
using Ruzdi_DB.Entityes;
using System.Collections;
using System.Collections.ObjectModel;
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
    public class VMWindowForUZ1 : ViewModel
    {       
        public VMWindowForUZ1(VM_ForGlavnaya vM_ForGlavnaya, DB_Ruzdi db, IWindowService service)
        {
            this.vM_ForGlavnaya = vM_ForGlavnaya;
            contextNotification = db;
            serviceWindow = service;

            #region Команды

            ToPledgorsCommand = new RelayCommand(OnToPledgorsCommandExecute, CanToPledgorsCommandExecute);
            ToPledgeeCommand = new RelayCommand(OnToPledgeeCommandExecute, CanToPledgeeCommandExecute);

            ToProperty = new RelayCommand(OnToPropertyCommandExecute, CanToPropertyCommandExecute);

            ToContractCommand = new RelayCommand(OnToContractCommandExecute, CanToContractCommandExecute);
            ToApplicantCommand = new RelayCommand(OnToApplicantCommandExecute, CanToApplicantCommandExecute);




            SendNotification = new RelayCommand(OnSendNotificationCommandExecute, CanSendNotificationCommandExecute);

            #endregion

            CurrentContentVM = VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgor>();

            #region Конструкция чтения хранилища сертификатов и сохранения их перечня в коллекции

            using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                ArrayList List = new ArrayList();
                ArrayList ListThumbprint = new ArrayList();
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
        }

        private readonly VM_ForGlavnaya vM_ForGlavnaya;
        private readonly IWindowService serviceWindow;
        private readonly DB_Ruzdi contextNotification;

        #region Флаг просмотра/создания уведомления
        private bool isView;
        /// <summary>
        /// Флаг просмотр/создание уведомления
        /// </summary>
        public bool IsView
        {
            get => isView;
            set => Set(ref isView, value);
        }
        #endregion

        Contracts contract;
        Notification Notification;
        List<Ruzdi_DB.Entityes.Pledgor> Pledgors = new();

        #region Выбранная VM


        private ViewModel currentContentVM;
        public ViewModel CurrentContentVM
        {
            get => currentContentVM;
            set => Set(ref currentContentVM, value);
        }
        #endregion

        #region Команды навигации

        #region ToPledgors - команда перехода к залогодателю
        /// <summary>
        /// команда сохранения залогодателя юр. лица РФ
        /// </summary>
        public ICommand ToPledgorsCommand { get; }

        public bool CanToPledgorsCommandExecute(object p) => true;

        public void OnToPledgorsCommandExecute(object p)
        {
            CurrentContentVM = VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgor>();
            IsCheckedPledgor = true;
        }
        #endregion

        #region ToPledgee - команда перехода к залогодержателю
        /// <summary>
        /// команда перехода к залогодержателю
        /// </summary>
        public ICommand ToPledgeeCommand { get; }

        public bool CanToPledgeeCommandExecute(object p) => (VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgor>().Pledgors.Count > 0 && !IsView) || IsView;

        public void OnToPledgeeCommandExecute(object p)
        {
            CurrentContentVM = VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgee>();
            IsCheckedPledgee = true;
        }
        #endregion

        #region ToProperty - команда перехода к имуществу
        /// <summary>
        /// команда перехода к имуществу
        /// </summary>
        public ICommand ToProperty { get; }
        public bool CanToPropertyCommandExecute(object p)
        {
            //должна быть логика проверки, что валидация успешна
            return (VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgee>().Pledgee.Count > 0 && !IsView) || IsView;
        }
        public void OnToPropertyCommandExecute(object p)
        {
            CurrentContentVM = VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Property>();
            IsCheckedProperty = true;
        }
        #endregion

        #region ToContractCommand - команда перехода к договору залога
        /// <summary>
        /// команда перехода к договору залога
        /// </summary>
        public ICommand ToContractCommand { get; }

        public bool CanToContractCommandExecute(object p) => (VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Property>().PersonalProperty.Count > 0 && !IsView) || IsView;

        public void OnToContractCommandExecute(object p)
        {
            CurrentContentVM = VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Contract>();
            IsCheckedContract = true;
        }
        #endregion

        #region ToApplicantCommand - команда перехода к заявителю
        /// <summary>
        /// команда перехода к договору залога
        /// </summary>
        public ICommand ToApplicantCommand { get; }
        public bool CanToApplicantCommandExecute(object p)
        {
            //должна быть логика проверки, что валидация успешна
            return (VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgor>().Pledgors.Count > 0
                && VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgee>().Pledgee.Count > 0
                && VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Property>().PersonalProperty.Count > 0
                && VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Contract>().Contract.IsValid
                && !IsView) || IsView;
        }
        public void OnToApplicantCommandExecute(object p)
        {
            CurrentContentVM = VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Applicant>();
            IsCheckedApplicant = true;
        }
        #endregion

        #endregion

        #region Поля и св-ва выбранного Radiobutton

        private bool isCheckedPledgor = true;
        public bool IsCheckedPledgor
        {
            get => isCheckedPledgor;
            set
            {
                isCheckedPledgor = value;
                OnPropertyChanged();
            }
        }

        private bool isCheckedPledgee;
        public bool IsCheckedPledgee
        {
            get => isCheckedPledgee;
            set
            {
                isCheckedPledgee = value;
                OnPropertyChanged();
            }
        }

        private bool isCheckedProperty;
        public bool IsCheckedProperty
        {
            get => isCheckedProperty;
            set
            {
                isCheckedProperty = value;
                OnPropertyChanged();
            }
        }

        private bool isCheckedContract;
        public bool IsCheckedContract
        {
            get => isCheckedContract;
            set
            {
                isCheckedContract = value;
                OnPropertyChanged();
            }
        }

        private bool isCheckedApplicant;


        public bool IsCheckedApplicant
        {
            get => isCheckedApplicant;
            set
            {
                isCheckedApplicant = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region метод подписания уведомления 
        bool Signxml(ArrayList list2)
        {
            //подписание уведомления
            string csp = "\"C:/Program Files/Crypto Pro/CSP/csptest.exe\"";
            string putxml = App.Temp + App.NotificationId + "/" + App.NotificationId;

            #region Использование внутренней криптографии
            //using (FileStream fstr = File.OpenRead(putxml + ".xml"))
            //{//создаем поток из архива
            // // преобразуем строку в байты
            //    byte[] array = new byte[fstr.Length]; //создаем массив байтов длинною в поток архива
            //                                          //читаем поток файла
            //    fstr.Read(array, 0, array.Length); //записываем в массив байты из архива
            //    App.array = array;          //использую переменную статического класса чтобы вывести значение за using
            //}
            //byte[] sig_array = EisCmsSigner.Sign(App.array);


            //using (FileStream fstream = new FileStream("Temp/" + putxml + ".sig", FileMode.OpenOrCreate))
            //{
            //    // запись массива байтов в поток файла
            //    fstream.Write(sig_array, 0, sig_array.Length);

            //}

            //return true; 
            #endregion

            Process Process = Process.Start(new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/C " + csp + " -sfsign -sign -detached -add -in " + putxml + ".xml -out " + putxml + ".xml.sig -my " + list2[VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Applicant>().SelectedCertInCombobox].ToString(),
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true

            });
            Process.WaitForExit();
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

        #region Метод проверки наличия в БД залогодателей и формирование списка объектов залогодателей Entity
        void CheckAndSavePledgorDB(ObservableCollection<Model.Pledgor_Classes.Pledgor> ListPledgor)
        {
            foreach (Model.Pledgor_Classes.Pledgor pledgor in ListPledgor) //перебираем коллекцию залогодателей
            {
                if (pledgor.PrivatePerson != null) //если это физ. лицо
                {
                    #region Поиск залогодателя в БД
                    Persons person = contextNotification.Persons.Include(p => p.Region)
                                      .FirstOrDefault(p => p.First == pledgor.PrivatePerson.Name.First
                                      && p.Last == pledgor.PrivatePerson.Name.Last
                                      && p.Middle == pledgor.PrivatePerson.Name.Middle
                                      && p.BirthDate == DateOnly.FromDateTime(pledgor.PrivatePerson.BirthDate)
                                      && p.Region.Region == pledgor.PrivatePerson.PersonAddress.AddressRF.Region);
                    #endregion

                    #region Поиск региона в БД
                    Regions reg = contextNotification.Regions.FirstOrDefault(r => r.Region == pledgor.PrivatePerson.PersonAddress.AddressRF.Region);
                    #endregion

                    Ruzdi_DB.Entityes.Pledgor pledgorentity;


                    pledgorentity = (person != null) ? person : new Persons
                    {
                        BirthDate = DateOnly.FromDateTime(pledgor.PrivatePerson.BirthDate),
                        First = pledgor.PrivatePerson.Name.First,
                        Last = pledgor.PrivatePerson.Name.Last,
                        Middle = pledgor.PrivatePerson.Name.Middle,
                        PersonDocument = pledgor.PrivatePerson.PersonDocument.SeriesNumber,
                        Region = (reg != null) ? reg : new Regions
                        {
                            CodeRegion = pledgor.PrivatePerson.PersonAddress.AddressRF.RegionCode,
                            Region = pledgor.PrivatePerson.PersonAddress.AddressRF.Region
                        }
                    };
                    pledgorentity.Id = pledgorentity.GetHashCode().ToString();

                    Pledgors.Add(pledgorentity); //внесение в список Notification
                }
                else //если юр. лицо
                {
                    #region Поиск залогодателя в БД
                    Organizations organization = contextNotification.Organizations.Include(p => p.Region)
                                      .FirstOrDefault(p => p.NameFull == pledgor.Organization.RussianOrganization.NameFull
                                      && p.INN == pledgor.Organization.RussianOrganization.INN
                                      && p.OGRN == pledgor.Organization.RussianOrganization.OGRN
                                      && p.Region.Region == pledgor.Organization.RussianOrganization.Address.Region);
                    #endregion

                    #region Поиск региона в БД
                    Regions reg = contextNotification.Regions.FirstOrDefault(r => r.Region == pledgor.Organization.RussianOrganization.Address.Region);
                    #endregion

                    Ruzdi_DB.Entityes.Pledgor pledgorentity;
                    pledgorentity = (organization != null) ? organization : new Organizations
                    {
                        NameFull = pledgor.Organization.RussianOrganization.NameFull,
                        INN = pledgor.Organization.RussianOrganization.INN,
                        OGRN = pledgor.Organization.RussianOrganization.OGRN,
                        Email = pledgor.Organization.RussianOrganization.Email,
                        Region = (reg != null) ? reg : new Regions()
                        {
                            CodeRegion = pledgor.Organization.RussianOrganization.Address.RegionCode,
                            Region = pledgor.Organization.RussianOrganization.Address.Region
                        }

                    };
                    pledgorentity.Id = pledgorentity.GetHashCode().ToString();
                    Pledgors.Add(pledgorentity);
                }
            }
        }
        #endregion

        #region Метод создания объекта договора залога Entity для БД
        void CheckContractAndSaveDB(PledgeContract PledgeContract)
        {
            contract = new Contracts
            {
                Id = PledgeContract.GetHashCode().ToString(),
                Data = DateOnly.FromDateTime(PledgeContract.Date),
                Name = PledgeContract.Name,
                Number = PledgeContract.Number,
                TermOfContract = DateOnly.FromDateTime(PledgeContract.TermOfContract)
            };

        }
        #endregion

        #region BackCollection - метод отката преобразования коллекций
        public static void BackCollection()
        {
            #region коллекция имущества
            for (int i = 0; i < VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Property>().PersonalProperty.Count; i++)
            {
                if (VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Property>().PersonalProperty[i].OtherProperty is null) //если элемент коллекции это ТС
                {
                    VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Property>().PersonalProperty[i] = VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Property>().PersonalProperty[i].VehicleProperty;
                }
                else if (VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Property>().PersonalProperty[i].VehicleProperty is null)
                {
                    VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Property>().PersonalProperty[i] = VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Property>().PersonalProperty[i].OtherProperty;
                }
            }
            #endregion

            #region коллекция залогодателей
            for (int i = 0; i < VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgor>().Pledgors.Count; i++)
            {
                if (VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgor>().Pledgors[i].Organization is null)
                {
                    VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgor>().Pledgors[i] = VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgor>().Pledgors[i].PrivatePerson;
                }
                else if (VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgor>().Pledgors[i].PrivatePerson is null)
                {
                    VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgor>().Pledgors[i] = VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgor>().Pledgors[i].Organization;
                }
            }
            #endregion

            #region коллекция залогодержателей
            for (int i = 0; i < VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgee>().Pledgee.Count; i++)
            {
                if (VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgee>().Pledgee[i].PrivatePerson is null)
                {
                    VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgee>().Pledgee[i] = VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgee>().Pledgee[i].Organization;
                }
                else if (VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgee>().Pledgee[i].Organization is null)
                {
                    VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgee>().Pledgee[i] = VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgee>().Pledgee[i].PrivatePerson;
                }
            }
            #endregion

        }
        #endregion

        #region SendNotification - Команда отправки уведомления
        public ICommand SendNotification { get; }
        public bool CanSendNotificationCommandExecute(object p)
        {
            //должна быть логика проверки, что валидация успешна
            if (VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Applicant>().NotificationApplicant?.PrivatePerson == null)
            {
                return (VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Applicant>().SelectedApplicant != null)
                && (!IsView)
                && VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Applicant>().NotificationApplicant.Organization.IsValid
                && VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Applicant>().NotificationApplicant.Attorney.IsValid
                && VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Applicant>().NotificationApplicant.Attorney.Name.IsValid
                && VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Applicant>().NotificationApplicant.Attorney.PersonAddress.AddressRF.IsValid;
            }
            else
            {
                return (VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Applicant>().SelectedApplicant != null)
                && (!IsView)
                && VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Applicant>().NotificationApplicant.PrivatePerson.IsValid
                && VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Applicant>().NotificationApplicant.PrivatePerson.Name.IsValid;
            }

        }

        public async void OnSendNotificationCommandExecute(object p)
        {

            #region Вызов метода создания объекта договора залога Entity для БД
            CheckContractAndSaveDB(VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Contract>().Contract);
            #endregion

            #region преобразование коллекции имущества

            for (int i = 0; i < VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Property>().PersonalProperty.Count; i++)
            {
                if (VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Property>().PersonalProperty[i] is VehicleProperty vehicleProperty) //если элемент коллекции это ТС
                {
                    VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Property>().PersonalProperty[i] = new PersonalProperty
                    {
                        VehicleProperty = vehicleProperty
                    };
                }
                else if (VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Property>().PersonalProperty[i] is OtherProperty otherProperty)
                {
                    VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Property>().PersonalProperty[i] = new PersonalProperty
                    {
                        OtherProperty = otherProperty
                    };
                }
            }
            #endregion

            #region преобразование коллекции залогодателей

            for (int i = 0; i < VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgor>().Pledgors.Count; i++)
            {
                if (VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgor>().Pledgors[i] is PledgorOrganization pledgorOrganization)
                {
                    foreach (var region in contextNotification.Regions)
                    {
                        if (region.Region == pledgorOrganization.RussianOrganization.Address.Region)
                        {
                            pledgorOrganization.RussianOrganization.Address.RegionCode = region.CodeRegion;
                        }
                    }

                    VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgor>().Pledgors[i] = new Ruzdi_6.Model.Pledgor_Classes.Pledgor
                    {
                        Organization = pledgorOrganization
                    };
                }
                else if (VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgor>().Pledgors[i] is PledgorPrivatePerson pledgorPrivatePerson)
                {
                    foreach (var region in contextNotification.Regions)
                    {
                        if (region.Region == pledgorPrivatePerson.PersonAddress.AddressRF.Region)
                        {
                            pledgorPrivatePerson.PersonAddress.AddressRF.RegionCode = region.CodeRegion;
                            break;
                        }
                    }

                    VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgor>().Pledgors[i] = new Ruzdi_6.Model.Pledgor_Classes.Pledgor
                    {
                        PrivatePerson = pledgorPrivatePerson
                    };
                }
            }
            #endregion

            #region Вызов метода проверки наличия в БД залогодателя 
            CheckAndSavePledgorDB(VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgor>().Pledgors);
            #endregion

            #region преобразование коллекции залогодержателей
            for (int i = 0; i < VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgee>().Pledgee.Count; i++)
            {
                if (VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgee>().Pledgee[i] is PledgeeOrganization pledgeeOrganization)
                {

                    foreach (var region in contextNotification.Regions)
                    {
                        if (region.Region == pledgeeOrganization.RussianOrganization.Address.Region)
                        {
                            pledgeeOrganization.RussianOrganization.Address.RegionCode = region.CodeRegion;
                            break;
                        }
                    }

                    VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgee>().Pledgee[i] = new Pledgee
                    {
                        Organization = pledgeeOrganization
                    };
                }
                else if (VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgee>().Pledgee[i] is PledgeePrivatePerson pledgeePrivatePerson)
                {
                    foreach (var region in contextNotification.Regions)
                    {
                        if (region.Region == pledgeePrivatePerson.PersonAddress.AddressRF.Region)
                        {
                            pledgeePrivatePerson.PersonAddress.AddressRF.RegionCode = region.CodeRegion;
                            break;
                        }
                    }

                    VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgee>().Pledgee[i] = new Pledgee
                    {
                        PrivatePerson = pledgeePrivatePerson
                    };
                }
            }
            #endregion

            #region Заполняем код региона заявителя

            if (!(VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Applicant>().NotificationApplicant.Attorney is null) && String.IsNullOrEmpty(VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Applicant>().NotificationApplicant.Attorney.PersonAddress.AddressRF.RegionCode))
            {
                foreach (var region in contextNotification.Regions)
                {
                    if (region.Region == VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Applicant>().NotificationApplicant.Attorney.PersonAddress.AddressRF.Region)
                    {
                        VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Applicant>().NotificationApplicant.Attorney.PersonAddress.AddressRF.RegionCode = region.CodeRegion;
                        break;
                    }
                }
            }

            #endregion

            #region Генерация NotificationId и создание папки с именем NotificationId для временного хранения уведмления и подписи

            App.NotificationId = Guid.NewGuid().ToString(); //  генеируем NotificationId
            string TempNotificationId = App.Temp + App.NotificationId;    //папка для хранения уведомления и подписи
            string TempNotificationIdXml = TempNotificationId + "/" + App.NotificationId + ".xml";
            Directory.CreateDirectory(TempNotificationId);  // создаем папку куда потом сохраним уведомление 
            #endregion

            #region Создание объекта уведомления о залоге
            PledgeNotificationToNotary UZ1 = new PledgeNotificationToNotary
            {
                NotificationId = App.NotificationId,
                version = 2.3M,
                NotificationData = new NotificationData
                {
                    version = 2.3M,
                    FormUZ1 = new FormUZ1
                    {
                        PersonalProperties = VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Property>().PersonalProperty,
                        Pledgors = VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgor>().Pledgors,
                        Pledgee = VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgee>().Pledgee,
                        PledgeContract = VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Contract>().Contract,
                        NotificationApplicant = VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Applicant>().NotificationApplicant
                    }
                }
            };

            #endregion

            #region Сериализация xml-уведомления о залоге
            //сериализация xml
            using (FileStream fs = new FileStream(TempNotificationIdXml, FileMode.Create))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(PledgeNotificationToNotary));
                formatter.Serialize(fs, UZ1);
            }
            #endregion

            #region Подписание xml-файла уведомления
            if (!Signxml(VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Applicant>().ListThumbprint))
            {
                BackCollection(); //если возникла ошибка, то преобразуем коллекции обратно, чотбы они отображались в интерфейсе
                return;
            }// метод подписания уведомления 
            #endregion

            #region Архивирование уведомления и подписи
            // архивирование уведомления и подписи
            string sourceFile = TempNotificationId + "/"; // исходная папка(архивируем все, что в ней есть)
            string compressedFile = "Temp/" + App.NotificationId + ".zip"; // сжатый файл       
            ZipFile.CreateFromDirectory(sourceFile, compressedFile, CompressionLevel.Optimal, false);   // создание архива 
            #endregion

            #region Преобраование архива в массив byte для далейшего создания объекта-пакета
            using (FileStream fstr = File.OpenRead(compressedFile))
            {//создаем поток из архива
             // преобразуем строку в байты
                byte[] array = new byte[fstr.Length]; //создаем массив байтов длинною в поток архива
                                                      //читаем поток файла
                fstr.Read(array, 0, array.Length); //записываем в массив байты из архива
                App.array = array;          //использую переменную статического класса чтобы вывести значение за using
            }
            #endregion

            #region Генерируем гуид пакета
            App.guidp = Convert.ToString(Guid.NewGuid()); //генерируем гуид пакета 
            #endregion

            #region Создание объекта пакет-запрос
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

            #region Сохранение в БД архива и информации о созданном пакете
            //далее записываем в БД информацию о сформированном пакете
            //Запись сфорированного архива в БД
            string mystr;
            using (FileStream fstream = new FileStream(compressedFile, FileMode.OpenOrCreate))
            {
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                mystr = Convert.ToBase64String(array);
                Notification = new Notification
                {
                    Id = App.NotificationId,
                    Packageguid = App.guidp,
                    TypeNotification = "Возникновение",
                    ZipArchive = mystr,
                    PledgeContract = contract,
                    Pledgors = Pledgors,
                    Status = "Первичная отправка"
                };

                contextNotification.Add(Notification);
                contextNotification.SaveChanges();
                vM_ForGlavnaya.SourceDatagrid.Add(Notification);
            }
            #endregion

            #region Удаление временно созданных файлов из папки Temp
            // удаляем созданные файлы, т.к. более не нужны
            DirectoryInfo dirInfo = new DirectoryInfo(TempNotificationId);
            dirInfo.Delete(true);
            File.Delete(compressedFile);
            #endregion

            #region Создаем запрос и запускаем задачу выполнения данного запроса
            ruzdiUploadNotificationPackageService_v1_1PortTypeClient request = new ruzdiUploadNotificationPackageService_v1_1PortTypeClient(ruzdiUploadNotificationPackageService_v1_1PortTypeClient.EndpointConfiguration.ruzdiUploadNotificationPackageService_v1_1HttpSoap11Endpoint);

            Task<uploadNotificationPackageResponse> Zapros = Task.Run(() => request.uploadNotificationPackageAsync(package));
            #endregion

            #region Ожидаем завершения задачи и отображаем ответ от сервиса

            uploadNotificationPackageResponse response1 = await Zapros;

            if (!string.IsNullOrEmpty(response1.registrationId))
            {
                MessageBox.Show("Уведомление успешно отправлено");
            }
            else
            {
                MessageBox.Show($"response1.packageStateCode.message - {response1.packageStateCode.message}");
            }                        

            #endregion

            #region Сохранение registrationId в БД

            Notification.Packageid = response1.registrationId;
            contextNotification.Update(Notification);
            contextNotification.SaveChanges();

            #endregion

            serviceWindow.CloseWindowDialog("UZ1");
        }
        #endregion
    }
}