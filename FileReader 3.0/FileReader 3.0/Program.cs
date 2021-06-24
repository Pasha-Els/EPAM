using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace FileReader_3._0
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь или название файла(с расширением)");
            //string path = Console.ReadLine();
            string path = @"Tolstoy Leo. War and Peace.txt";
            if (File.Exists(path)) // Проверка на наличие файла
            {
                string fileName = Path.GetFileName(path); // Имя файла
                long fileSize = new FileInfo(path).Length; // Размер файла
                string pathjson = Path.GetFileNameWithoutExtension(path); // Имя файла без расширения
                pathjson = string.Concat(pathjson, ".json"); // Имя для файла JSON
                Dictionary<string, long> wordsDic = new Dictionary<string, long>(); // Коллекция слов
                Dictionary<char, long> letters = new Dictionary<char, long>(); // Коллекция букв
                long wordsWithHyphen = 0;
                long punctuationCount = 0;
                long wordsCount = 0;
                long lettersCount = 0;
                long digitsCount = 0;
                long numbersCount = 0;
                long linesCount = 0;
                string longestWord = "";
                Console.WriteLine("Чтение файла");
                StreamReader sr = new StreamReader(path, Encoding.Default);
                string line = null;
                char[] array;
                char[] punctuationarray = new char[] { '.', ',', ';', ':', '"', '\'', ' ' }; // Массив пунктуационных знаков 
                char[] lettersArray = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
                for (long i = 0; i < lettersArray.Length - 1; i++) // Заполнение коллекции с буквами
                {
                    letters.Add(lettersArray[i], 0);
                }
                while ((line = sr.ReadLine()) != null) // Чтение файла построчно
                {
                    linesCount++;
                    array = line.ToCharArray();
                    for (long i = 0; i < array.Count(); i++) //Кол-во знаков препинания
                    {
                        for (long j = 0; j < punctuationarray.Count(); j++)
                        {
                            if (array[i] == punctuationarray[j]) { punctuationCount++; }
                        }
                    }
                    line = Regex.Replace(line, @"[^a-zA-Z0-9 -]", ""); // Убираем все символы (кроме букв и цифр)
                    string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (long i = 0; i < line.Length; i++) // Кол-во букв
                    {
                        array = line.ToCharArray();
                        if (letters.ContainsKey(array[i]))
                        {
                            letters[array[i]] = letters[array[i]] + 1;
                            lettersCount++;
                        }
                    }
                    letters = letters.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value); // Сортировка по убыванию
                    for (long i = 0; i < words.Count(); i++) // Кол-во цифр и чисел
                    {
                        array = words[i].ToCharArray();
                        if (char.IsDigit(array[0]))
                        {
                            numbersCount++;
                            for (long j = 0; j < array.Count(); j++) { digitsCount++; }
                        }
                    }
                    line = Regex.Replace(line, @"[^a-zA-Z -]", ""); // Убираем все символы (кроме букв и цифр)
                    line = line.ToLower();
                    words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (long i = 0; i < words.Count(); i++) // Кол-во слов
                    {
                        
                            if (!wordsDic.ContainsKey(words[i]))
                        {
                            wordsDic.Add(words[i], 1); // Добавление нового слова
                            wordsCount++;
                        }
                        else
                        {
                            wordsDic[words[i]] = wordsDic[words[i]] + 1; // +1 к счётчику слова
                            wordsCount++;
                        }
                        
                        
                    }
                    wordsDic = wordsDic.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value); // Сортировка по убыванию
                    

                    for (long i = 0; i < words.Count(); i++) // Кол-во слов с дефисом
                    {
                        array = words[i].ToCharArray();
                        if (!words[i].StartsWith("-"))
                        {
                            for (long j = 0; j < array.Count(); j++)
                            {
                                if (array[j] == '-') { wordsWithHyphen++; }
                            }
                        }
                    }
                    for (long i = 0; i < words.Count(); i++) // Длинное слово
                    {
                         if (words[i].Length > longestWord.Length)
                        {
                            longestWord = words[i];
                        }
                    }
                }
                Console.WriteLine("Чтение завершено");

                sr.Close();
                var jsonObj = new
                {
                    filename = fileName,
                    fileSize = fileSize,
                    lettersCount = lettersCount,
                    letters = letters,
                    wordsCount = wordsCount,
                    words = wordsDic,
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
