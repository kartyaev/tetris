using System;
using System.Collections.Generic;

namespace Game
{
    /// <summary>
    /// Класс, реализующий консольную игру Тетрис.
    /// </summary>
    class TerisClass
    {
        // Параметры игры (устанавливаются при выборе сложности)
        private int Rows;
        private int Cols;

        // Игровое поле
        private char[,] field;

        private Random rand = new Random();

        // Фигуры
        private List<char[,]> lineShape = new List<char[,]>
        {
            new char[,] { {'#','#','#','#'} },
            new char[,] { {'#'}, {'#'}, {'#'}, {'#'} }
        };

        private List<char[,]> squareShape = new List<char[,]>
        {
            new char[,] { {'#','#'}, {'#','#'} }
        };

        private List<char[,]> tShape = new List<char[,]>
        {
            new char[,] { {'#','#','#'}, {' ','#',' '} },
            new char[,] { {' ','#',' '}, {'#','#','#'} },
            new char[,] { {' ',' ','#'}, {' ','#','#'}, {' ',' ','#'} },
            new char[,] { {'#',' ',' '}, {'#','#',' '}, {'#',' ',' '} },
        };

        private List<char[,]> lShape = new List<char[,]>
        {
            new char[,] { {'#',' ',' '}, {'#',' ',' '}, {'#','#','#'} },
            new char[,] { {'#','#','#'}, {'#',' ',' '}, {'#',' ',' '} },
            new char[,] { {'#','#','#'}, {' ',' ','#'}, {' ',' ','#'} },
            new char[,] { {' ',' ','#'}, {' ',' ','#'}, {'#','#','#'} }
        };

        private List<List<char[,]>> figures = new List<List<char[,]>>();

        private List<char[,]> currentFigureRotations;
        private int currentRotationIndex;
        private int currentFigureRow;
        private int currentFigureCol;

        private int score = 0;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TerisClass"/>.
        /// </summary>
        public TerisClass()
        {
            figures.Add(lineShape);
            figures.Add(squareShape);
            figures.Add(tShape);
            figures.Add(lShape);
        }

        /// <summary>
        /// Запускает игру Тетрис.
        /// </summary>
        public void StartTetrisGame()
        {
            ChooseDifficulty();

            field = new char[Rows, Cols];
            InitializeField();
            score = 0;

            bool gameOver = false;

            while (!gameOver)
            {
                // Создаем новую фигуру
                GenerateRandomFigure();
                currentFigureRow = 0;
                currentFigureCol = Cols / 2 - currentFigureRotations[currentRotationIndex].GetLength(1) / 2;

                if (!CanPlaceFigure(currentFigureRow, currentFigureCol, currentFigureRotations[currentRotationIndex]))
                {
                    // Не можем разместить новую фигуру — игра окончена
                    gameOver = true;
                    break;
                }

                PlaceFigure(currentFigureRow, currentFigureCol, currentFigureRotations[currentRotationIndex], true);

                bool figureFixed = false;
                while (!figureFixed && !gameOver)
                {
                    DrawField();
                    Console.WriteLine("Нажмите ←/A или →/D для движения, ↑/W для поворота, ↓/S для опускания, любую другую клавишу — следующий шаг (автопадение). O - Выход");

                    // Ожидаем нажатия клавиши
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    // Убираем фигуру, чтобы проверить новое положение
                    PlaceFigure(currentFigureRow, currentFigureCol, currentFigureRotations[currentRotationIndex], false);

                    switch (key.Key)
                    {
                        case ConsoleKey.LeftArrow:
                        case ConsoleKey.A:
                            if (CanPlaceFigure(currentFigureRow, currentFigureCol - 1, currentFigureRotations[currentRotationIndex]))
                            {
                                currentFigureCol--;
                            }
                            break;
                        case ConsoleKey.RightArrow:
                        case ConsoleKey.D:
                            if (CanPlaceFigure(currentFigureRow, currentFigureCol + 1, currentFigureRotations[currentRotationIndex]))
                            {
                                currentFigureCol++;
                            }
                            break;
                        case ConsoleKey.UpArrow:
                        case ConsoleKey.W:
                            int oldRotationIndex = currentRotationIndex;
                            currentRotationIndex = (currentRotationIndex + 1) % currentFigureRotations.Count;
                            if (!CanPlaceFigure(currentFigureRow, currentFigureCol, currentFigureRotations[currentRotationIndex]))
                            {
                                // Не можем повернуть, откатываемся
                                currentRotationIndex = oldRotationIndex;
                            }
                            break;
                        case ConsoleKey.O:
                            gameOver = true;
                            break;
                        case ConsoleKey.DownArrow:
                        case ConsoleKey.S:
                            if (CanPlaceFigure(currentFigureRow + 1, currentFigureCol, currentFigureRotations[currentRotationIndex]))
                            {
                                currentFigureRow++;
                            }
                            else
                            {
                                // Фигура упала окончательно
                                figureFixed = true;
                            }
                            break;
                        default:
                            if (CanPlaceFigure(currentFigureRow + 1, currentFigureCol, currentFigureRotations[currentRotationIndex]))
                            {
                                currentFigureRow++;
                            }
                            else
                            {
                                figureFixed = true;
                            }
                            break;
                    }

                    // Возвращаем фигуру на поле
                    PlaceFigure(currentFigureRow, currentFigureCol, currentFigureRotations[currentRotationIndex], true);

                    // Если фигура зафиксирована, проверяем линии
                    if (figureFixed)
                    {
                        int linesCleared = ClearCompleteLines();
                        score += linesCleared * 100;
                    }
                }
            }

            Console.Clear();
            Console.WriteLine("Игра окончена!");
            Console.WriteLine("Ваш счет: " + score);
            Console.WriteLine("Нажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        /// <summary>
        /// Предлагает пользователю выбрать уровень сложности и устанавливает параметры игры.
        /// </summary>
        private void ChooseDifficulty()
        {
            Console.Clear();
            Console.WriteLine("Выберите уровень сложности:");
            Console.WriteLine("1. Легкий (поле 12x10)");
            Console.WriteLine("2. Средний (поле 10x10)");
            Console.WriteLine("3. Сложный (поле 8x8)");
            Console.Write("Введите номер: ");
            string diffChoice = Console.ReadLine();

            switch (diffChoice)
            {
                case "1":
                    Rows = 12;
                    Cols = 10;
                    break;
                case "2":
                    Rows = 10;
                    Cols = 10;
                    break;
                case "3":
                    Rows = 8;
                    Cols = 8;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Установлен средний уровень.");
                    Rows = 10;
                    Cols = 10;
                    break;
            }
        }

        /// <summary>
        /// Инициализирует игровое поле пустыми значениями.
        /// </summary>
        private void InitializeField()
        {
            field = new char[Rows, Cols];
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    field[r, c] = ' ';
                }
            }
        }

