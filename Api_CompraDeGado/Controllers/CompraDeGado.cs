using Microsoft.AspNetCore.Mvc;
using Entidade;
using Negocio;
using Microsoft.AspNetCore.Authorization;

namespace Api_CompraDeGado.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompraDeGado : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        private readonly ILogger<CompraDeGado> _logger;

        Obter oObter = new Obter();
        Alterar oAlterar = new Alterar();

        public CompraDeGado(ILogger<CompraDeGado> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("ObterCompraDeGado")]
        public List<Entidade.ModelCompraDeGado> ObterCompraDeGado(string? idCompraDeGado, string? idPecuarista, string? dataIni, string? dataFim)
        {
            return oObter.ObterCompraDeGado(idCompraDeGado, idPecuarista, dataIni, dataFim);
        }

        [HttpGet]
        [Route("ObterCompraDeGadoItem")]
        public List<Entidade.ModelCompraDeGadoItem> ObterCompraDeGadoItem(int idCompraDeGado)
        {
            return oObter.ObterCompraDeGadoItem(idCompraDeGado);
        }

        [HttpGet]
        [Route("ObterPecuarista")]
        public List<Entidade.ModelPecuarista> ObterPecuarista()
        {
            return oObter.ObterPecuarista();
        }

        [HttpGet]
        [Route("ObterAnimal")]
        public List<Entidade.ModelAnimal> ObterAnimal()
        {
            return oObter.ObterAnimal();
        }

        [HttpDelete]
        [Route("ExcluirCompraDeGado")]
        public bool ExcluirCompraDeGado(int idCompraDeGado)
        {
            return oAlterar.ExcluirCompraDeGado(idCompraDeGado);
        }

        [HttpDelete]
        [Route("ExcluirCompraDeGadoItem")]
        public bool ExcluirCompraDeGadoItem(int idCompraDeGadoItem)
        {
            return oAlterar.ExcluirCompraDeGadoItem(idCompraDeGadoItem);
        }

        [HttpPost("Adicionar")]
        public bool Adicionar([FromBody] List<Entidade.CompraDeGado> compraDeGado)
        {            
            return oAlterar.Adicionar(compraDeGado);
        }

        [HttpPost("Salvar")]
        public bool Salvar([FromBody] List<Entidade.CompraDeGado> compraDeGado)
        {
            return oAlterar.Salvar(compraDeGado);
        }

        [HttpPost("AtualizarIsPrinted")]
        public bool AtualizarIsPrinted([FromBody] int idCompraDeGado)
        {
            return oAlterar.AtualizarIsPrinted(idCompraDeGado);
        }
    }
}