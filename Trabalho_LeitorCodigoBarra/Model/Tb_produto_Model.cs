using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_LeitorCodigoBarra.Model
{
    public class Tb_produto_Model
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string nome { get; set; }
        
        public Tb_produto_Model()
        {
            id = 0;
            codigo = string.Empty;
            nome = string.Empty;
        }
    }
}
