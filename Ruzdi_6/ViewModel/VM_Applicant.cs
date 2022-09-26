using Ruzdi_6.Model.Applicant_Classes;
using Ruzdi_6.Model.Other_Classes;
using Ruzdi_6.Model.Pledgor_Classes;
using Ruzdi_6.Model.Pledgee_Classes;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Ruzdi_6.ViewModel;

public class VM_Applicant : ViewModel
{
    public VM_Applicant()
    {
        if (App.DesignMode)
        {
            //DisplayApplicant = new ApplicantPrivatePerson
            //{
            //    Name = new PrivatePersonName
            //    {
            //        Last = "Last",
            //        First = "First",
            //        Middle = "Middle"
            //    },
            //    Email = "12@123.ru"
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

            DisplayApplicant = new NotificationApplicant
            {
                Role = 2,
                Organization = new ApplicantOrganization
                {
                    NameFull = "NameFull",
                    UINN = "INN",
                    URN = "OGRN",
                    //Email = ""
                },
                Attorney = new ApplicantAttorney
                {
                    Name = new ApplicantAttorneyName
                    {
                        First = "First",
                        Last = "Last",
                        Middle = "Middle"
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
        else
        {

            #region ApplicantList - список возможных заявителей

            ApplicantList = new CompositeCollection(2);

            CollectionContainer ApplicantPledgors = new CollectionContainer
            {
                Collection = VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgor>().Pledgors
            };

            CollectionContainer ApplicantPledgee = new CollectionContainer
            {
                Collection = VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgee>().Pledgee
            };

            ApplicantList.Add(ApplicantPledgors);
            ApplicantList.Add(ApplicantPledgee);

            #endregion

            SourceComboRegion = App.Region_list;

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
        }
    }

  

    private NotificationApplicant displayApplicant;
    public NotificationApplicant DisplayApplicant
    {
        get => displayApplicant;
        set => Set(ref displayApplicant, value);
    }


    #region ApplicantList - Коллекция для отображения перечня возможных заявителей в combobx
    public CompositeCollection ApplicantList { get; set; }
    #endregion

    private NotificationApplicant notificationApplicant;
    public NotificationApplicant NotificationApplicant
    {
        get => notificationApplicant;
        set => Set(ref notificationApplicant, value);
    }
    public bool IsView { get; set; }

    #region SelectedApplicant - Выбранный из списка заявитель используется тип класса родителя для upcast

    private FormUZ1 selectedapplicant;
    public FormUZ1 SelectedApplicant
    {
        get => selectedapplicant;
        set
        {
            Set(ref selectedapplicant, value);
            #region Определяем тип выбранного заявителя и на его основании формируем объект-заявителя
            if (value is PledgorOrganization PledgorOrganization) //если это юр. лицо залогодатель
            {
                NotificationApplicant = new NotificationApplicant
                {
                    Role = 1,
                    Organization = new ApplicantOrganization
                    {
                        NameFull = PledgorOrganization.RussianOrganization.NameFull,
                        UINN = PledgorOrganization.RussianOrganization.INN,
                        URN = PledgorOrganization.RussianOrganization.OGRN,
                        Email = PledgorOrganization.RussianOrganization.Email,

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
                                registration = true,
                                Region = "",
                                RegionCode = "",
                                City = "",
                                Locality = "",
                                Street = "",
                                District = "",
                                House = "",
                                Building = "",
                                Apartment = ""
                            }
                        }
                    }
                };
                DisplayApplicant = NotificationApplicant; // установка значения для шаблонов в xaml 
            }
            else if (value is PledgorPrivatePerson PledgorPrivatePerson) //если это физ. лицо залогодатель
            {
                NotificationApplicant = new NotificationApplicant
                {
                    Role = 1,
                    PrivatePerson = new ApplicantPrivatePerson
                    {
                        Name = new PrivatePersonName
                        {
                            Last = PledgorPrivatePerson.Name.Last,
                            First = PledgorPrivatePerson.Name.First,
                            Middle = PledgorPrivatePerson.Name.Middle
                        },
                        Email = PledgorPrivatePerson.Email
                    }
                };
                DisplayApplicant = NotificationApplicant.PrivatePerson; // установка значения для шаблонов в xaml
            }
            else if (value is PledgeeOrganization PledgeeOrganization) //если это юр. лицо залогодержатель
            {
                NotificationApplicant = new NotificationApplicant
                {
                    Role = 2,                    
                    Organization = new ApplicantOrganization
                    {
                        NameFull = PledgeeOrganization.RussianOrganization.NameFull,
                        UINN = PledgeeOrganization.RussianOrganization.INN,
                        URN = PledgeeOrganization.RussianOrganization.OGRN,
                        Email = PledgeeOrganization.RussianOrganization.Email
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
                                registration = true,
                                Region = "",
                                RegionCode = "",
                                City = "",
                                Locality = "",
                                Street = "",
                                District = "",
                                House = "",
                                Building = "",
                                Apartment = ""
                            }
                        }
                    }
                };
                DisplayApplicant = NotificationApplicant; // установка значения для шаблонов в xaml 
            }
            else if (value is PledgeePrivatePerson PledgeePrivatePerson) //если это физ. лицо залогодержатель
            {
                NotificationApplicant = new NotificationApplicant
                {
                    Role = 2,
                    PrivatePerson = new ApplicantPrivatePerson
                    {
                        Name = new PrivatePersonName
                        {
                            Last = PledgeePrivatePerson.Name.Last,
                            First = PledgeePrivatePerson.Name.First,
                            Middle = PledgeePrivatePerson.Name.Middle
                        },
                        Email = PledgeePrivatePerson.Email
                    }
                };
                DisplayApplicant = NotificationApplicant.PrivatePerson; // установка значения для шаблонов в xaml
            }
            #endregion
        }
    }
    #endregion

    public List<string> SourceComboRegion { get; set; }

    #region ListThumbprint - Список, хранящий отпечатки сертификатов

    private ArrayList listThumbprint;
    public ArrayList ListThumbprint
    {
        get => listThumbprint;
        set => listThumbprint = value;
    }
    #endregion

    #region List - Список сертфикатов
    private ArrayList list;


    public ArrayList List
    {
        get => list;
        set => list = value;
    }
    #endregion

    #region SelectedCertInCombobox - индекс выбранного из списка сертифката
    public int SelectedCertInCombobox { get; set; }
    #endregion

}
