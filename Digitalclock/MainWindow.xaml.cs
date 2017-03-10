using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Media;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Digitalclock
{
   
    public partial class MainWindow : Window
       
    {
        public static List<Pais> Paises = new List<Pais>();
        String alarmSet;
        int alarmH = 00;
        int alarmM = 00;
        int alarmS = 00;

        const string FileName = "SavedAlarm.bin";
        public static string FileNamePais = "SavedPais.bin";

        public MainWindow()
        {

            //Comprobamos si existe el fichero con la lista de Paises.
            if (File.Exists(FileNamePais))
            {
                Stream FileStream2 = File.OpenRead(FileNamePais);
                BinaryFormatter deserializer = new BinaryFormatter();
                Paises = (List<Pais>)deserializer.Deserialize(FileStream2);
                FileStream2.Close();

            }

            //comprobamos si existe el fichero con la alarma
            if (File.Exists(FileName))
            {
                Stream FileStream = File.OpenRead(FileName);
                BinaryFormatter deserializer = new BinaryFormatter();
                
                Alarm alarmSaved = (Alarm)deserializer.Deserialize(FileStream);
                FileStream.Close();

                alarmH = alarmSaved.aHoras;
                alarmM = alarmSaved.aMinutos;
                alarmS = alarmSaved.aSegundos;
            }
           
            InitializeComponent();

            clock.Content = DateTime.Now.ToLongTimeString();

            DispatcherTimer time = new DispatcherTimer();
            time.Interval = TimeSpan.FromSeconds(1);
            time.Tick += Time_Tick;
            time.Start();
                                
            //Valores de la alarma por defecto.
            Horas.SelectedIndex = alarmH;
            Minutos.SelectedIndex = alarmM;
            Segundos.SelectedIndex = alarmS;

           
            //Llenamos el Combobox con los Paises.
            populateCB();

        }

        //Metodo para llenar el Combobox con los paises.
        public void populateCB()
        {
            cb_pais.Items.Clear();
            foreach (Pais Pais in MainWindow.Paises)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = Pais.Nombre+" ("+Pais.DiferenciaHoraria+")";
                cb_pais.Items.Add(item);

            }
        }

        private void Time_Tick(object sender, EventArgs e)
        {

            //Ponemos la hora en el label.
            clock.Content = DateTime.Now.ToLongTimeString();

            //Ponemso la hora secundaria.
            int dif = 0;
            if (cb_pais.SelectedIndex > -1 && cb_pais.SelectedIndex < MainWindow.Paises.Count)
                dif = Paises[cb_pais.SelectedIndex].DiferenciaHoraria;
            RelojPais.Content = DateTime.Now.AddHours(dif).ToLongTimeString();

            //Vamos volcando el valor seleccionado en la alarma en un String
            alarmSet = "";
            alarmSet = alarmSet + Horas.Text + ":" + Minutos.Text + ":" + Segundos.Text;

            //Si la hora coincide con la alarma y ademas está activada abre un cuadro de dialogo.
            if (DateTime.Now.ToLongTimeString().Equals(alarmSet) && (alarmAct.IsChecked))
            {

                SystemSounds.Exclamation.Play();
                MessageBox.Show("¡¡¡¡¡¡Alarma!!!!!!");
                
            }

            //si cambian los valores en algún comboBox modificamos el fichero que guarda la alarma.
            if (!(Horas.Text).Equals(alarmH.ToString()) || !(Minutos.Text).Equals(alarmM.ToString()) || !(Segundos.Text).Equals(alarmS.ToString()))
            {

                Alarm alarm = new Alarm(Int32.Parse(Horas.Text), Int32.Parse(Minutos.Text), Int32.Parse(Segundos.Text));

                if (File.Exists(FileName))

                {
                    File.Delete(FileName);
                }

                Stream FileStream = File.Create(FileName);
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(FileStream, alarm);
                FileStream.Close();
                
            }
                       

        }

        //Salir de la aplicacion.
        private void sortir_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        //Informacion de la apliacion en un cuadro de dialogo.
        private void sobre_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Fet Per: Cristian Jimenez Rodriguez");
        }

        //Guardar el fichero con la lista de paises
       

        //Añadir un Pais.
        private void add_Click(object sender, RoutedEventArgs e)
        {
            PaisAdd window = new PaisAdd();
            window.ShowDialog();
            populateCB();
            savePaises();
            
        }

        //Eliminar un Pais.
        private void delete_Click(object sender, RoutedEventArgs e)
        {

            PaisDel window2 = new PaisDel();
            window2.ShowDialog();
            populateCB();
            savePaises();

        }

        //Guardar documento con la lista 
        private void savePaises()
        {

            if (File.Exists(FileNamePais))
            {
                File.Delete(FileNamePais);

            }
                        
            Serialize(Paises, FileNamePais);
           

        }

        public void Serialize(List<Pais> list, string filePath)
         {
             using (Stream stream = File.OpenWrite(filePath))
             {
                 var formatter = new BinaryFormatter();
                 formatter.Serialize(stream, list);
             }
         }
    }
}
