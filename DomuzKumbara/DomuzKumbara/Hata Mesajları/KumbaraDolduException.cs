using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomuzKumbara.Exception_Library
{
    public class KumbaraDolduException : Exception
    {
        public KumbaraDolduException() : base("Kumbara dolu, parayı atamazsınız!")
        {

        }
    }
}
