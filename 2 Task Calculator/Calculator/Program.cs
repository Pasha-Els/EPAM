using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            
            BigInteger a = new BigInteger();
            BigInteger b = new BigInteger();
            Console.WriteLine("Введите первое значение");
            a = BigInteger.Parse(Console.ReadLine());
            Console.WriteLine("Введите второое значение");
            b = BigInteger.Parse(Console.ReadLine());
            BigInteger sum = BigInteger.Abs(a + b);
            BigInteger sub = BigInteger.Subtract(a, b);
            BigInteger mult = BigInteger.Multiply(a, b);
            BigInteger ost = new BigInteger();
            BigInteger div = BigInteger.DivRem(a, b, out ost);
            Console.WriteLine("Сумма = "+sum);
            Console.WriteLine("Разность = "+sub);
            Console.WriteLine("Произведение = "+mult);
            Console.WriteLine("Частное = " + div);
            Console.WriteLine("Остаток от деления = "+ost);
            Console.ReadKey();
            


        }
    }
}
