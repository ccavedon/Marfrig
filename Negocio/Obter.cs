using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dados;
using Entidade;

namespace Negocio
{
    public class Obter
    {
        public Dados.Obter dCompraDeGado = new Dados.Obter();

        public Obter() : base()
        {

        }
        public List<Entidade.ModelCompraDeGado> ObterCompraDeGado(string idCompraDeGado, string pecuarista, string dataIni, string dataFim)
        {
            return dCompraDeGado.ObterCompraDeGado(idCompraDeGado, pecuarista, dataIni, dataFim);
        }
        public List<Entidade.ModelCompraDeGadoItem> ObterCompraDeGadoItem(int idCompraDeGado)
        {
            return dCompraDeGado.ObterCompraDeGadoItem(idCompraDeGado);
        }
        public List<Entidade.ModelPecuarista> ObterPecuarista()
        {
            return dCompraDeGado.ObterPecuarista();
        }
        public List<Entidade.ModelAnimal> ObterAnimal()
        {
            return dCompraDeGado.ObterAnimal();
        }
    }
}
