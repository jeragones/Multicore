using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Multicore.Negocio;
using System.Diagnostics;

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
            //object[] words1 = new object[] { new string[] { "m", "m", "m", "m","m","Delgado Rojas Adriana" },
            //                                 new string[] { "m", "m", "m", "m","m","Ramirez Lambda Gerardo" }, 
            //                                 new string[] { "m", "m", "m", "m","m","Quesada Gutierrez Karla" },
            //                                 new string[] { "m", "m", "m", "m","m","Miranda Jimenez Nelson" } };

            //object[] words = new object[] { new string[] { "209002", "ALAJUELA" }, 
            //                                new string[] { "101001", "SAN JOSE" },
            //                                new string[] { "301031", "CARTAGO" },
            //                                new string[] { "301031", "HEREDIA" },
            //                                new string[] { "301031", "GUANACASTE" },
            //                                new string[] { "301031", "PUNTARENAS" },
            //                                new string[] { "301031", "LIMON" },
            //                                new string[] { "301031", "CONSULADO" }};


            //List<string[]> aoLineas = new List<string[]>();// insArchivo.cargarArchivo();
            //aoLineas.Add(new string[] { "2", "ALAJUELA" });
            //aoLineas.Add(new string[] { "1", "SAN JOSE" });
            //aoLineas.Add(new string[] { "3", "CARTAGO" });
            //aoLineas.Add(new string[] { "6", "HEREDIA" });
            //aoLineas.Add(new string[] { "5", "GUANACASTE" });
            //aoLineas.Add(new string[] { "4", "PUNTARENAS" });
            //aoLineas.Add(new string[] { "8", "LIMON" });
            //aoLineas.Add(new string[] { "7", "CONSULADO" });






            string jj = clsMergeSort.mergeSort(0,false,false);
            //var timer = Stopwatch.StartNew();
            //string h1 = clsMergeSort.mergeSort(0, false, true); // distritos
            //string h = clsMergeSort.mergeSort(0, false, false); // 
            //timer.Stop();
            //var timer1 = Stopwatch.StartNew();
            //string h1 = clsQuickSort.quickSort(0, false, true); // true -> desc
            //string h = clsQuickSort.quickSort(0,false,false); // false -> asc
            //timer1.Stop();
            int y = 0;









            //clsAnalisisTexto insAnalisisTexto = new clsAnalisisTexto();

            //var timer2 = Stopwatch.StartNew();
            //insAnalisisTexto.analizarTexto(true);
            //timer2.Stop();

            //var timer3 = Stopwatch.StartNew();
            //insAnalisisTexto.analizarTexto(false);
            //timer3.Stop();

            ////string c = insAnalisisTexto.getCantidadCaracteres();
            ////string p = insAnalisisTexto.getCantidadPalagras();
            ////StringBuilder pc = insAnalisisTexto.getPalabrasComunes();
            ////string i = insAnalisisTexto.getIdioma();
            //int p = 0;
        }
    }
}
