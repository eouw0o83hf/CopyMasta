using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using Castle.Windsor;
using CopyMasta.Core;
using CopyMasta.Core.Handler;
using Application = System.Windows.Application;
using ContextMenu = System.Windows.Controls.ContextMenu;

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

            SetupTrayMenu();

            Hide();
        }

        private void SetupTrayMenu()
        {
            var exitMenuItem = new ToolStripMenuItem
            {
                Text = "Exit",
            };
            exitMenuItem.Click += Exit_Handler;

            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add(exitMenuItem);

            var notifyIcon = new NotifyIcon
            {
                Icon = new Icon(GetIconStream()),
                Visible = true,
                ContextMenuStrip = contextMenu
            };

            notifyIcon.DoubleClick += NotifyIcon_DoubleClick;

            using (var stream = GetIconStream())
            {
                var iconImageSource = new BitmapImage();
                iconImageSource.BeginInit();
                iconImageSource.StreamSource = stream;
                iconImageSource.EndInit();

                Icon = iconImageSource;
            }
        }

        private static Stream GetIconStream()
        {
            return Assembly
                    .GetExecutingAssembly()
                    .GetManifestResourceStream("CopyMasta.cm_large.ico");
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
            }

            base.OnStateChanged(e);
        }

        private void Exit_Handler(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = WindowState.Normal;
        }
    }
}
