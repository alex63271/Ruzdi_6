using Ruzdi_6.Commands;
using Ruzdi_6.Model.Other_Classes;
using Ruzdi_6.Model.Pledgee_Classes;
using Ruzdi_6.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Ruzdi_6.ViewModel
{
    public class VM_Pledgee : ViewModel
    {
        public VM_Pledgee()
        {

            if (App.DesignMode)
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


                /* SelectPledgee = new PledgeeOrganization
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
                  };*/
                SelectPledgee = new PledgeePrivatePerson
                {
                    Name = new PrivatePersonName
                    {
                        Last = "last",
                        First = "first",
                        Middle = "middle"
                    },
                    BirthDate = DateTime.Now,
                    PersonAddress = new PrivatePersonPersonAddress
                    {
                        AddressRF = new PrivatePersonPersonAddressAddressRF
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
                        }
                    },
                    // Email = "123@132.ru",
                    PersonDocument = new PrivatePersonDocument
                    {
                        Code = 21,
                        Name = "Паспорт",
                        SeriesNumber = "123456790"

                    }
                };

                #region Команды

                AddCommandPledgeeOrganization = new RelayCommand(AddCommandPledgeeOrganizationExecute, CanAddCommandPledgeeOrganizationExecute);
                AddCommandPersonPledgee = new RelayCommand(AddCommandPersonPledgeeExecute, CanAddCommandPersonPledgeeExecute);
                SavePledgeeCommand = new RelayCommand(OnSavePledgeeCommandExecute, CanSavePledgeeCommandExecute);
                EditCommandPledgee = new RelayCommand(EditCommandPledgeeExecute, CanEditCommandPledgeeExecute);
                RemoveCommandPledgee = new RelayCommand(OnRemoveCommandPledgeeExecute, CanRemoveCommandPledgeeExecute);
                #endregion
            }
            else
            {
                Pledgee = new ObservableCollection<Pledgee>();
                SourceComboRegion = App.Region_list;

                #region Команды

                AddCommandPledgeeOrganization = new RelayCommand(AddCommandPledgeeOrganizationExecute, CanAddCommandPledgeeOrganizationExecute);
                AddCommandPersonPledgee = new RelayCommand(AddCommandPersonPledgeeExecute, CanAddCommandPersonPledgeeExecute);
                SavePledgeeCommand = new RelayCommand(OnSavePledgeeCommandExecute, CanSavePledgeeCommandExecute);
                EditCommandPledgee = new RelayCommand(EditCommandPledgeeExecute, CanEditCommandPledgeeExecute);
                RemoveCommandPledgee = new RelayCommand(OnRemoveCommandPledgeeExecute, CanRemoveCommandPledgeeExecute);
                #endregion
            }

        }
        public bool IsView { get; set; }
        public List<string> SourceComboRegion { get; set; }

        #region Поле и свойство для дизайнера
        private static VM_Pledgee vM_Pledgee_UZ1_ForDesiner;
        public static VM_Pledgee VM_Pledgee_UZ1_ForDesiner
        {
            get
            {
                if (vM_Pledgee_UZ1_ForDesiner == null)
                {
                    vM_Pledgee_UZ1_ForDesiner = new VM_Pledgee();
                }
                return vM_Pledgee_UZ1_ForDesiner;
            }
        }
        #endregion

        #region Поля и методы Singletone
        private static VM_Pledgee VM_Pledgee_UZ1;
        public static VM_Pledgee GetVM_Pledgee_UZ1()
        {
            if (VM_Pledgee_UZ1 == null)
            {
                VM_Pledgee_UZ1 = new VM_Pledgee();
            }
            return VM_Pledgee_UZ1;
        }

        public static void SetNullVM_Pledgee_UZ1()
        {
            VM_Pledgee_UZ1 = null;
        }
        #endregion

        #region SelectPledgee Св-во, содержащее выбранного из списка залогодержателя
        /// <summary>
        /// Св-во, содержащее выбранного из списка залогодержателя
        /// </summary>
        private Pledgee selectPledgee;

        public Pledgee SelectPledgee
        {
            get => selectPledgee;
            set => Set(ref selectPledgee, value);
        }
        #endregion

        #region Коллекция залогодержателей
        /// <summary>
        /// Коллекция залогодержателей
        /// </summary>
        public ObservableCollection<Pledgee> Pledgee { get; set; }
        #endregion

        #region Команды

        #region AddCommandPledgeeOrganization - команда добавления юр. лица РФ залогодержателя
        /// <summary>
        /// команда добавления физ. лица
        /// </summary>
        public ICommand AddCommandPledgeeOrganization { get; }
        public bool CanAddCommandPledgeeOrganizationExecute(object p)
        {
            return !IsView;
        }
        public void AddCommandPledgeeOrganizationExecute(object p)
        {
            SelectPledgee = new PledgeeOrganization
            {
                RussianOrganization = new RussianOrganization
                {
                    NameFull = "",
                    INN = "",
                    OGRN = "",
                    Email = "",
                    Address = new RussianOrganizationAddress
                    {
                        Region = "",
                        RegionCode = "",
                        District = "",
                        City = "",
                        Street = "",
                        House = "",
                        Building = "",
                        Apartment = "",
                        Locality = ""
                    },
                },
            };
            IWindowService service = new ServiceWindow();
            service.ShowWindowDialog(SelectPledgee);

        }
        #endregion

        #region AddCommandPersonPledgee - команда добавления физ. лица залогодержателя
        /// <summary>
        /// команда добавления физ. лица
        /// </summary>
        public ICommand AddCommandPersonPledgee { get; }
        public bool CanAddCommandPersonPledgeeExecute(object p)
        {
            return !IsView;
        }
        public void AddCommandPersonPledgeeExecute(object p)
        {
            SelectPledgee = new PledgeePrivatePerson
            {
                Name = new PrivatePersonName
                {
                    First = "",
                    Last = "",
                    Middle = ""
                },
                BirthDate = DateTime.Now,
                PersonDocument = new PrivatePersonDocument
                {
                    Code = 21,
                    Name = "Паспорт",
                    SeriesNumber = ""
                },
                PersonAddress = new PrivatePersonPersonAddress
                {
                    AddressRF = new PrivatePersonPersonAddressAddressRF
                    {
                        Region = "",
                        RegionCode = "",
                        City = ""
                    }
                }
            };

            IWindowService service = new ServiceWindow();
            service.ShowWindowDialog(SelectPledgee);

            /*EditPledgeeWin PledgeeOrganization = new EditPledgeeWin
            {
                DataContext = GetVM_Pledgee_UZ1()
            };
            PledgeeOrganization.ShowDialog();*/
        }
        #endregion 

        #region SavePledgee - команда сохранения залогодержателя
        /// <summary>
        /// команда сохранения залогодателя физ. лица
        /// </summary>
        public ICommand SavePledgeeCommand { get; }
        public bool CanSavePledgeeCommandExecute(object p)
        {
            //должна быть логика проверки, что валидация успешна
            if (SelectPledgee is PledgeePrivatePerson privatePerson)
            {
                return !IsView && privatePerson.Name.IsValid && privatePerson.PersonDocument.IsValid && privatePerson.PersonAddress.AddressRF.IsValid && privatePerson.IsValid;
            }
            else if (SelectPledgee is PledgeeOrganization organization)
            {
                return !IsView && organization.RussianOrganization.IsValid && organization.RussianOrganization.Address.IsValid;
            }
            else
            {
                return !IsView;
            }
        }
        public void OnSavePledgeeCommandExecute(object p)
        {
            if (Pledgee.Contains(SelectPledgee))//если объект уже есть в коллекции(т.е. идет редактирование), то вновь этото объект не добавляем в коллекцию
            {
                IWindowService service = new ServiceWindow();
                service.CloseWindowDialog(SelectPledgee);
            }
            else
            {
                Pledgee.Insert(0, SelectPledgee);
                IWindowService service = new ServiceWindow();
                service.CloseWindowDialog(SelectPledgee);
            }
        }
        #endregion

        #region EditCommandPledgee - команда изменения залогодержателя
        /// <summary>
        /// команда изменения физ. лица
        /// </summary>
        public ICommand EditCommandPledgee { get; }
        public bool CanEditCommandPledgeeExecute(object p)
        {
            return Pledgee.Count > 0;
        }
        public void EditCommandPledgeeExecute(object p)
        {
            IWindowService service = new ServiceWindow();
            service.ShowWindowDialog(SelectPledgee);

            /*EditPledgeeWin pledgorPersonWin = new EditPledgeeWin
            {
                DataContext = GetVM_Pledgee_UZ1()
            };
            pledgorPersonWin.ShowDialog();*/
        }
        #endregion

        #region RemoveCommandPledgee - команда удаления из списка залогодержателей
        /// <summary>
        /// команда удаления
        /// </summary>
        public ICommand RemoveCommandPledgee { get; }
        public bool CanRemoveCommandPledgeeExecute(object p)
        {
            return (Pledgee.Count > 0) && (!IsView);
        }
        public void OnRemoveCommandPledgeeExecute(object p)
        {
            Pledgee.Remove(p as Pledgee);
        }
        #endregion

        #endregion

    }
}
