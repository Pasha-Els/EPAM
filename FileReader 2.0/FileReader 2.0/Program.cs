using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace FileReader_2._0
{
    
    
    class Program
    {
        static Dictionary<long, string> dictionary(string path) //Заполнение словаря
        {
            Dictionary<long, string> dic = new Dictionary<long, string>();
            long count = 0;
            StreamReader sr = new StreamReader(path);
            string line = null;
            while((line=sr.ReadLine())!=null)
            {
                
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
        static long LinesCount(string path) // Кол-во строк
        {
            string line = null;
            long linesCount = 0;
            StreamReader sr = new StreamReader(path);
            while ((line = sr.ReadLine()) != null)
            {
                linesCount++;
            }
            sr.Close();
            return linesCount;
        }
        static Dictionary<char,long> LettersCount(string path, Dictionary<long,string> dic, out long lettersCount) //Кол-во букв
        {
            lettersCount = 0;
            char[] array;
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
            Dictionary<char, long> alphabet = new Dictionary<char, long>(); // Словарь английского алфавита
            for (long i = 0; i < letters.Length;i++ ) // Заполнение словаря
            {
                alphabet.Add(letters[i],0);
            }
            for (long i = 0; i < dic.Count; i++)
            {
                array = dic[i].ToCharArray();
                for (long j= 0; j<array.Length-1; j++)
                {
                    if (alphabet.ContainsKey(array[j]))
                    {
                        alphabet[array[j]]=alphabet[array[j]]+1;
                        lettersCount++;
                    }
                }
            }
            
            return alphabet;
        }
        static long Punctuation(string path,Dictionary<long,string> dic) // Количество знаков препинания (.,:;'")
        {
            long punctuationCount = 0;
            char[] array;
            char[] punctuationarray = new char[] { '.', ',', ';', ':', '"', '\'', '@', ' ', '”', '“'};
            for (long i = 0; i < dic.Count; i++) // Перебор элемента словаря
			{
                array=dic[i].ToCharArray();
                for (long j = 0; j < array.Count(); j++)
                {
                    for (long q = 0; q < punctuationarray.Count(); q++)
                    {
                         if (punctuationarray[q]==array[j])
                            {
                                punctuationCount++;
                            } 
                    }
                   
                }
            }
            return punctuationCount;
        }
        public static long WordsWithHyphen(string path,Dictionary<long,string> dic) // Кол-во слов с дефисом
        {
            long count = 0;;
            char[] array;
            for (int i = 0; i < dic.Count; i++)
            {
                dic[i] = dic[i].ToLower();
                array = dic[i].ToCharArray();
                for (long j = 0; j < array.Length; j++)
                {
                    if (array[0] >= 'a' && array[0] <= 'z') // Проверка идет по первому символу
                    {
                        if (array[j] == '-') // Проверка на дефис
                        { count++; }
                    }

                }
            } 
            return count;
        }
        public static long NumbersCount(string path,Dictionary<long,string> dic,out long digitsCount) // Кол-во чисел
        {
            digitsCount = 0;
            long count = 0;
            char[] array;
            for (int i = 0; i < dic.Count; i++)
			{
                array = dic[i].ToCharArray();
                for (int j = 0; j < array.Count(); j++)
                {
                    if (array[0] >= '0' && array[0] <= '9') { count++; } // Проверка идет по первому символу
                    if (array[j] >= '0' && array[j] <= '9') { digitsCount++; }
                }
  
            }
            return count;
        }
        static Dictionary<string, long> Words(string path,Dictionary<long,string> dic, out long wordsCount) // Кол-во каждого слова
        {
            wordsCount = 0;
            Dictionary<string, long> words = new Dictionary<string, long>();
            char[] array;
            for (int i = 0; i < dic.Count; i++)
            {
                dic[i]=dic[i].ToLower();
                array=dic[i].ToCharArray();
                if ((array[0] >= 'a' && array[0] <= 'z') || (array[0] == '“' && array[1] >= 'a' && array[1] <= 'z'))
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
        static string LongestWord(string path,Dictionary<long,string> dic) // Самое длинное слово
        {
            string wordWithMaxLength = string.Empty;
            char[] array;
            for (int i = 0; i < dic.Count; i++)
            {
                dic[i] = dic[i].ToLower();
                array = dic[i].ToCharArray();
                if ((array[0] >= 'a' && array[0] <= 'z') || (array[0] == '“' && array[1] >= 'a' && array[1] <= 'z'))
                {
                    
                        if (dic[i].Length > wordWithMaxLength.Length)
                        {
                            wordWithMaxLength = dic[i];
                        }
                        
                }
            }


            return wordWithMaxLength;
        } 

        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь или название файла(с расширением)");
            string path = Console.ReadLine();
            //string path = @"Tolstoy Leo. War and Peace.txt";
            string fileName = Path.GetFileName(path);
            long fileSize = new FileInfo(path).Length;
            string pathjson = Path.GetFileNameWithoutExtension(path);
            pathjson = string.Concat(pathjson, ".json");
            Console.WriteLine("Чтение файла");
            Dictionary<long, string> dic = dictionary(path);
            Console.WriteLine("Чтение завершено");
            long linesCount = LinesCount(path);
            long lettersCount = 0;
            Dictionary<char, long> letters = LettersCount(path, dic, out lettersCount);
            long punctuationCount = Punctuation(path, dic);
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
            Console.ReadKey();
        }
    }
}
