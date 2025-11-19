using ApiContatos.Data;
using ApiContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiContatos.Services;

public class PessoaServico : IPessoaServico
{
    private readonly AppDbContext _contexto;

    public PessoaServico(AppDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<List<Pessoa>> ObterTodasAsync()
    {
        return await _contexto.Pessoas.Include(p => p.Telefones).ToListAsync();
    }

    public async Task<Pessoa?> ObterPorIdAsync(int id)
    {
        return await _contexto
            .Pessoas.Include(p => p.Telefones)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Pessoa> CriarAsync(Pessoa pessoa)
    {
        _contexto.Pessoas.Add(pessoa);
        await _contexto.SaveChangesAsync();
        return pessoa;
    }

    public async Task<bool> AtualizarAsync(int id, Pessoa pessoa)
    {
        var existente = await _contexto
            .Pessoas.Include(p => p.Telefones)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (existente == null)
            return false;

        existente.Nome = pessoa.Nome;
        existente.Email = pessoa.Email;
        existente.DataNascimento = pessoa.DataNascimento;

        await _contexto.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoverAsync(int id)
    {
        var existente = await _contexto.Pessoas.FindAsync(id);
        if (existente == null)
            return false;

        _contexto.Pessoas.Remove(existente);
        await _contexto.SaveChangesAsync();
        return true;
    }
}
