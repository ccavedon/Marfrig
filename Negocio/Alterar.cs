using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dados;
using Entidade;

namespace Negocio
{
    public class Alterar
    {
        public Dados.Alterar dCompraDeGado = new Dados.Alterar();
        public Alterar() : base()
        {

        }
        public bool ExcluirCompraDeGado(int idCompraDeGado)
        {
            return dCompraDeGado.ExcluirCompraDeGado(idCompraDeGado);
        }
        public bool ExcluirCompraDeGadoItem(int idCompraDeGado)
        {
            return dCompraDeGado.ExcluirCompraDeGadoItem(idCompraDeGado);
        }
        public bool Adicionar(List<Entidade.CompraDeGado> compraDeGado)
        {
            return dCompraDeGado.Adicionar(compraDeGado);
        }
        public bool Salvar(List<Entidade.CompraDeGado> compraDeGado)
        {
            return dCompraDeGado.Salvar(compraDeGado);
        }
        public bool AtualizarIsPrinted(int idCompraDeGado)
        {
            return dCompraDeGado.AtualizarIsPrinted(idCompraDeGado);
        }
    }
}