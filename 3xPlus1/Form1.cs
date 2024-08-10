using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace _3xPlus1
{
    public partial class Form1 : Form
    {
        // Create an instance of the T3XPlus1cs class
        private T3XPlus1cs t3XPlus1cs = new T3XPlus1cs();
        public Form1()
        {
            InitializeComponent();
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            PopulateSampleData(); // Call the method to populate sample data
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Get the seed value from the user
            if (long.TryParse(textBox1.Text, out long seedValue))
            {
                if (t3XPlus1cs.ProcessAlgorithm(seedValue))
                {
                    // Clear existing series
                    Charty.Series.Clear();

                    // Create a new series
                    var series = new Series("Collatz Sequence")
                    {
                        ChartType = SeriesChartType.Line
                    };

                    // Add data points to the series
                    for (int i = 0; i < t3XPlus1cs.DataListPublic.Count; i++)
                    {
                        series.Points.AddXY(i + 1, t3XPlus1cs.DataListPublic[i]);
                    }

                    // Add the series to the chart
                    Charty.Series.Add(series);
                }
                else
                {
                    MessageBox.Show("An error occurred while processing the sequence.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Clear the chart
            Charty.Series.Clear();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            int period = (int)numericUpDown1.Value;
            UpdateMovingAverageChart(period);
        }

        public decimal MA_Simple(int period, int ii)
        {
            if (period > 0 && ii >= period && ii < Data.Close.Count)
            {
                decimal summ = 0;
                for (int i = ii; i > ii - period; i--)
                {
                    summ += Data.Close[i];
                }
                return summ / period;
            }
            else
            {
                throw new ArgumentOutOfRangeException("ii", "Index ii is out of the valid range.");
            }
        }


        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            // No need to handle this event for the current requirement
        }

        private void UpdateMovingAverageChart(int period)
        {
            // Clear existing moving average series
            var movingAverageSeries = Charty.Series.FirstOrDefault(s => s.Name == "Moving Average");
            if (movingAverageSeries == null)
            {
                movingAverageSeries = new Series("Moving Average")
                {
                    ChartType = SeriesChartType.Line
                };
                Charty.Series.Add(movingAverageSeries);
            }
            else
            {
                movingAverageSeries.Points.Clear();
            }

            // Add data points to the moving average series
            for (int i = period; i < Data.Close.Count; i++)
            {
                decimal ma = MA_Simple(period, i);
                movingAverageSeries.Points.AddXY(i + 1, ma);
            }

            // Refresh the chart
            Charty.Invalidate();
        }

        private void PopulateSampleData()
        {
            // Add sample data to Data.Close
            Data.Close = new List<decimal> { 1.1m, 1.2m, 1.3m, 1.4m, 1.5m, 1.6m, 1.7m, 1.8m, 1.9m, 2.0m, 2.1m, 2.2m, 2.3m, 2.4m, 2.5m };
        }
    }


    public class Data
    {
        public static List<decimal> Close { get; set; } = new List<decimal>();
    }
}