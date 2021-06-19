using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace FileReader
{
    class Program
    {
        
        public static string LongestWord(string path) // Самое длинное слово
        {
            string wordWithMaxLength = string.Empty;
            StreamReader sr = new StreamReader(path);
            string line = null;
            char[] array;
            while ((line = sr.ReadLine()) != null)
            {
                line = line.ToLower(); // Убираем верхний регистр
                line = Regex.Replace(line, "[@,\\.\";'\\\\]", string.Empty); // Убираем ненужные символы
                string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); // Разделяем строку на "слова" по пробелу
                for (int i = 0; i < words.Length; i++) // Проверка на слово (начинается ли "слово" на букву)
                {
                    array = words[i].ToCharArray();
                    if (array[0] >= 'a' && array[0] <= 'z') // Проверка идет по первому символу
                    {
                        for (int j = 0; j < words.Length; j++)
                        {
                            if (words[j].Length > wordWithMaxLength.Length)
                            {
                                wordWithMaxLength = words[j];
                            }
                        }    
                    }
                }
            }
            sr.Close();
            return wordWithMaxLength;
        }
        public static int WordsWithHyphen(string path)
        {
            int count = 0;
            string line = null;
            StreamReader sr = new StreamReader(path);
            char[] array;
            while ((line = sr.ReadLine()) != null)
            {
                line = line.ToLower(); // Убираем верхний регистр
                string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); // Разделяем строку на "слова" по пробелу
                for (int i = 0; i < words.Length; i++) 
                {
                    array = words[i].ToCharArray();
                    for (int j = 0; j < array.Length; j++)
                    {
                        if (array[0] >= 'a' && array[0] <= 'z') // Проверка идет по первому символу
                        {
                            if (array[j] == '-') // Проверка на дефис
                            { count++; }
                        }			      
                        
                    }
                }
            }
            sr.Close();
            return count;
        }// Кол-во слов с дефисом
        public static int NumbersCount(string path) // Кол-во чисел
        {
            int count = 0;
            string line = null;
            StreamReader sr = new StreamReader(path);
            char[] array;
            while ((line = sr.ReadLine()) != null)
            {
                string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); // Разделяем строку на "слова" по пробелу
                for (int i = 0; i < words.Length; i++) // Проверка на число
                {
                    array = words[i].ToCharArray();
                    if (array[0] >= '0' && array[0] <= '9') // Проверка идет по первому символу
                    {
                        count++;
                    }
                }
            }
            sr.Close();
            return count;
        }
        public static Dictionary<string, int> Words(string path) // Кол-во каждого слова
        {
            StreamReader sr = new StreamReader(path);
            string line = null;
            Dictionary<string, int> dic = new Dictionary<string, int>();
            char[] array;
            while ((line = sr.ReadLine()) != null)
            {
                line = line.ToLower(); // Убираем верхний регистр
                line = Regex.Replace(line, "[@,\\.\";'\\\\]", string.Empty); // Убираем ненужные символы
                string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); // Разделяем строку на "слова" по пробелу
                for (int i = 0; i < words.Length; i++) // Проверка на слово (начинается ли "слово" на букву)
                {
                    array = words[i].ToCharArray();
                    if (array[0] >= 'a' && array[0] <= 'z') // Проверка идет по первому символу
                    {
                        if (!dic.ContainsKey(words[i]))
                        {
                            dic.Add(words[i], 1); // Добавление нового слова
                        }
                        else
                        {
                            dic[words[i]]=dic[words[i]]+1; // +1 к счётчику слова
                        }
                    }
                }
            }
            dic = dic.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value); // Сортировка словаря по убыванию
            sr.Close();
            return dic;
        }
        public static int WordsCount(string path) // Кол-во слов
        {
            int wordCount = 0;
            string line = null;
            StreamReader sr = new StreamReader(path);
            char[] punctuationarray = new char[] { '.', ',', ';', ':', '"' };
            char[] array;
            while ((line=sr.ReadLine())!=null)
            {
                line = line.ToLower(); // Убираем верхний регистр
                string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); // Разделяем строку на "слова" по пробелу
                for (int i = 0; i < words.Length; i++) // Проверка на слово (начинается ли "слово" на букву)
			    {
                    array = words[i].ToCharArray();
                    if (array[0] >= 'a' && array[0] <= 'z') // Проверка идет по первому символу
                       {
                           wordCount++;
                       }
                }
                        
            }
            sr.Close(); 
            return wordCount;
        }
        public static int DigitsCount(string path) // Кол-во цифр
        {
            int digitscount = 0;
            string line = null;
            StreamReader sr = new StreamReader(path);
            char[] array;
            char[] punctuation = new char[] { '.', ',', ';', ':', '"' };
            char[] digits = "1234567890".ToCharArray();
            while ((line = sr.ReadLine()) != null)
            {
                array = line.ToCharArray();
                for (int i = 0; i < line.Length; i++)
                {
                    for (int j = 0; j < digits.Length; j++)
                    {
                        if (array[i] == digits[j]) { digitscount++; }
                    }
                }
            }
            sr.Close();
            return digitscount;
        } // Кол-во цифр
        public static Dictionary<char, int> Letters(string path)
        {
            StreamReader sr = new StreamReader(path);
            string line = null;
            Dictionary<char, int> alphabet = new Dictionary<char, int>(); // Словарь английского алфавита
            for (char c = 'a'; c <= 'z'; c++) // Заполнение словаря
            {
                int key = 0;
                alphabet.Add(c,key);
            }
            char[] array;
            char charDictionary; // Костыль
            while ((line = sr.ReadLine()) != null) // Будем читать построчно
            {
                line = line.ToLower(); // Убираем верхний регистр
                array = line.ToCharArray();
                Array.Sort(array);
                for (char c = 'a'; c <='z' ; c++) // Перебираем алфавит 
                {
                    charDictionary = c; //Костыль
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (charDictionary== array[i]) // Перебираем строку по букве
                        {
                            alphabet[c]++;                          
                        }
                    }
                }
                
               
            }
            alphabet = alphabet.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value); // Сортировка словаря по убыванию           
            sr.Close();
            return alphabet;
        } // Количество каждой буквы
        public static int Punctuation(string path)
        {
            string line = null;
            int punctuationCount = 0;
            StreamReader sr = new StreamReader(path);
            char[] array;
            char[] punctuationarray = new char[] { '.', ',', ';', ':', '"','\''};
            while ((line = sr.ReadLine()) != null)
            {
                array = line.ToCharArray();
                for (int i = 0; i < line.Length; i++)
                {
                    for (int j = 0; j < punctuationarray.Length; j++)
                    {
                        if (array[i] == punctuationarray[j]) { punctuationCount++; }
                    }
                }
            }
            sr.Close();
            return punctuationCount;
        } // Количество знаков препинания (.,:;'")
        public static int LettersCount(string path)
        {
            string line = null;
            int lettersCount = 0;
            StreamReader sr = new StreamReader(path);
            char[] array;
            char[] punctuation = new char[] { '.', ',', ';', ':', '"' };
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
            while ((line=sr.ReadLine())!=null)
            { 
                array = line.ToCharArray();
                for (int i = 0; i < line.Length; i++)
                {
                    for (int j = 0; j < letters.Length; j++)
                    {
                        if (array[i] == letters[j]) { lettersCount++; }                       
                    }
                }
            }
            sr.Close();
            return lettersCount;
        }// Количество букв всего
        public static int LinesCount(string path)
        {
            string line = null;
            int linesCount=0;
            StreamReader sr = new StreamReader(path);
            while ((line = sr.ReadLine()) != null)
            {
                linesCount++;
            }
            sr.Close();
            return linesCount;
        }// Количество строк
        static void Main(string[] args)
        {

            string path = @"test.txt";
            string fileName = Path.GetFileName(path);
            long fileSize = new FileInfo(path).Length;
            string pathjson = Path.GetFileNameWithoutExtension(path);
            pathjson = string.Concat(pathjson, ".json");

            int linesCount = LinesCount(path);
            Console.WriteLine("linesCount - " + linesCount); // Кол-во строк

            int lettersCount = LettersCount(path);
            Console.WriteLine("LettersCount - " + lettersCount); // Кол-во букв

            int punctuationCount = Punctuation(path);
            Console.WriteLine("PunctuationCount - " + punctuationCount); // Кол-во знаков препинания (.,:;'")

            Dictionary<char, int> alphabet = new Dictionary<char, int>();
            alphabet = Letters(path);
            Console.WriteLine("Letters:");
            foreach (var item in alphabet) // Буквы и их кол-во
            Console.WriteLine(item.Key + " " + item.Value);

            int digitsCount = DigitsCount(path);
            Console.WriteLine("DigitsCount - " + digitsCount); // Кол-во цифр

            int wordsCount = WordsCount(path);
            Console.WriteLine("WordsCount - " + wordsCount); // Кол-во слов

            Dictionary<string, int> dic = new Dictionary<string, int>(); // Слова и их кол-во
            dic = Words(path);
            Console.WriteLine("Words:");
            foreach (var item in dic) //Вывод словаря
            Console.WriteLine(item.Key + " " + item.Value);

            int numberCount = NumbersCount(path);
            Console.WriteLine("NumbersCount - " + numberCount); // Кол-во чисел

            int WithHyphen = WordsWithHyphen(path);
            Console.WriteLine("WordsWithHyphen - " + WithHyphen);// Дефис

            string longestWord = LongestWord(path);
            Console.WriteLine("LongestWord - " + longestWord); // Самое длинное слово

            var jsonObj = new
            {
                filename = fileName,
                fileSize = fileSize,
                lettersCount = lettersCount,
                letters = alphabet,
                wordsCount = wordsCount,
                words = dic,
                linesCount = linesCount,
                digitsCount = digitsCount,
                numbersCount = numberCount,
                longestWord = longestWord,
                wordsWithHyphen = WithHyphen,
                punctuations = punctuationCount
            }; // Анонимный объект JSON


            var json = JsonConvert.SerializeObject(jsonObj, Formatting.Indented); //Сериализация в JSON
                File.WriteAllText(pathjson, json); //Запись в файл JSON













                Console.WriteLine("Для выхода нажмите любую клавишу");
                Console.ReadKey();
           
        }
        
    }
}
