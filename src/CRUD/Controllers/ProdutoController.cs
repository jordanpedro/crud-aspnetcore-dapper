using CRUD.Domain;
using CRUD.Dto;
using CRUD.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController: ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;
        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var retorno = await _produtoRepository.GetAsync(id);
            return retorno != null ?  Ok(retorno) : BadRequest();
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync()
        {
            var retorno = await _produtoRepository.GetAllAsync();
            return retorno?.Count > 0 ? Ok(retorno) : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var retorno = await _produtoRepository.DeleteAsync(id);

            if (retorno)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, ProdutoRequest produtoRequest)
        {
            Produto produto = new Produto() { Id = id, Nome = produtoRequest.Nome };
            var retorno = await _produtoRepository.UpdateAsync(produto);

            if (retorno)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost()]
        public async Task<IActionResult> PostAsync(ProdutoRequest produtoRequest)
        {
            Produto produto = new Produto() { Nome = produtoRequest.Nome };
            var retorno = await _produtoRepository.CreateAsync(produto);
            if (retorno)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
