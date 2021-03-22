using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Context;
using Models.Entities;
using Models.Validation;
using Servicos;
using static Models.Validation.WrapperValidation;

namespace LOJA_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private ProdutoServico produtoServico;
        private ProdutoValidation produtoValidation;

        public ProdutoController(LOJAContext context)
        {
            produtoServico = new ProdutoServico(context);
            produtoValidation = new ProdutoValidation();
        }

        [HttpGet]
        public ActionResult Get()
        {
            var Produtos = produtoServico.ObterTodosAtivos().ToList();

            return Ok(Produtos);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var Produto = produtoServico.ObterPorId(id);

            return Ok(Produto);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Produto produto)
        {
            try
            {
                produtoValidation.validate(produto);

                produto = produtoServico.Salvar(produto);

                return StatusCode(StatusCodes.Status201Created, produto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Produto produto)
        {
            try
            {
                if (produto.CodProduto <= 0)
                {
                    return BadRequest(new { message = "Codigo do produto não informado" });
                }

                produtoValidation.validate(produto);

                produto = produtoServico.Atualizar(produto);

                return StatusCode(StatusCodes.Status201Created, produto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var produto = produtoServico.ObterPorId(id);

                if (produto != null)
                {
                    produtoServico.Deletar(produto);
                }
                else
                {
                    return BadRequest(new { message = "Produto não encontrado" });
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
