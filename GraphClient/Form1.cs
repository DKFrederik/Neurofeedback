using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphClient
{
    public partial class Form1 : Form
    {
        /**
         * 
         * Just testing Graph client.... Using CPU timing for testing!
         * 
         * */


        private Thread cpuThread;
        //This is the time shown on the diagram
        private double[] cpuArray = new double[60];
        

        public Form1()
        {
            InitializeComponent();
        }

        private void GetPrefomanceCounters()
        {
            var cpuPerformanceCounter = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
            while (true)
            {
                cpuArray[cpuArray.Length - 1] = Math.Round(cpuPerformanceCounter.NextValue(), 0);
                Array.Copy(cpuArray, 1, cpuArray, 0, cpuArray.Length - 1);

                if (chart.IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)delegate { UpdateCpuChart(); });
                }
                else
                {
                    //Do somthing 
                }

                //This is the update interval
                Thread.Sleep(400);
            }
        }

        private void UpdateCpuChart()
        {
            chart.Series["Series1"].Points.Clear();

            for (int i = 0; i < cpuArray.Length - 1; ++i)
            {
                chart.Series["Series1"].Points.AddY(cpuArray[i]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cpuThread = new Thread(new ThreadStart(this.GetPrefomanceCounters));
            cpuThread.IsBackground = true;
            cpuThread.Start();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            cpuThread = new Thread(new ThreadStart(this.GetPrefomanceCounters));
            cpuThread.IsBackground = true;
            cpuThread.Start();
        }
    }
}
