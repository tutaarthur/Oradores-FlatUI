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

namespace Oradores___FlatUI
{
    /// <summary>
    /// Interaction logic for Lista.xaml
    /// </summary>
    /// 
    public partial class Lista : UserControl
    {

        public List<String> Nomes = new List<String>();

        public Lista()
        {
            InitializeComponent();
            listView.ItemsSource = Nomes;

        }
        public void remover(String nome, int tipo)
        {

            if(Nomes.Count > 0)
            if(nome != String.Empty && tipo == 0)
                Nomes.Remove(nome);
            else if(tipo == 0)
            {
                string remov = (string)listView.SelectedItem;
                Nomes.Remove(remov);
            }
            else if(tipo == 1)
            {
                Nomes.RemoveAt(0);

            }
            listView.Items.Refresh();

        }
        public void add(String nome)
        {
            Nomes.Add(nome);
            listView.Items.Refresh();
            //CollectionViewSource.GetDefaultView(CustomObservableCollection).Refresh();

        }
    }
}
