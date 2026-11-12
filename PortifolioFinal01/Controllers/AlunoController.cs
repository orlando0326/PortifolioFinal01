using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioFinal01.Data;
using PortfolioFinal01.DTOs;
using PortfolioFinal01.Model;

namespace PortfolioFinal01.Controllers
{
    using Microsoft.AspNetCore.Http;

    [Tags("Cadastro de Alunos")]
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlunosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunos()
        {
            return await _context.Alunos
                .Include(a => a.Turma)
                .Include(a => a.Projetos)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
            var aluno = await _context.Alunos
                .Include(a => a.Turma)
                .Include(a => a.Projetos)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aluno == null)
            {
                return NotFound("Aluno não encontrado.");
            }

            return aluno;
        }


        [HttpPost]
        public async Task<ActionResult<Aluno>> PostAluno(AlunoDto alunoDto)
        {
            var turma = await _context.Turmas.FindAsync(alunoDto.TurmaId);

            if (turma == null)
            {
                return BadRequest("Turma não encontrada.");
            }

            var aluno = new Aluno
            {
                Nome = alunoDto.Nome,
                Email = alunoDto.Email,
                Tecnologias = alunoDto.Tecnologias,
                Certificados = alunoDto.Certificados,
                Experiencias = alunoDto.Experiencias,
                TurmaId = alunoDto.TurmaId
            };

            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAluno),
                new { id = aluno.Id }, aluno);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluno(int id, AlunoDto alunoDto)
        {
            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null)
            {
                return NotFound("Aluno não encontrado.");
            }

            var turma = await _context.Turmas.FindAsync(alunoDto.TurmaId);

            if (turma == null)
            {
                return BadRequest("Turma não encontrada.");
            }

            aluno.Nome = alunoDto.Nome;
            aluno.Email = alunoDto.Email;
            aluno.Tecnologias = alunoDto.Tecnologias;
            aluno.Certificados = alunoDto.Certificados;
            aluno.Experiencias = alunoDto.Experiencias;
            aluno.TurmaId = alunoDto.TurmaId;

            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAluno(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null)
            {
                return NotFound("Aluno não encontrado.");
            }

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}