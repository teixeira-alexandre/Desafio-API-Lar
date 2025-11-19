using ApiContatos.Models;
using ApiContatos.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiContatos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoasController : ControllerBase
{
    private readonly IPessoaServico _pessoaServico;

    public PessoasController(IPessoaServico pessoaServico)
    {
        _pessoaServico = pessoaServico;
    }

    /// <summary>
    /// Lista todas as pessoas cadastradas.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pessoa>>> ObterTodas()
    {
        var pessoas = await _pessoaServico.ObterTodasAsync();
        return Ok(pessoas);
    }

    /// <summary>
    /// Obtém uma pessoa pelo seu ID.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Pessoa>> ObterPorId(int id)
    {
        var pessoa = await _pessoaServico.ObterPorIdAsync(id);
        if (pessoa == null)
            return NotFound();

        return Ok(pessoa);
    }

    /// <summary>
    /// Cadastra uma nova pessoa.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Pessoa>> Criar(Pessoa pessoa)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var criada = await _pessoaServico.CriarAsync(pessoa);
        return CreatedAtAction(nameof(ObterPorId), new { id = criada.Id }, criada);
    }

    /// <summary>
    /// Atualiza os dados de uma pessoa existente.
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, Pessoa pessoa)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var ok = await _pessoaServico.AtualizarAsync(id, pessoa);
        if (!ok)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Remove uma pessoa pelo ID.
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remover(int id)
    {
        var ok = await _pessoaServico.RemoverAsync(id);
        if (!ok)
            return NotFound();

        return NoContent();
    }
}
