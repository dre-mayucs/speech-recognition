using System;
using System.Windows.Forms;

namespace mayu.AI3
{
    public partial class GW : Form
    {
        public GW()
        {
            InitializeComponent();
        }

        private void DrawButton_Click(object sender, EventArgs e)
        {
            Chart.Series.Clear();
            Chart.Series.Add("Sin(x)");
            Chart.Series.Add("sin(sin(x)*3)");
            Chart.Series.Add("Cos(x)");
            Chart.Series.Add("Cos(Cos(x)*3)");

            Chart.Series["Sin(x)"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            Chart.Series["sin(sin(x)*3)"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            Chart.Series["Cos(x)"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            Chart.Series["Cos(Cos(x)*3)"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            try
            {
                if (SinBox.Text != null && CosBox.Text == null) { Sin_Draw(); }
                else if (CosBox.Text != null && SinBox.Text == null) { Cos_Draw(); }
                else { Sin_Cos_Draw(); }
            }
            catch
            {
                MessageBox.Show("Please number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SinBox.Text = null;
                CosBox.Text = null;
            }
        }
        private void Sin_Draw()
        {
            for (double value = 0.0; value <= double.Parse(SinBox.Text) * Math.PI; value += Math.PI / 180)
            {
                Chart.Series["Sin(x)"].Points.AddXY(value, Math.Sin(value));
                Chart.Series["Sin(Sin(x)*3)"].Points.AddXY(value, Math.Sin(Math.Sin(value) * 3));
            }
        }

        private void Cos_Draw()
        {
            for (double value = 0.0; value <= double.Parse(CosBox.Text) * Math.PI; value += Math.PI / 180)
            {
                Chart.Series["Cos(x)"].Points.AddXY(value, Math.Sin(value));
                Chart.Series["Cos(Cos(x)*3)"].Points.AddXY(value, Math.Cos(Math.Cos(value) * 3));
            }
        }

        private void Sin_Cos_Draw()
        {
            Sin_Draw();
            Cos_Draw();
        }
    }
}
