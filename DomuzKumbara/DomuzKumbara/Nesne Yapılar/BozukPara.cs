using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomuzKumbara.Concrete
{
    public class BozukPara : Para
    {
        public const double Pi = 3.14;
        public double Cap { get; set; }
        public double Yukseklik { get; set; }
        public override double Hacim()
        {
            return Yukseklik * (Cap / 2) * (Cap / 2) * Pi;
        }
        public double FazladanHacim(double hacim)
        {
            double bosluk;
            Random rnd = new Random();
            return bosluk = hacim * rnd.Next(25, 75) * 0.01;
        }
        public override string ToString()
        {
            return $"{Ad}";
        }
    }
}
