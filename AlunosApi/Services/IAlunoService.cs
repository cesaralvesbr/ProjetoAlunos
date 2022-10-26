using AlunosApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlunosApi.Services
{
    public interface IAlunoService
    {
        Task<IEnumerable<Aluno>> ObterTodosAlunos();
        Task<Aluno> ObterAluno(int Id);
        Task<IEnumerable<Aluno>> ObterAlunoPorNome(string nome);
        Task CadastrarAluno(Aluno aluno);
        Task AtualizarAluno(Aluno aluno);
        Task DeletarAluno(Aluno aluno);
    }
}
