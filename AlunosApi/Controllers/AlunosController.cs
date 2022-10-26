using AlunosApi.Models;
using AlunosApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlunosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class AlunosController : ControllerBase
    {
        private IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> ObterAlunos()
        {
            try
            {
                var alunos = await _alunoService.ObterTodosAlunos();
                return Ok(alunos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter Alunos");
            }
        }

        [HttpGet("AlunosPorNome")]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> ObterAlunosPorNome([FromQuery] string nome)
        {
            try
            {
                var alunos = await _alunoService.ObterAlunoPorNome(nome);

                if (alunos.Count() == 0)
                    return NotFound($"Não existe alunos com o critério {nome}");

                return Ok(alunos);
            }
            catch (Exception)
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpGet("{id:int}", Name = "ObterAluno")]
        public async Task<ActionResult<Aluno>> ObterAlunoPorId(int id)
        {
            try
            {
                var aluno = await _alunoService.ObterAluno(id);

                if (aluno == null)
                    return NotFound($"Não existe aluno com o Id {id}");

                return Ok(aluno);
            }
            catch (Exception)
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Aluno>> CadastrarAluno(Aluno aluno)
        {
            try
            {
                await _alunoService.CadastrarAluno(aluno);
                return CreatedAtAction(nameof(CadastrarAluno), new { id = aluno.Id }, aluno);
            }
            catch (Exception)
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Aluno>> AtualizarAluno(int id, [FromBody] Aluno aluno)
        {
            try
            {
                if (aluno.Id == id)
                {
                    await _alunoService.AtualizarAluno(aluno);
                    return Ok($"Aluno com id {id} foi atualizado com sucesso");
                }
                else
                {
                    return BadRequest("Dados inconsistentes");
                }
            }
            catch (Exception)
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Aluno>> DeletarAluno(int id)
        {
            try
            {
                var aluno = await _alunoService.ObterAluno(id);
                if (aluno == null)
                    return NotFound($"Não existe aluno com o Id {id} para excluir");

                await _alunoService.DeletarAluno(aluno);
                return Ok($"Aluno com Id {id} excluído com sucesso!");

            }
            catch (Exception)
            {
                return BadRequest("Request inválido");
            }
        }
    }
}
