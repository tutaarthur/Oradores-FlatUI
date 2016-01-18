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
    /// Interaction logic for InputBox.xaml
    /// </summary>
    public partial class InputBox : Window
    {
        public InputBox()
        {
            InitializeComponent();
            Nome_Aba.Focus();
        }
        public string nome { get { return Nome_Aba.Text; } }

        public int ok_pressed = 0;
        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            ok_pressed = 1;
            this.Close();
        }
    }
}
