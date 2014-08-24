using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using CopyMasta.Core;
using CopyMasta.Core.Handler;

namespace CopyMasta
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var stream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("CopyMasta.cm_large.ico");

            var icon = new NotifyIcon
                {
                    Icon = new Icon(stream),
                    Visible = true
                };

            icon.DoubleClick += (a, b) =>
                {
                    Show();
                    WindowState = WindowState.Normal;
                };

            Hide();
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
            }

            base.OnStateChanged(e);
        }
    }
}
