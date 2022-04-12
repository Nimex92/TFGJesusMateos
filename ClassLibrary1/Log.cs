using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotec
{
    public class Log
    {
        public int Id { get; set; }
        public string TipoEvento { get; set; }
        public string DescripcionEvento { get; set; }

        public Log()
        {

        }

        public Log(string tipoevento, string descripcionevento)
        {
            this.TipoEvento = tipoevento;
            this.DescripcionEvento=descripcionevento;
        }
    }
}
