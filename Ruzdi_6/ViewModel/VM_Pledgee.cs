using Ruzdi_6.Commands;
using Ruzdi_6.Model.Other_Classes;
using Ruzdi_6.Model.Pledgee_Classes;
using Ruzdi_6.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Ruzdi_6.ViewModel
{
    public class VM_Pledgee : ViewModel
    {
        public VM_Pledgee(IWindowService service)
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

            this.service = service;
        }

        private readonly IWindowService service;

        public bool IsView { get; set; }

        public List<string> SourceComboRegion { get; set; }

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

        public bool CanAddCommandPledgeeOrganizationExecute(object p) => !IsView;

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

            service.ShowWindowDialog(SelectPledgee);

        }
        #endregion

        #region AddCommandPersonPledgee - команда добавления физ. лица залогодержателя
        /// <summary>
        /// команда добавления физ. лица
        /// </summary>
        public ICommand AddCommandPersonPledgee { get; }
        public bool CanAddCommandPersonPledgeeExecute(object p) => !IsView;

        public void AddCommandPersonPledgeeExecute(object p)
        {
            SelectPledgee = new PledgeePrivatePerson
            {
                Name = new PrivatePersonName
                {
                    Last = "",
                    First = "",
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
                },
                Email = ""
            };

            service.ShowWindowDialog(SelectPledgee);
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
                service.CloseWindowDialog(SelectPledgee);
            }
            else
            {
                Pledgee.Insert(0, SelectPledgee);

                service.CloseWindowDialog(SelectPledgee);
            }
        }
        #endregion

        #region EditCommandPledgee - команда изменения залогодержателя
        /// <summary>
        /// команда изменения физ. лица
        /// </summary>
        public ICommand EditCommandPledgee { get; }

        public bool CanEditCommandPledgeeExecute(object p) => Pledgee.Count > 0;

        public void EditCommandPledgeeExecute(object p) => service.ShowWindowDialog(SelectPledgee);
        #endregion

        #region RemoveCommandPledgee - команда удаления из списка залогодержателей
        /// <summary>
        /// команда удаления
        /// </summary>
        public ICommand RemoveCommandPledgee { get; }

        public bool CanRemoveCommandPledgeeExecute(object p) => (Pledgee.Count > 0) && (!IsView);

        public void OnRemoveCommandPledgeeExecute(object p) => Pledgee.Remove(p as Pledgee);
        #endregion

        #endregion

    }
}
