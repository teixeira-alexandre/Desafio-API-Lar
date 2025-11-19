using ApiContatos.Models;
using ApiContatos.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiContatos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TelefonesController : ControllerBase
{
    private readonly ITelefoneServico _telefoneServico;

    public TelefonesController(ITelefoneServico telefoneServico)
    {
        _telefoneServico = telefoneServico;
    }

    /// <summary>
    /// Lista os telefones de uma pessoa.
    /// </summary>
    [HttpGet("pessoa/{pessoaId:int}")]
    public async Task<ActionResult<IEnumerable<Telefone>>> ObterPorPessoa(int pessoaId)
    {
        var telefones = await _telefoneServico.ObterPorPessoaAsync(pessoaId);
        return Ok(telefones);
    }

    /// <summary>
    /// Cadastra um telefone para uma pessoa.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Telefone>> Criar(Telefone telefone)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var criado = await _telefoneServico.CriarAsync(telefone);
        return CreatedAtAction(nameof(ObterPorPessoa), new { pessoaId = criado.PessoaId }, criado);
    }

    /// <summary>
    /// Remove um telefone pelo ID.
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remover(int id)
    {
        var ok = await _telefoneServico.RemoverAsync(id);
        if (!ok)
            return NotFound();

        return NoContent();
    }
}