        /// <summary>
        /// Отображает текущее состояние игрового поля и счет на консоли.
        /// </summary>
        private void DrawField()
        {
            Console.Clear();
            Console.WriteLine("Счет: " + score);
            for (int r = 0; r < Rows; r++)
            {
                Console.Write("|");
                for (int c = 0; c < Cols; c++)
                {
                    Console.Write(field[r, c]);
                }
                Console.WriteLine("|");
            }
            Console.WriteLine(new string('-', Cols + 2));
        }

        /// <summary>
        /// Генерирует новую случайную фигуру для игры.
        /// </summary>
        private void GenerateRandomFigure()
        {
            int figIndex = rand.Next(figures.Count);
            currentFigureRotations = figures[figIndex];
            currentRotationIndex = 0;
        }

        /// <summary>
        /// Проверяет, может ли фигура быть размещена на заданной позиции на поле.
        /// </summary>
        /// <param name="row">Строка для размещения фигуры.</param>
        /// <param name="col">Столбец для размещения фигуры.</param>
        /// <param name="figure">Фигура, которую необходимо разместить.</param>
        /// <returns>Возвращает <c>true</c>, если фигуру можно разместить; иначе <c>false</c>.</returns>
        private bool CanPlaceFigure(int row, int col, char[,] figure)
        {
            for (int r = 0; r < figure.GetLength(0); r++)
            {
                for (int c = 0; c < figure.GetLength(1); c++)
                {
                    if (figure[r, c] == '#')
                    {
                        int fr = row + r;
                        int fc = col + c;
                        if (fr < 0 || fr >= Rows || fc < 0 || fc >= Cols)
                            return false;
                        if (field[fr, fc] == '#')
                            return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Размещает или удаляет фигуру на игровом поле.
        /// </summary>
        /// <param name="row">Строка для размещения фигуры.</param>
        /// <param name="col">Столбец для размещения фигуры.</param>
        /// <param name="figure">Фигура для размещения или удаления.</param>
        /// <param name="place">Если <c>true</c>, фигура размещается; если <c>false</c>, фигура удаляется.</param>
        private void PlaceFigure(int row, int col, char[,] figure, bool place)
        {
            char ch = place ? '#' : ' ';
            for (int r = 0; r < figure.GetLength(0); r++)
            {
                for (int c = 0; c < figure.GetLength(1); c++)
                {
                    if (figure[r, c] == '#')
                    {
                        field[row + r, col + c] = ch;
                    }
                }
            }
        }

        /// <summary>
        /// Удаляет полностью заполненные линии и сдвигает верхние линии вниз.
        /// </summary>
        /// <returns>Количество удаленных линий.</returns>
        private int ClearCompleteLines()
        {
            int cleared = 0;
            for (int r = Rows - 1; r >= 0; r--)
            {
                bool fullLine = true;
                for (int c = 0; c < Cols; c++)
                {
                    if (field[r, c] != '#')
                    {
                        fullLine = false;
                        break;
                    }
                }

                if (fullLine)
                {
                    // Сдвигаем всё вниз
                    for (int rr = r; rr > 0; rr--)
                    {
                        for (int cc = 0; cc < Cols; cc++)
                        {
                            field[rr, cc] = field[rr - 1, cc];
                        }
                    }
                    for (int cc = 0; cc < Cols; cc++)
                    {
                        field[0, cc] = ' ';
                    }

                    cleared++;
                    r++; // Проверяем эту же строку снова после сдвига
                }
            }
            return cleared;
        }
    }
}
