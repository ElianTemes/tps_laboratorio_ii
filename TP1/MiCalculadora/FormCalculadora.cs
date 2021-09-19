using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiCalculadora
{
    public partial class FormCalculadora : Form
    {
        public FormCalculadora()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Al cargar el formulario llama al metodo Limpiar y a la fiesta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormCalculadora_Load(object sender, EventArgs e)
        {
            Limpiar();
        }
        /// <summary>
        /// Limpia los datos que tenga txtNumero1, txtNumero2, lblResultado y cmbOperador
        /// </summary>
        private void Limpiar()
        {
            txtNumero1.Clear();
            txtNumero2.Clear();
            lblResultado.Text = " ";
            cmbOperador.SelectedIndex = -1;
        }
        /// <summary>
        /// Al presionar el boton "Limpiar" llama al metodo limpiar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        /// <summary>
        /// Recibe tres strings, dos numeros y un operador, llama al metodo Calculadora.Operar y retorna el resultado
        /// de la operacion
        /// </summary>
        /// <param name="numero1"></param>
        /// <param name="numero2"></param>
        /// <param name="operador"></param>
        /// <returns></returns>
        private static double Operar(string numero1, string numero2, string operador)
        {
            Operando num1 = new(numero1);
            Operando num2 = new(numero2);
            return Calculadora.Operar(num1, num2, char.Parse(operador));
        }
        /// <summary>
        /// Al presionar el boton Operar, hace la operacion pertinente entre los dos numeros de los Textbox
        /// y muestra el resultado en el Label de resultado, ademas, le da play a tremenda cancion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOperar_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new();
            SoundPlayer Chanito = new(Recursos.ciudadMagica);
            Chanito.Play();

            if (cmbOperador.SelectedItem is null)
            {
                cmbOperador.SelectedItem = "+";
            }
            if(txtNumero2.Text == "0" || txtNumero2.Text == "" && cmbOperador.SelectedItem.ToString() == "/")
            {
                FormGif FormGif = new();
                FormGif.Show();
                SoundPlayer OmaeWo = new(Recursos.omaewo);
                OmaeWo.Play();
            }
            string resultado = Operar(txtNumero1.Text, txtNumero2.Text, cmbOperador.SelectedItem.ToString().Trim()).ToString();
            sb.AppendLine($"{resultado}");
            lblResultado.Text = sb.ToString();
            sb.Clear();
            if(Double.TryParse(txtNumero1.Text, out double num1))
            {
                num1 = Math.Round(num1, 3, MidpointRounding.AwayFromZero);
                sb.Append($"{num1} {cmbOperador.SelectedItem.ToString().Trim()}");
            }
            else
            {
                num1 = 0;
                sb.Append($"{num1} {cmbOperador.SelectedItem.ToString().Trim()}");
            }
            if(Double.TryParse(txtNumero2.Text, out double num2))
            {
                num2 = Math.Round(num2, 3, MidpointRounding.AwayFromZero);
                sb.AppendLine($" {num2} = {resultado}");
            }
            else
            {
                num2 = 0;
                sb.AppendLine($" {num2} = {resultado}");                
            }
                lstOperaciones.Items.Add(sb.ToString());    
        }
        /// <summary>
        /// Al presionar el boton cerrar, cierra el programa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// En el evento de que el formulario este por cerrar, le pregunta al usuario si esta seguro que desea salir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormCalculadora_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Seguro que desea salir?", "Cierre de Calculadora", 
                                                      MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(resultado == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        /// <summary>
        /// Al presionar el boton Convertir a Binario, convierte  el resultado mostrado en lblResultado en un binario
        /// de ser posible, si no, muestra el string "Valor invalido"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConvertirABinario_Click(object sender, EventArgs e)
        {
            string valorActual = lblResultado.Text;
            if (Operando.DecimalBinario(lblResultado.Text) != "Valor inválido")
            {
                lblResultado.Text = Operando.DecimalBinario(lblResultado.Text);
            }
            else
            {
                lblResultado.Text = Operando.DecimalBinario(lblResultado.Text);
                Task.Delay(1000).Wait();
                lblResultado.Text = valorActual;
            }
        }
        /// <summary>
        /// Al presionar el boton Convertir a Decimal, si el numero mostrado en lblResultado es un binario
        /// lo convierte a decimal, caso contrario muestra en lblResultado el string "Valor invalido"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConvertirADecimal_Click(object sender, EventArgs e)
        {
            string valorActual = lblResultado.Text;
            if(Operando.BinarioDecimal(lblResultado.Text.Trim()) != "Valor inválido")
            {
                lblResultado.Text = Operando.BinarioDecimal(lblResultado.Text.Trim());
            }
            else
            {
                lblResultado.Text = Operando.BinarioDecimal(lblResultado.Text.Trim());
                Task.Delay(1000).Wait();
                lblResultado.Text = valorActual;
            }
        }
    }
}
