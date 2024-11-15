using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace App9
{
    /// <summary>
    /// Interação lógica para SegundaPagina.xam
    /// </summary>
    public partial class SegundaPagina : Page
    {
        Disciplina minhaDisciplina;
        public SegundaPagina(Disciplina aDisciplina)
        {
            InitializeComponent();
            minhaDisciplina = aDisciplina;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PaginaInicial nova = new PaginaInicial();

            this.NavigationService.Navigate(nova);
        }

        private void AtualizarDatagrids()
        {
            datagridAlunosDisponiveis.ItemsSource = Dados.AlunosNaoNaDisciplina(minhaDisciplina.Id); 
            datagridAlunosDisciplina.ItemsSource = Dados.AlunosDaDisciplina(minhaDisciplina.Id);
        }  

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txtboxNomeSegunda.Text = minhaDisciplina.Nome;
            txtboxProfessorSegunda.Text = minhaDisciplina.Professor;
            txtboxHorasSegunda.Text = minhaDisciplina.Horas.ToString();

            AtualizarDatagrids();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Aluno idaluno = datagridAlunosDisciplina.SelectedItem as Aluno; ;
            int iddisciplina = minhaDisciplina.Id;
            
            Dados.AlunoDeletar(minhaDisciplina.Id, idaluno.IdAlunos);
            AtualizarDatagrids();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Aluno idaluno = datagridAlunosDisponiveis.SelectedItem as Aluno; ;
            int iddisciplina = minhaDisciplina.Id;

            Dados.AlunoInserir(minhaDisciplina.Id, idaluno.IdAlunos);

            AtualizarDatagrids();
        }

        private void btnPesquisar_Click(object sender, RoutedEventArgs e)
        {
            string nome = txtboxPesquisar.Text;
            int iddisciplina = minhaDisciplina.Id;

            datagridAlunosDisponiveis.ItemsSource = Dados.PesquisarAluno(iddisciplina, nome);
        }
    }
}
