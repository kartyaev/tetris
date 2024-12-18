using System;
using Исходник;

namespace Game
{
    /// <summary>
    /// Статический класс, реализующий игру "Отгадай ответ".
    /// </summary>
    static class GuessingGame
    {
        /// <summary>
        /// Запускает игру "Отгадай ответ".
        /// </summary>
        public static void PlayGuessingGame()
        {
            double a = InputValidator.GetDoubleInput("Введите значение переменной A (больше 0): ", true);
            double b = InputValidator.GetDoubleInput("Введите значение переменной B (больше 0): ", true);
            double correctAnswer = CalculateFunction(a, b);
            GuessTheAnswer(correctAnswer);
        }

        /// <summary>
        /// Вычисляет значение функции в соответствии с заданными параметрами.
        /// </summary>
        /// <param name="a">Значение переменной A.</param>
        /// <param name="b">Значение переменной B.</param>
        /// <returns>Результат вычисления функции.</returns>
        public static double CalculateFunction(double a, double b)
        {
            return Math.Round((Math.Pow(Math.Log(b), 2)) / (Math.Cos(a) - 1), 2);
        }

        /// <summary>
        /// Осуществляет процесс угадывания правильного ответа пользователем.
        /// </summary>
        /// <param name="correctAnswer">Правильный ответ для сравнения с вводом пользователя.</param>
        public static void GuessTheAnswer(double correctAnswer)
        {
            short attempts = 0;
            while (attempts < 3)
            {
                double userGuess = InputValidator.GetDoubleInput("Введите ваше предположение: ");
                if (Math.Round(userGuess, 2) == correctAnswer)
                {
                    Console.WriteLine("Угадали!");
                    return;
                }
                else
                {
                    Console.WriteLine(attempts < 2 ? "Неверно, попробуйте еще раз." : $"Попытки закончились, правильный ответ: {correctAnswer}");
                }
                attempts++;
            }
        }
    }
}
