namespace BugReport
{
    partial class MainWin
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabInfo = new System.Windows.Forms.TabPage();
            this.txt2 = new System.Windows.Forms.TextBox();
            this.lbl2 = new System.Windows.Forms.Label();
            this.txt1 = new System.Windows.Forms.TextBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.tabList = new System.Windows.Forms.TabPage();
            this.txtReportContent = new System.Windows.Forms.TextBox();
            this.lbReports = new System.Windows.Forms.ListBox();
            this.cbRestart = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAbortSendBugReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl.SuspendLayout();
            this.tabInfo.SuspendLayout();
            this.tabList.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabInfo);
            this.tabControl.Controls.Add(this.tabList);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(410, 408);
            this.tabControl.TabIndex = 2;
            // 
            // tabInfo
            // 
            this.tabInfo.Controls.Add(this.txt2);
            this.tabInfo.Controls.Add(this.lbl2);
            this.tabInfo.Controls.Add(this.txt1);
            this.tabInfo.Controls.Add(this.lbl1);
            this.tabInfo.Controls.Add(this.lblInfo);
            this.tabInfo.Location = new System.Drawing.Point(4, 26);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabInfo.Size = new System.Drawing.Size(402, 378);
            this.tabInfo.TabIndex = 0;
            this.tabInfo.Text = "报告信息";
            this.tabInfo.UseVisualStyleBackColor = true;
            // 
            // txt2
            // 
            this.txt2.AcceptsReturn = true;
            this.txt2.AcceptsTab = true;
            this.txt2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt2.Location = new System.Drawing.Point(9, 252);
            this.txt2.Multiline = true;
            this.txt2.Name = "txt2";
            this.txt2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt2.Size = new System.Drawing.Size(387, 120);
            this.txt2.TabIndex = 8;
            // 
            // lbl2
            // 
            this.lbl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(6, 232);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(68, 17);
            this.lbl2.TabIndex = 7;
            this.lbl2.Text = "其他信息：";
            // 
            // txt1
            // 
            this.txt1.AcceptsReturn = true;
            this.txt1.AcceptsTab = true;
            this.txt1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt1.Location = new System.Drawing.Point(9, 101);
            this.txt1.Multiline = true;
            this.txt1.Name = "txt1";
            this.txt1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt1.Size = new System.Drawing.Size(387, 120);
            this.txt1.TabIndex = 6;
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(6, 81);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(116, 17);
            this.lbl1.TabIndex = 5;
            this.lbl1.Text = "错误是如何发生的？";
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfo.Location = new System.Drawing.Point(6, 3);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(390, 78);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "　　在产品组件 {0} 中发生未知错误导致程序崩溃。我们迫切希望得到错误信息以及时修复。\r\n　　对给您带来的不便表示真诚的歉意。";
            // 
            // tabList
            // 
            this.tabList.Controls.Add(this.txtReportContent);
            this.tabList.Controls.Add(this.lbReports);
            this.tabList.Location = new System.Drawing.Point(4, 26);
            this.tabList.Name = "tabList";
            this.tabList.Padding = new System.Windows.Forms.Padding(3);
            this.tabList.Size = new System.Drawing.Size(402, 378);
            this.tabList.TabIndex = 1;
            this.tabList.Text = "报告列表";
            this.tabList.UseVisualStyleBackColor = true;
            // 
            // txtReportContent
            // 
            this.txtReportContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReportContent.Location = new System.Drawing.Point(6, 101);
            this.txtReportContent.Multiline = true;
            this.txtReportContent.Name = "txtReportContent";
            this.txtReportContent.ReadOnly = true;
            this.txtReportContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtReportContent.Size = new System.Drawing.Size(390, 271);
            this.txtReportContent.TabIndex = 3;
            this.txtReportContent.Text = "双击列表项浏览报告内容。";
            this.txtReportContent.WordWrap = false;
            // 
            // lbReports
            // 
            this.lbReports.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbReports.FormattingEnabled = true;
            this.lbReports.ItemHeight = 17;
            this.lbReports.Location = new System.Drawing.Point(6, 6);
            this.lbReports.Name = "lbReports";
            this.lbReports.Size = new System.Drawing.Size(390, 89);
            this.lbReports.TabIndex = 2;
            // 
            // cbRestart
            // 
            this.cbRestart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRestart.AutoEllipsis = true;
            this.cbRestart.Location = new System.Drawing.Point(12, 428);
            this.cbRestart.Name = "cbRestart";
            this.cbRestart.Size = new System.Drawing.Size(248, 21);
            this.cbRestart.TabIndex = 0;
            this.cbRestart.Text = "重新启动 {0} 。";
            this.cbRestart.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(347, 426);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSend.Location = new System.Drawing.Point(266, 426);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "发送(&S)";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAbortSendBugReport});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(189, 26);
            // 
            // tsmiAbortSendBugReport
            // 
            this.tsmiAbortSendBugReport.Name = "tsmiAbortSendBugReport";
            this.tsmiAbortSendBugReport.Size = new System.Drawing.Size(188, 22);
            this.tsmiAbortSendBugReport.Text = "取消发送错误报告(&C)";
            // 
            // MainWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 461);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.cbRestart);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(400, 500);
            this.Name = "MainWin";
            this.Text = "错误报告 - Wlan-edu Manager";
            this.tabControl.ResumeLayout(false);
            this.tabInfo.ResumeLayout(false);
            this.tabInfo.PerformLayout();
            this.tabList.ResumeLayout(false);
            this.tabList.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabInfo;
        private System.Windows.Forms.TextBox txt2;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.TextBox txt1;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.TabPage tabList;
        private System.Windows.Forms.CheckBox cbRestart;
        private System.Windows.Forms.TextBox txtReportContent;
        private System.Windows.Forms.ListBox lbReports;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbortSendBugReport;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
    }
}

