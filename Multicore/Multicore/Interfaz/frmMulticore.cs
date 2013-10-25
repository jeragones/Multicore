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
    }
}
