using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.NetworkInformation;

namespace JSONS
{
    public partial class Form1 : Form
    {
        string ruta = @"C:\tmp\abastecimiento.json";
        string json;

        int numCliente = 0;
        List<DatosCliente> lista = new List<DatosCliente>();
        List<Abastecimiento> datosPorMandar = new List<Abastecimiento>();
        public Form1()
        {
            InitializeComponent();
            Arduino.Open();
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime fechaActual = DateTime.Now;
            txtfecha.Text = fechaActual.ToString("dd/MM/yyyy"); // Mostrar la fecha en el formato "dd/MM/yyyy"
            num.Text = numCliente.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label6.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        public  string JsonParaArduino(string cadenaJson)
        {
              StringBuilder listaCaracteres = new StringBuilder();

             // Recorrer cada caracter de la cadena JSON
             foreach (char c in cadenaJson)
             {
              // Si el caracter es una comilla doble, agregar una barra invertida antes de ella
                 if (c == '"')
                 {
                     listaCaracteres.Append("\\\"");
                 }
                 else
                 {
                      // De lo contrario, simplemente agregar el caracter a la cadena
                      listaCaracteres.Append(c);
                 }
           }
           return "\"" + listaCaracteres.ToString() + "\"";
        }
         private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
          {
          // Imprimir los datos del serial monitos en Visual, ayuda a llevar un control
           string data = Arduino.ReadLine(); // Leer los datos del puerto serial
           UpdateListBox(data);
         }
private void UpdateListBox(string data)
{
    if (InvokeRequired)
    {
        Invoke(new Action<string>(UpdateListBox), data);
    }
    else
    {
        listBox1.Items.Add(data); // Mostrar los datos en el listbox
    }
}
private void MandarDatos_Click(object sender, EventArgs e)
{
    listView.Items.Clear();
    listBox1.Items.Clear();
    //recoger todos los datos
    double prepago = double.Parse(galones.Text);
    string nombreCliente = nombre.Text.ToString();
    string nitCliente = nit.Text;
    string Fecha = fecha.Text;
    string horaActual = hora.Text;
    double precioDia = 20.00;
    double cantidadGalones = prepago / precioDia;
    //Se guardan todos los datos en una lista
    lista.Add(new DatosCliente(Fecha, horaActual, nombreCliente, nitCliente, numCliente, cantidadGalones, precioDia, prepago));
    nombre.Text = "";
    nit.Text = "";
    galones.Text = "";
    num.Text = "";
    //Imprimir datos del cliente
    foreach (DatosCliente cliente in lista)
    {
        ListViewItem listaImprimir = new ListViewItem(cliente._NumCliente.ToString());
        listaImprimir.SubItems.Add(cliente._Fecha);
        listaImprimir.SubItems.Add(cliente._Hora);
        listaImprimir.SubItems.Add(cliente._Nombre);
        listaImprimir.SubItems.Add(cliente._Nit.ToString());
        listaImprimir.SubItems.Add(cliente._PrecioGalon.ToString());
        listaImprimir.SubItems.Add(cliente._CantidadGalones.ToString());
        listaImprimir.SubItems.Add(cliente._Total.ToString());
        listView.Items.Add(listaImprimir);
    }
    numCliente++;
    num.Text = numCliente.ToString();
    //Crear el Json,leerlo y mandarlo al Arduino
    datosPorMandar.Add(new Abastecimiento(1, cantidadGalones,"regular"));
    json = JsonSerializer.Serialize(datosPorMandar);
    //Se crea el documento
    File.WriteAllText(ruta, json);
    //Opcional
    dataGridView1.DataSource = datosPorMandar;
    //Leer el Json y obtener su contenido
    string jsonContent = File.ReadAllText(ruta);
    prueba.Text = JsonParaArduino(jsonContent);
    // Enviar el contenido del archivo al Arduino a través del puerto serie
    Arduino.WriteLine(jsonContent);

    }
    



    }
}
        
