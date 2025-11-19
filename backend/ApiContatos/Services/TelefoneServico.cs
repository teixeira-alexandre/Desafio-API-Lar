using ApiContatos.Data;
using ApiContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiContatos.Services;

public class TelefoneServico : ITelefoneServico
{
    private readonly AppDbContext _contexto;

    public TelefoneServico(AppDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<List<Telefone>> ObterPorPessoaAsync(int pessoaId)
    {
        return await _contexto.Telefones
            .Where(t => t.PessoaId == pessoaId)
            .ToListAsync();
    }

    public async Task<Telefone> CriarAsync(Telefone telefone)
    {
        _contexto.Telefones.Add(telefone);
        await _contexto.SaveChangesAsync();
        return telefone;
    }

    public async Task<bool> RemoverAsync(int id)
    {
        var existente = await _contexto.Telefones.FindAsync(id);
        if (existente == null)
            return false;

        _contexto.Telefones.Remove(existente);
        await _contexto.SaveChangesAsync();
        return true;
    }
}
