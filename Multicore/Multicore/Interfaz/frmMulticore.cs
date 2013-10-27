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
            //insAnalisisTexto.analizarTexto(txtTexto.Text, false);
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos txt|*.txt";
            //openFileDialog.FileName = "Seleccione un archivo";
            openFileDialog.Title = "Seleccione un archivo";
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.FileName = this.txtTexto.Text;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtTexto.Text = openFileDialog.FileName;
            }
            


            System.IO.StreamReader sr = new System.IO.StreamReader(@txtTexto.Text, System.Text.Encoding.Default);
            string texto;
            texto = sr.ReadToEnd();
            sr.Close();
            txtTexto.Text = texto;
            








        }

        private void btnEncriptar_Click(object sender, EventArgs e)
        {
            //clsEncriptar.hola(Msg.Text);
            string mensaje = Msg.Text;
            rest.AppendText("Esto es el mensaje sin cifrar: " + mensaje);



            SymmetricAlgorithm algoritmo = SymmetricAlgorithm.Create("Rijndael");
            //Se podría haber creado el algoritmo de esta otra manera:
            //RijndaelManaged algoritmoEncriptador = new RijndaelManaged();
            clsEncriptar.ConfigurarAlgoritmo(algoritmo);
            clsEncriptar.GenerarClave(algoritmo);
            clsEncriptar.GenerarIV(algoritmo);
            byte[] mensajeEncriptado = clsEncriptar.Encriptar(mensaje, algoritmo);
            rest.AppendText("Esto es el mensaje cifrado:");
            foreach (byte b in mensajeEncriptado)
            {
                String rr = ("{0:X2} " + b);
                rest.AppendText(rr);
            }
            byte[] mensajeDesencriptado = clsEncriptar.Desencriptar(mensajeEncriptado, algoritmo);
            string mensajeDescrifrado = Encoding.UTF8.GetString(mensajeDesencriptado);
            rest.AppendText("Esto es el mensaje descifrado: " + mensajeDescrifrado);
            
            algoritmo.Clear();
        }
    }
}
