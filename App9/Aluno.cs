using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App9
{
    internal class Aluno
    {
        int id;
        string nome;

        public Aluno(int id, string nome)
        {
            IdAlunos = id;
            Nome = nome;
        }

        public int IdAlunos { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
    }
}
