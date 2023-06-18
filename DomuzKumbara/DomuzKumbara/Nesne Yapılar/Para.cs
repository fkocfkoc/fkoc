using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomuzKumbara.Concrete
{
    public abstract class Para
    {
        public string Ad { get; set; }
        public double Deger { get; set; }
        public abstract double Hacim();
        public double TotalHacim()
        {
            Random rnd = new Random();
            double fazlaRnd = rnd.Next(25, 76);
            double fazlaHacim = (fazlaRnd * Hacim()) / 100;
            return fazlaHacim;
        }
    }
}
