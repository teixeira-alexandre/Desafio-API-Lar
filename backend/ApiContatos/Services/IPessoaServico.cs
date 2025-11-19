using ApiContatos.Models;

namespace ApiContatos.Services;

public interface IPessoaServico
{
    Task<List<Pessoa>> ObterTodasAsync();
    Task<Pessoa?> ObterPorIdAsync(int id);
    Task<Pessoa> CriarAsync(Pessoa pessoa);
    Task<bool> AtualizarAsync(int id, Pessoa pessoa);
    Task<bool> RemoverAsync(int id);
}
