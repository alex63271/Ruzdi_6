using Ruzdi_6.Commands;
using Ruzdi_6.Model;
//
//using Ruzdi_6.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Ruzdi_6.ViewModel
{
    public class VM_SettingsDB : ViewModel
    {
        #region Конструкторы

        private VM_SettingsDB() : this(App.GetPathCspTestFromConfig())
        { }

        private VM_SettingsDB(string pathCsptest = @"C:\Program Files\Crypto Pro\CSP\csptest.exe")
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                Srv = new ServerConnect
                {
                    Server = "Server",
                    Database = "Database",
                    User = "User",
                    Password = "Password",
                    Port = "Port",
                    PathCSP = pathCsptest
                };

                //SaveSettings = new RelayCommand(OnSaveSettingsCommandExecute, CanSaveSettingsCommandExecute);
            }
            else
            {
                string zap = ";";
                string connectionString = App.GetConnectionStringFromConfig() + zap;

                Srv = new ServerConnect
                {
                    Server = connectionString.Substring(connectionString.IndexOf("server=") + "server=".Length, connectionString.IndexOf(zap, connectionString.IndexOf("server=")) - (connectionString.IndexOf("server=") + "server=".Length)),
                    Database = connectionString.Substring(connectionString.IndexOf("database=") + "database=".Length, connectionString.IndexOf(zap, connectionString.IndexOf("database=")) - (connectionString.IndexOf("database=") + "database=".Length)),
                    User = connectionString.Substring(connectionString.IndexOf("user id=") + "user id=".Length, connectionString.IndexOf(zap, connectionString.IndexOf("user id=")) - (connectionString.IndexOf("user id=") + "user id=".Length)),
                    Password = connectionString.Substring(connectionString.IndexOf("Password=") + "Password=".Length, connectionString.IndexOf(zap, connectionString.IndexOf("Password=")) - (connectionString.IndexOf("Password=") + "Password=".Length)),
                    Port = connectionString.Substring(connectionString.IndexOf("port=") + "port=".Length, connectionString.IndexOf(zap, connectionString.IndexOf("port=")) - (connectionString.IndexOf("port=") + "port=".Length)),
                    PathCSP = pathCsptest
                };
                SaveSettings = new RelayCommand(OnSaveSettingsCommandExecute, CanSaveSettingsCommandExecute);
            }
        }
        #endregion

        #region Поля и методы Singletone
        private static VM_SettingsDB vM_SettingsDB;
        public static VM_SettingsDB GetVM_SettingsDB(string pathCsptest = @"C:\Program Files\Crypto Pro\CSP\csptest.exe")
        {
            if (vM_SettingsDB == null)
            {
                vM_SettingsDB = new VM_SettingsDB(pathCsptest);
            }
            return vM_SettingsDB;
        }

        public static void SetNull_VM_SettingsDB()
        {
            vM_SettingsDB = null;
        }
        #endregion

        #region Свойство для дизайнера
        public static VM_SettingsDB SettingsDB_ForDesiner
        {
            get
            {
                if (settingsDB_ForDesiner == null)
                {
                    settingsDB_ForDesiner = new VM_SettingsDB();
                }
                return settingsDB_ForDesiner;
            }
        }
        private static VM_SettingsDB settingsDB_ForDesiner;
        #endregion

        #region Свойство и поле Srv
        private ServerConnect srv;
        public ServerConnect Srv { get => srv; set => Set(ref srv, value); }
        #endregion

        #region SaveSettings - команда сохранения настроек
        /// <summary>
        /// команда сохранения настроек
        /// </summary>
        public ICommand SaveSettings { get; }
        public bool CanSaveSettingsCommandExecute(object p)
        {
            //должна быть логика проверки, что валидация успешна
            return true;
        }
        public void OnSaveSettingsCommandExecute(object p)
        {
            /*
           // Query query = new TestMysqlConnectPressButtonOk();
            if (string.IsNullOrEmpty(query.TestConnectPressButtonOK(Srv.Server, Srv.User, Srv.Database, Srv.Password, Srv.Port)))//если подключение успешное, то сохраняем строку подключения 
            {
                AddUpdateAppSettings("DefaultConnection", "server=" + Srv.Server + ";user id=" + Srv.User + ";persistsecurityinfo=True;database=" + Srv.Database + ";allowuservariables=True; Password=" + Srv.Password + ";port=" + Srv.Port);
            }
            else
            {
                MessageBox.Show(query.TestConnectDB(), "Ошибка подключения к БД!");
                return;
            }
            */
            FileInfo FileCsptestFromConfig = new FileInfo(Srv.PathCSP);
            if (FileCsptestFromConfig.Exists)
            {
                AddUpdateAppSettings("PathCsptest", Srv.PathCSP);
            }
            else
            {
                MessageBox.Show("На компьютере не обнаружена утилита csptest.exe\nУстановите ПО КриптоПро CSP или укажите путь к утилите csptest.exe", "Ошибка проверки csptest.exe!");
                return;
            }


            foreach (Window window in Application.Current.Windows)
            {
                if (window is SettingsDB_Win)    //находим нужное окно и обращаемся к нужному свойству
                {
                    window.DialogResult = true;
                    //(window as SettingsDB_Win).DialogResult = true;
                }
            }



            foreach (Window window in Application.Current.Windows)
            {
                if (window is SettingsDB_Win)    //находим нужное окно и обращаемся к нужному свойству
                {
                    window.DialogResult = true;
                    //(window as SettingsDB_Win).DialogResult = true;
                }
                else if (window.GetType() == typeof(GlavnayaWindow))
                {
                    if ((window as GlavnayaWindow).IsVisible)
                    {
                        return;
                    }
                    else
                    {
                        GlavnayaWindow glavnayaWindow = new GlavnayaWindow
                        {
                            DataContext = VM_ForGlavnaya.Get_VM_ForGlavnaya()
                        };
                        glavnayaWindow.Show();
                    }
                }
            }




        }
        #endregion

        #region Метод, обновляющий конфиг-файл
        /// <summary>
        /// Метод, обновляющий конфиг-файл
        /// </summary>
        /// <param name="key"> Значение индексатора коллекции</param>
        /// <param name="value"> Значен</param>
        static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                #region Коллекции настроек и строк подключения
                KeyValueConfigurationCollection AppSettings = configFile.AppSettings.Settings;
                ConnectionStringSettingsCollection CollectionConnectionStrings = configFile.ConnectionStrings.ConnectionStrings;
                #endregion

                if (CollectionConnectionStrings[key] != null)
                {
                    CollectionConnectionStrings[key].ConnectionString = value;
                }
                else if (AppSettings[key] != null)
                {
                    AppSettings[key].Value = value;
                }


                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
                ConfigurationManager.RefreshSection(configFile.ConnectionStrings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                MessageBox.Show("Error writing app settings");
            }
        }
        #endregion

    }
}
