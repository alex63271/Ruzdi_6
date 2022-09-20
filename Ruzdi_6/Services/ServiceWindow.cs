using Ruzdi_6.Model.Pledgee_Classes;
using Ruzdi_6.Model.Pledgor_Classes;
using Ruzdi_6.Model.Property_Classes;
using Ruzdi_6.View;
using Ruzdi_6.ViewModel;
using System.Windows;

namespace Ruzdi_6.Services
{
    public class  ServiceWindow : IWindowService
    {
        public void CloseWindowDialog(object p)
        {
            if (p is PersonalProperty)
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(EditPropertyWin))    //находим нужное окно и обращаемся к нужному свойству
                    {
                        (window as EditPropertyWin).DialogResult = true;
                    }
                }
            }
            else if (p is Pledgor)
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(EditPledgorWin))    //находим нужное окно и обращаемся к нужному свойству
                    {
                        (window as EditPledgorWin).DialogResult = true;
                    }
                }
            }
           
            else if (p is Pledgee)
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(EditPledgeeWin))    //находим нужное окно и обращаемся к нужному свойству
                    {
                        (window as EditPledgeeWin).DialogResult = true;
                    }
                }
            }
            else if (p is "UZ1")
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(WindowForUZ1))    //находим нужное окно и устаонвливаем значение в сво-во
                    {
                        (window as WindowForUZ1).DialogResult = true;
                    }
                }
            }
            else if (p is "UP1")
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(WindowForUP1))    //находим нужное окно и устаонвливаем значение в сво-во
                    {
                        (window as WindowForUP1).DialogResult = true;
                    }
                }
            }
        }

        public void ShowWindowDialog(object p)
        {
            if (p is Pledgor)
            {
                Window win = new EditPledgorWin();
                win.ShowDialog();
            }
            else if (p is Pledgee)
            {
                Window win = new EditPledgeeWin();
                win.ShowDialog();
            }
            else if (p is PersonalProperty)
            {
                Window win = new EditPropertyWin();
                win.ShowDialog();
            }
            else if (p is "Create_UZ1")
            {
                WindowForUZ1 WindowForUZ1 = new WindowForUZ1();
                WindowForUZ1.ShowDialog();
            }           
            else if (p is "Create_UP1")
            {
                WindowForUP1 WindowForUP1 = new WindowForUP1();
                WindowForUP1.ShowDialog();
            }
            else if (p is "Settings")
            {
                Settings dB_Win = new Settings();
                dB_Win.ShowDialog();
            }
        }
    }
}
