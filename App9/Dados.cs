using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Globalization;




namespace App9
{
    static class Dados
    {
        //============================================================================LIGACAO=========================================================================================
        public static string conString = "SERVER=127.0.0.1; DATABASE=bd_disciplinas; UID=root; PWD=mysql; PORT=3306;";
        public static MySqlConnection conn = null;
   

        private static void AbrirLigacao()
        {
            try
            {
                conn = new MySqlConnection(conString);
                conn.Open();
                //MessageBox.Show("Ligação estabelecida com sucesso");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro na ligação" + ex);
            }
        }

        private static void FecharLigacao()
        {
            try
            {

                conn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro na ligação" + ex);
            }
        }

        public static void CriarDisciplina(string nome, string prof, double horas)
        {
            AbrirLigacao();

            var querysql = "INSERT INTO `disciplina` (`nome`, `professor`, `horas`) VALUES (@nome, @prof, @horas)";


            MySqlCommand cmd = new MySqlCommand(querysql, conn);

            
            cmd.Parameters.AddWithValue("@nome",nome);
            cmd.Parameters.AddWithValue("@prof",prof);
            cmd.Parameters.AddWithValue("@horas",horas);

            



            cmd.ExecuteNonQuery();

            FecharLigacao();

        }

        public static List<Disciplina> Disciplinas()
        {

            AbrirLigacao();  //conn

            List<Disciplina> listadisciplinas = new List<Disciplina>();

            string querysql = "SELECT * FROM disciplina;";

            MySqlCommand cmd = new MySqlCommand(querysql, conn);
            MySqlDataReader leitor = null;

            leitor = cmd.ExecuteReader();

            while (leitor.Read())
            {
                Disciplina adisciplina = new Disciplina(Convert.ToInt32(leitor["id"]), leitor["nome"].ToString(), leitor["professor"].ToString(), Convert.ToDouble(leitor["horas"]));
                listadisciplinas.Add(adisciplina);
            }
            return listadisciplinas;

          
        }


        public static void AlunoInserir(int iddisciplina, int idaluno)
        {
            AbrirLigacao();

            var querysql = "INSERT INTO alunos_disciplina (iddisciplina, idaluno) VALUES (@iddisciplina, @idaluno)";

            MySqlCommand cmd = new MySqlCommand(querysql, conn);

            cmd.Parameters.AddWithValue("iddisciplina", iddisciplina);
            cmd.Parameters.AddWithValue("idaluno", idaluno);


            cmd.ExecuteNonQuery();

            FecharLigacao();

        }
        public static void AlunoDeletar(int iddisciplina, int idaluno)
        {
            AbrirLigacao();

            var querysql = "DELETE FROM bd_disciplinas.alunos_disciplina WHERE(iddisciplina = @iddisciplina) and(idaluno = @idaluno);";

            MySqlCommand cmd = new MySqlCommand(querysql, conn);

            cmd.Parameters.AddWithValue("iddisciplina", iddisciplina);
            cmd.Parameters.AddWithValue("idaluno", idaluno);


            cmd.ExecuteNonQuery();

            FecharLigacao();

        }
        public static List<Aluno> AlunosDaDisciplina(int iddisciplina)
        {

            AbrirLigacao();  //conn

            List<Aluno> lista = new List<Aluno>();

            string querysql = "select * from alunos_disciplina left join aluno on alunos_disciplina.idaluno=aluno.id where iddisciplina = @iddisciplina;";

            MySqlCommand cmd = new MySqlCommand(querysql, conn);
            cmd.Parameters.AddWithValue("@iddisciplina", iddisciplina);

            MySqlDataReader leitor = null;

            leitor = cmd.ExecuteReader();

            while (leitor.Read())
            {
                Aluno oAluno = new Aluno(Convert.ToInt32(leitor["id"]), leitor["nome"].ToString());
                lista.Add(oAluno);
            }
            return lista;
        }
        public static List<Aluno> AlunosNaoNaDisciplina(int iddisciplina)
        {

            AbrirLigacao();  //conn

            List<Aluno> lista = new List<Aluno>();

            string querysql = "select * from aluno where id not in (select idaluno from alunos_disciplina where iddisciplina = @iddisciplina ) ORDER BY nome ASC";

            MySqlCommand cmd = new MySqlCommand(querysql, conn);
            cmd.Parameters.AddWithValue("@iddisciplina", iddisciplina);

            MySqlDataReader leitor = null;

            leitor = cmd.ExecuteReader();

            while (leitor.Read())
            {
                Aluno oAluno = new Aluno(Convert.ToInt32(leitor["id"]), leitor["nome"].ToString());
                lista.Add(oAluno);
            }
            return lista;
        }
        public static List<Aluno> PesquisarAluno(int iddisciplina, string textoprocura)
        {

            AbrirLigacao();  //conn

            List<Aluno> lista = new List<Aluno>();

            string querysql = "select * from aluno where id not in (select idaluno from alunos_disciplina where iddisciplina = @iddisciplina ) and nome like @texto ORDER BY nome ASC";

            MySqlCommand cmd = new MySqlCommand(querysql, conn);
            cmd.Parameters.AddWithValue("@iddisciplina", iddisciplina);
            cmd.Parameters.AddWithValue("@texto", "%"+textoprocura+"%");
            MySqlDataReader leitor = null;

            leitor = cmd.ExecuteReader();

            while (leitor.Read())
            {
                Aluno oAluno = new Aluno(Convert.ToInt32(leitor["id"]), leitor["nome"].ToString());
                lista.Add(oAluno);
            }
            return lista;
        }

