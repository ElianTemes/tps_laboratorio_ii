using System;
using System.Text.RegularExpressions;

namespace Entidades
{
    public static class Calculadora
    {
        /// <summary>
        /// retorna el char del operador ingresado si es valido, si no, retorna un '+' por defecto
        /// </summary>
        /// <param name="operador"></param>
        /// <returns></returns>
        private static char ValidarOperador(char operador)
        {
            if (Regex.IsMatch(operador.ToString(), "^[+*/-]+$"))
            {
                return operador;
            }
            return '+';
        }
        /// <summary>
        /// Recibe dos numeros por medio del objeto Operando y un char indicando el operador, luego de validar el operador
        /// retorna el resultado de la operacion redondeado a 3 cifras maximo
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <param name="operador"></param>
        /// <returns></returns>
        public static double Operar(Operando num1, Operando num2, char operador)
        {
            char auxOperador = ValidarOperador(operador);
            double resultado;

            switch (auxOperador)
            {                
                case '-':
                    resultado = num1 - num2;
                break;

                case '*':
                    resultado = num1 * num2;
                break;

                case '/':
                    resultado = num1 / num2;
                break;

                default:
                    resultado = num1 + num2;
                break;
            }

            return Math.Round(resultado, 3, MidpointRounding.AwayFromZero);
        }
    }

}