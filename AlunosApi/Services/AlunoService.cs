using AlunosApi.Context;
using AlunosApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlunosApi.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly AppDbContext _context;

        public AlunoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Aluno> ObterAluno(int Id)
        {
            return await _context.Alunos.FindAsync(Id);
        }

        public async Task<IEnumerable<Aluno>> ObterTodosAlunos()
        {
            try
            {
                return await _context.Alunos.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Aluno>> ObterAlunoPorNome(string nome)
        {
            IEnumerable<Aluno> alunos;

            if (!string.IsNullOrEmpty(nome))
            {
                alunos = await _context.Alunos.Where(x => x.Nome.Contains(nome)).ToListAsync();
            }
            else
            {
                alunos = await ObterTodosAlunos();
            }
            return alunos;
        }
        public async Task CadastrarAluno(Aluno aluno)
        {

            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();
        }
        public async Task AtualizarAluno(Aluno aluno)
        {
            _context.Entry(aluno).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletarAluno(Aluno aluno)
        {
            _context.Remove(aluno);
           await _context.SaveChangesAsync();
        }
    }
}
