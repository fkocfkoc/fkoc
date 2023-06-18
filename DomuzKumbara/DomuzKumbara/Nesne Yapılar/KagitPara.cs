using DomuzKumbara.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomuzKumbara.Concrete
{
    public class KagitPara : Para, IKatla
    {
        public double En { get; set; }
        public double Boy { get; set; }
        public double Yukseklik { get; set; } = 0.25;
        public override double Hacim()
        {
            return En * Boy * Yukseklik;
        }
        public double FazladanHacim(double hacim)
        {
            double bosluk;
            Random rnd = new Random();
            return bosluk = hacim * rnd.Next(25, 75) * 0.01;
        }
        public double Katla(double kagitParaHacim)
        {
            return (En / 4) * Boy * (Yukseklik * 4);
        }
        public override string ToString()
        {
            return $"{Ad}";
        }
    }
}
