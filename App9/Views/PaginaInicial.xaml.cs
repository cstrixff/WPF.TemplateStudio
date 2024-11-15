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

namespace App9.Views
{
    /// <summary>
    /// Interação lógica para PaginaInicial.xam
    /// </summary>
    public partial class PaginaInicial : Page
    {
        public PaginaInicial()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Disciplina oselecionado = ((Disciplina)datagridDisci.SelectedItem);
            if (oselecionado != null)
            {
                SegundaPagina nova = new SegundaPagina(oselecionado);

                this.NavigationService.Navigate(nova);
            }
            else
            {   
                MessageBox.Show("Selecione uma disciplina");
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            datagridDisci.ItemsSource = Dados.Disciplinas();
        }
    }
}
