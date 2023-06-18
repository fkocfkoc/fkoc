using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomuzKumbara.Exception_Library
{
    public class KumbaraBosException : Exception
    {
        public KumbaraBosException() : base("Kumbarada para yok!")
        {

        }
    }
}
