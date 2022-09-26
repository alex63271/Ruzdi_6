using Ruzdi_6.Model.Other_Classes;
using Ruzdi_6.Model.Pledgor_Classes;
using System.Collections.ObjectModel;

namespace Ruzdi_6.ViewModel_DesignData
{
    public class VM_PledgorDD 
    {
        public VM_PledgorDD()
        {

            Pledgors = new ObservableCollection<Pledgor>()
                {
                    new PledgorPrivatePerson
                    {
                        Name = new PrivatePersonName
                        {
                            Last = "Петров",
                            First = "Иван",
                            Middle = "Михалыч"
                        },
                        BirthDate = DateTime.Now,
                        PersonDocument = new PrivatePersonDocument
                        {
                            Code = 21,
                            Name = "паспорт",
                            SeriesNumber = "123456789"
                        },
                        PersonAddress = new PrivatePersonPersonAddress
                        {
                            AddressRF = new PrivatePersonPersonAddressAddressRF
                            {
                                RegionCode = "10",
                                Region = "Москва",
                                City = "789"
                            }
                        }
                    },
                    new PledgorPrivatePerson
                              {
                                  Name = new PrivatePersonName
                                  {
                                      Last = "Никифоров",
                                      First = "Петр",
                                      Middle = "Захарыч"
                                  },
                                  BirthDate = DateTime.Now,
                                  PersonDocument = new PrivatePersonDocument
                                  {
                                      Code = 21,
                                      Name = "паспорт",
                                      SeriesNumber = "123456789"
                                  },
                                  PersonAddress = new PrivatePersonPersonAddress
                                  {
                                      AddressRF = new PrivatePersonPersonAddressAddressRF
                                      {
                                          RegionCode = "10",
                                          Region = "Москва",
                                          City = "789"
                                      }
                                  },
                              },
                    new PledgorOrganization
                          {
                              RussianOrganization = new RussianOrganization
                              {
                                  NameFull = "Рога и копыта",
                                  INN = "123456",
                                  OGRN = "1324567890",
                                  Address = new RussianOrganizationAddress
                                  {
                                      Region = "Москва",
                                      RegionCode = "10",
                                      City = "Город"
                                  },
                                  Email = "13@kk.ru"
                              }
                          }
                };



            //SelectPledgor = new PledgorOrganization
            //{
            //    RussianOrganization = new RussianOrganization
            //    {
            //        NameFull = "Рога и копыта",
            //        INN = "123456",
            //        OGRN = "1324567890",
            //        Address = new RussianOrganizationAddress
            //        {
            //            Region = "Москва",
            //            RegionCode = "",
            //            District = "район",
            //            City = "Москва",
            //            Locality = "Нас. пункт",
            //            Street = "улица",
            //            House = "Дом",
            //            Building = "строение",
            //            Apartment = "квартира",
            //        },
            //        Email = "13@kk.ru"
            //    }
            //};
            SelectPledgor = new PledgorPrivatePerson
            {
                Name = new PrivatePersonName
                {
                    Last = "Петров",
                    First = "Иван",
                    Middle = "Михалыч"
                },
                BirthDate = DateTime.Now,
                PersonDocument = new PrivatePersonDocument
                {
                    Code = 21,
                    Name = "паспорт",
                    SeriesNumber = "123456789"
                },
                PersonAddress = new PrivatePersonPersonAddress
                {
                    AddressRF = new PrivatePersonPersonAddressAddressRF
                    {
                        RegionCode = "10",
                        Region = "Москва",
                        City = "789"
                    }
                }
            };

        }

        #region SelectPledgor Св-во, содержащее выбранного из списка залогодателя
        /// <summary>
        /// Св-во, содержащее выбранного из списка залогодателя
        /// </summary>
        private Pledgor selectPledgor;


        public Pledgor SelectPledgor { get; set; }
        #endregion

        public ObservableCollection<Pledgor> Pledgors { get; set; }

        public bool IsView { get; set; }
    }

}
