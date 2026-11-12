using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioFinal01.Data;
using PortfolioFinal01.DTOs;
using PortfolioFinal01.Model;

namespace PortfolioFinal01.Controllers
{
    using Microsoft.AspNetCore.Http;

    [Tags("Gerenciamento de Turmas")]
    [Route("api/[controller]")]
    [ApiController]
    public class TurmasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TurmasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Turma>>> GetTurmas()
        {
            return await _context.Turmas.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Turma>> GetTurma(int id)
        {
            var turma = await _context.Turmas.FindAsync(id);

            if (turma == null)
            {
                return NotFound("Turma não encontrada.");
            }

            return turma;
        }


        [HttpPost]
        public async Task<ActionResult<Turma>> PostTurma(TurmaDto turmaDto)
        {
            var turma = new Turma
            {
                Nome = turmaDto.Nome,
                Descricao = turmaDto.Descricao
            };

            _context.Turmas.Add(turma);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTurma),
                new { id = turma.Id }, turma);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutTurma(int id, TurmaDto turmaDto)
        {
            var turma = await _context.Turmas.FindAsync(id);

            if (turma == null)
            {
                return NotFound("Turma não encontrada.");
            }

            turma.Nome = turmaDto.Nome;
            turma.Descricao = turmaDto.Descricao;

            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTurma(int id)
        {
            var turma = await _context.Turmas.FindAsync(id);

            if (turma == null)
            {
                return NotFound("Turma não encontrada.");
            }

            _context.Turmas.Remove(turma);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}