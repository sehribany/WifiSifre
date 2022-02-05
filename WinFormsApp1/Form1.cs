using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string wifiname, password;
        private void Form1_Load(object sender, EventArgs e)
        {
            using (var process1 = new Process())
            {
                process1.StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments = "/C netsh wlan show profile",
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                };
                process1.Start();
                wifiname = process1.StandardOutput.ReadToEnd();
                process1.WaitForExit();
            }
            richTextBox1.Text = wifiname.Trim();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var process2 = new Process())
            {
                string wifi = textBox1.Text;
                process2.StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments = "/C netsh wlan show profile " + wifi + " key = clear" + "| findstr Key",
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                };
                process2.Start();
                password = process2.StandardOutput.ReadToEnd();
                process2.WaitForExit();
            }
            textBox2.Text = password.Trim();
        }
    }
}
