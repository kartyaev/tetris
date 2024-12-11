using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    using System;
    using System.Collections.Generic;

    
        class TerisClass

        {
            // Параметры игры (устанавливаются при выборе сложности)
            static int Rows;
            static int Cols;

            // Игровое поле
            static char[,] field;

            static Random rand = new Random();

            // Фигуры
            static List<char[,]> lineShape = new List<char[,]>
        {
            new char[,] {
                {'#','#','#','#'}
            },
            new char[,] {
                {'#'},
                {'#'},
                {'#'},
                {'#'}
            }
        };

            static List<char[,]> squareShape = new List<char[,]>
        {
            new char[,] {
                {'#','#'},
                {'#','#'}
            }
        };

            static List<char[,]> tShape = new List<char[,]>
        {
            new char[,] {
                {'#','#','#'},
                {' ','#',' '}
            },
            new char[,] {
                {' ','#',' '},
                {'#','#','#'}
            },
            new char[,] {
                {' ',' ','#'},
                {' ','#','#'},
                {' ',' ','#'}
            },
            new char[,] {
                {'#',' ',' '},
                {'#','#',' '},
                {'#',' ',' '}
            },
        };

            static List<char[,]> lShape = new List<char[,]>
        {
            new char[,] {
                {'#',' ',' '},
                {'#',' ',' '},
                {'#','#','#'}
            },

            new char[,] {
                {'#','#','#'},
                {'#',' ',' '},
                {'#',' ',' '}
            },

            new char[,] {
                {'#','#','#'},
                {' ',' ','#'},
                {' ',' ','#'}
            },

            new char[,] {
                {' ',' ','#'},
                {' ',' ','#'},
                {'#','#','#'}
            }

        };

            static List<List<char[,]>> figures = new List<List<char[,]>>();

            static List<char[,]> currentFigureRotations;
            static int currentRotationIndex;
            static int currentFigureRow;
            static int currentFigureCol;

            static int score = 0;

           

            static void ChooseDifficulty()
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

           public static void StartTetrisGame()
            {
                ChooseDifficulty();

                figures.Clear();
                figures.Add(lineShape);
                figures.Add(squareShape);
                figures.Add(tShape);
                figures.Add(lShape);

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

                        // Ожидаем нажатия клавиши (блокирующий вызов)
                        ConsoleKeyInfo key = Console.ReadKey(true);

                        // Убираем фигуру, чтобы проверить новое положение
                        PlaceFigure(currentFigureRow, currentFigureCol, currentFigureRotations[currentRotationIndex], false);

                        bool moved = false;
                        switch (key.Key)
                        {
                            case ConsoleKey.LeftArrow:
                            case ConsoleKey.A:
                                if (CanPlaceFigure(currentFigureRow, currentFigureCol - 1, currentFigureRotations[currentRotationIndex]))
                                {
                                    currentFigureCol--;
                                    moved = true;
                                }
                                break;
                            case ConsoleKey.RightArrow:
                            case ConsoleKey.D:
                                if (CanPlaceFigure(currentFigureRow, currentFigureCol + 1, currentFigureRotations[currentRotationIndex]))
                                {
                                    currentFigureCol++;
                                    moved = true;
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
                                moved = true;
                                break;
                            case ConsoleKey.O:
                                gameOver = true;
                                break;
                            case ConsoleKey.DownArrow:
                            case ConsoleKey.S:
                                // Опускаем фигуру на одну клетку, если можем
                                if (CanPlaceFigure(currentFigureRow + 1, currentFigureCol, currentFigureRotations[currentRotationIndex]))
                                {
                                    currentFigureRow++;
                                    moved = true;
                                }
                                else
                                {
                                    // Фигура упала окончательно
                                    moved = true;
                                    figureFixed = true;
                                }
                                break;
                            default:
                                // Любая другая клавиша — делаем шаг падения
                                if (CanPlaceFigure(currentFigureRow + 1, currentFigureCol, currentFigureRotations[currentRotationIndex]))
                                {
                                    currentFigureRow++;
                                    moved = true;
                                }
                                else
                                {
                                    figureFixed = true;
                                    moved = true;
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

                    // Проверяем, сможем ли добавить новую фигуру в следующий цикл
                    // если не сможем — игра закончится на начало следующего цикла
                }

                Console.Clear();
                Console.WriteLine("Игра окончена!");
                Console.WriteLine("Ваш счет: " + score);
                Console.WriteLine("Нажмите любую клавишу для возврата в меню...");
                Console.ReadKey();
            }

            static void InitializeField()
            {
                for (int r = 0; r < Rows; r++)
                {
                    for (int c = 0; c < Cols; c++)
                    {
                        field[r, c] = ' ';
                    }
                }
            }

            static void DrawField()
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

            static void GenerateRandomFigure()
            {
                int figIndex = rand.Next(figures.Count);
                currentFigureRotations = figures[figIndex];
                currentRotationIndex = 0;
            }

            static bool CanPlaceFigure(int row, int col, char[,] figure)
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

            static void PlaceFigure(int row, int col, char[,] figure, bool place)
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

            static int ClearCompleteLines()
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
                        r++; // перепроверяем эту строку заново после сдвига
                    }
                }
                return cleared;
            }
        }
    }






