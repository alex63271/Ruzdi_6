using Ruzdi_6.Model.Other_Classes;
using Ruzdi_6.Model.Pledgee_Classes;
using System.Collections.ObjectModel;

namespace Ruzdi_6.ViewModel_DesignData
{
    public class VM_PledgeeDD
    {
        public VM_PledgeeDD()
        {
            Pledgee = new ObservableCollection<Pledgee>()
                {
                    new PledgeeOrganization
                     {
                        RussianOrganization= new RussianOrganization
                        {
                            NameFull="Какой-то залогодержатель",
                            INN="132456",
                            OGRN = "1234567890",
                            Email = "14546",
                            Address = new RussianOrganizationAddress
                            {
                                Region = "Москва",
                                RegionCode = "77",
                                City = "Город",
                                Locality = "",
                                Street="",
                                District="",
                                House="",
                                Building ="",
                                Apartment=""
                            },
                        }
                     },
                    new PledgeePrivatePerson
                     {
                          Name = new PrivatePersonName
                          {
                              Last ="Кирриллов",
                              First="Киррилл",
                              Middle="Иваныч"
                          },
                          BirthDate = DateTime.Now,
                          PersonDocument= new PrivatePersonDocument
                          {
                              Code=21,
                              Name="паспорт",
                              SeriesNumber="1234567890"
                          },
                          PersonAddress = new PrivatePersonPersonAddress
                          {
                              AddressRF= new PrivatePersonPersonAddressAddressRF
                              {
                                Region = "Москва",
                                RegionCode = "77",
                                City = "Город",
                                Locality = "",
                                Street="",
                                District="",
                                House="",
                                Building ="",
                                Apartment=""
                              }
                          }
                     }
                };

            SourceComboRegion = App.Region_list;

            SelectPledgee = new PledgeeOrganization
            {
                RussianOrganization = new RussianOrganization
                {
                    NameFull = "Какой-то залогодержатель",
                    INN = "132456",
                    OGRN = "1234567890",
                    Email = "14546",
                    Address = new RussianOrganizationAddress
                    {
                        Region = "Москва",
                        RegionCode = "77",
                        City = "Город",
                        Locality = "нас. пункт",
                        Street = "улица",
                        District = "округ",
                        House = "дом",
                        Building = "строение",
                        Apartment = "квартира"
                    },
                }
            };
            //SelectPledgee = new PledgeePrivatePerson
            //{
            //    Name = new PrivatePersonName
            //    {
            //        Last = "last",
            //        First = "first",
            //        Middle = "middle"
            //    },
            //    BirthDate = DateTime.Now,
            //    PersonAddress = new PrivatePersonPersonAddress
            //    {
            //        AddressRF = new PrivatePersonPersonAddressAddressRF
            //        {
            //            Region = "Москва",
            //            RegionCode = "77",
            //            City = "Город",
            //            Locality = "нас. пункт",
            //            Street = "улица",
            //            District = "округ",
            //            House = "дом",
            //            Building = "строение",
            //            Apartment = "квартира"
            //        }
            //    },
            //    // Email = "123@132.ru",
            //    PersonDocument = new PrivatePersonDocument
            //    {
            //        Code = 21,
            //        Name = "Паспорт",
            //        SeriesNumber = "123456790"

            //    }
            //};

            #region Команды

            //AddCommandPledgeeOrganization = new RelayCommand(AddCommandPledgeeOrganizationExecute, CanAddCommandPledgeeOrganizationExecute);
            //AddCommandPersonPledgee = new RelayCommand(AddCommandPersonPledgeeExecute, CanAddCommandPersonPledgeeExecute);
            //SavePledgeeCommand = new RelayCommand(OnSavePledgeeCommandExecute, CanSavePledgeeCommandExecute);
            //EditCommandPledgee = new RelayCommand(EditCommandPledgeeExecute, CanEditCommandPledgeeExecute);
            //RemoveCommandPledgee = new RelayCommand(OnRemoveCommandPledgeeExecute, CanRemoveCommandPledgeeExecute);
            #endregion
        }



        public bool IsView { get; set; }

        public List<string> SourceComboRegion { get; set; }

        #region SelectPledgee Св-во, содержащее выбранного из списка залогодержателя
        /// <summary>
        /// Св-во, содержащее выбранного из списка залогодержателя
        /// </summary>
        private Pledgee selectPledgee;

        public Pledgee SelectPledgee { get; set; }
        #endregion

        #region Коллекция залогодержателей
        /// <summary>
        /// Коллекция залогодержателей
        /// </summary>
        public ObservableCollection<Pledgee> Pledgee { get; set; }
        #endregion
    }
}
