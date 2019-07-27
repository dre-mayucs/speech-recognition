using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace mayu.AI3
{
    public partial class main : Form
    {
        //初期化
        public main() => InitializeComponent();
        /// <summary>
        /// 学習データとコマンド用変数
        /// <param name="EMsg">AMN</param>
        /// <param name="Commands">システムコマンド用定数</param>
        /// <param name="=SNS">ダイレクトリンクと呼び出し用キャッシュ</param>
        /// <param name="LData">学習データ用変数(cache)</param>
        /// <param name="Engine">音声認識エンジン</param>
        /// </summary>
        /// 
        private const string EMsg = "mayu.AI3: ";
        private readonly string[] Commands = Properties.Resources.Commands.Split(',');
        private string[] SNSCache = Properties.Resources.SNS.Split('|');
        private SpeechRecognitionEngine Engine;

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                SystemBox.Text += EMsg + DateTime.Now + "Loading Color mode..." + Environment.NewLine;

                //カラーモードの設定読み込みと適用(0:Night 1:Day)
                if (Properties.Settings.Default.ColorMode == '0')
                    NightColor();
                else if (Properties.Settings.Default.ColorMode == '1')
                    DayColor();

                //エンジンエントリポイント
                SystemBox.Text += EMsg + DateTime.Now + "Starting Engine..." + Environment.NewLine;
                Start_Engine();

                //強制GC
                GC.Collect();
            }
            catch
            {
                var ER = new Error();
                ER.ErrorMsg("E:101");
            }
        }

        /// <summary>
        /// <param name="Engie_Recognized">音声認識イベントハンドラ</param>
        /// </summary>
        private void Start_Engine()
        {
            try
            {
                //音声入力デバイス設定
                Engine = new SpeechRecognitionEngine();
                Engine.SetInputToDefaultAudioDevice();

                var Cache = Commands.Concat(SNSCache[0].Split(',')).ToArray();
                var choices = new Choices(Cache);
                var GBuilder = new GrammarBuilder();
                GBuilder.Append(choices);

                var grammar = new Grammar(GBuilder);
                Engine.LoadGrammar(grammar);

                //入力モード設定
                Engine.RecognizeAsync(RecognizeMode.Multiple);

                //イベントハンドラ
                Engine.SpeechRecognized += Engie_Recognized;
                SystemBox.Text += EMsg + DateTime.Now + "Ready :D" + Environment.NewLine;

                //メモリ解放
                Cache = null;
                GC.Collect();
            }
            catch
            {
                var ER = new Error();
                ER.ErrorMsg("E:102");
            }
        }

        /// <summary>
        /// <param name="Voice">自然言語音声出力関数</param>
        /// <param name="sender"></param>
        /// <param name="e">イベント</param>
        /// </summary>
        private void Engie_Recognized(object sender, SpeechRecognizedEventArgs e)
        {
            try
            {
                var Voice = new SpeechSynthesizer();
                //認識率が30%以下なら聞き返す
                if (e.Result.Confidence <= 0.3000000)
                {
                    Voice.Speak("ごめんもう一回いいかな");
                    SystemBox.Text += "ごめんもう一回いいかな...?" + Environment.NewLine;
                }

                //認識率30%以上で実行
                else
                {
                    //コマンド処理
                    for (int i = 0; i < Commands.Length; i++)
                        if (e.Result.Text == Commands[i]) { cmd(Commands[i]); }

                    //SNSダイレクトリンク
                    string[] SNSName = SNSCache[0].Split(',');
                    for (int i = 0; i < SNSName.Length; i++)
                        if (e.Result.Text == SNSName[i]) { Voice.Speak(DLink(e.Result.Text, i)); }

                    //ニューラルネットワーク処理
                    //だるいからまた今度実装する
                }
                Voice.Dispose();

                //SystemLogをスクロール
                SystemBox.SelectionStart = SystemBox.Text.Length;
                SystemBox.Focus();
                SystemBox.ScrollToCaret();

                //メモリ解放
                GC.Collect();
            }
            catch
            {
                var ER = new Error();
                ER.ErrorMsg("E:103");
            }
        }

        //コマンドを実行する、コマンドー...w
        private void cmd(string content)
        {
            switch (content)
            {
                case "Shutdown now":Shutdown();
                    break;
                case "Reboot now":Reboot();
                    break;
                case "Application exit":AppExit();
                    break;
                case "Application restart":AppRestart();
                    break;
                case "Day mode":DayColor();
                    break;
                case "Night mode":NightColor();
                    break;
                case "Wave": new GW().ShowDialog();
                    break;
            }
        }

        //ダイレクトリンク踏んで、Die...w
        private string DLink(string Service, int List)
        {
            var Link = SNSCache[1].Split(',');
            Process.Start(Link[List]);
            return Service.Replace("開いて","") + "開くよ!";
        }

        //OS Shutdown
        private void Shutdown()
        {
            DialogResult result = MessageBox.Show("PCをシャットダウンしますか?", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                var process = new ProcessStartInfo
                {
                    FileName = "shutdown.exe",
                    Arguments = "-s -t 0",
                    CreateNoWindow = true
                };
                Process.Start(process);
                Application.Exit();
            }
        }

        //OS Reboot
        private void Reboot()
        {
            DialogResult result = MessageBox.Show("PCを再起動しますか?", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                var process = new ProcessStartInfo
                {
                    FileName = "shutdown.exe",
                    Arguments = "-r -t 0",
                    CreateNoWindow = true
                };
                Process.Start(process);
                Application.Exit();
            }
        }

        //Application Exit
        private void AppExit()
        {
            DialogResult result = MessageBox.Show("アプリケーションを終了しますか?", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK) { Application.Exit(); }
        }

        //Application Restart
        private void AppRestart()
        {
            DialogResult result = MessageBox.Show("アプリケーションを再起動しますか?", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK) { Application.Restart(); }
        }

        //夜間モード:0
        private void NightColor()
        {
            BackColor = Color.FromArgb(64, 64, 64);

            SystemBox.BackColor = Color.FromArgb(30, 30, 30);
            SystemBox.BorderStyle = BorderStyle.None;
            SystemBox.ForeColor = Color.White;

            Properties.Settings.Default.ColorMode = '0';
            Properties.Settings.Default.Save();
        }

        //昼間モード:1
        private void DayColor()
        {
            BackColor = Color.White;
            SystemBox.BackColor = Color.FromArgb(254, 254, 254);
            SystemBox.BorderStyle = BorderStyle.Fixed3D;
            SystemBox.ForeColor = Color.Black;

            Properties.Settings.Default.ColorMode = '1';
            Properties.Settings.Default.Save();
        }
    }
}
