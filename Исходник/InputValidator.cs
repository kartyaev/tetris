using System;
using System.Windows.Forms;

namespace Исходник
{
    /// <summary>
    /// Статический класс, содержащий методы для проверки вводимых данных.
    /// </summary>
    static class InputValidator
    {
        /// <summary>
        /// Получает целочисленный ввод от пользователя с дополнительной проверкой на положительность.
        /// </summary>
        /// <param name="message">Сообщение, выводимое пользователю перед вводом.</param>
        /// <param name="greaterThanZero">Если <c>true</c>, метод требует, чтобы введенное число было больше нуля.</param>
        /// <returns>Целое число, введенное пользователем.</returns>
        public static int GetIntInput(string message, bool greaterThanZero = false)
        {
            int value;
            do
            {
                Console.Write(message);
                while (!int.TryParse(Console.ReadLine(), out value))
                {
                    Console.WriteLine("Введите корректное целое число.");
                }
            } while (greaterThanZero && value <= 0);
            return value;
        }

        /// <summary>
        /// Получает ввод числа с плавающей точкой от пользователя с дополнительной проверкой на положительность.
        /// </summary>
        /// <param name="message">Сообщение, выводимое пользователю перед вводом.</param>
        /// <param name="greaterThanZero">Если <c>true</c>, метод требует, чтобы введенное число было больше нуля.</param>
        /// <returns>Число с плавающей точкой, введенное пользователем.</returns>
        public static double GetDoubleInput(string message, bool greaterThanZero = false)
        {
            double value;
            do
            {
                Console.Write(message);
                while (!double.TryParse(Console.ReadLine(), out value))
                {
                    Console.WriteLine("Введите корректное число.");
                }
            } while (greaterThanZero && value <= 0);
            return value;
        }

        /// <summary>
        /// Получает значение double из TextBox с проверкой на положительность.
        /// </summary>
        public static double GetDoubleFromTextBox(TextBox textBox, bool greaterThanZero = false)
        {
            double value;
            while (!double.TryParse(textBox.Text, out value) || (greaterThanZero && value <= 0))
            {
                MessageBox.Show("Введите корректное число.");
                return 0; // Или выбросьте исключение
            }
            return value;
        }
    }
}
