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
    /// Interaction logic for Fonte_Janela.xaml
    /// </summary>
    public partial class Fonte_Janela : Window
    {
        public int OK = 0;
        public Fonte_Janela()
        {
            InitializeComponent();
            Size_Slider_ValueChanged(null, null);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            this.Hide();
            e.Cancel = true;
            
        }

        private void Size_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            int valor = Convert.ToInt16(Size_Slider.Value);
            string resultado = Convert.ToString(valor);
            if(Size_Label != null)
            Size_Label.Content = resultado;
            
            if(Preview_Label != null)
            Preview_Label.FontSize = valor;



        }

        private void Bold_CheckBox_Click(object sender, RoutedEventArgs e)
        {


            if (Preview_Label != null)
                if ((bool)Bold_CheckBox.IsChecked)
                    Preview_Label.FontWeight = FontWeights.Bold;
                else
                    Preview_Label.FontWeight = FontWeights.Normal;

        }

        private void Italic_CheckBox_Click(object sender, RoutedEventArgs e)
        {

            if (Preview_Label != null)
                if ((bool)Italic_CheckBox.IsChecked)
                    Preview_Label.FontStyle = FontStyles.Italic;
                else
                    Preview_Label.FontStyle = FontStyles.Normal;

        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            OK = 1;
            this.Hide();
        }
    }
}
