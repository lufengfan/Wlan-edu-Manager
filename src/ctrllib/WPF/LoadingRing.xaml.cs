using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WPF
{
    /// <summary>
    /// LoadingRing.xaml 的交互逻辑
    /// </summary>
    public partial class LoadingRing : UserControl
    {
        private bool LoadingRing_fInitialize = false;
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            if (this.LoadingRing_fInitialize)
                this.contentControl.Content = newContent;
            else
            {
                base.OnContentChanged(oldContent, newContent);
                this.LoadingRing_fInitialize = true;
            }
        }

        public LoadingRing()
        {
            InitializeComponent();

            this.CellCount = 100;
        }
    }
}
