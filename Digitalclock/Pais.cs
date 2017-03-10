using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digitalclock
{
   
    [Serializable()]
    public class Pais : System.ComponentModel.INotifyPropertyChanged
    {
        public int DiferenciaHoraria { get; set; }
        public String Nombre { get; set; }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public Pais(int h,
                    String m)
        {
            this.DiferenciaHoraria = h;
            this.Nombre = m;
            
        }
    }
   
}
