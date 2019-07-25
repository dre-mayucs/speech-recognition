using System;

namespace mayu.AI3
{
    class Error
    {
        public void ErrorMsg(string Code)
        {
            var EMsg = "";
            switch (Code)
            {
                case "E:101": EMsg = "設定ファイルが破損しています" + Environment.NewLine + "設定ファイルを修復してください";
                    break;
                case "E:102": EMsg = "入力デバイスが有効でないか学習データが破損しています";
                    break;
            }
        }
    }
}
