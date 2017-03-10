using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace Digitalclock
{
    /// <summary>
    /// Lógica de interacción para PaisAdd.xaml
    /// </summary>
    public partial class PaisAdd : Window

    {
       
        public PaisAdd()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Pais item = new Pais(Int32.Parse(tb_Horario.Text), tb_Name.Text);
            MainWindow.Paises.Add(item);
            this.Close();

        }
    }
}
