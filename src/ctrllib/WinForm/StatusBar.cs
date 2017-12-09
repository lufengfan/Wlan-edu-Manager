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
    }
}
