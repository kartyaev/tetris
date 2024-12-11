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
            throw new NotImplementedException();
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

        const int max_speed = 9;
        const double default_speed = 0.9;

        const string msg1 = "Game Over";
        const string msg2 = "Нажмите на любую кнопку что бы завершить";
        const string msg3 = "Поздравляю,";
        const string msg5 = "Нажмите на клавишу чтобы начать ";
        const string msg6 = "Нажмите на любую кнопку что бы продолжить";

        static int Speed = 1;
        static int Score = 0;
        static int Lines = 0;

        static Point ptBlock = new Point(); //позиция
        static WindowRect wrBlockAdj = new WindowRect(); // регулировка блока поворот жесткий
        static WindowRect PlayWindow = new WindowRect(); // размер игрового поля
        static bool isRows = false;
        static bool isGameExit = false;

        static Game.StructBlock nextBlock = new Game.StructBlock();
        static Game.StructBlock currBlock = new Game.StructBlock();

        static ConsoleKeyInfo kb;
        static Game.TerisClass Tetris;

        static void Playingtetris(string[] args)
        {



        }
        private static void Tetris_Process(object o, Game.EventArgs e)
        {
            if (e.RowsCompleted > 0)
            {
                isRows = true;
                Score += e.RowsCompleted * (e.RowsCompleted > 1 ? 15 : 10);
                Lines += e.RowsCompleted;
                if((Lines >=11) && (Lines <= 20))
                {
                    Speed = 2;
                }
                else if ((Lines >= 21) && (Lines <= 30))
                {
                    Speed = 3;
                }
                else if ((Lines >= 31) && (Lines <= 40))
                {
                    Speed = 4;
                }
                else if ((Lines >= 41) && (Lines <= 50))
                {
                    Speed = 5;
                }
                else if ((Lines >= 51) && (Lines <= 60))
                {
                    Speed = 6;
                }
                else if ((Lines >= 61) && (Lines <= 70))
                {
                    Speed = 7;
                }
                else if ((Lines >= 71) && (Lines <= 80))
                {
                    Speed = 8;
                }
                else if ((Lines >= 81) && (Lines <= 90))
                {
                    Speed = 9;
                }
                ShowStatus();

            }









        }



        private static void ShowStatus() // жесточайшее меню, отображение скороти, очко, линии
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(PlayWindow.width + PlayWindow.left + 3, 2);
            Console.WriteLine("Счет");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(PlayWindow.width + PlayWindow.left + 3, 2);
            Console.WriteLine("Линии");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(PlayWindow.width + PlayWindow.left + 3, 2);
            Console.WriteLine("Speed");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(PlayWindow.width + PlayWindow.left + 2, 3);
            Console.WriteLine(String.Format("{0:D8}"), Score);
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(PlayWindow.width + PlayWindow.left + 2, 6);
            Console.WriteLine(String.Format("{0:D8}"),Lines);
            Console.SetCursorPosition(PlayWindow.width + PlayWindow.left + 5, 18);
            Console.WriteLine(String.Format("{0:D82}"), Speed);

            if (Lines >= 100)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition((Console.WindowWidth - msg3.Length) / 2,
                    Console.WindowWidth / 2);

                Console.WriteLine(msg3);
                Console.ReadKey();
                Console.ResetColor();

                //sndEffect.Play(global: :Tetris.Properties.Resorces.S104);
                isGameExit = true;
            }
        }
    private static void ShowNextBlock()
        {
            nextBlock = Tetris.Block.Generate();

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(PlayWindow.width + PlayWindow.left + 2, 8);
            Console.Write("Следующий");
            Console.SetCursorPosition(PlayWindow.width + PlayWindow.left + 2, 9);
            Console.Write("▄▄▄▄▄▄▄▄"); //границы АЛТ 219

            for(int i = 1; i < 6; i++)
            {
                Console.SetCursorPosition(PlayWindow.width + PlayWindow.left + 2, i+9);
                Console.Write("▌▌▌▌▌▌▌▌");
            }
            Console.SetCursorPosition(PlayWindow.width + PlayWindow.left + 2,9);
            Console.Write("▀▀▀▀▀▀▀▀");
            Console.ResetColor ();

            Tetris.Block.Preview(new Point(PlayWindow.width + PlayWindow.left +6, 12), nextBlock );

        }
    private static void PlayBlock(Game.StructBlock sbBlock, bool isNew)
        {
            if (isNew)
            {
                sbBlock = Tetris.Block.Generate();
                
            }
            else
            {
                Tetris.SendToField(ptBlock, wrBlockAdj);

            }
            Tetris.Block.Assign(sbBlock);
            Tetris.Block.Build();
            Tetris.Block.Adjustment(ref wrBlockAdj);

            ptBlock.x = (PlayWindow.left - wrBlockAdj.left) +(PlayWindow.width - wrBlockAdj.width) / 2;
            ptBlock.y = PlayWindow.top;

            Tetris.Block.Draw(ptBlock, wrBlockAdj, true);
            ShowNextBlock();

            if (Tetris.IsCollided(ptBlock, wrBlockAdj))
            {
                
            }
        }
    }
}