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

namespace JSONS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime fechaActual = DateTime.Now;
            txtfecha.Text = fechaActual.ToString("dd/MM/yyyy"); // Mostrar la fecha en el formato "dd/MM/yyyy"

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label6.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void btngenerar_Click(object sender, EventArgs e)
        {
            string nombre = txtCliente.Text;
            string tipo = txtabastecimeinto.Text;
            cliente clienytes = new cliente(nombre,tipo);
            var data = new
            {
                Nombre = nombre,
                Tipo = tipo,
            };
            string json = JsonSerializer.Serialize(data);
            textBox1.Text = json;

        }


    }
}
        