using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidade
{
    public class ModelCompraDeGadoItem
    {
        public string Descricao { get; set; }
        public string Pedido { get; set; }
        public int Quantidade { get; set; }
        public string Preco { get; set; }
        public string ValorTotal { get; set; }
        public int IdCompraDeGado { get; set; }
        public int IdCompraDeGadoItem { get; set; }
        public int IdPecuarista { get; set; }
        public int IdAnimal { get; set; }
        public string DataEntrega { get; set; }
        public string IsPrinted { get; set; }
    }
}