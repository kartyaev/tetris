using System;
using System.Threading;
using System.Diagnostics;
namespace Tetris
{
    class Program
    {
        static void Main()
        {
            bool exitProgram = false;
            while (!exitProgram)
            {
                int choice = ShowMenu();
                switch (choice)
                {
                    case 1:
                        PlayGuessingGame();
                        break;
                    case 2:
                        ShowAuthorInfo();
                        break;
                    case 3:
                        SortArray();
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

        private static void Playingtetris()
        {
            Game.TerisClass.StartTetrisGame();
        }

        static int ShowMenu()
        {
            Console.WriteLine("\nВыберите нужный пункт меню:");
            Console.WriteLine("1. Отгадай ответ");
            Console.WriteLine("2. Об авторе");
            Console.WriteLine("3. Сортировка массива");
            Console.WriteLine("4. Игра Тетрис");
            Console.WriteLine("5. Выход");
            return GetIntInput("Введите номер пункта меню: ");
        }
        static void PlayGuessingGame()
        {
            double a = GetDoubleInput("Введите значение переменной A (больше 0): ", true);
            double b = GetDoubleInput("Введите значение переменной B (больше 0): ", true);
            double correctAnswer = CalculateFunction(a, b);
            GuessTheAnswer(correctAnswer);
        }
        static double GetDoubleInput(string message, bool greaterThanZero = false)
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
        static double CalculateFunction(double a, double b)
        {
            return Math.Round((Math.Pow(Math.Log(b), 2)) / (Math.Cos(a) - 1), 2);
        }
        static void GuessTheAnswer(double correctAnswer)
        {
            short attempts = 0;
            while (attempts < 3)
            {
                double userGuess = GetDoubleInput("Введите ваше предположение: ");
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
        static void ShowAuthorInfo()
        {
            Console.WriteLine("ФИО: Картеяв Глеб Владимирович");
            Console.WriteLine("Группа: 6106-090301D");
        }
        static void SortArray()
        {
            int n = GetArrayLength();
            int[] originalArray = CreateAndInitializeArray(n);
            Console.WriteLine("Исходный массив:");
            PrintArray(originalArray);
            int[] bubbleSortArray = CloneArray(originalArray);
            int[] insertionSortArray = CloneArray(originalArray);
            var bubbleSortTime = MeasureSortingTime(() => BubbleSort(bubbleSortArray));
            Console.WriteLine($"Время выполнения пузырьковой сортировки: {bubbleSortTime} мс");
            PrintArray(bubbleSortArray);
            var insertionSortTime = MeasureSortingTime(() => InsertionSort(insertionSortArray));
            Console.WriteLine($"Время выполнения сортировки вставками: {insertionSortTime} мс");
            PrintArray(insertionSortArray);
        }
        static bool ConfirmExit()
        {
            Console.WriteLine("Чтобы выйти нажмите y, чтобы отменить n ");
            string choice = Console.ReadLine();
            return choice.ToLower() == "y";
        }
        static int GetArrayLength()
        {
            return GetIntInput("Введите длину массива (больше 0): ", true);
        }
        static int[] CreateAndInitializeArray(int length)
        {
            int[] array = new int[length];
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                array[i] = random.Next(1, 100);
            }
            return array;
        }
        static int[] CloneArray(int[] sourceArray)
        {
            int[] newArray = new int[sourceArray.Length];
            Array.Copy(sourceArray, newArray, sourceArray.Length);
            return newArray;
        }
        static void PrintArray(int[] array)
        {
            if (array.Length > 10)
            {
                Console.WriteLine("Массивы не могут быть выведены на экран, так как длина массива больше 10.");
            }
            else
            {
                Console.WriteLine("Массив: " + string.Join(", ", array));
            }
        }
        static long MeasureSortingTime(Action sortMethod)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            sortMethod();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
        static void BubbleSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }
        static void InsertionSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 1; i < n; i++)
            {
                int key = arr[i];
                int j = i - 1;
                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = key;
            }
        }
        static int GetIntInput(string message, bool greaterThanZero = false)
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

    }
}
