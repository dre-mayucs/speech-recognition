using System.Drawing;
using System.Drawing.Drawing2D;

namespace mayu.AI3
{
    partial class main
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.ResetButton = new System.Windows.Forms.Button();
            this.RestrictionCheck = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ResetButton
            // 
            this.ResetButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.ResetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResetButton.Location = new System.Drawing.Point(332, 458);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(50, 50);
            this.ResetButton.TabIndex = 0;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = false;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // RestrictionCheck
            // 
            this.RestrictionCheck.AutoSize = true;
            this.RestrictionCheck.Checked = true;
            this.RestrictionCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RestrictionCheck.Location = new System.Drawing.Point(332, 436);
            this.RestrictionCheck.Name = "RestrictionCheck";
            this.RestrictionCheck.Size = new System.Drawing.Size(48, 16);
            this.RestrictionCheck.TabIndex = 1;
            this.RestrictionCheck.Text = "制限";
            this.RestrictionCheck.UseVisualStyleBackColor = true;
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(394, 520);
            this.Controls.Add(this.RestrictionCheck);
            this.Controls.Add(this.ResetButton);
            this.Name = "main";
            this.Text = "Form1";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Main_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Main_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Main_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region ここからは自分デザイナーコードだでやんでい！！！
        private void GUIInit()
        {
            //GraphicsPathの作成
            var path = new GraphicsPath();
            var rect = new Rectangle(1, 1, 45, 45);
            path.AddArc(rect, 0, 360);
            //コントロールの形を変更
            this.ResetButton.Region = new Region(path);

            this.ResetButton.Hide();
            this.RestrictionCheck.Hide();
        }
        #endregion

        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.CheckBox RestrictionCheck;
    }
}

