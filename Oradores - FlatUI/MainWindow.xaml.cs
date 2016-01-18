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
using System.Windows.Threading;
using System.IO;

namespace Oradores___FlatUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public Fonte_Janela fonte_Mostrador = new Fonte_Janela();
        public Fonte_Janela fonte_Lista = new Fonte_Janela();
        public JanelaExtra janela = new JanelaExtra();
        public int quorum;
        public bool salvo = false;

        public struct tipo_lista
        {
            public FontStyle estilo;
            public double tamanho;
            public FontWeight expessura;

        }
        public tipo_lista f_lista = new tipo_lista();

        private DispatcherTimer timer = new DispatcherTimer();
        

        public MainWindow()
        {
            InitializeComponent();
            fonte_Mostrador.Size_Slider.Value = Mostrador_Label.FontSize;
            f_lista.estilo = FontStyles.Normal;
            f_lista.tamanho = 20;
            f_lista.expessura = FontWeights.Normal;
            fonte_Lista.Size_Slider.Value = f_lista.tamanho;
            update_Listas_Fonts();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += timer_Tick;
            timer.Start();
            timer.IsEnabled = false;
            clock_reset();
            checkar_Numero_Abas();

            //Iguala as fontes da Janela Extra e da Principal
            janela.label.FontSize = Mostrador_Label.FontSize;
            janela.label.FontStyle = Mostrador_Label.FontStyle;
            janela.label.FontWeight = Mostrador_Label.FontWeight;
            janela.listView.FontSize = f_lista.tamanho;
            janela.listView.FontStyle = f_lista.estilo;
            janela.listView.FontWeight = f_lista.expessura;
         }

        private void FonteLista_Button_Click(object sender, RoutedEventArgs e)
        {
            fonte_Lista.ShowDialog();
            if(fonte_Lista.OK == 1)
            {
                f_lista.estilo = fonte_Lista.Preview_Label.FontStyle;
                f_lista.tamanho = fonte_Lista.Preview_Label.FontSize;
                f_lista.expessura = fonte_Lista.Preview_Label.FontWeight;
                update_Listas_Fonts();

            }
        }

        private void FonteRelogio_Button_Click(object sender, RoutedEventArgs e)
        {
            fonte_Mostrador.ShowDialog();
            if (fonte_Mostrador.OK == 1)
            {
                Mostrador_Label.FontSize = fonte_Mostrador.Preview_Label.FontSize;
                Mostrador_Label.FontStyle = fonte_Mostrador.Preview_Label.FontStyle;
                Mostrador_Label.FontWeight = fonte_Mostrador.Preview_Label.FontWeight;

                //Mudando a fonte do relogio da janela extra
                janela.label.FontSize = fonte_Mostrador.Preview_Label.FontSize;
                janela.label.FontStyle = fonte_Mostrador.Preview_Label.FontStyle;
                janela.label.FontWeight = fonte_Mostrador.Preview_Label.FontWeight;

                fonte_Mostrador.OK = 0;
            }
        }

        private void Criar_Button_Click(object sender, RoutedEventArgs e)
        {

            var input = new InputBox();
            var nova_aba = new TabItem();
            var lista = new Lista();

            
            input.ShowDialog();
            if (input.nome != String.Empty && input.ok_pressed == 1)
            {
                nova_aba.Header = input.nome;
                lista.ClipToBounds = true;
                lista.listView.FontWeight = f_lista.expessura;
                lista.listView.FontStyle = f_lista.estilo;
                lista.listView.FontSize = f_lista.tamanho;
                input.ok_pressed = 0;
                //nova_aba.Background = this.Background;

                nova_aba.Content = lista;
                tabControl.Items.Add(nova_aba);
                tabControl.SelectedItem = nova_aba;
            }
            checkar_Numero_Abas();
        }

        void criar_aba(string nome)
        {

            var nova_aba = new TabItem();
            var lista = new Lista();

            nova_aba.Header = nome;
            lista.ClipToBounds = true;
            lista.listView.FontWeight = f_lista.expessura;
            lista.listView.FontStyle = f_lista.estilo;
            lista.listView.FontSize = f_lista.tamanho;
            //nova_aba.Background = this.Background;

            nova_aba.Content = lista;
            tabControl.Items.Add(nova_aba);
            tabControl.SelectedItem = nova_aba;
        
            checkar_Numero_Abas();

    }

    private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            var selecionado = (TabItem)tabControl.SelectedItem;

            if (selecionado != null)
            {
                var lista = (Lista)selecionado.Content;
                if (Nome_Box.Text != String.Empty)
                    lista.add(Nome_Box.Text);

                Nome_Box.Text = "";
                update_JanelaExtra();
            }

        }
        

        private void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            var selecionado = (TabItem)tabControl.SelectedItem;

            if (selecionado != null)
            {

                var lista = (Lista)selecionado.Content;
                if (Nome_Box.Text != String.Empty)
                    lista.remover(Nome_Box.Text, 0);
                else
                    lista.remover(String.Empty, 0);

                Nome_Box.Text = "";
                update_JanelaExtra();
            }

        }


        private void checkar_Numero_Abas()
        {
            if(tabControl.Items.Count > 0)
            {
                Add_Button.Visibility = Visibility.Visible;
                Remove_Button.Visibility = Visibility.Visible;
                Remover_Button.IsEnabled = true;

            }
            else
            {

                Add_Button.Visibility = Visibility.Hidden;
                Remove_Button.Visibility = Visibility.Hidden;
                //Muda status botão remover abas quando não houver abas.
                Remover_Button.IsEnabled = false;
            }


        }

        private void Remover_Button_Click(object sender, RoutedEventArgs e)
        {
            tabControl.Items.Remove(tabControl.SelectedItem);
            checkar_Numero_Abas();
        }

        private void Nome_Box_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Add_Button_Click(null, null);

        }
        private void update_Listas_Fonts()
        {
            int qnt_abas = tabControl.Items.Count;
            for(int i = 0; i < qnt_abas; i++)
            {
                TabItem tab = (TabItem)tabControl.Items[i];
                Lista l = (Lista)tab.Content;
                l.listView.FontSize = f_lista.tamanho;
                l.listView.FontWeight = f_lista.expessura;
                l.listView.FontStyle = f_lista.estilo;

                //Mudando a fonte da Janela Separada
                janela.listView.FontSize = f_lista.tamanho;
                janela.listView.FontWeight = f_lista.expessura;
                janela.listView.FontStyle = f_lista.estilo;
            }



        }
        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            update_JanelaExtra();
        }

        private void update_JanelaExtra()
        {
            if (tabControl.Items.Count > 0 && tabControl.SelectedItem != null)
            {
                TabItem tab = (TabItem)tabControl.SelectedItem;
                Lista l = (Lista)tab.Content;
                janela.listView.ItemsSource = l.Nomes;
                janela.listView.Items.Refresh();
            }
        }



        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Finalizando as janelas das fontes, que permaneciam abertas quando o programa encerrava
            var restult = MessageBox.Show("Deseja salvar antes de sair?", "Atenção", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation);
            if (restult == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            else if (restult == MessageBoxResult.Yes)
            {
                salvar_Lista();
                Application.Current.Shutdown();
            }
            else if (restult == MessageBoxResult.No)
                Application.Current.Shutdown();
        }

        //Estabelecendo as teclas de atalho
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.N && (Keyboard.Modifiers & ModifierKeys.Control) == (ModifierKeys.Control))
            {
                Criar_Button_Click(null, null);
                e.Handled = true;
            }
            if (e.Key == Key.R && (Keyboard.Modifiers & ModifierKeys.Control) == (ModifierKeys.Control))
            {

                Remover_Button_Click(null, null);
                e.Handled = true;
            }
            if (e.Key == Key.J && (Keyboard.Modifiers & ModifierKeys.Control) == (ModifierKeys.Control))
            {

                MenuItem_Click(null, null);
                e.Handled = true;
            }

            if (e.Key == Key.T && (Keyboard.Modifiers & ModifierKeys.Control) == (ModifierKeys.Control))
            {

                Tempo_Button_Click(null, null);
                e.Handled = true;
            }

            if (e.Key == Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == (ModifierKeys.Control))
            {

                salvar_Lista();
                e.Handled = true;
            }
            if (e.Key == Key.O && (Keyboard.Modifiers & ModifierKeys.Control) == (ModifierKeys.Control))
            {

                carregar_Lista();
                e.Handled = true;
            }

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            janela.Owner = this;
            janela.Show();
            Janela_Button.IsChecked = true;

        }
        public void Janela_Fechou()
        {
            Janela_Button.IsChecked = false;
        }

        private void Next_Button_Click(object sender, RoutedEventArgs e)
        {
            if (tabControl.Items.Count > 0)
            {
                var selecionado = (TabItem)tabControl.SelectedItem;
                var lista = (Lista)selecionado.Content;
                lista.remover(String.Empty, 1);
            }

        }

        private void Tempo_Button_Click(object sender, RoutedEventArgs e)
        {
            var input = new NumeroDialog("Tempo", "Digite o tempo em segundos", Convert.ToString(tempo));
            input.ShowDialog();
            if (input.ok_click == 1 && input.Input_TextBox.Text != String.Empty)
            {
                int.TryParse(input.Input_TextBox.Text, out tempo);
                clock_reset();
            }
        }

        private void Quórum_Button_Click(object sender, RoutedEventArgs e)
        {
            var input = new NumeroDialog("Quórum", "Digite o número de representações \n\t    presentes:", "");
            input.ShowDialog();
            if (input.ok_click == 1 && input.Input_TextBox.Text != String.Empty)
            {
                Int32.TryParse(input.Input_TextBox.Text, out quorum);
                if(quorum > 0)
                {
                    int ms, mq;
                    ms = quorum / 2;
                    mq = (2 * quorum) / 3;
                    Quorum_Label.Content = "Quórum: " + quorum +"  Simples: " + ms +"  Qualificada: " + mq;
                }
            }
                        
        }

        public void salvar_Lista()
        {
            List<String> Nomes = new List<String>();
            int qnt_abas = tabControl.Items.Count;
            //Nomes.Add(tempo + "//" + qnt_abas);
            for (int i = 0; i < qnt_abas; i++)
            {
                TabItem tab = (TabItem)tabControl.Items[i];
                Lista l = (Lista)tab.Content;
                Nomes.Add("-/-");
                Nomes.Add(tab.Header.ToString());
                foreach(string rep in l.Nomes)
                Nomes.Add(rep);

            }

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Oradores"; // Default file name
            dlg.DefaultExt = ".ord"; // Default file extension
            dlg.Filter = "Oradores Files (.ord)|*.ord"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;

                System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(filename);
                foreach (string s in Nomes)
                {
                    SaveFile.WriteLine(s);
                }
                salvo = true;
                SaveFile.Close();

            }


        }

        private void Salvar_Button_Click(object sender, RoutedEventArgs e)
        {
            
            salvar_Lista();
        }

        public void carregar_Lista()
        {


            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".ord";
            dlg.Filter = "Oradores Files (.ord)|*.ord";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;

                System.IO.StreamReader ReadFile = new System.IO.StreamReader(filename);

                var entrada = ReadFile.ReadLine();


                while (entrada != null)
                {



                    if (entrada == "-/-")
                    {
                        entrada = ReadFile.ReadLine();
                        criar_aba(entrada);

                    }
                    else
                    {
                        entrada = ReadFile.ReadLine();

                        var selecionado = (TabItem)tabControl.SelectedItem;
                        var lista = (Lista)selecionado.Content;
                        if (entrada != "-/-")
                        {
                            lista.add(entrada);
                            update_JanelaExtra();
                        }
                    }

                }

                ReadFile.Close();

            }


         }


        private void Carregar_Button_Click(object sender, RoutedEventArgs e)
        {
            carregar_Lista();
        }

        //Começo do código do contador, mostrador e timer
        //Seria mais trabalhoso e confuso criar um módulo para organizar 

        private void Play_Button_Click(object sender, RoutedEventArgs e)
        {
            clock_play_pause();
        }

        private void Stop_Button_Click(object sender, RoutedEventArgs e)
        {
            clock_stop();
        }

        private void Reset_Button_Click(object sender, RoutedEventArgs e)
        {
            clock_reset();
        }




        private int tempo = 30, atual;
        private void clock_reset()
        {
            atual = tempo;
            update_Mostrador(atual);
            update_JanelaExtra();

        }
        private void clock_play_pause()
        {
            if (timer.IsEnabled)
            {
                timer.IsEnabled = false;
            }
            else
            {
                timer.IsEnabled = true;
            }

        }
        private void clock_stop()
        {
            timer.IsEnabled = false;
            atual = tempo;
            update_Mostrador(atual);

            //Alterar se acrescentar função para verificar se deve tirar o primeiro
            var selecionado = (TabItem)tabControl.SelectedItem;
            if (selecionado != null)
            {
                var lista = (Lista)selecionado.Content;
                lista.remover(String.Empty, 1);
                update_JanelaExtra();
            }

        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if(atual > 0)
            {
                atual--;
                update_Mostrador(atual);

            }
            else
            {
                timer.IsEnabled = false;
                atual = tempo;
                
                

                clock_stop();
            }

        }

        private void Sair_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void update_Mostrador(int total_seg)
        {
            int seg, min;
            string s_seg, s_min;
        
            min = total_seg / 60;
            seg = total_seg % 60;

            if (seg < 10)
                s_seg = "0" + seg;
            else
                s_seg = Convert.ToString(seg);

            if (min < 10)
                s_min = "0" + min;
            else
                s_min = Convert.ToString(min);

            Mostrador_Label.Content = s_min + ":" + s_seg;

            janela.label.Content = s_min + ":" + s_seg;

        }
    }
}
