namespace mayu.AI3
{
    partial class GW
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.Chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.SinBox = new System.Windows.Forms.TextBox();
            this.CosBox = new System.Windows.Forms.TextBox();
            this.Sin = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Chart)).BeginInit();
            this.SuspendLayout();
            // 
            // Chart
            // 
            chartArea1.Name = "ChartArea1";
            this.Chart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.Chart.Legends.Add(legend1);
            this.Chart.Location = new System.Drawing.Point(12, 12);
            this.Chart.Name = "Chart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.Chart.Series.Add(series1);
            this.Chart.Size = new System.Drawing.Size(934, 506);
            this.Chart.TabIndex = 0;
            this.Chart.Text = "chart1";
            // 
            // SinBox
            // 
            this.SinBox.Location = new System.Drawing.Point(830, 431);
            this.SinBox.Name = "SinBox";
            this.SinBox.Size = new System.Drawing.Size(100, 19);
            this.SinBox.TabIndex = 1;
            // 
            // CosBox
            // 
            this.CosBox.Location = new System.Drawing.Point(830, 456);
            this.CosBox.Name = "CosBox";
            this.CosBox.Size = new System.Drawing.Size(100, 19);
            this.CosBox.TabIndex = 2;
            // 
            // Sin
            // 
            this.Sin.AutoSize = true;
            this.Sin.BackColor = System.Drawing.Color.White;
            this.Sin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Sin.Location = new System.Drawing.Point(803, 434);
            this.Sin.Name = "Sin";
            this.Sin.Size = new System.Drawing.Size(21, 12);
            this.Sin.TabIndex = 3;
            this.Sin.Text = "Sin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Location = new System.Drawing.Point(803, 459);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Cos";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(855, 481);
            this.button1.Name = "DrawButton";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Draw";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.DrawButton_Click);
            // 
            // GW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 530);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Sin);
            this.Controls.Add(this.CosBox);
            this.Controls.Add(this.SinBox);
            this.Controls.Add(this.Chart);
            this.Name = "GW";
            this.Text = "GW";
            ((System.ComponentModel.ISupportInitialize)(this.Chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart Chart;
        private System.Windows.Forms.TextBox SinBox;
        private System.Windows.Forms.TextBox CosBox;
        private System.Windows.Forms.Label Sin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}