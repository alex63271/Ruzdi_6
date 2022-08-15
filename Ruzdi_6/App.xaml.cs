using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Ruzdi_6.ViewModel;
using Ruzdi_6.View;
using System;
using System.Collections.Generic;
//using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Ruzdi_6.Services;
using Ruzdi_6.Data;

namespace Ruzdi_6
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Exception);
        }

        #region Host и Services
        private static IHost host;
        public static IHost Host => (host is null) ? (host = Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build()) : host;

        public static IServiceProvider Services => Host.Services;

        //internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
        //    .AddDatabase(host.Configuration.GetSection("Database")) // секция из appsettings.json
        //    .AddServices()
        //    .AddViewModels()

        //    ;
        #endregion

        static void Exception(object sender, UnhandledExceptionEventArgs args)
        {
            /*
            ExceptionWindow ExceptionWindow = new ExceptionWindow();
            ExceptionWindow.ExceptionText.Text = args.ExceptionObject.ToString();
            ExceptionWindow.ShowDialog();
            */
        }

        #region Статические поля для других класов

        public static string sql = "SELECT ZipArchive, NotificationId, Packageid, CONCAT(Person.Last , ' ', Person.First, ' ',Person.Middle) AS ФИО, DATE_FORMAT(notification.DataTime, '%d.%m.%Y  %H.%i') as DataTime, notification.NumberNotification, Contracts.Number, notification.Status, notification.TypeNotification  FROM notification LEFT OUTER  join Person ON Person.Hash = notification.Pledgor LEFT OUTER join Contracts ON Contracts.Hash = notification.PledgeContract;";  // sql-запрос для создания кэша таблицы notification

        public static bool DesignMode { get; set; } = true;

        public static List<string> Region_list = new List<string>();

        public static string NotificationId;

        public static string guidp;

        public static string Temp = "Temp/";

        public static int HashContract;

        public static int HashPledgor;

        public static int HashPledgee;

        public static bool IsView;

        public static byte[] array;

        public static List<string> packageid_list = new List<string>();
        /*
       public static void SetNull_Subsidiary_VM_UZ1()
       {
           VM_Pledgor.Set_Null_VM_Pledgor();
           VM_Pledgee.SetNullVM_Pledgee_UZ1();
           VM_Property.SetNull_VM_Property();
           VM_Contract.SetNull_VM_Contract();
           VM_Applicant.SetNull_VM_Applicant();
       }

       public static void SetFlag_True()
       {
           VMWindowForUZ1.Get_VW_UZ1().IsView = true;
           VM_Pledgor.Get_VM_Pledgor_UZ1().IsView = true;
           VM_Pledgee.GetVM_Pledgee_UZ1().IsView = true;
           VM_Property.GetVM_Property_UZ1().IsView = true;
           VM_Contract.GetVM_Contract_UZ1().IsView = true;
           VM_Applicant.Get_VM_Applicant().IsView = true;
       }

       public static void SetFlag_False()
       {
           VMWindowForUZ1.Get_VW_UZ1().IsView = false;
           VM_Pledgor.Get_VM_Pledgor_UZ1().IsView = false;
           VM_Pledgee.GetVM_Pledgee_UZ1().IsView = false;
           VM_Property.GetVM_Property_UZ1().IsView = false;
           VM_Contract.GetVM_Contract_UZ1().IsView = false;
           VM_Applicant.Get_VM_Applicant().IsView = false;
       }

      public static bool ExistContracts(string hash)
       {
           HashContract = Convert.ToInt32(hash);
           bool result;
           using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))  //создаем объект подключения к mysql
           {
               connection.Open();
               MySqlCommand SELECT = new MySqlCommand("SELECT * FROM Contracts WHERE HASH=" + hash, connection);
               using (DbDataReader reader = SELECT.ExecuteReader())
               {
                   result = reader.HasRows;
               }
           }
           return result;
       }
       public static bool ExistPerson(string hash)
       {
           HashPledgor = Convert.ToInt32(hash);
           bool result;
           using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))  //создаем объект подключения к mysql
           {
               connection.Open();
               MySqlCommand SELECT = new MySqlCommand("SELECT * FROM Person WHERE HASH=" + hash, connection);
               using (DbDataReader reader = SELECT.ExecuteReader())
               {
                   result = reader.HasRows;
               }
           }
           return result;
       }
       public static bool ExistOrganization(string hash)
       {
           HashPledgee = Convert.ToInt32(hash);
           bool result;
           using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))  //создаем объект подключения к mysql
           {
               connection.Open();
               MySqlCommand SELECT = new MySqlCommand("SELECT * FROM Organization WHERE HASH=" + hash, connection);
               using (DbDataReader reader = SELECT.ExecuteReader())
               {
                   result = reader.HasRows;
               }
           }
           return result;
       }
       */
        #endregion

        protected async override void OnStartup(StartupEventArgs e)
        {
            var host = Host;
           
            DesignMode = false;
            using (var scope = Services.CreateScope())
            {
                Task migration = scope.ServiceProvider.GetRequiredService<DbInizialaizer>().InizializeAsync();

                
                await migration;
               
            }
            base.OnStartup(e);
            await host.StartAsync();

            
            #region Проверка наличия и создание папки Temp
            DirectoryInfo dirInfo = new DirectoryInfo("Temp");
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            #endregion


            /* if (ExistCspTestPathFromConfig())
             {
                 Query query = new TestMysqlConnect();
                 if (string.IsNullOrEmpty(query.TestConnectDB()))//если подключение успешное, то сохраняем строку подключения и путь csptest
                 {
                     GlavnayaWindow glavnayaWindow = new GlavnayaWindow
                     {
                         DataContext = VM_ForGlavnaya.Get_VM_ForGlavnaya()
                     };
                     glavnayaWindow.Show();
                 }
                 else    //если возникло исключение - выводим ошибку на экран
                 {
                     MessageBox.Show(query.TestConnectDB(), "Ошибка подключения к БД!");
                     SettingsDB_Win dB_Win = new SettingsDB_Win
                     {
                         DataContext = VM_SettingsDB.GetVM_SettingsDB()
                     };
                     dB_Win.ShowDialog();
                 }
             }
             else
             {
                 MessageBox.Show("На компьютере не обнаружена утилита csptest.exe\nУстановите ПО КриптоПро CSP или укажите путь к утилите csptest.exe", "Ошибка проверки csptest.exe!");
                 SettingsDB_Win Settings = new SettingsDB_Win
                 {
                     DataContext = VM_SettingsDB.GetVM_SettingsDB(GetPathCspTestFromConfig())
                 };
                 Settings.ShowDialog();
             }
             */
        }

        protected async override void OnExit(ExitEventArgs e)
        {
            using (Host)
            {
                base.OnExit(e);
                await Host.StopAsync();
            }
        }

        //public static string GetConnectionStringFromConfig() => ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;



        //public static string GetPathCspTestFromConfig()
        //{
        //    Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //    KeyValueConfigurationCollection AppSettings = configFile.AppSettings.Settings;
        //    return AppSettings["PathCsptest"].Value;
        //}

        //public static bool ExistCspTestPathFromConfig()
        //{
        //    FileInfo FileCsptestFromConfig = new FileInfo(GetPathCspTestFromConfig());
        //    return FileCsptestFromConfig.Exists;
        //}

    }
}
