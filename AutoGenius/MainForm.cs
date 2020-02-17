using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenCvSharp;

namespace AutoGenius
{
    public partial class MainForm : Form
    {
        readonly HTTPServer httpServer = new HTTPServer();

        public MainForm()
        {
            InitializeComponent();
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
    }
}
