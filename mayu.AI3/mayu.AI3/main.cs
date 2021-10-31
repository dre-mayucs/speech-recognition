using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using DxLibDLL;

namespace mayu.AI3
{
    public partial class main : Form
    {
        #region ローカル変数
        //グラフィック変数(モデル)
        private int pmxmodel;

        //グラフィック変数(アニメーション)
        private int Index;
        private float Total;
        private float PlayTime;
        private const float Speed = 0.6f;

        //グラフィック変数(カメラ)
        private float PosX = 0.0f, PosY = 0.0f;
        private float MaxPosX = 0.3f, MinPosX = -0.3f;
        private float RotateSpeed = 0.03f;

        //システム変数
        private Point MousePoint;
        #endregion

        #region 初期設定
        public main()
        {
            //Forms初期化とGUI設定
            InitializeComponent();
            GUIInit();

            //DXLib設定
            DX.SetOutApplicationLogValidFlag(DX.FALSE);
            DX.SetUserWindow(Handle);

            //描画深度関係設定
            DX.SetZBufferBitDepth(24);
            DX.SetCreateDrawValidGraphZBufferBitDepth(24);

            //アンチエイリアス
            DX.SetFullSceneAntiAliasingMode(8, 8);

            //DXLibの初期化と描画設定
            DX.DxLib_Init();
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);

            //モデルデータ読み込み
            pmxmodel = DX.MV1LoadModel("3DData/sion.pmx");

            //vmdモーションデータ読み込み
            Index = DX.MV1AttachAnim(pmxmodel, 0, -1, DX.FALSE);
            Total = DX.MV1GetAttachAnimTotalTime(pmxmodel, Index);
            PlayTime = 0.0f;

            DX.SetCameraNearFar(0.1f, 500.0f);
            DX.SetCameraPositionAndTarget_UpVecY(DX.VGet(-0.5f, 10.0f, -20.0f), DX.VGet(0.0f, 10.0f, 0.0f));
        }
        #endregion

        #region GUI
        /// <summary>
        /// フォームGUI設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Shown(object sender, EventArgs e)
        {
            //フォームのGUI設定
            FormBorderStyle = FormBorderStyle.None;
            TransparencyKey = Color.FromArgb(0, 0, 0);
        }
        #endregion

        #region グラフィック処理
        public async void Graphics()
        {
            //裏描画削除
            DX.ClearDrawScreen();
            DX.DrawBox(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, DX.GetColor(0, 0, 0), DX.TRUE);

            //スピードの加算と再生時間リセット
            PlayTime += Speed;
            if (PlayTime >= Total) { PlayTime = 0.0f; }

            //再生位置設定とその位置でのモデル描画
            DX.MV1SetAttachAnimTime(pmxmodel, Index, PlayTime);
            DX.MV1DrawModel(pmxmodel);

            //3Dモデルの回転
            DX.MV1SetRotationXYZ(pmxmodel, DX.VGet(PosX, PosY, 0.0f));

            //RotateX
            if (DX.CheckHitKey(DX.KEY_INPUT_UP) == DX.TRUE && RestrictionCheck.Checked == true && PosX <= MaxPosX)
            {
                PosX += 0.03f;
                if (PosX == MaxPosX) { PosX += 0; }
            }
            if (DX.CheckHitKey(DX.KEY_INPUT_UP) == DX.TRUE && RestrictionCheck.Checked == false) { PosX += 0.03f; }

            if (DX.CheckHitKey(DX.KEY_INPUT_DOWN) == DX.TRUE && RestrictionCheck.Checked == true && PosX >= MinPosX)
            {
                PosX -= 0.03f;
                if (PosX == MinPosX) { PosX += 0; }
            }
            if (DX.CheckHitKey(DX.KEY_INPUT_DOWN) == DX.TRUE && RestrictionCheck.Checked == false) { PosX -= 0.03f; }

            //RotateY
            if (DX.CheckHitKey(DX.KEY_INPUT_LEFT) == DX.TRUE) { PosY += RotateSpeed; }
            if (DX.CheckHitKey(DX.KEY_INPUT_RIGHT) == DX.TRUE) { PosY -= RotateSpeed; }

            //Close
            if (DX.CheckHitKey(DX.KEY_INPUT_ESCAPE) == DX.TRUE) { Close(); }

            //表描画
            DX.ScreenFlip();
            await Task.Delay(1);
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            PosX = 0.0f;
            PosY = 0.0f;
        }
        #endregion

