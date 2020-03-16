using System;
using System.Windows.Forms;

namespace mayu.AI3
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            main Window = new main();
            Window.Show();

            while (Window.Created)
            {
                Window.Graphics();
                Application.DoEvents();
            }
        }
    }
}