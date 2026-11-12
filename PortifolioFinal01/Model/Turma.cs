namespace PortfolioFinal01.Model
{
    public class Turma
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public List<Aluno> Alunos { get; set; }
    }
}