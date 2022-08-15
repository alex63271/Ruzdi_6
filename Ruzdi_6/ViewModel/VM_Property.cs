﻿using Ruzdi_6.Commands;
using Ruzdi_6.Model.Property_Classes;
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
    public class VM_Property : ViewModel
    {
        public VM_Property()
        {
            if (App.DesignMode)
            {
                PersonalProperty = new ObservableCollection<PersonalProperty>
                {
                     new VehicleProperty
                     {
                         VIN = "vin-number",
                         PIN="PIN",
                         BodyNumber = "BodyNumber",
                         ChassisNumber = "ChassisNumber",
                         Description = "decript"
                     },
                     new OtherProperty
                     {
                         ID="Id-объекта",
                         Description ="описание иного имущества"
                     }
                };

                SelectProperty = new OtherProperty
                {
                    ID = "Id-объекта",
                    Description = "описание иного имущества"
                };
                /*SelectProperty = new VehicleProperty
                {
                    VIN = "vin-number",
                    PIN = "PIN",
                    BodyNumber = "BodyNumber",
                    ChassisNumber = "ChassisNumber",
                    Description = "decript"
                };*/


                #region Команды имущества

                AddVehiclePropertyCommand = new RelayCommand(AddAddVehiclePropertyCommandExecute, CanAddVehiclePropertyCommandExecute);

                EditPropertyCommand = new RelayCommand(EditPropertyCommandExecute, CanEditPropertyCommandExecute);

                RemovePropertyCommand = new RelayCommand(OnRemovePropertyCommandExecute, CanRemovePropertyCommandExecute);

                SavePropertyCommand = new RelayCommand(OnSavePropertyCommandExecute, CanSavePropertyCommandExecute);

                AddOtherPropertyCommand = new RelayCommand(AddOtherPropertyCommandExecute, CanAddOtherPropertyCommandExecute);
                #endregion
            }
            else
            {
                PersonalProperty = new ObservableCollection<PersonalProperty>();
                #region Команды имущества

                AddVehiclePropertyCommand = new RelayCommand(AddAddVehiclePropertyCommandExecute, CanAddVehiclePropertyCommandExecute);

                EditPropertyCommand = new RelayCommand(EditPropertyCommandExecute, CanEditPropertyCommandExecute);

                RemovePropertyCommand = new RelayCommand(OnRemovePropertyCommandExecute, CanRemovePropertyCommandExecute);

                SavePropertyCommand = new RelayCommand(OnSavePropertyCommandExecute, CanSavePropertyCommandExecute);

                AddOtherPropertyCommand = new RelayCommand(AddOtherPropertyCommandExecute, CanAddOtherPropertyCommandExecute);
                #endregion
            }
        }

        #region Поля и методы Singletone
        private static VM_Property VM_Property_UZ1;
        public static VM_Property GetVM_Property_UZ1()
        {
            if (VM_Property_UZ1 == null)
            {
                VM_Property_UZ1 = new VM_Property();
            }
            return VM_Property_UZ1;
        }

        public static void SetNull_VM_Property()
        {
            VM_Property_UZ1 = null;
        }
        #endregion


        #region Свойство для дизайнера
        public static VM_Property VM_Property_ForDesiner
        {
            get
            {
                if (vM_Property_ForDesiner == null)
                {
                    vM_Property_ForDesiner = new VM_Property();
                }
                return vM_Property_ForDesiner;
            }
        }
        private static VM_Property vM_Property_ForDesiner;
        #endregion

        public ObservableCollection<PersonalProperty> PersonalProperty { get; set; }

        public bool IsView { get; set; }

        #region SelectProperty- поле и св-во выбранного из списка имущества
        private PersonalProperty selectProperty;
        public PersonalProperty SelectProperty
        {
            get => selectProperty;
            set => Set(ref selectProperty, value);
        }
        #endregion

        #region Команды

        #region AddVehiclePropertyCommand - команда добавления ТС
        /// <summary>
        /// команда добавления физ. лица
        /// </summary>
        public ICommand AddVehiclePropertyCommand { get; }

        public bool CanAddVehiclePropertyCommandExecute(object p)
        {
            return !IsView;
        }
        public void AddAddVehiclePropertyCommandExecute(object p)
        {
            SelectProperty = new VehicleProperty
            {
                VIN = "",
                PIN = "",
                BodyNumber = "",
                ChassisNumber = "",
                Description = ""
            };

            IWindowService service = new ServiceWindow();
            service.ShowWindowDialog(SelectProperty);
        }
        #endregion

        #region AddOtherPropertyCommand - команда добавления иного имущества
        /// <summary>
        /// команда добавления иного имущества
        /// </summary>
        public ICommand AddOtherPropertyCommand { get; }
        public bool CanAddOtherPropertyCommandExecute(object p)
        {
            return !IsView;
        }
        public void AddOtherPropertyCommandExecute(object p)
        {
            SelectProperty = new OtherProperty
            {
                ID = "",
                Description = ""
            };
            IWindowService service = new ServiceWindow();
            service.ShowWindowDialog(SelectProperty);

            /*EditPropertyWin PropertyWin = new EditPropertyWin
            {
                DataContext = GetVM_Property_UZ1()
            };
            PropertyWin.ShowDialog();*/
        }
        #endregion

        #region EditPropertyCommand - команда изменения имущества
        /// <summary>
        /// команда добавления физ. лица
        /// </summary>
        public ICommand EditPropertyCommand { get; }
        public bool CanEditPropertyCommandExecute(object p)
        {
            return PersonalProperty.Count > 0;
        }
        public void EditPropertyCommandExecute(object p)
        {

            IWindowService service = new ServiceWindow();
            service.ShowWindowDialog(SelectProperty);

        }
        #endregion

        #region RemovePropertyCommand - команда удаления из списка имущества
        /// <summary>
        /// команда удаления
        /// </summary>
        public ICommand RemovePropertyCommand { get; }
        public bool CanRemovePropertyCommandExecute(object p)
        {
            return (PersonalProperty.Count > 0) && (!IsView);
        }
        public void OnRemovePropertyCommandExecute(object p)
        {
            PersonalProperty.Remove(p as PersonalProperty);
        }
        #endregion

        #region SavePropertyCommand - команда сохранения имущества
        /// <summary>
        /// команда сохранения имущества
        /// </summary>
        public ICommand SavePropertyCommand { get; }
        public bool CanSavePropertyCommandExecute(object p)
        {
            //должна быть логика проверки, что валидация успешна
            if (SelectProperty is VehicleProperty vehicleProperty)
            {
                return !IsView && vehicleProperty.IsValid;
            }
            else if (SelectProperty is OtherProperty otherProperty)
            {
                return !IsView && otherProperty.IsValid;
            }
            return !IsView;
        }
        public void OnSavePropertyCommandExecute(object p)
        {
            if (PersonalProperty.Contains(SelectProperty))//если объект уже есть в коллекции(т.е. идет редактирование), то вновь этот объект не добавляем в коллекцию
            {
                IWindowService service = new ServiceWindow();
                service.CloseWindowDialog(SelectProperty);

            }
            else
            {
                PersonalProperty.Insert(0, SelectProperty); //если объекта нет, то добавляем его в коллекцию
                IWindowService service = new ServiceWindow();
                service.CloseWindowDialog(SelectProperty);
            }
        }
        #endregion

        #endregion

    }
}