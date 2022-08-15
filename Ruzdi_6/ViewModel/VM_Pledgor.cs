using Ruzdi_6.Commands;
using Ruzdi_6.Model.Other_Classes;
using Ruzdi_6.Model.Pledgor_Classes;
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
    public class VM_Pledgor : ViewModel
    {
        public VM_Pledgor()
        {
            if (App.DesignMode)
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
                /*SelectPledgor = new PledgorPrivatePerson
                {
                    Name = new PrivatePersonName
                    {
                        Last = "Петров",
                        First = "Иван",
                        Middle = "Михалыч"
                    },
                    BirthDate = DateTime.Now,
                    PersonDocument = new PledgorPrivatePersonPersonDocument
                    {
                        Code = 21,
                        Name = "паспорт",
                        SeriesNumber = "123456789"
                    },
                    PersonAddress = new PrivatePersonPersonAddress
                    {
                        AddressRF = new PrivatePersonPersonAddressAddressRF
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
                    },
                    Email = "123456"
                };*/

                SelectPledgor = new PledgorOrganization
                {
                    RussianOrganization = new RussianOrganization
                    {
                        NameFull = "Рога и копыта",
                        INN = "123456",
                        OGRN = "1324567890",
                        Address = new RussianOrganizationAddress
                        {
                            Region = "Москва",
                            RegionCode = "",
                            District = "район",
                            City = "Москва",
                            Locality = "Нас. пункт",
                            Street = "улица",
                            House = "Дом",
                            Building = "строение",
                            Apartment = "квартира",
                        },
                        Email = "13@kk.ru"
                    }
                };


                SourceComboRegion = new List<string>
                {
                    "Москва",
                    "Анапа",
                    "Алушта"
                };
                #region Команды залогодателя 

                AddCommandPersonPledgor = new RelayCommand(AddCommandPersonExecute, CanAddCommandPersonPledgorExecute);
                AddOrganizationRFPledgor = new RelayCommand(OnAddOrganizationRFExecute, CanAddOrganizationRFExecute);
                RemoveCommand = new RelayCommand(OnRemoveExecute, CanRemoveExecute);
                SavePledgorPersonCommand = new RelayCommand(OnSavePledgorPersonCommandExecute, CanSavePledgorPersonCommandExecute);
                SavePledgorOrganizationCommand = new RelayCommand(OnSavePledgorOrganizationCommandExecute, CanSavePledgorOrganizationCommandExecute);
                EditCommandPerson = new RelayCommand(EditCommandPersonExecute, CanEditCommandPersonExecute);
                #endregion
            }
            else
            {
                Pledgors = new ObservableCollection<Pledgor>();
                SourceComboRegion = App.Region_list;
                #region Команды залогодателя 

                AddCommandPersonPledgor = new RelayCommand(AddCommandPersonExecute, CanAddCommandPersonPledgorExecute);
                AddOrganizationRFPledgor = new RelayCommand(OnAddOrganizationRFExecute, CanAddOrganizationRFExecute);
                RemoveCommand = new RelayCommand(OnRemoveExecute, CanRemoveExecute);
                SavePledgorPersonCommand = new RelayCommand(OnSavePledgorPersonCommandExecute, CanSavePledgorPersonCommandExecute);
                SavePledgorOrganizationCommand = new RelayCommand(OnSavePledgorOrganizationCommandExecute, CanSavePledgorOrganizationCommandExecute);
                EditCommandPerson = new RelayCommand(EditCommandPersonExecute, CanEditCommandPersonExecute);
                #endregion
            }
        }

        #region Приватные поля и меотды синглтона
        private static VM_Pledgor VM_Pledgor_UZ1;
        public static VM_Pledgor Get_VM_Pledgor_UZ1()
        {
            if (VM_Pledgor_UZ1 == null)
            {
                VM_Pledgor_UZ1 = new VM_Pledgor();
            }
            return VM_Pledgor_UZ1;
        }

        public static void Set_Null_VM_Pledgor()
        {
            VM_Pledgor_UZ1 = null;
        }



        #endregion

        #region Свойство для дизайнера
        public static VM_Pledgor VM_Pledgor_ForDesiner
        {
            get
            {
                if (vM_Pledgor_ForDesiner == null)
                {
                    vM_Pledgor_ForDesiner = new VM_Pledgor();
                }
                return vM_Pledgor_ForDesiner;
            }
        }
        private static VM_Pledgor vM_Pledgor_ForDesiner;
        #endregion

        #region Залогодатель (Поля, св-ва и команды)

        #region Команды

        #region AddCommandPersonPledgor - команда добавления физ. лица
        /// <summary>
        /// команда добавления физ. лица
        /// </summary>
        public ICommand AddCommandPersonPledgor { get; }
        public bool CanAddCommandPersonPledgorExecute(object p)
        {

            return !IsView;
        }
        public void AddCommandPersonExecute(object p)
        {

            SelectPledgor = new PledgorPrivatePerson
            {
                Name = new PrivatePersonName
                {
                    First = "",
                    Middle = "",
                    Last = ""
                },
                BirthDate = DateTime.Now,
                PersonAddress = new PrivatePersonPersonAddress
                {
                    AddressRF = new PrivatePersonPersonAddressAddressRF
                    {
                        registration = true,
                        RegionCode = "",
                        Region = ""
                        /* District = "",
                         Locality = "",
                         City = "",
                         Street = "",
                         House = "",
                         Building = "",
                         Apartment = ""*/
                    }
                },
                PersonDocument = new PrivatePersonDocument
                {
                    Code = 21,
                    Name = "Паспорт",
                    SeriesNumber = ""
                }
            };


            IWindowService service = new ServiceWindow();
            service.ShowWindowDialog(SelectPledgor);
        }
        #endregion

        #region RemoveCommand - команда удаления из списка залогодателей
        /// <summary>
        /// команда удаления
        /// </summary>
        public ICommand RemoveCommand { get; }
        public bool CanRemoveExecute(object p)
        {

            return (Pledgors.Count > 0) && (!IsView);
        }
        public void OnRemoveExecute(object p)
        {
            Pledgor Pledgor = p as Pledgor;
            int index = Pledgors.IndexOf(Pledgor);
            Pledgors.RemoveAt(index);
        }
        #endregion

        #region AddOrganizationRFPledgor - команда добавления юр. лица РФ
        /// <summary>
        /// команда добавления юр. лица РФ
        /// </summary>
        public ICommand AddOrganizationRFPledgor { get; }

        public bool CanAddOrganizationRFExecute(object p)
        {
            return !IsView;
        }

        public void OnAddOrganizationRFExecute(object p)
        {
            SelectPledgor = new PledgorOrganization
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
                        Locality = "",
                        City = "",
                        Street = "",
                        House = "",
                        Building = "",
                        Apartment = ""
                    },
                },
            };

            IWindowService service = new ServiceWindow();
            service.ShowWindowDialog(SelectPledgor);

            /* PledgorPersonWin pledgorPersonWin = new PledgorPersonWin
             {
                 DataContext = Get_VM_Pledgor_UZ1()
             };
             pledgorPersonWin.ShowDialog();*/
        }
        #endregion

        #region EditCommandPerson - команда изменения залогодателя
        /// <summary>
        /// команда изменения залогодателя
        /// </summary>
        public ICommand EditCommandPerson { get; }

        public bool CanEditCommandPersonExecute(object p)
        {

            return Pledgors.Count > 0;

        }
        public void EditCommandPersonExecute(object p)
        {
            IWindowService service = new ServiceWindow();
            service.ShowWindowDialog(SelectPledgor);
        }
        #endregion

        #region SavePledgorPersonCommand - команда сохранения залогодателя физ. лица
        /// <summary>
        /// команда сохранения залогодателя физ. лица
        /// </summary>
        public ICommand SavePledgorPersonCommand { get; }
        public bool CanSavePledgorPersonCommandExecute(object p)
        {
            //должна быть логика проверки, что валидация успешна
            if (SelectPledgor is PledgorPrivatePerson privatePerson)
            {
                return !IsView && privatePerson.Name.IsValid && privatePerson.PersonDocument.IsValid && privatePerson.PersonAddress.AddressRF.IsValid && privatePerson.IsValid;
            }
            else if (SelectPledgor is PledgorOrganization organization)
            {
                return !IsView && organization.RussianOrganization.IsValid && organization.RussianOrganization.Address.IsValid;
            }
            else
            {
                return !IsView;
            }

        }
        public void OnSavePledgorPersonCommandExecute(object p)
        {

            if (Pledgors.Contains(SelectPledgor))//если объект уже есть в коллекции(т.е. идет редактирование), то вновь этот объект не добавляем в коллекцию
            {
                IWindowService service = new ServiceWindow();
                service.CloseWindowDialog(SelectPledgor);
            }
            else
            {
                Pledgors.Insert(0, SelectPledgor);
                IWindowService service = new ServiceWindow();
                service.CloseWindowDialog(SelectPledgor);
            }


        }
        #endregion

        #region SavePledgorOrganizationCommand - команда сохранения залогодателя юр. лица РФ
        /// <summary>
        /// команда сохранения залогодателя юр. лица РФ
        /// </summary>
        public ICommand SavePledgorOrganizationCommand { get; }
        public bool CanSavePledgorOrganizationCommandExecute(object p)
        {
            //должна быть логика проверки, что валидация успешна
            return !IsView;
        }
        public void OnSavePledgorOrganizationCommandExecute(object p)
        {

            if (Pledgors.Contains(SelectPledgor))//если объект уже есть в коллекции(т.е. идет редактирование), то вновь этото объект не добавляем в коллекцию
            {
                IWindowService service = new ServiceWindow();
                service.CloseWindowDialog(SelectPledgor);
            }
            else
            {
                Pledgors.Insert(0, SelectPledgor);
                IWindowService service = new ServiceWindow();
                service.CloseWindowDialog(SelectPledgor);
            }


        }
        #endregion

        #endregion

        public List<string> SourceComboRegion { get; set; }


        #region SelectPledgor Св-во, содержащее выбранного из списка залогодателя
        /// <summary>
        /// Св-во, содержащее выбранного из списка залогодателя
        /// </summary>
        private Pledgor selectPledgor = new Pledgor();


        public Pledgor SelectPledgor
        {
            get => selectPledgor;
            set => Set(ref selectPledgor, value);
        }
        #endregion

        public ObservableCollection<Pledgor> Pledgors { get; set; }

        public bool IsView { get; set; }

        #endregion
    }
}
