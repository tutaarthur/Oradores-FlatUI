﻿using System;
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
    /// Interaction logic for NumeroDialog.xaml
    /// </summary>
    public partial class NumeroDialog : Window
    {
        public NumeroDialog(string title, string texto, string defaultcontent)
        {
            InitializeComponent();
            this.Title = title;
            Texto_Label.Content = texto;
            Input_TextBox.Text = defaultcontent;
            Input_TextBox.Focus();
        }
        public int ok_click = 0;
        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            ok_click = 1;
            this.Close();

        }
    }
}
