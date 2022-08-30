using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidade
{
    public class ModelCompraDeGado
    {
        public int Id { get; set; }
        public string Pecuarista { get; set; }
        public string DataEntrega { get; set; }
        public string ValorTotal { get; set; }
    }
}
