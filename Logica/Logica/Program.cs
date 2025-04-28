using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    internal class Program
    {
        //🧠 1. FizzBuzz
        //Escreva um programa que imprime os números de 1 a 100, mas para múltiplos de 3 imprime "Fizz", para múltiplos de 5 imprime "Buzz" e para múltiplos de ambos imprime "FizzBuzz".
        static void FizzBuzz()
        {
            for (int i = 1; i <= 100; i++)
            {
                if (i % 3 == 0)
                    Console.Write($"Fizz");

                if (i % 5 == 0)
                    Console.Write($"Buzz");

                Console.Write(i + "\n");
            }
        }

        //🔄 2. Inversão de array
        //Dado um array de inteiros int[] numeros = {1, 2, 3, 4, 5}, inverta seus elementos sem usar métodos prontos como Array.Reverse().
        static void InverteArray()
        {
            int[] array = { 1, 2, 3, 4, 5 };
            int[] arrayInvertido = new int [array.Length];

            int iInvertido = 0;

            for (int i = array.Length - 1; i >= 0; --i)
            {
                arrayInvertido[iInvertido++] = array[i];
            }

            foreach (var num in arrayInvertido)
            {
                Console.WriteLine(num);
            }
        }

        static void InverteArrayInPlace()
        {
            int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            int length = array.Length / 2;

            if (array.Length % 2 == 0)
                length--;

            for (int i = array.Length - 1, j = 0; i >= 0 && i != length; --i, j++)
            {
                int aux = array[j];
                array[j] = array[i];
                array[i] = aux;
            }

            foreach (var num in array)
                Console.WriteLine(num);
        }

        //🔁 3. Soma de números ímpares
        //Escreva um algoritmo que calcule a soma dos números ímpares de 1 a 100.
        static void SomaNumeroImpar()
        {
            int length = 100;

            int sum = 0;
            for (int i = 1; i <= length; ++i)
            {
                if (i % 2 != 0)
                    sum += i;
            }

            Console.WriteLine(sum);
        }

        //🧮 4. Fatorial com recursão
        //Implemente uma função recursiva que receba um número inteiro e retorne seu fatorial.
        static double FatorialRecursao(double n)
        {
            if (n == 0 || n == 1)
                return 1;

            return n * FatorialRecursao(n - 1);
        }

        //🪞 5. Palíndromo
        //Escreva uma função que determine se uma string é um palíndromo (ex: "arara", "radar"). Ignore espaços e maiúsculas/minúsculas.
        static void Palindromo()
        {
            string palavra = "abba";

            bool ehPalindromo = true;
            for (int i = palavra.Length - 1, j = 0; i >= 0; --i, j++)
            {
                if (palavra[i] != palavra[j])
                {
                    ehPalindromo = false;
                    break;
                }
            }

            if (ehPalindromo)
                Console.WriteLine("É palindromo");
            else
                Console.WriteLine("NÃO é palindromo");
        }

        //🔢 6. Números primos em um intervalo
        //Escreva uma função que receba dois números inteiros (início e fim) e retorne todos os números primos nesse intervalo.
        static List<int> Primos(int n1, int n2)
        {
            List<int> list = new List<int>();

            if (n1 < 2)
                n1 = 2;

            for (; n1 <= n2; ++n1)
            {
                bool ehPrimo = true;

                for (int j = 2; j <= n1; ++j)
                {
                    if (n1 != j && n1 % j == 0)
                    {
                        ehPrimo = false;
                        break;
                    }
                }

                if (ehPrimo)
                    list.Add(n1);
            }

            return list;
        }

        //🕵️ 7. Primeiro caractere não repetido
        //Dada uma string, encontre o primeiro caractere que não se repete.
        //Exemplo: "abacabad" → retorna 'c'
        static void PrimeiroCaractereNaoRepetido(string texto)
        {
            texto = texto.ToLower().Trim();

            for (int i = 0; i < texto.Length; ++i)
            {
                char letra = texto[i];
                bool ehUnica = true;

                for (int j = 0; j < texto.Length; j++)
                {
                    if (j != i && letra == texto[j])
                    {
                        ehUnica = false;
                        break;
                    }
                }

                if (ehUnica)
                {
                    Console.WriteLine($"A letra '{letra}' é o primeiro caractere que não se repete.");
                    break;
                }
            }
        }

        //📊 8. Frequência de caracteres
        //Crie um dicionário (Dictionary<char, int>) que conta quantas vezes cada caractere aparece em uma string.
        static Dictionary<char, int> FrequenciaCaracteres(string texto)
        {
            Dictionary<char, int> frequencia = new Dictionary<char, int>();

            for (int i = 0;i < texto.Length; ++i)
            {
                char caractere = texto[i];

                if (frequencia.ContainsKey(caractere))
                    frequencia[caractere]++;
                else
                    frequencia.Add(caractere, 1);
            }

            return frequencia;
        }

        //📐 9. Verificar anagramas
        //Escreva uma função que verifica se duas palavras são anagramas uma da outra. Ex: "amor" e "roma".
        static bool Anagrama(string palavra1, string palavra2)
        {
            if (palavra1.Length != palavra2.Length)
                return false;

            for (int i = palavra1.Length - 1; i >= 0 ; --i)
            {
                int indexChar = palavra1.IndexOf(palavra2[i]);

                if (indexChar == -1)
                    return false;

                palavra1 = palavra1.Remove(indexChar, 1);
            }

            return true;
        }

        //⛓️ 10. Substituir duplicatas
        //Dado um array de inteiros, retorne uma nova lista sem elementos duplicados preservando a ordem de aparição.
        //Exemplo: [3, 5, 3, 2, 5, 7] → [3, 5, 2, 7]

        static int[] SubstituiDuplicado(int[] lista)
        {
            HashSet<int> result = new HashSet<int>();

            foreach (int i in lista)
            {
                result.Add(i);
            }

            return result.ToList().ToArray();
        }

        static void Main(string[] args)
        {
            var b = SubstituiDuplicado(new int[] { 3, 5, 3, 2, 5, 7});

            Console.WriteLine(string.Join(",", b));
        }
    }
}
