using Ruzdi_6.Model.Applicant_Classes;
using Ruzdi_6.Model.Other_Classes;

namespace Ruzdi_6.ViewModel_DesignData
{
    public class VM_ApplicantDD
    {
        public VM_ApplicantDD()
        {
            DisplayApplicant = new ApplicantPrivatePerson
            {
                Name = new PrivatePersonName
                {
                    Last = "Last",
                    First = "First",
                    Middle = "Middle"
                },
                Email = "12@123.ru"
            };
            //DisplayApplicant = new NotificationApplicant
            //{
            //    Role = 2,
            //    Organization = new ApplicantOrganization
            //    {
            //        NameFull = "NameFull",
            //        UINN = "INN",
            //        URN = "OGRN",
            //        Email = ""
            //    },
            //    Attorney = new ApplicantAttorney
            //    {
            //        Name = new ApplicantAttorneyName
            //        {
            //            First = "фамилия",
            //            Last = "имя",
            //            Middle = "отчество"
            //        },
            //        BirthDate = DateTime.Now,
            //        Authority = "основания полномочий",
            //        PersonDocument = new ApplicantAttorneyPersonDocument
            //        {
            //            Code = 21,
            //            Name = "Паспорт",
            //            SeriesNumber = "1234567890"
            //        },
            //        PersonAddress = new ApplicantAttorneyPersonAddress
            //        {
            //            AddressRF = new ApplicantAttorneyPersonAddressAddressRF
            //            {
            //                registration = true,
            //                Region = "Москва",
            //                District = "район",
            //                City = "Москва",
            //                Locality = "Нас. пункт",
            //                Street = "улица",
            //                House = "Дом",
            //                Building = "строение",
            //                Apartment = "квартира",
            //            }
            //        }
            //    }
            //};

            //#region Конструкция чтения хранилища сертификатов и сохранения их перечня в коллекции
            //using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            //{
            //    List = new ArrayList();
            //    ListThumbprint = new ArrayList();
            //    store.Open(OpenFlags.ReadOnly);
            //    // Проходим по всем сертификатам 
            //    foreach (X509Certificate2 cert in store.Certificates)
            //    {
            //        if (cert.NotAfter > DateTime.Now) // выбираем сертификаты с действующим сроком
            //        {
            //            string zap = ",";
            //            string otvet = cert.SubjectName.Name + zap; //добавляем запятую в конец чтобы искался последний элемент в строке
            //            if (otvet.Contains("CN=") && otvet.Contains("SN=") && otvet.Contains("G="))  //отсеиваем сертификаты без нужных атрибутов
            //            {
            //                if (otvet.Contains("ОГРН="))//если есть ОГРН. значит юр лицо
            //                {
            //                    string s = "CN=";
            //                    string CN = otvet.Substring(otvet.IndexOf(s) + s.Length, otvet.IndexOf(zap, otvet.IndexOf(s)) - (otvet.IndexOf(s) + s.Length));
            //                    s = "SN=";
            //                    string SN = otvet.Substring(otvet.IndexOf(s) + s.Length, otvet.IndexOf(zap, otvet.IndexOf(s)) - (otvet.IndexOf(s) + s.Length));
            //                    s = "G=";
            //                    string G = otvet.Substring(otvet.IndexOf(s) + s.Length, otvet.IndexOf(zap, otvet.IndexOf(s)) - (otvet.IndexOf(s) + s.Length));
            //                    string stroka = CN + ", " + SN + " " + G;    //создаем строку для записи её в лист
            //                    List.Add(stroka);   //записываем строку в лист для отображения в интерфейсе
            //                    ListThumbprint.Add(cert.Thumbprint);  // лист2 для программного выбора сертификата(содержит отпечатки сертификатов)
            //                }
            //                else //если физ. лицо
            //                {
            //                    string CN = otvet.Substring(otvet.IndexOf("CN=") + "CN=".Length, otvet.IndexOf(zap, otvet.IndexOf("CN=")) - (otvet.IndexOf("CN=") + "CN=".Length));
            //                    List.Add(CN);   //записываем строку в лист для отображения в интерфейсе
            //                    ListThumbprint.Add(cert.Thumbprint);  // лист2 для программного выбора сертификата(содержит отпечатки сертификатов)
            //                }
            //            }
            //        }
            //    }
            //}
            //#endregion

            #region ApplicantList - список возможных заявителей
            /*
            ApplicantList = new CompositeCollection(2);

            CollectionContainer ApplicantPledgors = new CollectionContainer
            {
                Collection = VM_Pledgor.Get_VM_Pledgor_UZ1().Pledgors
            };

            CollectionContainer ApplicantPledgee = new CollectionContainer
            {
                Collection = VM_Pledgee.GetVM_Pledgee_UZ1().Pledgee
            };

            ApplicantList.Add(ApplicantPledgors);
            ApplicantList.Add(ApplicantPledgee);
            */
            #endregion
            SelectedApplicant = new NotificationApplicant
            {
                Role = 2,
                Organization = new ApplicantOrganization
                {
                    NameFull = "NameFull",
                    UINN = "INN",
                    URN = "OGRN",
                    Email = ""
                },
                Attorney = new ApplicantAttorney
                {
                    Name = new ApplicantAttorneyName
                    {
                        First = "фамилия",
                        Last = "имя",
                        Middle = "отчество"
                    },
                    BirthDate = DateTime.Now,
                    Authority = "основания полномочий",
                    PersonDocument = new ApplicantAttorneyPersonDocument
                    {
                        Code = 21,
                        Name = "Паспорт",
                        SeriesNumber = "1234567890"
                    },
                    PersonAddress = new ApplicantAttorneyPersonAddress
                    {
                        AddressRF = new ApplicantAttorneyPersonAddressAddressRF
                        {
                            registration = true,
                            Region = "Москва",
                            District = "район",
                            City = "Москва",
                            Locality = "Нас. пункт",
                            Street = "улица",
                            House = "Дом",
                            Building = "строение",
                            Apartment = "квартира",
                        }
                    }
                }
            };



           



        }

        public NotificationApplicant DisplayApplicant { get; set; }

        public NotificationApplicant SelectedApplicant { get; set; }
    }
}
