namespace PortfolioFinal01.Model
{
    public class Projeto
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public string Tecnologias { get; set; }

        public string Link { get; set; }

        public string Imagem { get; set; }

        public int AlunoId { get; set; }

        public Aluno Aluno { get; set; }
    }
}