        public static void InserirAluno(int iddaDisciplinaApagar)
        {
            AbrirLigacao();

            var querysql = "INSERT INTO bd_disciplinas.alunos_disciplina (@iddisciplina, @idaluno);";

            MySqlCommand cmd = new MySqlCommand(querysql, conn);

            cmd.Parameters.AddWithValue("@id", iddaDisciplinaApagar);


            cmd.ExecuteNonQuery();

            FecharLigacao();

        }

        public static void AtualizarDisciplina(Disciplina adisciplina)
        {
            AbrirLigacao();

            var querysql = "UPDATE `disciplina` SET nome= @nome, professor=@prof, horas=@horas WHERE id=@id";
            ;

            MySqlCommand cmd = new MySqlCommand(querysql, conn);

            cmd.Parameters.AddWithValue("@id", adisciplina.Id);
            cmd.Parameters.AddWithValue("@nome", adisciplina.Nome);
            cmd.Parameters.AddWithValue("@prof", adisciplina.Professor);
            cmd.Parameters.AddWithValue("@horas", adisciplina.Horas);



            cmd.ExecuteNonQuery();

            FecharLigacao();

        }

        public static List<Disciplina> PesquisarProfessor(string professor)
        {

            AbrirLigacao();  //conn

            List<Disciplina> lista = new List<Disciplina>();

            string querysql = "SELECT * FROM disciplina WHERE professor LIKE @pesquisa";

            MySqlCommand cmd = new MySqlCommand(querysql, conn);

            cmd.Parameters.AddWithValue("@pesquisa", "%" + professor + "%");

            MySqlDataReader leitor = null;

            leitor = cmd.ExecuteReader();

            while (leitor.Read())
            {
                Disciplina adisciplina = new Disciplina(Convert.ToInt32(leitor["id"]), leitor["nome"].ToString(), leitor["professor"].ToString(), Convert.ToDouble(leitor["horas"]));
                lista.Add(adisciplina);
            }
            return lista;
        }

        public static List<Disciplina> PesquisarDisciplina(string nome)
        {

            AbrirLigacao();  //conn

            List<Disciplina> listadisciplina = new List<Disciplina>();

            string querysql = "SELECT * FROM disciplina WHERE nome LIKE @procurar";

            MySqlCommand cmd = new MySqlCommand(querysql, conn);

            cmd.Parameters.AddWithValue("@procurar", "%" + nome + "%");

            MySqlDataReader leitor = null;

            leitor = cmd.ExecuteReader();

            while (leitor.Read())
            {
                Disciplina adisciplina = new Disciplina(Convert.ToInt32(leitor["id"]), leitor["nome"].ToString(), leitor["professor"].ToString(), Convert.ToDouble(leitor["horas"]));
                listadisciplina.Add(adisciplina);
            }
            return listadisciplina;
        }

        //==========================================================================================================================================================
        /* public static List<Imovel> Imoveis()
         {

             AbrirLigacao();  //conn

             List<Imovel> listaimoveis = new List<Imovel>();

             string querysql = "SELECT imovel.*, localidade.nome as nomelocalidade FROM imovel left join localidade on imovel.idlocalidade = localidade.id";

             MySqlCommand cmd = new MySqlCommand(querysql, conn);
             MySqlDataReader leitor = null;

             leitor = cmd.ExecuteReader();

             while (leitor.Read())
             {
                 Imovel oimovel = new Imovel(Convert.ToInt32(leitor["id"]),leitor["nome"].ToString(),Convert.ToDouble(leitor["preco"]),  Convert.ToInt32(leitor["idlocalidade"]), leitor["nomelocalidade"].ToString());
                 listaimoveis.Add(oimovel);   
             }
             return listaimoveis;
         }









         public static void CriarImovel(Imovel oImovel)
         {
             AbrirLigacao();

             var querysql = "INSERT INTO imovel (nome, preco,idlocalidade) VALUES(@nome, @preco, @idlocalidade)";


             MySqlCommand cmd = new MySqlCommand(querysql, conn);

             cmd.Parameters.AddWithValue("@id", oImovel.Id);
             cmd.Parameters.AddWithValue("@nome", oImovel.Nome);
             cmd.Parameters.AddWithValue("@preco", oImovel.Preco);

             cmd.Parameters.AddWithValue("@idlocalidade", oImovel.Idlocalidade);



             cmd.ExecuteNonQuery();

             FecharLigacao();

         }
         public static void AtualizarImovel(Imovel oImovel)
         {
             AbrirLigacao();

             var querysql = "UPDATE imovel SET nome = @nome, preco = @preco, idlocalidade = @idlocalidade Where id = @id ";

             MySqlCommand cmd = new MySqlCommand(querysql, conn);

             cmd.Parameters.AddWithValue("@id", oImovel.Id);
             cmd.Parameters.AddWithValue("@nome", oImovel.Nome);
             cmd.Parameters.AddWithValue("@preco", oImovel.Preco);
             cmd.Parameters.AddWithValue("@idlocalidade", oImovel.Idlocalidade);



             cmd.ExecuteNonQuery();

             FecharLigacao();

         }
         public static void ApagarImovel(int idoImovelApagar)
         {
             AbrirLigacao();

             var querysql = "DELETE from imovel WHERE id = @id ";

             MySqlCommand cmd = new MySqlCommand(querysql, conn);

             cmd.Parameters.AddWithValue("@id", idoImovelApagar);


             cmd.ExecuteNonQuery();

             FecharLigacao();

         }
    */
    }


}
       