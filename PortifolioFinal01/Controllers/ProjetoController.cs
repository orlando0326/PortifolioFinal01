using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioFinal01.Data;
using PortfolioFinal01.DTOs;
using PortfolioFinal01.Model;

namespace PortfolioFinal01.Controllers
{
    using Microsoft.AspNetCore.Http;

    [Tags("Gerenciamento de Projetos")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjetosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Projeto>>> GetProjetos()
        {
            return await _context.Projetos
                .Include(p => p.Aluno)
                .ToListAsync();
        }

    
        [HttpGet("{id}")]
        public async Task<ActionResult<Projeto>> GetProjeto(int id)
        {
            var projeto = await _context.Projetos
                .Include(p => p.Aluno)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (projeto == null)
            {
                return NotFound("Projeto não encontrado.");
            }

            return projeto;
        }

        [HttpPost]
        public async Task<ActionResult<Projeto>> PostProjeto(ProjetoDto projetoDto)
        {
            var aluno = await _context.Alunos.FindAsync(projetoDto.AlunoId);

            if (aluno == null)
            {
                return BadRequest("Aluno não encontrado.");
            }

            var projeto = new Projeto
            {
                Titulo = projetoDto.Titulo,
                Descricao = projetoDto.Descricao,
                Tecnologias = projetoDto.Tecnologias,
                Link = projetoDto.Link,
                Imagem = projetoDto.Imagem,
                AlunoId = projetoDto.AlunoId
            };

            _context.Projetos.Add(projeto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProjeto),
                new { id = projeto.Id }, projeto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjeto(int id, ProjetoDto projetoDto)
        {
            var projeto = await _context.Projetos.FindAsync(id);

            if (projeto == null)
            {
                return NotFound("Projeto não encontrado.");
            }

            var aluno = await _context.Alunos.FindAsync(projetoDto.AlunoId);

            if (aluno == null)
            {
                return BadRequest("Aluno não encontrado.");
            }

            projeto.Titulo = projetoDto.Titulo;
            projeto.Descricao = projetoDto.Descricao;
            projeto.Tecnologias = projetoDto.Tecnologias;
            projeto.Link = projetoDto.Link;
            projeto.Imagem = projetoDto.Imagem;
            projeto.AlunoId = projetoDto.AlunoId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjeto(int id)
        {
            var projeto = await _context.Projetos.FindAsync(id);

            if (projeto == null)
            {
                return NotFound("Projeto não encontrado.");
            }

            _context.Projetos.Remove(projeto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}