using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Multicore.Negocio;

namespace Multicore.Interfaz
{
    public partial class borreme : Form
    {
        public borreme()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*object[] words1 = new object[] { new string[] { "m", "m", "m", "m","m","Delgado Rojas Adriana" },
                                             new string[] { "m", "m", "m", "m","m","Ramirez Lambda Gerardo" }, 
                                             new string[] { "m", "m", "m", "m","m","Quesada Gutierrez Karla" },
                                             new string[] { "m", "m", "m", "m","m","Miranda Jimenez Nelson" } };

            object[] words = new object[] { new string[] { "209002", "ALAJUELA" }, 
                                            new string[] { "101001", "SAN JOSE" },
                                            new string[] { "301031", "CARTAGO" },
                                            new string[] { "301031", "HEREDIA" },
                                            new string[] { "301031", "GUANACASTE" },
                                            new string[] { "301031", "PUNTARENAS" },
                                            new string[] { "301031", "LIMON" },
                                            new string[] { "301031", "CONSULADO" }};

            //int[] nums = new int[] { 8, 6, 1, 4, 7, 2, 9, 5, 3 };
            //string[] cadenas = { "z", "e", "x", "c", "m", "q", "a" };
            //clsOrdenar.quicksort(cadenas, 1);



            clsOrdenar.mergeSort(words, 0, true); // distritos
            clsOrdenar.mergeSort(words1, 5, false); // 
            //int h = 0;
            //clsOrdenar.quicksort(words, 1,false);
            //clsOrdenar.quicksort(words1, 1,false);
            int g = 0;*/


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
            clsAnalisisTexto insAnalisisTexto = new clsAnalisisTexto();
            insAnalisisTexto.analizarTexto(txtTexto.Text, true);
        }
    }
}
