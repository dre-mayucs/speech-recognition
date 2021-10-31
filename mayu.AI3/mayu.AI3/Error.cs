using System;
using System.Windows.Forms;

namespace mayu.AI3
{
    class Error
    {
        private string EMsg;
        public void ErrorMsg(string Code)
        {
            switch (Code)
            {
                case "E:101": EMsg = "設定ファイルが破損しています" + Environment.NewLine + "設定ファイルを修復してください";
                    break;
                case "E:102": EMsg = "入力デバイスが有効でないか学習データが破損しています";
                    break;
                case "E:103": EMsg = "アプリケーションが破損しています";
                    break;
                case "E:201": EMsg = "解析データが破損しているかアプリケーションが破損しています";
                    break;
            }
            DialogResult result = MessageBox.Show(EMsg, Code, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            if (result == DialogResult.Retry) { Application.Restart(); }
            else if (result == DialogResult.Cancel) { Application.Exit(); }
        }
    }
}
