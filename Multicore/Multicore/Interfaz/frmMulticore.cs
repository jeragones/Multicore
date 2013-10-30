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
            
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "Archivos txt|*.txt";
            ////openFileDialog.FileName = "Seleccione un archivo";
            //openFileDialog.Title = "Seleccione un archivo";
            //openFileDialog.InitialDirectory = "C:\\";
            //openFileDialog.FileName = this.txtTexto.Text;
            //if (openFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    this.txtTexto.Text = openFileDialog.FileName;
            //}
            


            //System.IO.StreamReader sr = new System.IO.StreamReader(@txtTexto.Text, System.Text.Encoding.Default);
            //string texto;
            //texto = sr.ReadToEnd();
            //sr.Close();
            //txtTexto.Text = texto;
            //insAnalisisTexto.analizarTexto(txtTexto.Text, false);








        }

        private void btnEncriptar_Click(object sender, EventArgs e)
        {
            
            string mensaje ="";
            string texto="";
            string encriptado = "";
            int salto = 0;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos txt|*.txt";
            //openFileDialog.FileName = "Seleccione un archivo";
            openFileDialog.Title = "Seleccione un archivo";
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.FileName = mensaje;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                mensaje = openFileDialog.FileName;
                System.IO.StreamReader sr = new System.IO.StreamReader(mensaje, System.Text.Encoding.Default);
                texto = sr.ReadToEnd();
            }

            salto = Convert.ToInt16(comboSalto.SelectedItem);
            if (checkParallel.Checked)
            {
                var timer = Stopwatch.StartNew();
                encriptado = clsEncriptar.encrip(texto, salto, true);
                timer.Stop();
                labelresultado.Text = Convert.ToString(timer.Elapsed);
            }
            else
            {
                var timer = Stopwatch.StartNew();
                encriptado = clsEncriptar.encrip(texto, salto, false);
                timer.Stop();
                labelresultado.Text = Convert.ToString(timer.Elapsed);
            }
            //Console.WriteLine(resultado[0]);
            //Console.WriteLine(resultado[1]);
            
            


            



             }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pnlEncriptacion_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
