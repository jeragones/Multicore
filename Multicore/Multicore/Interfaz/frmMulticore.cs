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
        public string clave = "";
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
            string mensaje = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos Key|*.sk";
            openFileDialog.FileName = "Seleccione un Archivo Clave ";
            openFileDialog.Title = "Seleccione un archivo";
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.FileName = mensaje;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                mensaje = openFileDialog.FileName;
                System.IO.StreamReader sr = new System.IO.StreamReader(mensaje, System.Text.Encoding.Default);
                clave = sr.ReadToEnd();
            }


            

        }

        private void generarClave_Click(object sender, EventArgs e)
        {
            string mensaje = "";
            string texto = "";
            List<object> res = null;
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
            res = clsEncriptarXOR.encriptarXOR(texto,true);
            string[] enc = (string[])res[1];

            string fileNameEncriptado = (@"C:\Users\jdbr\Desktop\Encriptado_xor.cfr");
            string fileNameClave = (@"C:\Users\jdbr\Desktop\Clave_xor.sk");
            StreamWriter writerEncrip = File.CreateText(fileNameEncriptado);
            StreamWriter writerClave = File.CreateText(fileNameClave);
            writerEncrip.Write(string.Join(" ",enc));
            writerClave.Write(res[0]);
            writerEncrip.Close();
            writerClave.Close();
        
            /*
            string s = clsEncriptarXOR.generarClave();
            string en = clsEncriptarXOR.encriptXOR(s, "Daniel .-.,123456");
            string des = clsEncriptarXOR.desencriptXOR(s, en);
            */
            
        }

        private void desencriptar_Click(object sender, EventArgs e)
        {
            string mensaje = "";
            string texto = "";
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos Cifrados|*.cfr";
            openFileDialog.FileName = "Seleccione un archivo";
            openFileDialog.Title = "Seleccione un archivo";
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.FileName = mensaje;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                mensaje = openFileDialog.FileName;
                System.IO.StreamReader sr = new System.IO.StreamReader(mensaje, System.Text.Encoding.UTF8);
                texto = sr.ReadToEnd();
            }

            string[] des=clsEncriptarXOR.desencriptarXOR(clave,texto,true);
            string fileNameEncriptado = (@"C:\Users\jdbr\Desktop\DesencriptadoXOR.txt");
           
            StreamWriter writerEncrip = File.CreateText(fileNameEncriptado);
            
            writerEncrip.WriteLine(string.Join(" ",des));
            
            writerEncrip.Close();
            
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
