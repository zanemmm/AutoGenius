﻿namespace AutoGenius
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
            this.SuspendLayout();
            // 
            // LabelHttpPort
            // 
            this.LabelHttpPort.AutoSize = true;
            this.LabelHttpPort.Font = new System.Drawing.Font("宋体", 12F);
            this.LabelHttpPort.Location = new System.Drawing.Point(4, 17);
            this.LabelHttpPort.Name = "LabelHttpPort";
            this.LabelHttpPort.Size = new System.Drawing.Size(112, 16);
            this.LabelHttpPort.TabIndex = 0;
            this.LabelHttpPort.Text = "HTTP服务端口:";
            // 
            // TextboxHttpPort
            // 
            this.TextboxHttpPort.BackColor = System.Drawing.Color.White;
            this.TextboxHttpPort.Font = new System.Drawing.Font("宋体", 12F);
            this.TextboxHttpPort.Location = new System.Drawing.Point(122, 12);
            this.TextboxHttpPort.Name = "TextboxHttpPort";
            this.TextboxHttpPort.Size = new System.Drawing.Size(100, 26);
            this.TextboxHttpPort.TabIndex = 1;
            this.TextboxHttpPort.Text = "2020";
            this.TextboxHttpPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxHttpPort_KeyPress);
            // 
            // ButtonHttpStart
            // 
            this.ButtonHttpStart.Font = new System.Drawing.Font("宋体", 12F);
            this.ButtonHttpStart.Location = new System.Drawing.Point(228, 12);
            this.ButtonHttpStart.Name = "ButtonHttpStart";
            this.ButtonHttpStart.Size = new System.Drawing.Size(72, 26);
            this.ButtonHttpStart.TabIndex = 2;
            this.ButtonHttpStart.Text = "开启";
            this.ButtonHttpStart.UseVisualStyleBackColor = true;
            this.ButtonHttpStart.Click += new System.EventHandler(this.ButtonHttpStart_Click);
            // 
            // ButtonHttpStop
            // 
            this.ButtonHttpStop.Enabled = false;
            this.ButtonHttpStop.Font = new System.Drawing.Font("宋体", 12F);
            this.ButtonHttpStop.Location = new System.Drawing.Point(306, 12);
            this.ButtonHttpStop.Name = "ButtonHttpStop";
            this.ButtonHttpStop.Size = new System.Drawing.Size(72, 26);
            this.ButtonHttpStop.TabIndex = 3;
            this.ButtonHttpStop.Text = "停止";
            this.ButtonHttpStop.UseVisualStyleBackColor = true;
            this.ButtonHttpStop.Click += new System.EventHandler(this.ButtonHttpStop_Click);
            // 
            // RichTextBoxLog
            // 
            this.RichTextBoxLog.BackColor = System.Drawing.Color.White;
            this.RichTextBoxLog.Font = new System.Drawing.Font("宋体", 9F);
            this.RichTextBoxLog.Location = new System.Drawing.Point(7, 44);
            this.RichTextBoxLog.Name = "RichTextBoxLog";
            this.RichTextBoxLog.ReadOnly = true;
            this.RichTextBoxLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.RichTextBoxLog.Size = new System.Drawing.Size(371, 144);
            this.RichTextBoxLog.TabIndex = 4;
            this.RichTextBoxLog.Text = "";
            this.RichTextBoxLog.WordWrap = false;
            // 
            // ButtonExportLog
            // 
            this.ButtonExportLog.Location = new System.Drawing.Point(222, 194);
            this.ButtonExportLog.Name = "ButtonExportLog";
            this.ButtonExportLog.Size = new System.Drawing.Size(75, 23);
            this.ButtonExportLog.TabIndex = 5;
            this.ButtonExportLog.Text = "导出日志";
            this.ButtonExportLog.UseVisualStyleBackColor = true;
            this.ButtonExportLog.Click += new System.EventHandler(this.ButtonExportLog_Click);
            // 
            // ButtonEmptyLog
            // 
            this.ButtonEmptyLog.Location = new System.Drawing.Point(303, 194);
            this.ButtonEmptyLog.Name = "ButtonEmptyLog";
            this.ButtonEmptyLog.Size = new System.Drawing.Size(75, 23);
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
            this.ButtonAbout.Location = new System.Drawing.Point(7, 194);
            this.ButtonAbout.Name = "ButtonAbout";
            this.ButtonAbout.Size = new System.Drawing.Size(75, 23);
            this.ButtonAbout.TabIndex = 7;
            this.ButtonAbout.Text = "关于软件";
            this.ButtonAbout.UseVisualStyleBackColor = true;
            this.ButtonAbout.Click += new System.EventHandler(this.ButtonAbout_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 223);
            this.Controls.Add(this.ButtonAbout);
            this.Controls.Add(this.ButtonEmptyLog);
            this.Controls.Add(this.ButtonExportLog);
            this.Controls.Add(this.RichTextBoxLog);
            this.Controls.Add(this.ButtonHttpStop);
            this.Controls.Add(this.ButtonHttpStart);
            this.Controls.Add(this.TextboxHttpPort);
            this.Controls.Add(this.LabelHttpPort);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.Text = "自动精灵(AutoGenius)";
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}

