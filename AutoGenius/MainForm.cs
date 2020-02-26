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
using System.Threading;
using System.Threading.Tasks;
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

        private void ButtonOpenScriptDialog_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                TextBoxScriptFile.Text = OpenFileDialog.FileName;
            }
        }

        private Task ScriptTask;
        private Process p;
        private void ButtonStartScript_Click(object sender, EventArgs e)
        {
            if (ScriptTask != null && !ScriptTask.IsCompleted)
            {
                KillProcessAndChildren(p.Id);
                ButtonStartScript.Text = "执行";
                return;
            }
            p = GetScriptProcess();
            RichTextBoxScriptOutput.Text = "";
            ScriptTask = new Task(() => {
                SetButtonText("停止");
                p.Start();
                var cmd = @".\php\php.exe -c .\php\php.ini bootloader.php 127.0.0.1 " + TextboxHttpPort.Text + " " + TextBoxScriptFile.Text + " &exit";
                p.StandardInput.WriteLine(cmd);
                p.StandardInput.AutoFlush = true;
                StreamReader reader = p.StandardOutput;//截取输出流
                string line = reader.ReadLine();//每次读取一行
                UpdateScriptOutput(line);
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    UpdateScriptOutput(line);
                }
                p.WaitForExit();//等待程序执行完退出进程
                p.Close();
                SetButtonText("执行");
            });
            ScriptTask.Start();
        }

        private Process GetScriptProcess()
        {
            var p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;  //由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;
            return p;
        }

        private delegate void DelegateSetButtonText();
        private void SetButtonText(string text)
        {
            DelegateSetButtonText set = delegate ()
            {
                ButtonStartScript.Text = text;
            };
            ButtonStartScript.Invoke(set);
        }

        private delegate void DelegateUpdateScriptOutput();
        private void UpdateScriptOutput(string line)
        {
            DelegateUpdateScriptOutput output = delegate ()
            {
                RichTextBoxScriptOutput.AppendText(line + "\n");
            };
            RichTextBoxScriptOutput.Invoke(output);
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

        private void ButtonClearScriptOutput_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("确定清空脚本输出?", "提示", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                RichTextBoxScriptOutput.Text = "";
            }
        }

        private void ButtonExportScriptOutput_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(@".\output"))
            {
                Directory.CreateDirectory(@".\output");
            }
            var filename = @".\output\" + DateTime.Now.ToString(@"MM-dd-hh_mm_ss_") + "output.txt";
            File.WriteAllText(filename, RichTextBoxScriptOutput.Text);
            MessageBox.Show(string.Format("导出日志为 {0}", filename), "提示");
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            KillProcessAndChildren(Process.GetCurrentProcess().Id);
        }
    }
}
