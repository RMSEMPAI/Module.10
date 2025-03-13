using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practicum
{
    public interface IAdder
    {
        double Add(double a, double b);
    }

    /// <summary>
    /// Реализация интерфейса
    /// </summary>
    public class Adder : IAdder
    {
        private readonly ILogger _logger;

        // Внедрение зависимости через конструктор
        public Adder(ILogger logger)
        {
            _logger = logger;
        }

        public double Add(double a, double b)
        {
            _logger.LogEvent($"Выполняется сложение чисел: {a} и {b}");
            double result = a + b;
            _logger.LogEvent($"Результат сложения: {result}");
            return result;
        }
    }

    /// <summary>
    /// Интерфейс для логгера
    /// </summary>
    public interface ILogger
    {
        void LogEvent(string message);
        void LogError(string message);
    }

    /// <summary>
    /// Реализация логгера
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        public void LogEvent(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"[Событие] {message}");
            Console.ResetColor();
        }

        public void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[Ошибка] {message}");
            Console.ResetColor();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = new ConsoleLogger();

            IAdder adder = new Adder(logger);

            try
            {
                Console.Write("Введите первое число: ");
                double number1 = Convert.ToDouble(Console.ReadLine());

                Console.Write("Введите второе число: ");
                double number2 = Convert.ToDouble(Console.ReadLine());

                double result = adder.Add(number1, number2);

                Console.WriteLine($"Результат сложения: {result}");
            }
            catch (FormatException)
            {
                logger.LogError("Введено некорректное число.");
            }
            catch (Exception ex)
            {
                logger.LogError($"Произошла ошибка: {ex.Message}");
            }
            finally
            {
                logger.LogEvent("Программа завершена.");
            }
        }
    }
}
