
namespace Ruzdi_6.ViewModel_DesignData
{
    public class VMWindowForUZ1DD
    {
        public VMWindowForUZ1DD()
        {
            CurrentContentVM = new VM_ApplicantDD();
        }

        #region Выбранная VM

        public VM_ApplicantDD CurrentContentVM { get; set; }
        #endregion
    }
}
