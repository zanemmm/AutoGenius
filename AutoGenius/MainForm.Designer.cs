namespace AutoGenius
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.LabelHttpPort = new System.Windows.Forms.Label();
            this.TextboxHttpPort = new System.Windows.Forms.TextBox();
            this.ButtonHttpStart = new System.Windows.Forms.Button();
            this.ButtonHttpStop = new System.Windows.Forms.Button();
            this.RichTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.ButtonExportLog = new System.Windows.Forms.Button();
            this.ButtonEmptyLog = new System.Windows.Forms.Button();
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ButtonAbout = new System.Windows.Forms.Button();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ButtonStartScript = new System.Windows.Forms.Button();
            this.RichTextBoxScriptOutput = new System.Windows.Forms.RichTextBox();
            this.ButtonOpenScriptDialog = new System.Windows.Forms.Button();
            this.TextBoxScriptFile = new System.Windows.Forms.TextBox();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.MainTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelHttpPort
            // 
            this.LabelHttpPort.AutoSize = true;
            this.LabelHttpPort.Font = new System.Drawing.Font("宋体", 12F);
            this.LabelHttpPort.Location = new System.Drawing.Point(3, 18);
            this.LabelHttpPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelHttpPort.Name = "LabelHttpPort";
            this.LabelHttpPort.Size = new System.Drawing.Size(139, 20);
            this.LabelHttpPort.TabIndex = 0;
            this.LabelHttpPort.Text = "HTTP服务端口:";
            // 
            // TextboxHttpPort
            // 
            this.TextboxHttpPort.BackColor = System.Drawing.Color.White;
            this.TextboxHttpPort.Font = new System.Drawing.Font("宋体", 12F);
            this.TextboxHttpPort.Location = new System.Drawing.Point(161, 13);
            this.TextboxHttpPort.Margin = new System.Windows.Forms.Padding(4);
            this.TextboxHttpPort.Name = "TextboxHttpPort";
            this.TextboxHttpPort.Size = new System.Drawing.Size(132, 30);
            this.TextboxHttpPort.TabIndex = 1;
            this.TextboxHttpPort.Text = "2020";
            this.TextboxHttpPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxHttpPort_KeyPress);
            // 
            // ButtonHttpStart
            // 
            this.ButtonHttpStart.Font = new System.Drawing.Font("宋体", 12F);
            this.ButtonHttpStart.Location = new System.Drawing.Point(302, 12);
            this.ButtonHttpStart.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonHttpStart.Name = "ButtonHttpStart";
            this.ButtonHttpStart.Size = new System.Drawing.Size(96, 32);
            this.ButtonHttpStart.TabIndex = 2;
            this.ButtonHttpStart.Text = "开启";
            this.ButtonHttpStart.UseVisualStyleBackColor = true;
            this.ButtonHttpStart.Click += new System.EventHandler(this.ButtonHttpStart_Click);
            // 
            // ButtonHttpStop
            // 
            this.ButtonHttpStop.Enabled = false;
            this.ButtonHttpStop.Font = new System.Drawing.Font("宋体", 12F);
            this.ButtonHttpStop.Location = new System.Drawing.Point(406, 12);
            this.ButtonHttpStop.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonHttpStop.Name = "ButtonHttpStop";
            this.ButtonHttpStop.Size = new System.Drawing.Size(96, 32);
            this.ButtonHttpStop.TabIndex = 3;
            this.ButtonHttpStop.Text = "关闭";
            this.ButtonHttpStop.UseVisualStyleBackColor = true;
            this.ButtonHttpStop.Click += new System.EventHandler(this.ButtonHttpStop_Click);
            // 
            // RichTextBoxLog
            // 
            this.RichTextBoxLog.BackColor = System.Drawing.Color.White;
            this.RichTextBoxLog.Font = new System.Drawing.Font("宋体", 9F);
            this.RichTextBoxLog.Location = new System.Drawing.Point(7, 52);
            this.RichTextBoxLog.Margin = new System.Windows.Forms.Padding(4);
            this.RichTextBoxLog.Name = "RichTextBoxLog";
            this.RichTextBoxLog.ReadOnly = true;
            this.RichTextBoxLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.RichTextBoxLog.Size = new System.Drawing.Size(493, 179);
            this.RichTextBoxLog.TabIndex = 4;
            this.RichTextBoxLog.Text = "";
            this.RichTextBoxLog.WordWrap = false;
            // 
            // ButtonExportLog
            // 
            this.ButtonExportLog.Location = new System.Drawing.Point(294, 239);
            this.ButtonExportLog.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonExportLog.Name = "ButtonExportLog";
            this.ButtonExportLog.Size = new System.Drawing.Size(100, 29);
            this.ButtonExportLog.TabIndex = 5;
            this.ButtonExportLog.Text = "导出日志";
            this.ButtonExportLog.UseVisualStyleBackColor = true;
            this.ButtonExportLog.Click += new System.EventHandler(this.ButtonExportLog_Click);
            // 
            // ButtonEmptyLog
            // 
            this.ButtonEmptyLog.Location = new System.Drawing.Point(402, 239);
            this.ButtonEmptyLog.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonEmptyLog.Name = "ButtonEmptyLog";
            this.ButtonEmptyLog.Size = new System.Drawing.Size(100, 29);
            this.ButtonEmptyLog.TabIndex = 6;
            this.ButtonEmptyLog.Text = "清空日志";
            this.ButtonEmptyLog.UseVisualStyleBackColor = true;
            this.ButtonEmptyLog.Click += new System.EventHandler(this.ButtonEmptyLog_Click);
            // 
            // NotifyIcon
            // 
            this.NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon.Icon")));
            this.NotifyIcon.Text = "自动精灵(AutoGenius)";
            this.NotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseClick);
            // 
            // ButtonAbout
            // 
            this.ButtonAbout.Location = new System.Drawing.Point(7, 239);
            this.ButtonAbout.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonAbout.Name = "ButtonAbout";
            this.ButtonAbout.Size = new System.Drawing.Size(100, 29);
            this.ButtonAbout.TabIndex = 7;
            this.ButtonAbout.Text = "关于软件";
            this.ButtonAbout.UseVisualStyleBackColor = true;
            this.ButtonAbout.Click += new System.EventHandler(this.ButtonAbout_Click);
            // 
            // MainTabControl
            // 
            this.MainTabControl.Controls.Add(this.tabPage1);
            this.MainTabControl.Controls.Add(this.tabPage2);
            this.MainTabControl.Location = new System.Drawing.Point(12, 12);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(520, 306);
            this.MainTabControl.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.RichTextBoxLog);
            this.tabPage1.Controls.Add(this.ButtonAbout);
            this.tabPage1.Controls.Add(this.LabelHttpPort);
            this.tabPage1.Controls.Add(this.ButtonEmptyLog);
            this.tabPage1.Controls.Add(this.TextboxHttpPort);
            this.tabPage1.Controls.Add(this.ButtonExportLog);
            this.tabPage1.Controls.Add(this.ButtonHttpStart);
            this.tabPage1.Controls.Add(this.ButtonHttpStop);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(512, 277);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "服务端";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ButtonStartScript);
            this.tabPage2.Controls.Add(this.RichTextBoxScriptOutput);
            this.tabPage2.Controls.Add(this.ButtonOpenScriptDialog);
            this.tabPage2.Controls.Add(this.TextBoxScriptFile);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(512, 277);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "PHP脚本";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ButtonStartScript
            // 
            this.ButtonStartScript.Font = new System.Drawing.Font("宋体", 12F);
            this.ButtonStartScript.Location = new System.Drawing.Point(403, 238);
            this.ButtonStartScript.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonStartScript.Name = "ButtonStartScript";
            this.ButtonStartScript.Size = new System.Drawing.Size(96, 32);
            this.ButtonStartScript.TabIndex = 6;
            this.ButtonStartScript.Text = "执行";
            this.ButtonStartScript.UseVisualStyleBackColor = true;
            this.ButtonStartScript.Click += new System.EventHandler(this.ButtonStartScript_Click);
            // 
            // RichTextBoxScriptOutput
            // 
            this.RichTextBoxScriptOutput.BackColor = System.Drawing.Color.White;
            this.RichTextBoxScriptOutput.Font = new System.Drawing.Font("宋体", 9F);
            this.RichTextBoxScriptOutput.Location = new System.Drawing.Point(14, 57);
            this.RichTextBoxScriptOutput.Margin = new System.Windows.Forms.Padding(4);
            this.RichTextBoxScriptOutput.Name = "RichTextBoxScriptOutput";
            this.RichTextBoxScriptOutput.ReadOnly = true;
            this.RichTextBoxScriptOutput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.RichTextBoxScriptOutput.Size = new System.Drawing.Size(485, 173);
            this.RichTextBoxScriptOutput.TabIndex = 5;
            this.RichTextBoxScriptOutput.Text = "";
            this.RichTextBoxScriptOutput.WordWrap = false;
            // 
            // ButtonOpenScriptDialog
            // 
            this.ButtonOpenScriptDialog.Font = new System.Drawing.Font("宋体", 12F);
            this.ButtonOpenScriptDialog.Location = new System.Drawing.Point(366, 17);
            this.ButtonOpenScriptDialog.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonOpenScriptDialog.Name = "ButtonOpenScriptDialog";
            this.ButtonOpenScriptDialog.Size = new System.Drawing.Size(133, 32);
            this.ButtonOpenScriptDialog.TabIndex = 3;
            this.ButtonOpenScriptDialog.Text = "选择PHP脚本";
            this.ButtonOpenScriptDialog.UseVisualStyleBackColor = true;
            this.ButtonOpenScriptDialog.Click += new System.EventHandler(this.ButtonOpenScriptDialog_Click);
            // 
            // TextBoxScriptFile
            // 
            this.TextBoxScriptFile.BackColor = System.Drawing.Color.White;
            this.TextBoxScriptFile.Font = new System.Drawing.Font("宋体", 12F);
            this.TextBoxScriptFile.Location = new System.Drawing.Point(14, 18);
            this.TextBoxScriptFile.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxScriptFile.Name = "TextBoxScriptFile";
            this.TextBoxScriptFile.Size = new System.Drawing.Size(343, 30);
            this.TextBoxScriptFile.TabIndex = 2;
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.Filter = "|*.php";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 328);
            this.Controls.Add(this.MainTabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.Text = "自动精灵(AutoGenius)";
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LabelHttpPort;
        private System.Windows.Forms.TextBox TextboxHttpPort;
        private System.Windows.Forms.Button ButtonHttpStart;
        private System.Windows.Forms.Button ButtonHttpStop;
        private System.Windows.Forms.RichTextBox RichTextBoxLog;
        private System.Windows.Forms.Button ButtonExportLog;
        private System.Windows.Forms.Button ButtonEmptyLog;
        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private System.Windows.Forms.Button ButtonAbout;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button ButtonStartScript;
        private System.Windows.Forms.RichTextBox RichTextBoxScriptOutput;
        private System.Windows.Forms.Button ButtonOpenScriptDialog;
        private System.Windows.Forms.TextBox TextBoxScriptFile;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
    }
}

