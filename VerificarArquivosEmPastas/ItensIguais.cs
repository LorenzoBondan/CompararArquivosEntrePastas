using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerificarArquivosEmPastas
{
    class ItensIguais
    {
        public string Nome { get; set; }
        public string Data { get; set; }

        public ItensIguais()
        {

        }
        public ItensIguais(string nome, string data)
        {
            Nome = nome;
            Data = data;
        }
    }
}
