
using UploadNotification;

namespace Ruzdi_6.ViewModel_DesignData
{
    public class VMWindowForUZ1DD
    {
        public VMWindowForUZ1DD()
        {
            CurrentContentVM = new VM_ApplicantDD();
        }

        public string Textsendbutton { get; set; } = "Подписать и отправить";

        #region Выбранная VM

        public VM_ApplicantDD CurrentContentVM { get; set; }
        #endregion

        Task<uploadNotificationPackageResponse> TaskSend { get; set; }

        public bool TaskResultOK => TaskSend.IsCompleted;
    }
}
