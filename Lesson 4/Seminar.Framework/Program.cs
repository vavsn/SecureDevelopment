using System;

namespace Seminar.Framework
{
    internal class Program
    {
        public abstract class BaseException : Exception { }
        public class ExceptionUsingProgramm : BaseException { }
        static void Main(string[] args)
        {
#if DEBUG
            Console.Title = Properties.Settings.Default.ApplicationNameDebug;
#else
            Console.Title = Properties.Settings.Default.ApplicationName;

#endif
            if (string.IsNullOrEmpty(Properties.Settings.Default.Fio) || Properties.Settings.Default.Age <= 0
                || string.IsNullOrEmpty(Properties.Settings.Default.Country) || string.IsNullOrEmpty(Properties.Settings.Default.City))
            {
                Console.Write("Введите ФИО: ");
                Properties.Settings.Default.Fio = Console.ReadLine();

                Console.Write("Укажите ваш возраст: ");
                if (int.TryParse(Console.ReadLine(), out int age))
                {
                    Properties.Settings.Default.Age = age;
                }
                else
                {
                    Properties.Settings.Default.Age = 0;
                }

                Console.Write("Введите страну: ");
                string country = Console.ReadLine();
                if (string.Compare(country, Properties.Settings.Default.BannedCountry) != 0)
                {
                    Properties.Settings.Default.Country = country;
                }
                else
                {
                    Console.Write("В вашей стране запрещено использовать эту программу!");
                    throw new ExceptionUsingProgramm();
                }

                Console.Write("Введите город: ");
                Properties.Settings.Default.City = Console.ReadLine();

                Properties.Settings.Default.Save();
            }

            Console.WriteLine($"ФИО: {Properties.Settings.Default.Fio}");
            Console.WriteLine($"Возраст: {Properties.Settings.Default.Age}");
            Console.WriteLine($"ФИО: {Properties.Settings.Default.Country}");
            Console.WriteLine($"Возраст: {Properties.Settings.Default.City}");

            Console.ReadKey(true);

        }
    }
}
