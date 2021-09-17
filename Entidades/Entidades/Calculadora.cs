using System;
using System.Text.RegularExpressions;

namespace Entidades
{
    public static class Calculadora
    {
        private static char ValidarOperador(char operador)
        {
            if (Regex.IsMatch(operador.ToString(), "^[+*/-]+$"))
            {
                return operador;
            }
            return '+';
        }
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