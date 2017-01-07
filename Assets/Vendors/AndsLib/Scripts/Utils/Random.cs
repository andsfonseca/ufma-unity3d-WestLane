using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AndsLib.Util
{
    public class Random
    {

        /// <summary>
        /// Contem a lista de todos os numeros inteiros aleatórios gerados
        /// </summary>
        private static Dictionary<string, List<int>> m_numbersInteger = new Dictionary<string, List<int>>();

        /// <summary>
        /// Contem a lista de todos os numeros decimais aleatórios gerados
        /// </summary>
        private static Dictionary<string, List<float>> m_numbersFloat = new Dictionary<string, List<float>>();

        /// <summary>
        /// Recupera um valor entre um inteiro minimo e o inteiro maximo
        /// </summary>
        /// <param name="min">Valor Minimo para ser buscado</param>
        /// <param name="max">Valor Máximo para ser buscado</param>
        public static int Range(int min, int max)
        {
            return getNumber(min, max);
        }

        /// <summary>
        /// Recupera um valor entre um decimal minimo e um decimal maximo
        /// </summary>
        /// <param name="min">Valor Minimo para ser buscado</param>
        /// <param name="max">Valor Máximo para ser buscado</param>
        public static float Range(float min, float max)
        {
            return 0;
        }

        /// <summary>
        /// Retorna um valor Booleano aleatório; verdadeiro ou falso;
        /// </summary>
        public static bool boolean{
			get{
				return (Range(0,2) == 1);
			}
		}

        /// <summary>
        /// Gera uma key a partir de dois inteiros
        /// </summary>
        /// <param name="a">Valor numérico para o primeiro termo da Key</param>
        /// <param name="b">Valor numérico para o segundo termo da Key</param>
        private static string keyFormat(int a, int b)
        {
            return a.ToString() + "," + b.ToString();
        }

        /// <summary>
        /// Gera uma key a partir de dois decimais
        /// </summary>
        /// <param name="a">Valor numérico para o primeiro termo da Key</param>
        /// <param name="b">Valor numérico para o segundo termo da Key</param>
        private static string keyFormat(float a, float b)
        {
            return a.ToString() + "," + b.ToString();
        }

        /// <summary>
        /// Recupera de uma lista de numero Randomicos um proximo numero aleatório já existente.
        /// </summary>
        /// <param name="min">Valor Minimo para ser buscado</param>
        /// <param name="max">Valor Máximo para ser buscado</param>
        private static int getNumber(int min, int max)
        {
            int number = 0;
            string key = keyFormat(min, max);

            //Verifica se existe uma lista com esta Key
            if (m_numbersInteger.ContainsKey(key))
            {
                //Recupera um número da lista
                number = m_numbersInteger[key][0];
                m_numbersInteger[key].RemoveAt(0);

                //Caso a lista fique vazia, ele irá gerar novos numeros
                if (m_numbersInteger[key].Count == 0)
                {
                    GenerateRandomNumbers(min, max);
                }
            }

            //Caso contrario,
            else {
                
                //Gera uma nova lista de numeros Aleatórios
                GenerateRandomNumbers(min, max);

                //Recupera um número da lista
                number = m_numbersInteger[key][0];
                m_numbersInteger[key].RemoveAt(0);
            }

            return number;
        }

        /// <summary>
        /// Recupera de uma lista de numero Randomicos um proximo numero aleatório já existente.
        /// </summary>
        /// <param name="min">Valor Minimo para ser buscado</param>
        /// <param name="max">Valor Máximo para ser buscado</param>
        private static float getNumber(float min, float max)
        {
            float number = 0;
            string key = keyFormat(min, max);

            //Verifica se existe uma lista com esta Key
            if (m_numbersFloat.ContainsKey(key))
            {
                //Recupera um número da lista
                number = m_numbersFloat[key][0];
                m_numbersFloat[key].RemoveAt(0);

                //Caso a lista fique vazia, ele irá gerar novos numeros
                if (m_numbersFloat[key].Count == 0)
                {
                    GenerateRandomNumbers(min, max);
                }
            }

            //Caso contrario,
            else {

                //Gera uma nova lista de numeros Aleatórios
                GenerateRandomNumbers(min, max);

                //Recupera um número da lista
                number = m_numbersFloat[key][0];
                m_numbersFloat[key].RemoveAt(0);
            }

            return number;
        }

        /// <summary>
        /// Verifica se existe uma lista correspondente ao valores e inseri uma lista de novos numeros aleatórios
        /// </summary>
        /// <param name="min">Inteiro Minimo para ser gerado</param>
        /// <param name="max">Inteiro Máximo para ser gerado</param>
        public static void GenerateRandomNumbers(int min, int max)
        {
            List<int> list = new List<int>();
            string key = keyFormat(min, max);
            
            //Adiciona numeros aleátorios a partir do Range dentro de uma lista
            for (int i = 0; i < 100; i++)
            {
                list.Add(UnityEngine.Random.Range(min, max));
            }

            //Caso a Lista não exista no Dictionary então ela é adicionada se associando a essa key
            if (!m_numbersInteger.ContainsKey(key))
            {
                m_numbersInteger.Add(key, list);
            }

            //Caso contrario, a lista correspondente recebe a nova lista
            else {
                m_numbersInteger[key] = list;
            }
        }

        /// <summary>
        /// Verifica se existe uma lista correspondente ao valores e inseri uma lista de novos numeros aleatórios
        /// </summary>
        /// <param name="min">Decimal Minimo para ser gerado</param>
        /// <param name="max">Decimal Máximo para ser gerado</param>
        public static void GenerateRandomNumbers(float min, float max)
        {
            List<float> list = new List<float>();
            string key = keyFormat(min, max);

            //Adiciona numeros aleátorios a partir do Range dentro de uma lista
            for (int i = 0; i < 100; i++)
            {
                list.Add(UnityEngine.Random.Range(min, max));
            }

            //Caso a Lista não exista no Dictionary então ela é adicionada se associando a essa key
            if (!m_numbersFloat.ContainsKey(key))
            {
                m_numbersFloat.Add(key, list);
            }

            //Caso contrario, a lista correspondente recebe a nova lista
            else {
                m_numbersFloat[key] = list;
            }
        }

        /// <summary>
        /// Retorna uma lista de todos os numeros aleatórios gerados
        /// </summary>
        /// <param name="min">Valor minimo dos numeros aleatórios gerados</param>
        /// <param name="max">Valor minimo dos numeros aleatórios gerados</param>
        /// <param name="currentList">Indicativo se quer pegar a lista atual, em vez de gerar novos numeros</param>
        public List<int> getGeneratedNumbers(int min, int max, bool currentList) {
            List<int> list = new List<int>();
            string key = keyFormat(min, max);

            if (currentList && m_numbersInteger.ContainsKey(key))
            {
                list = m_numbersInteger[key];
            }
            else {
                GenerateRandomNumbers(min, max);
                list = m_numbersInteger[key];
            }
            return list;
        }

        /// <summary>
        /// Retorna uma lista de todos os numeros aleatórios gerados
        /// </summary>
        /// <param name="min">Valor minimo dos numeros aleatórios gerados</param>
        /// <param name="max">Valor minimo dos numeros aleatórios gerados</param>
        /// <param name="currentList">Indicativo se quer pegar a lista atual, em vez de gerar novos numeros</param>
        public List<float> getGeneratedNumbers(float min, float max, bool currentList)
        {
            List<float> list = new List<float>();
            string key = keyFormat(min, max);

            if (currentList && m_numbersFloat.ContainsKey(key))
            {
                list = m_numbersFloat[key];
            }
            else {
                GenerateRandomNumbers(min, max);
                list = m_numbersFloat[key];
            }
            return list;
        }

		/// <summary>
		/// Retorna um objeto aleatório de uma lista qualquer;
		/// http://wiki.unity3d.com/index.php?title=ExtRandom
		/// </summary>
		/// <param name="list">Lista com objetos</param>
		public static T Choice<T>(ref List<T> list)
		{
			int index = Range (0, list.Count);
			return list [index];
		}
    }
}