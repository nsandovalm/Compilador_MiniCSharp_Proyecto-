using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Antlr4.Runtime;
using Generated;






namespace Editor
{
    public partial class Edit : Form
    {
        public Edit()
        {
            InitializeComponent();
            this.CenterToScreen();
            txtContenido.KeyUp += txtContenido_KeyUp;
        }
        

        private void btnBuscar_Click(object sender, EventArgs e) //Funcionalidad para el botón de buscar
        {
            txtArchivo.InitialDirectory = "C:/";

            if (txtArchivo.ShowDialog() == DialogResult.OK) { 
                this.txtDireccion.Text = txtArchivo.FileName; 
            }
            System.IO.StreamReader sr = new System.IO.StreamReader(txtDireccion.Text, System.Text.Encoding.Default);

            string contenido; //Almacenamos el texto que este en el lector Io que contiene el archivo
            contenido= sr.ReadToEnd(); //Lo lee hasta el final (hasta la última linea)
            sr.Close(); //Lo cerramos

            txtContenido.Text = contenido;


        }

        private void txtGuardar_Click(object sender, EventArgs e){ //Funcionalidad para el botón de guardar
            try {
                if (txtDialogo.ShowDialog() == DialogResult.OK){
                    if (File.Exists(txtDialogo.FileName)) {  //En caso de que exista el archivo

                        string txt = txtDialogo.FileName;

                        StreamWriter textoAGuardar = File.CreateText(txt);
                        textoAGuardar.Write(txtContenido.Text);
                        textoAGuardar.Flush(); //Liberar memoria
                        textoAGuardar.Close(); //Cerramos


                        txtDireccion.Text = txt;

                    }
                    else{ //En caso de que no exista la información

                        string txt = txtDialogo.FileName;

                        StreamWriter textoAGuardar = File.CreateText(txt);
                        textoAGuardar.Write(txtContenido.Text);
                        textoAGuardar.Flush(); //Liberar memoria
                        textoAGuardar.Close(); //Cerramos


                        txtDireccion.Text = txt;

                    }

                }

            }
            catch (Exception){
                MessageBox.Show("Error al guardar");

            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Deshabilitar la opción de escritura del TextBox llamado textBox1
            txtPosicion.ReadOnly = true;
            txtCursor.ReadOnly = true;
        }

        private void btnCompilar_Click(object sender, EventArgs e)
        {
            //Limpiar el contenido del textBox donde se muestran los msg de error
            txtCursor.Text = "";
            ICharStream input = CharStreams.fromString(txtContenido.Text);
            Scanner inst = new Scanner(input);
            CommonTokenStream tokens = new CommonTokenStream(inst);
            MiniCSharpParser parser = new MiniCSharpParser(tokens);
            
            parser.RemoveErrorListeners();
            MyErrorListener errorListener = new MyErrorListener();
            parser.AddErrorListener(errorListener);

            parser.ErrorHandler = new MyErrorSpanish();
            
            inst.RemoveErrorListeners();
            MyErrorListenerScanner errorListenerScanner = new MyErrorListenerScanner();
            inst.AddErrorListener(errorListenerScanner);
            
            
            MiniCSharpParser.ProgramContext tree = parser.program();

            if (errorListener.hasErrors()){
                txtCursor.Text += errorListener.ToString();
                AContextual mv = new AContextual();
                mv.Visit(tree);
            }

            if (errorListenerScanner.hasErrors()){
                txtCursor.Text += errorListenerScanner.ToString(); 
                
            }
            
            


        }
        
        //Metodo que permite obtener la fila y columna, mostrarndolas en el textBox(txtLineColumn) 
        private void txtContenido_KeyUp(object sender, KeyEventArgs e)
        {
            int index = txtContenido.SelectionStart;
            int line = txtContenido.GetLineFromCharIndex(index) + 1;
            int column = index - txtContenido.GetFirstCharIndexOfCurrentLine() + 1;
            txtPosicion.Text =  line + ":" + column;
        }


       
    }
}
