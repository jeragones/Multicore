using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Multicore.Negocio;
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Threading.Tasks;
using Multicore.Interfaz;

namespace Multicore
{
    public partial class frmMulticore : Form
    {
        public frmMulticore()
        {
            InitializeComponent();
        }

        private void btnAnalisis_Click(object sender, EventArgs e)
        {
            clsAnalisisTexto insAnalisisTexto = new clsAnalisisTexto();
            borreme form = new borreme();
            form.Show();

        }

        private void btnEncriptar_Click(object sender, EventArgs e)
        {
            
            string mensaje ="";
            string texto="";
            string encriptado = "";
            int salto = 3;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos txt|*.txt";
            openFileDialog.FileName = "Seleccione un archivo";
            openFileDialog.Title = "Seleccione un archivo";
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.FileName = mensaje;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                mensaje = openFileDialog.FileName;
                System.IO.StreamReader sr = new System.IO.StreamReader(mensaje, System.Text.Encoding.Default);
                texto = sr.ReadToEnd();
            }

            
            if (checkParallel.Checked)
            {
                var timer = Stopwatch.StartNew();
                encriptado = clsEncriptarCesar.encriptCesar(texto, salto, true);
                timer.Stop();
                labelresultado.Text = Convert.ToString(timer.Elapsed);
            }
            else
            {
                var timer = Stopwatch.StartNew();
                encriptado = clsEncriptarCesar.encriptCesar(texto, salto, false);
                timer.Stop();
                labelresultado.Text = Convert.ToString(timer.Elapsed);
            }
            string fileName = (@"C:\Users\jdbr\Desktop\Encriptado.txt");
            StreamWriter writer = File.CreateText(fileName);

            writer.WriteLine(encriptado);
            writer.Close();
            
            
            
          
            
             }

        private void btnDesencriptar_Click(object sender, EventArgs e)
        {
            string mensaje = "";
            string texto = "";
            string desencriptado = "";
            int salto = 3;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos txt|*.txt";
            openFileDialog.FileName = "Seleccione un archivo";
            openFileDialog.Title = "Seleccione un archivo";
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.FileName = mensaje;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                mensaje = openFileDialog.FileName;
                System.IO.StreamReader sr = new System.IO.StreamReader(mensaje, System.Text.Encoding.Default);
                texto = sr.ReadToEnd();
            }

            
            if (checkParallel.Checked)
            {
                var timer = Stopwatch.StartNew();
                desencriptado = clsEncriptarCesar.desencriptCesar(texto, salto, true);
                timer.Stop();
                labelresultado.Text = Convert.ToString(timer.Elapsed);
            }
            else
            {
                var timer = Stopwatch.StartNew();
                desencriptado = clsEncriptarCesar.desencriptCesar(texto, salto, false);
                timer.Stop();
                labelresultado.Text = Convert.ToString(timer.Elapsed);
            }
            string fileName = (@"C:\Users\jdbr\Desktop\Desencriptado.txt");
            StreamWriter writer = File.CreateText(fileName);

            writer.WriteLine(desencriptado);
            writer.Close();
            
        }

        private void encriptDesencript_Click(object sender, EventArgs e)
        {

        }

        private void generarClave_Click(object sender, EventArgs e)
        {
            string mensaje = "";
            string texto = "";
            string desencriptado = "";
            string[] res = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos txt|*.txt";
            openFileDialog.FileName = "Seleccione un archivo";
            openFileDialog.Title = "Seleccione un archivo";
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.FileName = mensaje;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                mensaje = openFileDialog.FileName;
                System.IO.StreamReader sr = new System.IO.StreamReader(mensaje, System.Text.Encoding.Default);
                texto = sr.ReadToEnd();
            }
            res = clsEncriptarXOR.XOR(texto);

            string fileName = (@"C:\Users\jdbr\Desktop\DesencriptadoXOR.txt");
            StreamWriter writer = File.CreateText(fileName);

            writer.WriteLine(desencriptado);
            writer.Close();
        }

    }
}
