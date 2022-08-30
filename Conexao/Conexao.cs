using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados.Conexao
{
    public class Conexao
    { 
        public string ObterConexao()
        {
            return @"Data Source=DESKTOP-43V2TLN;Initial Catalog=CompraDeGadoTeste;Integrated Security=True";
        }
    }
}
