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

        private void FormCalculadora_Load(object sender, EventArgs e)
        {
            Limpiar();
        }
        private void Limpiar()
        {
            txtNumero1.Clear();
            txtNumero2.Clear();
            lblResultado.Text = " ";
            cmbOperador.SelectedIndex = -1;
        }
        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        private static double Operar(string numero1, string numero2, string operador)
        {
            Operando num1 = new Operando(numero1);
            Operando num2 = new Operando(numero2);
            return Calculadora.Operar(num1, num2, char.Parse(operador));
        }

        private void BtnOperar_ClickAsync(object sender, EventArgs e)
        {
            StringBuilder sb = new();

            if(cmbOperador.SelectedItem is null)
            {
                cmbOperador.SelectedItem = "+";
            }
            if(txtNumero2.Text == "0" || txtNumero2.Text == "" && cmbOperador.SelectedItem.ToString() == "/")
            {
                FormGif FormGif = new();
                FormGif.Show();
                SoundPlayer OmaeWo = new(MiCalculadora.EfectoSonido.omaeWo);
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
        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void FormCalculadora_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Seguro que desea salir?", "Cierre de Calculadora", 
                                                      MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(resultado == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
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
        private void BtnConvertirADecimal_Click(object sender, EventArgs e)
        {
            string valorActual = lblResultado.Text;
            if(Operando.BinarioDecimal(lblResultado.Text) != "Valor inválido")
            {
                lblResultado.Text = Operando.BinarioDecimal(lblResultado.Text);
            }
            else
            {
                lblResultado.Text = Operando.BinarioDecimal(lblResultado.Text);
                Task.Delay(1000).Wait();
                lblResultado.Text = valorActual;
            }
        }
    }
}
