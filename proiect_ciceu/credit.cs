using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect_ciceu
{
    class credit
    {
        protected long _suma; //campuri, contin valorile propriuzise
        protected float _dobanda;
        protected int _durata;
        public long Suma //proprietatea nu are paranteze
        {
            set { _suma = value; }
            get { return _suma; }
        }
        public float Dobanda//proprietatea nu are paranteze
        {
            set { _dobanda = value; }
            get { return _dobanda; }
        }
        public int Durata//proprietatea nu are paranteze
        {
            set { _durata= value; }
            get { return _durata; }
        }
        public credit() //constructor implicit
        {
            _suma = 0;
            _durata = 0;
            _dobanda = 0;
        }
       
        public string Luna() //metodele au paranteze
        {
            return Convert.ToString((int)(((_dobanda / 100 * _suma)/12 +_suma/ _durata)*100)/100.0); 
        }
        public string Total()
        {
            return Convert.ToString((int)(((_dobanda / 100 * _suma)/12*_durata+_suma)*100)/100.0);
        }
        
    }
}
