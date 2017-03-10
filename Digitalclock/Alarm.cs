using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digitalclock
{
    [Serializable()]
    public class Alarm : System.ComponentModel.INotifyPropertyChanged
    {
        public int aHoras { get; set; }
        public int aMinutos { get; set; }
        public int aSegundos { get; set; }
        

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public Alarm(int h,
                    int m,
                    int s)
        {
            this.aHoras = h;
            this.aMinutos = m;
            this.aSegundos = s;
        }
    }
}
