using System;
using Game;
using Исходник;

namespace Tetris
{
    /// <summary>
    /// Главный класс программы, содержащий метод <c>Main</c> и меню выбора.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Точка входа в программу.
        /// </summary>
        static void Main()
        {
            bool exitProgram = false;
            while (!exitProgram)
            {
                int choice = ShowMenu();
                switch (choice)
                {
                    case 1:
                        GuessingGame.PlayGuessingGame();
                        break;
                    case 2:
                        ShowAuthorInfo();
                        break;
                    case 3:
                        RunArrayOperations();
                        break;
                    case 4:
                        Playingtetris();
                        break;
                    case 5:
                        exitProgram = ConfirmExit();
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор. Пожалуйста, попробуйте снова.");
                        break;
                }
            }
        }

        /// <summary>
        /// Запускает игру "Тетрис".
        /// </summary>
        private static void Playingtetris()
        {
            TerisClass tetrisGame = new TerisClass();
            tetrisGame.StartTetrisGame();
        }

        /// <summary>
        /// Отображает меню и получает выбор пользователя.
        /// </summary>
        /// <returns>Возвращает выбранный пункт меню.</returns>
        static int ShowMenu()
        {
            Console.WriteLine("\nВыберите нужный пункт меню:");
            Console.WriteLine("1. Отгадай ответ");
            Console.WriteLine("2. Об авторе");
            Console.WriteLine("3. Работа с массивом");
            Console.WriteLine("4. Игра Тетрис");
            Console.WriteLine("5. Выход");
            return InputValidator.GetIntInput("Введите номер пункта меню: ");
        }

        /// <summary>
        /// Запускает операции с массивами.
        /// </summary>
        static void RunArrayOperations()
        {
            Console.WriteLine("Использовать размер массива по умолчанию (10 элементов)? (y/n)");
            string response = Console.ReadLine();
            ArrayHelper arrayHelper;
            if (response.ToLower() == "y")
            {
                arrayHelper = new ArrayHelper();
            }
            else
            {
                int size = InputValidator.GetIntInput("Введите количество элементов в массиве: ", true);
                arrayHelper = new ArrayHelper(size);
            }
            arrayHelper.PerformArrayOperations();
        }

        /// <summary>
        /// Отображает информацию об авторе.
        /// </summary>
        static void ShowAuthorInfo()
        {
            Console.WriteLine("ФИО: Картеяв Глеб Владимирович");
            Console.WriteLine("Группа: 6106-090301D");
        }

        /// <summary>
        /// Подтверждает выход из программы.
        /// </summary>
        /// <returns>Возвращает <c>true</c>, если пользователь подтвердил выход; иначе <c>false</c>.</returns>
        static bool ConfirmExit()
        {
            Console.WriteLine("Чтобы выйти, нажмите y. Чтобы отменить, нажмите любую другую клавишу.");
            string choice = Console.ReadLine();
            return choice.ToLower() == "y";
        }
    }
}
