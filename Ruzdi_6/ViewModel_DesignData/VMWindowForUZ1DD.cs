using Ruzdi_6.Model.Applicant_Classes;
using Ruzdi_6.ViewModel;

namespace Ruzdi_6.ViewModel_DesignData
{
    public class VMWindowForUZ1DD
    {
        public VMWindowForUZ1DD()
        {
            CurrentContentVM = new VM_Applicant
            {
                DisplayApplicant = new NotificationApplicant
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
                }
            };
        }

        #region Выбранная VM

        public ViewModel.ViewModel CurrentContentVM { get; set; }
        #endregion
    }

}
