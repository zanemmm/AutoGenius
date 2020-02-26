using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Windows.Forms;
using OpenCvSharp;

namespace AutoGenius
{
    public partial class MainForm : Form
    {
        readonly HTTPServer httpServer = new HTTPServer();

        private readonly Form about;

        public MainForm()
        {
            InitializeComponent();
            about = new About();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Log.Setup(RichTextBoxLog);
            Log.Info("AutoGenius 启动");
        }

        private void TextboxHttpPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ButtonHttpStart_Click(object sender, EventArgs e)
        {
            if (!httpServer.serverStart)
            {
                ButtonHttpStart.Enabled = !httpServer.Start(TextboxHttpPort.Text);
                ButtonHttpStop.Enabled = !ButtonHttpStart.Enabled;
                TextboxHttpPort.Enabled = ButtonHttpStart.Enabled;
            }
        }

        private void ButtonHttpStop_Click(object sender, EventArgs e)
        {
            if (httpServer.serverStart)
            {
               ButtonHttpStop.Enabled = !httpServer.Stop();
               ButtonHttpStart.Enabled = !ButtonHttpStop.Enabled;
               TextboxHttpPort.Enabled = ButtonHttpStart.Enabled;
            }
        }

        private void ButtonEmptyLog_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("确定清空日志?", "提示", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                RichTextBoxLog.Text = "";
            }
        }

        private void ButtonExportLog_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(@".\logs"))
            {
                Directory.CreateDirectory(@".\logs");

            }
            var filename = @".\logs\" + DateTime.Now.ToString(@"MM-dd-hh_mm_ss_") + "log.txt";
            File.WriteAllText(filename, RichTextBoxLog.Text);
            MessageBox.Show(string.Format("导出日志为 {0}", filename), "提示");
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Visible = false;
                NotifyIcon.Visible = true;
            }
        }


        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Visible = true;
                WindowState = FormWindowState.Normal;
                Activate();
                NotifyIcon.Visible = false;
            }
        }

        private void ButtonAbout_Click(object sender, EventArgs e)
        {
            about.Visible = true;
            about.Activate();
        }

        private static void KillProcessAndChildren(int pid)
        {
            // Cannot close 'system idle process'.
            if (pid == 0)
            {
                return;
            }
            ManagementObjectSearcher searcher = new ManagementObjectSearcher
                    ("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection moc = searcher.Get();
            foreach (ManagementObject mo in moc)
            {
                KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]));
            }
            try
            {
                Process proc = Process.GetProcessById(pid);
                proc.Kill();
            }
            catch (ArgumentException)
            {
                // Process already exited.
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            KillProcessAndChildren(Process.GetCurrentProcess().Id);
        }
    }
}
