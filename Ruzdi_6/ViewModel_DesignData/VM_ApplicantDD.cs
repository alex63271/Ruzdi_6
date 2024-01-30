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
            SelectedApplicant = new ApplicantPrivatePerson
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
