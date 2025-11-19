using ApiContatos.Models;

namespace ApiContatos.Services;

public interface ITelefoneServico
{
    Task<List<Telefone>> ObterPorPessoaAsync(int pessoaId);
    Task<Telefone> CriarAsync(Telefone telefone);
    Task<bool> RemoverAsync(int id);
}
