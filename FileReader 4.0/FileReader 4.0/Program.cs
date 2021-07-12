using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace FileReader_4._0
{
    class Program
    {
        static Dictionary<long, string> Read(string path, out long linesCount, out long punctuationCount) // Коллекция слов и цифр
        {
            punctuationCount = 0;
            linesCount = 0;
            Dictionary<long, string> dic = new Dictionary<long,string>();
            StreamReader sr = new StreamReader(path,Encoding.Default);
            string line = null;
            char[] array;
            int count = 0;
            char[] punctuationarray = new char[] { '.', ',', ';', ':', '"', '\'', ' ' }; // Массив пунктуационных знаков
            while ((line = sr.ReadLine()) != null) // Чтение файла построчно
            {
                linesCount++; // Кол-во строк
                array = line.ToCharArray();
                for (long i = 0; i < array.Count(); i++) //Кол-во знаков препинания
                {
                    for (long j = 0; j < punctuationarray.Count(); j++)
                    {
                        if (array[i] == punctuationarray[j]) { punctuationCount++; }
                    }
                }
                line = Regex.Replace(line, @"[^a-zA-Z0-9 -]", " "); // Убираем все символы (кроме букв и цифр)
                string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (long i = 0; i < words.Count(); i++)
                {
                    if (!dic.ContainsValue(words[i]))
                    {
                        dic.Add(count, words[i]);
                        count++;
                    }
                }

            }
            sr.Close();
            return dic;
        } 
        static Dictionary<string, long> Words(string path, Dictionary<long, string> dic, out long wordsCount) // Коллекция слов
        {
            wordsCount = 0;
            char[] array;
            Dictionary<string, long> words = new Dictionary<string, long>();
            for (int i = 0; i < dic.Count(); i++)
            {
                dic[i] = dic[i].ToLower();
                array=dic[i].ToCharArray();
                if (char.IsLetter(array[0])) 
                {
                    wordsCount++;
                    if (!words.ContainsKey(dic[i]))
                    {
                        words.Add(dic[i], 1); // Добавление нового слова
                    }
                    else
                    {
                        words[dic[i]] = words[dic[i]] + 1; // +1 к счётчику слова
                    }
                }
                
            }



            words = words.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value); // Сортировка словаря по убыванию
            return words;
        }
        static long NumbersCount(string path, Dictionary<long, string> dic, out long digitsCount) // Кол-во чисел
        {

            digitsCount = 0;
            long numbersCount = 0;
            char[] array;
            for (long i = 0; i < dic.Count; i++)
            {
                array = dic[i].ToCharArray();
                if (char.IsDigit(array[0]))
                {
                    numbersCount++;
                    for (long j = 0; j < array.Count(); j++) { digitsCount++; }
                }
            }
            return numbersCount;
        }
        static Dictionary<char, long> Letters(string path, Dictionary<long, string> dic, out long lettersCount) //Кол-во букв
        {
            lettersCount = 0;
            char[] array;
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
            Dictionary<char, long> alphabet = new Dictionary<char, long>(); // Словарь английского алфавита
            for (long i = 0; i < letters.Length; i++) // Заполнение словаря
            {
                alphabet.Add(letters[i], 0);
            }
            for (long i = 0; i < dic.Count; i++)
            {
                array = dic[i].ToCharArray();
                for (long j = 0; j < array.Length - 1; j++)
                {
                    if (alphabet.ContainsKey(array[j]))
                    {
                        alphabet[array[j]] = alphabet[array[j]] + 1;
                        lettersCount++;
                    }
                }
            }
            alphabet = alphabet.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value); // Сортировка по убыванию
            return alphabet;
        }
        static long WordsWithHyphen(string path, Dictionary<long, string> dic) // Кол-во слов с дефисом
        {
            char[] array;
            long count = 0;
            for (long i = 0; i < dic.Count(); i++) // Кол-во слов с дефисом
            {
                array = dic[i].ToCharArray();
                if (!dic[i].StartsWith("-"))
                {
                    for (long j = 0; j < array.Count(); j++)
                    {
                        if (array[j] == '-') { count++; }
                    }
                }
            }
            return count;
        }
        static string LongestWord(string path, Dictionary<long, string> dic) // Самое длинное слово
        {
            string longestWord = string.Empty;
            for (long i = 0; i < dic.Count(); i++) // Длинное слово
            {
                if (char.IsLetter(dic[i].ToCharArray()[0]))
                {
                    if (dic[i].Length > longestWord.Length)
                    {
                        longestWord = dic[i];
                    }
                }
                
            }


            return longestWord;
        } 
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь или название файла(с расширением)");
            string path = Console.ReadLine();
            //string path = @"Tolstoy Leo. War and Peace.txt";
            if (File.Exists(path)) // Проверка на наличие файла
            {
                Console.WriteLine("Чтение файла");
                string fileName = Path.GetFileName(path);
                long fileSize = new FileInfo(path).Length;
                string pathjson = Path.GetFileNameWithoutExtension(path);
                pathjson = string.Concat(pathjson, ".json");
                long linesCount;
                long punctuationCount;
                long lettersCount;
                Dictionary<long,string> dic = Read(path, out linesCount, out punctuationCount);
                Dictionary<char, long> letters = Letters(path, dic, out lettersCount);
                long wordsWithHyphen = WordsWithHyphen(path, dic);
                long digitsCount = 0;
                long numbersCount = NumbersCount(path, dic, out digitsCount);
                long wordsCount = 0;
                Dictionary<string, long> words = Words(path, dic, out wordsCount);
                string longestWord = LongestWord(path, dic);
            
            var jsonObj = new
                {
                    filename = fileName,
                    fileSize = fileSize,
                    lettersCount = lettersCount,
                    letters = letters,
                    wordsCount = wordsCount,
                    words = words,
                    linesCount = linesCount,
                    digitsCount = digitsCount,
                    numbersCount = numbersCount,
                    longestWord = longestWord,
                    wordsWithHyphen = wordsWithHyphen,
                    punctuations = punctuationCount
                }; // Анонимный объект JSON


                var json = JsonConvert.SerializeObject(jsonObj, Formatting.Indented); //Сериализация в JSON
                Console.WriteLine(json);
                File.WriteAllText(pathjson, json); //Запись в файл JSON
                Console.WriteLine("Для выхода нажмите любую клавишу...");
                Console.ReadKey();
            }
            else
            { 
                Console.WriteLine("Файл не найден!");
                Console.WriteLine("Для выхода нажмите любую клавишу...");
                Console.ReadKey();
            }
        }
    }
}
