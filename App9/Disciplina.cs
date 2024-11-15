using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App9
{
     public class Disciplina
    {
        int id;
        string nome;
        string professor;
        double horas;

        public Disciplina(int id, string nome, string professor, double horas)
        {
            Id = id;
            Nome = nome;
            Professor = professor;
            Horas = horas;
        }

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Professor { get => professor; set => professor = value; }
        public double Horas { get => horas; set => horas = value; }
    }
}
