using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Entidades
{
    public class Operando
    {
        private double numero;

        /// <summary>
        /// Constructor por defecto de Operando, asigna 0 al atributo numero
        /// </summary>
        public Operando()
        {
            this.numero = 0;
        }
        /// <summary>
        /// Constructor parametrizado de Operando, recibe un Double y lo asigna al atributo de clase numero
        /// </summary>
        /// <param name="numero"></param>
        public Operando(double numero)
        {           
            
            this.numero = numero;
            
        }
        /// <summary>
        /// Constructor parametrizado de Operando que recibe una string, comprueba si es un numero y de serlo la parsea
        /// a Double y asigna el valor al atributo de clase numero, caso contrario, asigna 0
        /// </summary>
        /// <param name="numero"></param>
        public Operando(string numero)
        {
            if (Double.TryParse(numero, out _))
            {
                this.numero = Convert.ToDouble(numero);
            }
            else
            {
                this.numero = 0;
            }
       
        }
        /// <summary>
        /// Valida el operando recibiendo una string y comprobando si esta compuesta por solamente numeros, si es valido
        /// retorna el numero parseado a Double, si no, retorna 0
        /// </summary>
        /// <param name="strNumero"></param>
        /// <returns></returns>
        private double ValidarOperando(string strNumero)
        {
            if(Double.TryParse(strNumero, out double num))
            {
                return num;
            }
            return 0;
        }
        /// <summary>
        /// Propiedad de Operando.numero, valida que lo que se le pasa sea un double y lo setea, caso contrario setea 0
        /// </summary>
        public string Numero
        {
            set
            {
                this.numero = ValidarOperando(value);
            }
        }
        /// <summary>
        /// Comprueba que la string que se le pasa sea un binario por medio de una expresion regular
        /// </summary>
        /// <param name="binario"></param>
        /// <returns></returns>
        private static bool EsBinario(string binario)
        {
            if (Regex.IsMatch(binario.Trim(), "^[01]+$"))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Valida que la string ingresada sea un binario, si lo es transforma a decimal, caso contrario retorna 
        /// la string "Valor Invalido"
        /// </summary>
        /// <param name="binario"></param>
        /// <returns></returns>
        public static string BinarioDecimal(string binario)
        {
            if (EsBinario(binario) && binario.Trim() != "0")
            {
                return Convert.ToString(Convert.ToInt32(binario, 2), 10);
            }
            return "Valor inválido";
        }
        /// <summary>
        /// Recibe un Double, si el numero es mayor a 0 y no es un numero binario, lo convierte a binario y retorna el numero
        /// como una string, caso contrario retorna la string "Valor invalido"
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        public static string DecimalBinario(double numero)
        {
            if ( numero > 0 && !EsBinario(numero.ToString()) )
            {
                int numEnteroPos = Convert.ToInt32(numero);
                return Convert.ToString(numEnteroPos, 2);
            }
            return "Valor inválido";
        }
        /// <summary>
        /// Comprueba que la string pasada como parametro sea un numero y no sea un binario, si ese es el caso y el numero es 
        /// mayor a 0, lo convierte a binario y retorna el numero como string, caso contrario retorna la string "Valor invalido"
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        public static string DecimalBinario(string numero)
        {
            if ( double.TryParse(numero, out double num) && !EsBinario(numero) )
            {
                if(num > 0)
                {
                   return DecimalBinario(num);
                }
            }
            return "Valor inválido";
        }
        public static double operator -(Operando num1, Operando num2)
        {
            return num1.numero - num2.numero;
        }
        public static double operator *(Operando num1, Operando num2)
        {
            return num1.numero * num2.numero;
        }
        public static double operator +(Operando num1, Operando num2)
        {
            return num1.numero + num2.numero;
        }
        public static double operator /(Operando num1, Operando num2)
        {
            if (num2.numero == 0)
            {
            return double.MinValue;
            }
            return num1.numero / num2.numero;
        }
    }
}
