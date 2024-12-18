using System;
using System.Diagnostics;
using System.Linq;

namespace Tetris
{
    /// <summary>
    /// Класс, содержащий методы для работы с массивами.
    /// </summary>
    class ArrayHelper
    {
        /// <summary>
        /// Количество элементов в массиве.
        /// </summary>
        private int n;

        /// <summary>
        /// Массив целых чисел.
        /// </summary>
        private int[] array;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ArrayHelper"/> с числом элементов по умолчанию (10).
        /// </summary>
        public ArrayHelper()
        {
            n = 10;
            array = new int[n];
            InitializeArray();
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ArrayHelper"/> с указанным количеством элементов.
        /// </summary>
        /// <param name="size">Количество элементов в массиве.</param>
        public ArrayHelper(int size)
        {
            n = size;
            array = new int[n];
            InitializeArray();
        }

        /// <summary>
        /// Возвращает массив.
        /// </summary>
        /// <returns>Массив целых чисел.</returns>
        public int[] GetArray()
        {
            return array;
        }

        /// <summary>
        /// Выполняет основные операции с массивом: отображает исходный массив, сортирует его разными методами и выводит результаты.
        /// </summary>
        public void PerformArrayOperations()
        {
            Console.WriteLine("Исходный массив:");
            PrintArray(array);

            int[] bubbleSortArray = CloneArray(array);
            int[] insertionSortArray = CloneArray(array);

            var bubbleSortTime = MeasureSortingTime(() => BubbleSort(bubbleSortArray));
            Console.WriteLine($"Время выполнения пузырьковой сортировки: {bubbleSortTime} мс");
            PrintArray(bubbleSortArray);

            var insertionSortTime = MeasureSortingTime(() => InsertionSort(insertionSortArray));
            Console.WriteLine($"Время выполнения сортировки вставками: {insertionSortTime} мс");
            PrintArray(insertionSortArray);
        }

        /// <summary>
        /// Инициализирует массив случайными числами от 1 до 99.
        /// </summary>
        private void InitializeArray()
        {
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(1, 100);
            }
        }

        /// <summary>
        /// Создает копию указанного массива.
        /// </summary>
        /// <param name="sourceArray">Исходный массив для копирования.</param>
        /// <returns>Новый массив, содержащий копию исходного массива.</returns>
        private int[] CloneArray(int[] sourceArray)
        {
            int[] newArray = new int[sourceArray.Length];
            Array.Copy(sourceArray, newArray, sourceArray.Length);
            return newArray;
        }

        /// <summary>
        /// Выводит элементы массива на консоль.
        /// </summary>
        /// <param name="array">Массив для вывода.</param>
        private void PrintArray(int[] array)
        {
            if (array.Length > 10)
            {
                Console.WriteLine("Массив слишком длинный для вывода на экран (более 10 элементов).");
            }
            else
            {
                Console.WriteLine("Массив: " + string.Join(", ", array));
            }
        }

        /// <summary>
        /// Измеряет время выполнения заданного метода сортировки.
        /// </summary>
        /// <param name="sortMethod">Делегат метода сортировки.</param>
        /// <returns>Время выполнения в миллисекундах.</returns>
        private long MeasureSortingTime(Action sortMethod)
        {
            var stopwatch = Stopwatch.StartNew();
            sortMethod();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Сортирует массив методом пузырьковой сортировки.
        /// </summary>
        /// <param name="arr">Массив для сортировки.</param>
        public void SortArray()
        {
            BubbleSort(array);
        }

        /// <summary>
        /// Сортирует массив методом пузырьковой сортировки.
        /// </summary>
        /// <param name="arr">Массив для сортировки.</param>
        private void BubbleSort(int[] arr)
        {
            int length = arr.Length;
            for (int i = 0; i < length - 1; i++)
            {
                for (int j = 0; j < length - i - 1; j++)
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

        /// <summary>
        /// Сортирует массив методом вставок.
        /// </summary>
        /// <param name="arr">Массив для сортировки.</param>
        private void InsertionSort(int[] arr)
        {
            int length = arr.Length;
            for (int i = 1; i < length; i++)
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

        /// <summary>
        /// Находит максимальное значение в массиве.
        /// </summary>
        /// <returns>Максимальное значение в массиве.</returns>
        public int FindMax()
        {
            return array.Max();
        }

        /// <summary>
        /// Находит минимальное значение в массиве.
        /// </summary>
        /// <returns>Минимальное значение в массиве.</returns>
        public int FindMin()
        {
            return array.Min();
        }

        /// <summary>
        /// Вычисляет среднее арифметическое значение массива.
        /// </summary>
        /// <returns>Среднее арифметическое значение массива.</returns>
        public double CalculateAverage()
        {
            return array.Average();
        }
    }
}