        #region 音声認識エンジン
        /// <summary>
        /// 学習データとコマンド用変数
        /// <param name="EMsg">AMN</param>
        /// <param name="Commands">システムコマンド用定数</param>
        /// <param name="=SNS">ダイレクトリンクと呼び出し用キャッシュ</param>
        /// <param name="LData">学習データ用変数(cache)</param>
        /// <param name="Engine">音声認識エンジン</param>
        /// </summary>
        /// 
        private readonly string[] Commands = Properties.Resources.Commands.Split(',');
        private string[] SNSCache = Properties.Resources.SNS.Split('|');
        private SpeechRecognitionEngine Engine;

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                //エンジンエントリ
                Start_Engine();
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
        public void Start_Engine()
        {
            try
            {
                //音声入力デバイス設定
                Engine = new SpeechRecognitionEngine();
                Engine.SetInputToDefaultAudioDevice();

                //ダイレクトリンク
                var SNSLinkCache = SNSCache[0].Split(',');
                for (int i = 0; i < SNSLinkCache.Length; i++)
                    SNSLinkCache[i] += "開いて";

                var Cache = Commands.Concat(SNSLinkCache).ToArray();
                var choices = new Choices(Cache);
                var GBuilder = new GrammarBuilder();
                GBuilder.Append(choices);

                var grammar = new Grammar(GBuilder);
                Engine.LoadGrammar(grammar);

                //入力モード設定
                Engine.RecognizeAsync(RecognizeMode.Multiple);

                //イベントハンドラ
                Engine.SpeechRecognized += Engie_Recognized;
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
            var SNSLinkCache = SNSCache[0].Split(',');
            for (int i = 0; i < SNSLinkCache.Length; i++)
                SNSLinkCache[i] += "開いて";
            try
            {
                //認識率30%以上で実行
                if (e.Result.Confidence >= 0.300000)
                {
                    //コマンド処理
                    for (int i = 0; i < Commands.Length; i++)
                        if (e.Result.Text == Commands[i]) { cmd(Commands[i]); }

                    //SNSダイレクトリンク
                    for (int i = 0; i < SNSLinkCache.Length; i++)
                        if (e.Result.Text == SNSLinkCache[i]) { DLink(i); }
                }
                //メモリ解放
                GC.Collect();
            }
            catch
            {
                var ER = new Error();
                ER.ErrorMsg("E:103");
            }
        }
        #endregion

        #region 自然言語コマンド処理
        //コマンドを実行する、コマンドー...w
        private void cmd(string content)
        {
            switch (content)
            {
                case "OS Shutdown":Shutdown();
                    break;
                case "Reboot now":Reboot();
                    break;
                case "Application exit":AppExit();
                    break;
                case "Application restart":AppRestart();
                    break;
                case "Graphic wave tool": new GW().ShowDialog();
                    break;

                default:
                    var voice = new SpeechSynthesizer();
                    var rem = new Remark();
                    string data = rem.GetWeather(content);
                    voice.Speak("今日の天気は" + data + "です");
                    break;
            }
        }

        /// <summary>
        /// コマンド処理
        /// </summary>
        /// <param name="Service"></param>
        /// <param name="List"></param>
        /// <returns></returns>
        //ダイレクトリンク踏んで、Die...w
        private void DLink(int List)
        {
            var Link = SNSCache[1].Split(',');
            Process.Start(Link[List]);
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
        #endregion

        /// <summary>
        /// マウス追従-移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>    
        //
        #region マウス移動処理
        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            //マウス座標を作成
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left) { MousePoint = new Point(e.X, e.Y); }
        }

        private bool count = false;
        private void Main_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (count == false)
            {
                count = true;
                ResetButton.Show();
                RestrictionCheck.Show();
            }
            else if (count == true)
            {
                count = false;
                ResetButton.Hide();
                RestrictionCheck.Hide();
            }
        }

        private void Main_MouseMove(object sender, MouseEventArgs e)
        {
            //マウス座標が動いた分フォームを移動
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                Left += e.X - MousePoint.X;
                Top += e.Y - MousePoint.Y;
            }
        }
        #endregion

        #region フォーム最期処理
        //フォームが閉じられるときにDXライブラリを終了
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            DX.DxLib_End();
        }
        #endregion
    }
}
