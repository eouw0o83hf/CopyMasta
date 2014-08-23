using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CopyMasta.Core;
using CopyMasta.Core.Handler;

namespace CopyMasta
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IHandler
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Handle(KeyState state)
        {
            lbl_AltPressed.Content = state.MetaKeys.HasFlag(MetaKeys.Alt).ToString();
            lbl_ControlPressed.Content = state.MetaKeys.HasFlag(MetaKeys.Ctrl).ToString();
            lbl_ShiftPressed.Content = state.MetaKeys.HasFlag(MetaKeys.Shift).ToString();
        }
    }
}
