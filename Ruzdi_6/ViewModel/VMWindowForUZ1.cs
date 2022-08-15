using Microsoft.Extensions.DependencyInjection;
using Ruzdi_6.Commands;
using Ruzdi_6.Model.Other_Classes;
using Ruzdi_6.Model.Pledgee_Classes;
using Ruzdi_6.Model.Pledgor_Classes;
using Ruzdi_6.Model.Property_Classes;
using Ruzdi_6.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using UploadNotification;

namespace Ruzdi_6.ViewModel
{
    public class VMWindowForUZ1 : ViewModel
    {
        public VMWindowForUZ1()
        {
            #region Команды
            
            ToPledgorsCommand = new RelayCommand(OnToPledgorsCommandExecute, CanToPledgorsCommandExecute);
            ToPledgeeCommand = new RelayCommand(OnToPledgeeCommandExecute, CanToPledgeeCommandExecute);
            ToProperty = new RelayCommand(OnToPropertyCommandExecute, CanToPropertyCommandExecute);
            ToContractCommand = new RelayCommand(OnToContractCommandExecute, CanToContractCommandExecute);
            ToApplicantCommand = new RelayCommand(OnToApplicantCommandExecute, CanToApplicantCommandExecute);


            #region Команды заявителя
            /*
            SendNotification = new RelayCommand(OnSendNotificationCommandExecute, CanSendNotificationCommandExecute);

            */
            #endregion
            
            #endregion

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

            
            CurrentContentVM = App.Services.GetRequiredService<VM_Pledgor>();
            
        }

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

        #region Поля и методы Singletone
        private static VMWindowForUZ1 VW_UZ1;
        public static VMWindowForUZ1 Get_VW_UZ1()
        {
            if (VW_UZ1 == null)
            {
                VW_UZ1 = new VMWindowForUZ1();
            }
            return VW_UZ1;
        }

        /// <summary>
        /// Метод для сброса экземпляра VW_UZ1
        /// </summary>
        public static void SetNull_VW_UZ1()
        {
            VW_UZ1 = null;
        }
        #endregion

        #region Выбранная VM


        private ViewModel currentContentVM;
        public ViewModel CurrentContentVM
        {
            get => currentContentVM;
            set => Set(ref currentContentVM, value);
        }
        #endregion

        #region Коментировать
      
        #region Команды навигации

        #region ToPledgors - команда перехода к залогодателю
        /// <summary>
        /// команда сохранения залогодателя юр. лица РФ
        /// </summary>
        public ICommand ToPledgorsCommand { get; }
        public bool CanToPledgorsCommandExecute(object p)
        {
            //должна быть логика проверки, что валидация успешна
            return true;
        }
        public void OnToPledgorsCommandExecute(object p)
        {
            CurrentContentVM = VM_Pledgor.Get_VM_Pledgor_UZ1();
            IsCheckedPledgor = true;
        }
        #endregion

        #region ToPledgee - команда перехода к залогодержателю
        /// <summary>
        /// команда перехода к залогодержателю
        /// </summary>
        public ICommand ToPledgeeCommand { get; }
        public bool CanToPledgeeCommandExecute(object p)
        {
            return (VM_Pledgor.Get_VM_Pledgor_UZ1().Pledgors.Count > 0 && !IsView) || IsView;
        }
        public void OnToPledgeeCommandExecute(object p)
        {

            CurrentContentVM = VM_Pledgee.GetVM_Pledgee_UZ1();
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
            return (VM_Pledgee.GetVM_Pledgee_UZ1().Pledgee.Count > 0 && !IsView) || IsView;
        }
        public void OnToPropertyCommandExecute(object p)
        {
            CurrentContentVM = VM_Property.GetVM_Property_UZ1();
            IsCheckedProperty = true;
        }
        #endregion

