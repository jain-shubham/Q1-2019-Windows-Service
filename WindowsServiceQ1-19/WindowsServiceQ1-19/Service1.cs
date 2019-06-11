using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WindowsServiceQ1_19
{
    public partial class Service1 : ServiceBase
    {
        Timer t = null;
        string filePath = "../../ServiceLog.txt";
        string sourceFile = "../../Record.txt";

        public Service1()
        {
            InitializeComponent();
            t = new Timer();
            t.Elapsed += new ElapsedEventHandler(Time_Elapsed);
            t.Interval = 1 * 60 * 1000;
            t.Enabled = true;
            t.Start();
        }

        void Time_Elapsed(object sender, ElapsedEventArgs e)
        {
            logData();
        }

        public void logData()
        {
            string[] arySchedule = File.ReadAllLines(sourceFile);
            File.AppendAllLines(filePath, arySchedule);
            File.Delete(sourceFile);
        }
        
        protected override void OnStart(string[] args)
        {
            string[] lines = { "1. First", "2. Second", "3. Third" };
            File.AppendAllLines(sourceFile, lines);
        }

        protected override void OnStop()
        {
            t.Stop();
            t.Enabled = false;
            File.AppendAllText(filePath, "Service stoped : " + DateTime.Now.ToString() + Environment.NewLine);
        }
    }
}
