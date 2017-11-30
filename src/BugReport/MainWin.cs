using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows.Forms;

namespace BugReport
{
    public partial class MainWin : Form
    {
        private Dictionary<FILE, FileStream> reports;

        public MainWin()
        {
            this.InitializeComponent();

            this.Icon = Properties.Resources.Icon;

            this.Text = string.Format(this.Text, Program.ProductName);
            this.cbRestart.Text = string.Format(this.cbRestart.Text, Program.ProductName);
            this.lblInfo.Text = string.Format(this.lblInfo.Text, Program.ComName);

            this.cbRestart.Enabled = !string.IsNullOrEmpty(Program.ExecutiveName);

            this.reports = new Dictionary<FILE, FileStream>();
            if (Program.ReportFiles == null || Program.ReportFiles.Length == 0)
            {
                this.lbReports.Enabled = false;
                this.lbReports.BackColor = SystemColors.Control;
                this.lbReports.Items.Add("<空>");
            }
            else
            {
                foreach (string reportFile in Program.ReportFiles)
                {
                    FILE file = reportFile;
                    this.lbReports.Items.Add(file);
                    this.reports.Add(file, File.OpenRead(file.FullName));
                }
            }

            this.notifyIcon.Text = $"{Program.ProductName} 错误报告";
            this.notifyIcon.Icon = Properties.Resources.Icon;
        }

        [DebuggerDisplay("{FullName}")]
        struct FILE : IEquatable<FILE>
        {
            public string FullName { get; private set; }
            public string Name { get; private set; }

            public FILE(string path)
            {
                this.FullName = Path.GetFullPath(path);
                this.Name = Path.GetFileName(path);
            }

            public override bool Equals(object obj)
            {
                return (obj != null && obj is FILE file && this.Equals(file));
            }

            public bool Equals(FILE other)
            {
                return this.FullName == other.FullName;
            }

            public override int GetHashCode()
            {
                return this.FullName.GetHashCode() ^ this.Name.GetHashCode();
            }

            public override string ToString()
            {
                return this.Name;
            }

            public static implicit operator FILE(string path) =>
                new FILE(path ?? throw new ArgumentNullException(nameof(path)));

            public static explicit operator string(FILE file) => file.FullName;
        }

        private void lbReports_DoubleClick(object sender, EventArgs e)
        {
            if (this.lbReports.SelectedItems.Count == 0) return;

            FILE file = (FILE)this.lbReports.SelectedItem;
            this.txtReportContent.Text = File.ReadAllText(file.FullName);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            MailMessage msg = new MailMessage(
                new MailAddress($"549429518@qq.com", $"{Program.ProductName} Bug Report", Encoding.UTF8),
                new MailAddress("549429518@qq.com")
            )
            {
                Subject = $"{Program.ProductName} 错误报告", // 邮件标题  
                SubjectEncoding = Encoding.UTF8, // 邮件标题编码  
                Body = string.Format(
                    "<html><body><center><h1>{0} 错误报告</h1></center><h2>错误是如何发生的？</h2><p>{1}</p><h2>其他信息：</h2><p>{2}</p></body></html>",
                    Program.ProductName,
                    HttpUtility.HtmlEncode(this.txt1.Text),
                    HttpUtility.HtmlEncode(this.txt2.Text)
                ), // 邮件内容  
                BodyEncoding = Encoding.UTF8, // 邮件内容编码  
                IsBodyHtml = true, // 是否是HTML邮件  
                Priority = MailPriority.High // 邮件优先级 
            };
            foreach (var pair in this.reports)
            {
                msg.Attachments.Add(new Attachment(pair.Value, MediaTypeNames.Text.Plain) { Name = pair.Key.Name + ".xml" });
            }
            SmtpClient client = new SmtpClient("smtp.qq.com")
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("549429518@qq.com", "jbiinuegdlcgbcdi")
            };
            new Thread(() =>
            {
                Thread.Sleep(client.Timeout);
                this.abortSendBugReport(client);
            })
            { IsBackground = false }.Start();
            client.SendCompleted += this.client_SendCompleted;
            
            try
            {
                this.Visible = false;

                this.tsmiAbortSendBugReport.Click += (_sender, _e) =>
                    this.abortSendBugReport(client);
                this.notifyIcon.Visible = true;
                this.notifyIcon.ShowBalloonTip(5000, this.notifyIcon.Text, "我们将在后台为您发送错误报告。", ToolTipIcon.Info);

                client.SendAsync(msg, null);
            }
            catch (SmtpException ex)
            {
                //MessageBox.Show("错误报告发送失败。");
            }

            this.cleanReports();

            this.restart();
        }

        private void abortSendBugReport(SmtpClient client)
        {
            client.SendAsyncCancel();

            Thread.Sleep(15000);

            this.closeAfterBalloonTipClose();
            this.notifyIcon.ShowBalloonTip(5000, this.notifyIcon.Text, "我们注意到取消操作超时。稍后我们将自动关闭程序。", ToolTipIcon.Error);
        }

        private void client_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.tsmiAbortSendBugReport.Enabled = false;
            this.closeAfterBalloonTipClose();

            if (e.Cancelled)
            {
                this.notifyIcon.ShowBalloonTip(5000, this.notifyIcon.Text, "已取消发送错误报告。", ToolTipIcon.Warning);
            }
            else if (e.Error != null)
            {
                this.notifyIcon.ShowBalloonTip(5000, this.notifyIcon.Text, "错误报告发送失败。", ToolTipIcon.Error);
            }
            else
            {
                this.notifyIcon.ShowBalloonTip(5000, this.notifyIcon.Text, "错误报告发送成功。", ToolTipIcon.Info);
            }
        }

        // 当此气泡太快弹出（上一个气泡未关闭时）时，会触发上一个气泡的关闭事件，无法辨别触发事件的对象是此气泡还是上一个气泡。
        // 因此在此气泡弹出的事件中添加气泡关闭事件的处理代码。
        private void closeAfterBalloonTipClose()
        {
            this.notifyIcon.BalloonTipShown += (_sender, _e) =>
            {
                // 当气泡消失时关闭程序。
                this.notifyIcon.BalloonTipClosed += (__sender, __e) =>
                {
                    this.notifyIcon.Visible = false;
                    this.Close();
                };

                // 防止因气泡误操作导致程序长期滞留在后台无法正常关闭。
                Thread.Sleep(8000);
                this.notifyIcon.Visible = false;
                this.Close();
            };
        }

        private void cleanReports()
        {
            foreach (var pair in this.reports)
            {
                if (!File.Exists(pair.Key.FullName)) continue;
                pair.Value.Close();
                File.Delete(pair.Key.FullName);
            }

            this.reports.Clear();
        }

        private void restart()
        {
            if (this.cbRestart.Checked)
            {
                DialogResult result = DialogResult.Retry;
                while (result == DialogResult.Retry)
                {
                    result = DialogResult.Abort;
                    try
                    {
                        Process p = new Process();
                        p.StartInfo.FileName = Program.ExecutiveName;
                        p.StartInfo.Arguments = Program.RestartCmdLine;
                        p.StartInfo.UseShellExecute = false;
                        p.StartInfo.CreateNoWindow = true;
                        p.Start();
                    }
                    catch (Win32Exception ex)
                    {
                        result = MessageBox.Show(string.Format("重新启动 {0} 失败。 \"{1}\" {2}。", Program.ProductName, Program.ExecutiveName, ex.Message), this.Text, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.restart();

            this.Close();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.cleanReports();
        }
    }
}
