using dress.su.Forms;

namespace dress.su.Sys
{
    static class GlobalController
    {
        static FRM_Main _frmMain;

        public static void Init(FRM_Main in_frmMain) { _frmMain = in_frmMain; }

        public static void AddSkuAction() { _frmMain.AddSku(); }
        public static void SearchSkuAction() { _frmMain.SearchSku(); }
    };
}
