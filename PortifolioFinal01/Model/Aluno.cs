using PortfolioFinal01.Model;



namespace PortfolioFinal01.Model
{
    public class Aluno
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Tecnologias { get; set; }

        public string Certificados { get; set; }

        public string Experiencias { get; set; }

        public int TurmaId { get; set; }

        public Turma Turma { get; set; }

        public List<Projeto> Projetos { get; set; }
    }
}