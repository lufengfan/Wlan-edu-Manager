using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WinForm
{
    public class StatusBar : Label
    {
        private StatusBarState statusBarState = StatusBarState.None;
        [DefaultValue(StatusBarState.None)]
        public StatusBarState StatusBarState
        {
            get => this.statusBarState;
            set
            {
                if (this.statusBarState != value)
                {
                    this.statusBarState = value;
                    this.StatusBarStateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler StatusBarStateChanged;

        public StatusBar() : base()
        {
            this.Visible = false;
            this.AutoSize = false;
            this.ForeColor = Color.White;
            this.Dock = DockStyle.Bottom;
            this.TextAlign = ContentAlignment.MiddleLeft;

            this.StatusBarStateChanged += this.StatusBar_StatusBarStateChanged;
        }

        private void StatusBar_StatusBarStateChanged(object sender, EventArgs e)
        {
            switch (this.StatusBarState)
            {
                case StatusBarState.None:
                    this.Visible = false;
                    return;
                case StatusBarState.Information:
                    this.BackColor = Color.ForestGreen;
                    break;
                case StatusBarState.Warning:
                    this.BackColor = Color.Goldenrod;
                    break;
                case StatusBarState.Error:
                    this.BackColor = Color.IndianRed;
                    break;
            }
            this.Visible = true;
        }

        public void ShowStatus()
        {
            if (this.StatusBarState != StatusBarState.None)
                this.Visible = true;
        }

        public void ShowStatus(string text)
        {
            this.Text = text;
            this.ShowStatus();
        }

        public void ShowStatus(string text, StatusBarState state)
        {
            this.Text = text;
            this.StatusBarState = state;
            this.ShowStatus();
        }

        public delegate object InvokeHandler(Delegate method, params object[] args);

        public void ShowStatus(int timeout, InvokeHandler handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            this.ShowStatus();
            this.setTimeout(timeout, handler);
        }

        public void ShowStatus(int timeout, string text, InvokeHandler handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            this.ShowStatus(text);
            this.setTimeout(timeout, handler);
        }

        public void ShowStatus(int timeout, string text, StatusBarState state, InvokeHandler handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            this.ShowStatus(text, state);
            this.setTimeout(timeout, handler);
        }

        private void setTimeout(int timeout, InvokeHandler handler)
        {
            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(timeout);
                handler(new MethodInvoker(() => this.Visible = false));
            })
            { IsBackground = true }.Start();
        }
    }
}
