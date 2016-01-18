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
using System.Windows.Shapes;

namespace Oradores___FlatUI
{
    /// <summary>
    /// Interaction logic for JanelaExtra.xaml
    /// </summary>
    public partial class JanelaExtra : Window
    {
        public JanelaExtra()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            var main = this.Owner as MainWindow;
            if(main != null)
            main.Janela_Fechou();
            e.Cancel = true;
        }
    }
}
