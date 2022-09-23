using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ruzdi_6.ViewModel;
using System.IO;
using System.Windows;
using Ruzdi_DB.Context;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace Ruzdi_6
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() { }

        #region Host и Services
        private static IHost host;
        public static IHost Host => (host is null) ? (host = Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build()) : host;

        #endregion
        
        #region Статические поля для других класов


        public static bool DesignMode { get; set; } = true;

        public static List<string> Region_list = new List<string>();

        public static string NotificationId;

        public static string guidp;

        public static string Temp = "Temp/";

        public static bool IsView;

        public static byte[] array;

        public static List<string> packageid_list = new List<string>();

        #endregion

        protected override void OnStartup(StartupEventArgs e)
        {
            DesignMode = false;
            var host = Host;
            base.OnStartup(e);
            host.Start();
            
            VM_Locator.InitScopeUZ1();
            VM_Locator.InitScopeUP1();

            #region Получение списка регионов из БД
            try
            {
                if (!Region_list.Any())
                {
                    DB_Ruzdi regions = Host.Services.GetRequiredService<DB_Ruzdi>();

                    foreach (var obj in regions.Regions)
                    {
                        Region_list.Add(obj.Region);
                    }
                    Region_list.Sort();
                }
            }
            catch (Exception)
            {
                App.Current.Shutdown();
                Process.GetCurrentProcess().Kill();
                return;
            }
            #endregion

            #region Проверка наличия и создание папки Temp
            DirectoryInfo dirInfo = new DirectoryInfo("Temp");
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            #endregion

            if (!ExistCspTestPathFromConfig())
            {
                MessageBox.Show("На компьютере не обнаружена утилита csptest.exe\nУстановите ПО КриптоПро CSP.", "Ошибка проверки csptest.exe!");
                //SettingsDB_Win Settings = new SettingsDB_Win
                //{
                //    DataContext = VM_SettingsDB.GetVM_SettingsDB(GetPathCspTestFromConfig())
                //};
                //Settings.ShowDialog();
            }


        }

        protected async override void OnExit(ExitEventArgs e)
        {
            using (Host)
            {
                base.OnExit(e);
                await Host.StopAsync();
            }
        }

        internal static void SetFlag_True()
        {
            VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgor>().IsView = true;
            VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Pledgee>().IsView = true;
            VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Property>().IsView = true;
            VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Contract>().IsView = true;
            VM_Locator.scopeUZ1.ServiceProvider.GetRequiredService<VM_Applicant>().IsView = true;
        }

        public static string GetPathCspTestFromConfig() => host.Services.GetRequiredService<IConfiguration>().GetSection("CSP_Path").Value;

        public static bool ExistCspTestPathFromConfig()
        {
            FileInfo FileCsptestFromConfig = new FileInfo(GetPathCspTestFromConfig());
            return FileCsptestFromConfig.Exists;
        }

    }
}
