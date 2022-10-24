using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using CmlLib.Core.Auth.Microsoft;
using CmlLib.Core.VersionLoader;
using static System.Collections.Specialized.BitVector32;
using System.Media;
using CmlLib.Core.Auth;
using CmlLib.Core;
using Guna.UI2.WinForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SevenA_Launcher
{
    public partial class Form1 : Form
    {
        public static string user;
        public static int ram;
        public static string version;
        public static string serverip;
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            listBox1.Visible = false;
        
            panel2.BackColor = Color.FromArgb(250, 64, 64, 64);
            panel1.BackColor = Color.FromArgb(250, 64, 64, 64);
            panel3.BackColor = Color.FromArgb(100, 64, 64, 64);
        }
        private void path()
        {

            System.Net.ServicePointManager.DefaultConnectionLimit = 256;

            var path = new MinecraftPath();

            var launcher = new CMLauncher(path);


            //launcher.FileChanged += (e) =>
            //{
                //listBox1.Items.Add(string.Format("[{0}] {1} - {2}/{3}", e.FileKind.ToString(), e.FileName, e.ProgressedFileCount, e.TotalFileCount));
            //};
            //launcher.ProgressChanged += (s, e) =>
            //{
                //Console.WriteLine("{0}%", e.ProgressPercentage);
            //};


        }
        private void launch()
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 256;

            var path = new MinecraftPath();

            var launcher = new CMLauncher(path);


            launcher.FileChanged += (e) =>
            {
                listBox1.Items.Add(string.Format("[{0}] {1} - {2}/{3}", e.FileKind.ToString(), e.FileName, e.ProgressedFileCount, e.TotalFileCount));
                listBox1.TopIndex = listBox1.Items.Count - 1;
            };

            var launchOption = new MLaunchOption
            {
                MaximumRamMb = ram,
                Session = MSession.GetOfflineSession(user),
                ServerIp = serverip,
            };

            var process = launcher.CreateProcess("1.19.2-Aristois", launchOption);

            process.Start();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        
        }



        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            guna2GradientButton1.Enabled = false;
            user = guna2TextBox1.Text;
            serverip = guna2TextBox2.Text; 
            ram = guna2TrackBar1.Value * 1024;

            Thread thread = new Thread(() => launch());
            thread.IsBackground = true;
            thread.Start();
            System.Threading.Thread.Sleep(800);
            guna2GradientButton1.Enabled = true;
        }

    }
}