        #region ToContractCommand - команда перехода к договору залога
        /// <summary>
        /// команда перехода к договору залога
        /// </summary>
        public ICommand ToContractCommand { get; }
        public bool CanToContractCommandExecute(object p)
        {
            //должна быть логика проверки, что валидация успешна
            return (VM_Property.GetVM_Property_UZ1().PersonalProperty.Count > 0 && !IsView) || IsView;
        }
        public void OnToContractCommandExecute(object p)
        {
            CurrentContentVM = VM_Contract.GetVM_Contract_UZ1();
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
            return (VM_Pledgor.Get_VM_Pledgor_UZ1().Pledgors.Count > 0
                && VM_Pledgee.GetVM_Pledgee_UZ1().Pledgee.Count > 0
                && VM_Property.GetVM_Property_UZ1().PersonalProperty.Count > 0
                && VM_Contract.GetVM_Contract_UZ1().Contract.IsValid
                && !IsView) || IsView;
        }
        public void OnToApplicantCommandExecute(object p)
        {
            CurrentContentVM = VM_Applicant.Get_VM_Applicant();
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

       
       //#endregion

       #region метод подписания уведомления 
       bool Signxml(ArrayList list2)
       {
           //подписание уведомления
           string csp = "\"C:/Program Files/Crypto Pro/CSP/csptest.exe\"";
           string putxml = App.Temp + App.NotificationId + "/" + App.NotificationId;
           Process Process = Process.Start(new ProcessStartInfo
           {
               FileName = "cmd.exe",
               Arguments = "/C " + csp + " -sfsign -sign -detached -add -in " + putxml + ".xml -out " + putxml + ".xml.sig -my " + list2[VM_Applicant.Get_VM_Applicant().SelectedCertInCombobox].ToString(),
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
        /*
      #region Метод проверки наличия в БД залогодателя и сохранение его в БД
      void CheckAndSavePledgorDB(ObservableCollection<Pledgor> ListPledgor)
      {
          Query query = new SelectForSavePledgor();
          foreach (Pledgor pledgor in ListPledgor) //перебираем коллекцию залогодателей
          {
              if (pledgor is PledgorPrivatePerson PledgorPrivatePerson) //если это физ. лицо
              {
                  query.SetSelect(new SavePledgor());
                  if (!App.ExistPerson((PledgorPrivatePerson.Name.Last + PledgorPrivatePerson.Name.First + PledgorPrivatePerson.Name.Middle + PledgorPrivatePerson.BirthDate).GetHashCode().ToString())) //проверяем, есть ли в БД такое физ.лицо
                  {   // если нет, то вносим в БД                                                                                                                                
                      //метод сохраняет в БД данные физ. лица
                      query.SavePledgor(PledgorPrivatePerson.Name.Last, PledgorPrivatePerson.Name.First, PledgorPrivatePerson.Name.Middle, PledgorPrivatePerson.BirthDate, PledgorPrivatePerson.PersonDocument.SeriesNumber, PledgorPrivatePerson.PersonAddress.AddressRF.Region);
                  }
              }
              else //если юр. лицо
              {
                  query.SetSelect(new SavePledgee());
                  PledgorOrganization pledgor1 = (PledgorOrganization)pledgor;
                  if (!App.ExistOrganization(App.HashPledgee.ToString())) //проверяем, есть ли в БД такая организация
                  {    //если нет, то вносим в БД
                      query.SavePledgee(pledgor1.RussianOrganization.NameFull, pledgor1.RussianOrganization.OGRN.ToString(), pledgor1.RussianOrganization.INN.ToString(), pledgor1.RussianOrganization.Email, pledgor1.RussianOrganization.Address.Region); //метод сохраняет в Бд сведения о юр. лице         
                  }
              }
          }
      }
      #endregion

      #region Метод проверки наличия в БД договора залога и сохранение его туда
      void CheckContractAndSaveDB(PledgeContract PledgeContract)
      {
          Query query = new SelectForSavePledgor();
          if (!App.ExistContracts((PledgeContract.Date + PledgeContract.Number + PledgeContract.TermOfContract).GetHashCode().ToString()))
          {// если нет, то вносим в БД
              int HashContract = (PledgeContract.Date + PledgeContract.Number + PledgeContract.TermOfContract).GetHashCode();
              query.SetSelect(new SavePledgeContract());
              query.SavePledgeContract(PledgeContract.Date, PledgeContract.Number, PledgeContract.Name, PledgeContract.TermOfContract, HashContract); //метод сохраняет в БД информацию о договоре залога
          }
      }
      #endregion

      #region BackCollection - метод отката преобразования коллекций
      public static void BackCollection()
      {
          #region коллекция имущества
          for (int i = 0; i < VM_Property.GetVM_Property_UZ1().PersonalProperty.Count; i++)
          {
              if (VM_Property.GetVM_Property_UZ1().PersonalProperty[i].OtherProperty is null) //если элемент коллекции это ТС
              {
                  VM_Property.GetVM_Property_UZ1().PersonalProperty[i] = VM_Property.GetVM_Property_UZ1().PersonalProperty[i].VehicleProperty;
              }
              else if (VM_Property.GetVM_Property_UZ1().PersonalProperty[i].VehicleProperty is null)
              {
                  VM_Property.GetVM_Property_UZ1().PersonalProperty[i] = VM_Property.GetVM_Property_UZ1().PersonalProperty[i].OtherProperty;
              }
          }
          #endregion

          #region коллекция залогодателей
          for (int i = 0; i < VM_Pledgor.Get_VM_Pledgor_UZ1().Pledgors.Count; i++)
          {
              if (VM_Pledgor.Get_VM_Pledgor_UZ1().Pledgors[i].Organization is null)
              {
                  VM_Pledgor.Get_VM_Pledgor_UZ1().Pledgors[i] = VM_Pledgor.Get_VM_Pledgor_UZ1().Pledgors[i].PrivatePerson;
              }
              else if (VM_Pledgor.Get_VM_Pledgor_UZ1().Pledgors[i].PrivatePerson is null)
              {
                  VM_Pledgor.Get_VM_Pledgor_UZ1().Pledgors[i] = VM_Pledgor.Get_VM_Pledgor_UZ1().Pledgors[i].Organization;
              }
          }
          #endregion

          #region коллекция залогодержателей
          for (int i = 0; i < VM_Pledgee.GetVM_Pledgee_UZ1().Pledgee.Count; i++)
          {
              if (VM_Pledgee.GetVM_Pledgee_UZ1().Pledgee[i].PrivatePerson is null)
              {
                  VM_Pledgee.GetVM_Pledgee_UZ1().Pledgee[i] = VM_Pledgee.GetVM_Pledgee_UZ1().Pledgee[i].Organization;
              }
              else if (VM_Pledgee.GetVM_Pledgee_UZ1().Pledgee[i].Organization is null)
              {
                  VM_Pledgee.GetVM_Pledgee_UZ1().Pledgee[i] = VM_Pledgee.GetVM_Pledgee_UZ1().Pledgee[i].PrivatePerson;
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
          if (VM_Applicant.Get_VM_Applicant().NotificationApplicant?.PrivatePerson == null)
          {
              return (VM_Applicant.Get_VM_Applicant().SelectedApplicant != null)
              && (!IsView)
              && VM_Applicant.Get_VM_Applicant().NotificationApplicant.Organization.IsValid
              && VM_Applicant.Get_VM_Applicant().NotificationApplicant.Attorney.IsValid
              && VM_Applicant.Get_VM_Applicant().NotificationApplicant.Attorney.Name.IsValid
              && VM_Applicant.Get_VM_Applicant().NotificationApplicant.Attorney.PersonAddress.AddressRF.IsValid;
          }
          else
          {
              return (VM_Applicant.Get_VM_Applicant().SelectedApplicant != null)
              && (!IsView)
              && VM_Applicant.Get_VM_Applicant().NotificationApplicant.PrivatePerson.IsValid
              && VM_Applicant.Get_VM_Applicant().NotificationApplicant.PrivatePerson.Name.IsValid;
          }

      }
      public void OnSendNotificationCommandExecute(object p)
      {
          Query query = new SelectForGetCodeRegion();

          #region Проверка наличия в БД залогодателя и сохранение его в БД           
          CheckAndSavePledgorDB(VM_Pledgor.Get_VM_Pledgor_UZ1().Pledgors);
          #endregion

          #region Проверка наличия в БД договора залога и сохранение его туда
          CheckContractAndSaveDB(VM_Contract.GetVM_Contract_UZ1().Contract);
          #endregion

          #region Генерация NotificationId и создание папки с именем NotificationId для временного хранения уведмления и подписи

          App.NotificationId = Guid.NewGuid().ToString(); //  генеируем NotificationId
          string TempNotificationId = App.Temp + App.NotificationId;    //папка для хранения уведомления и подписи
          string TempNotificationIdXml = TempNotificationId + "/" + App.NotificationId + ".xml";
          Directory.CreateDirectory(TempNotificationId);  // создаем папку куда потом сохраним уведомление 
          #endregion       

          #region преобразование коллекции имущества

          for (int i = 0; i < VM_Property.GetVM_Property_UZ1().PersonalProperty.Count; i++)
          {
              if (VM_Property.GetVM_Property_UZ1().PersonalProperty[i] is VehicleProperty vehicleProperty) //если элемент коллекции это ТС
              {
                  VM_Property.GetVM_Property_UZ1().PersonalProperty[i] = new PersonalProperty
                  {
                      VehicleProperty = vehicleProperty
                  };
              }
              else if (VM_Property.GetVM_Property_UZ1().PersonalProperty[i] is OtherProperty otherProperty)
              {
                  VM_Property.GetVM_Property_UZ1().PersonalProperty[i] = new PersonalProperty
                  {
                      OtherProperty = otherProperty
                  };
              }
          }
          #endregion

          #region преобразование коллекции залогодателей

          for (int i = 0; i < VM_Pledgor.Get_VM_Pledgor_UZ1().Pledgors.Count; i++)
          {
              if (VM_Pledgor.Get_VM_Pledgor_UZ1().Pledgors[i] is PledgorOrganization pledgorOrganization)
              {
                  pledgorOrganization.RussianOrganization.Address.RegionCode = query.GetCodeRegion(pledgorOrganization.RussianOrganization.Address.Region);
                  VM_Pledgor.Get_VM_Pledgor_UZ1().Pledgors[i] = new Pledgor
                  {
                      Organization = pledgorOrganization
                  };
              }
              else if (VM_Pledgor.Get_VM_Pledgor_UZ1().Pledgors[i] is PledgorPrivatePerson pledgorPrivatePerson)
              {
                  pledgorPrivatePerson.PersonAddress.AddressRF.RegionCode = query.GetCodeRegion(pledgorPrivatePerson.PersonAddress.AddressRF.Region);
                  VM_Pledgor.Get_VM_Pledgor_UZ1().Pledgors[i] = new Pledgor
                  {
                      PrivatePerson = pledgorPrivatePerson
                  };
              }
          }
          #endregion

          #region преобразование коллекции залогодержателей
          for (int i = 0; i < VM_Pledgee.GetVM_Pledgee_UZ1().Pledgee.Count; i++)
          {
              if (VM_Pledgee.GetVM_Pledgee_UZ1().Pledgee[i] is PledgeeOrganization pledgeeOrganization)
              {
                  pledgeeOrganization.RussianOrganization.Address.RegionCode = query.GetCodeRegion(pledgeeOrganization.RussianOrganization.Address.Region);
                  VM_Pledgee.GetVM_Pledgee_UZ1().Pledgee[i] = new Pledgee
                  {
                      Organization = pledgeeOrganization
                  };
              }
              else if (VM_Pledgee.GetVM_Pledgee_UZ1().Pledgee[i] is PledgeePrivatePerson pledgeePrivatePerson)
              {
                  pledgeePrivatePerson.PersonAddress.AddressRF.RegionCode = query.GetCodeRegion(pledgeePrivatePerson.PersonAddress.AddressRF.Region);
                  VM_Pledgee.GetVM_Pledgee_UZ1().Pledgee[i] = new Pledgee
                  {
                      PrivatePerson = pledgeePrivatePerson
                  };
              }
          }
          #endregion

          #region Заполняем код региона заявителя

          if (!(VM_Applicant.Get_VM_Applicant().NotificationApplicant.Attorney is null) && String.IsNullOrEmpty(VM_Applicant.Get_VM_Applicant().NotificationApplicant.Attorney.PersonAddress.AddressRF.RegionCode))
          {
              VM_Applicant.Get_VM_Applicant().NotificationApplicant.Attorney.PersonAddress.AddressRF.RegionCode = query.GetCodeRegion(VM_Applicant.Get_VM_Applicant().NotificationApplicant.Attorney.PersonAddress.AddressRF.Region);
          }

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
                      PersonalProperties = VM_Property.GetVM_Property_UZ1().PersonalProperty,
                      Pledgors = VM_Pledgor.Get_VM_Pledgor_UZ1().Pledgors,
                      Pledgee = VM_Pledgee.GetVM_Pledgee_UZ1().Pledgee,
                      PledgeContract = VM_Contract.GetVM_Contract_UZ1().Contract,
                      NotificationApplicant = VM_Applicant.Get_VM_Applicant().NotificationApplicant
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
          if (!Signxml(VM_Applicant.Get_VM_Applicant().ListThumbprint))
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
          query.SetSelect(new SaveInfoUZ1());
          query.SaveInfoPackageUZ1();      //метод сохраняет в Бд информацию об архиве
                                           //
                                           //Запись сфорированного архива в БД
          using (FileStream fstream = new FileStream(compressedFile, FileMode.OpenOrCreate))
          {
              byte[] array = new byte[fstream.Length];
              // считываем данные
              fstream.Read(array, 0, array.Length);
              // декодируем байты в строку
              string mystr = Convert.ToBase64String(array);
              query.SetUpdate(new SaveArchiveNotificationAndSig());
              query.SaveArchiveNitificationAndSig(mystr); //метод сохраняет в БД архив с уведомление и подписью в виде base64  
          }
          #endregion

          #region Удаление временно созданных файлов из папки Temp
          // удаляем созданные файлы, т.к. более не нужны
          DirectoryInfo dirInfo = new DirectoryInfo(TempNotificationId);
          dirInfo.Delete(true);
          File.Delete(compressedFile);
          #endregion

          #region Создаем запрос и запускаем задачу выполнения данного запроса
          ruzdiUploadNotificationPackageService_v1_1PortTypeClient request = new ruzdiUploadNotificationPackageService_v1_1PortTypeClient("ruzdiUploadNotificationPackageService_v1_1HttpSoap11Endpoint");

          Task<uploadNotificationPackageResponse> Zapros = Task.Run(() => request.uploadNotificationPackageAsync(package));
          //await Zapros;
          #endregion

          #region Ожидает завершения задачи и отображаем ответ от сервиса

          uploadNotificationPackageResponse response1 = Zapros.Result;

          MessageBox.Show($"registrationId - { response1.registrationId}");
          MessageBox.Show($"response1.packageStateCode.message - { response1.packageStateCode.message}");

          //if (response.packageStateCode.code != "0")
          //{
          //    MessageBox.Show($"код ошибки - {response.packageStateCode.code}");
          //    MessageBox.Show($"код ошибки - {response.packageStateCode.message}");
          //} 
          #endregion

          #region Сохранение registrationId в БД
          query.SetUpdate(new SaveRegistrationID());
          query.SaveRegistrationID(response1.registrationId);
          #endregion

          #region Обновление коллекции главного окна
          query.SetSelect(new UpdateDatagrid());
          VM_ForGlavnaya.Get_VM_ForGlavnaya().DataGridCollection.Source = query.GetCollectionForDataGrid(); // обновление коллекции
          VM_ForGlavnaya.Get_VM_ForGlavnaya().DataGridCollection.View.Refresh();
          VM_ForGlavnaya.Get_VM_ForGlavnaya().UpdateCollectionDataGrid();

          #endregion

          IWindowService service = new ServiceWindow();
          service.CloseWindowDialog("UZ1");

      }
      #endregion



      #region Свойство для дизайнера
      public static VMWindowForUZ1 VM_UZ1_ForDesiner
      {
          get
          {
              if (vM_UZ1_ForDesiner == null)
              {
                  vM_UZ1_ForDesiner = new VMWindowForUZ1();
              }
              return vM_UZ1_ForDesiner;
          }
      }
      private static VMWindowForUZ1 vM_UZ1_ForDesiner;
      #endregion 
     */
        #endregion

    }
}