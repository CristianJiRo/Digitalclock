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

namespace Digitalclock
{
    /// <summary>
    /// Lógica de interacción para PaisDel.xaml
    /// </summary>
    public partial class PaisDel : Window
    {
        
        public string nombre = string.Empty;
        public PaisDel ()
        {

            InitializeComponent();

            //Llenamos el ComboBoxcon el nombre de los paises;
            foreach (var Pais in MainWindow.Paises)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content=Pais.Nombre;
                cb_PaisesList.Items.Add(item);

            }
      
            
        }

        private void Cancelar2_Click(object sender, RoutedEventArgs e)
        {

            //MainWindow.Paises

            this.Close();         

        }

        private void bt_Del_Click(object sender, RoutedEventArgs e)
        {
            //Si la seleccion en el combobox esta dentro del contenido de la lista la borramos.
            if (cb_PaisesList.SelectedIndex >-1 && cb_PaisesList.SelectedIndex < MainWindow.Paises.Count) 
            MainWindow.Paises.RemoveAt(cb_PaisesList.SelectedIndex);
            this.Close();
            
        }
    }
}
