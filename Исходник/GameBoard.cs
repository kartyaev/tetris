using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace Tetris
{
    public class GameBoard
    {
        private int[,] board;
        private int rows;
        private int columns;

        public GameBoard(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            board = new int[rows, columns];
        }

        public bool IsPositionValid(Point[] blocks)
        {
            foreach (var block in blocks)
            {
                if (block.X < 0 || block.X >= columns || block.Y < 0 || block.Y >= rows || board[block.Y, block.X] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        public void PlaceTetromino(Point[] blocks, int value)
        {
            foreach (var block in blocks)
            {
                board[block.Y, block.X] = value;
            }
        }

        public int ClearFullRows()
        {
            int clearedRows = 0;
            for (int y = 0; y < rows; y++)
            {
                bool isFull = true;
                for (int x = 0; x < columns; x++)
                {
                    if (board[y, x] == 0)
                    {
                        isFull = false;
                        break;
                    }
                }
                if (isFull)
                {
                    clearedRows++;
                    for (int row = y; row > 0; row--)
                    {
                        for (int col = 0; col < columns; col++)
                        {
                            board[row, col] = board[row - 1, col];
                        }
                    }
                    for (int col = 0; col < columns; col++)
                    {
                        board[0, col] = 0;
                    }
                }
            }
            return clearedRows;
        }

        public void Draw(Graphics g, int cellSize)
        {
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    if (board[y, x] != 0)
                    {
                        g.FillRectangle(Brushes.Blue, x * cellSize, y * cellSize, cellSize, cellSize);
                        g.DrawRectangle(Pens.Black, x * cellSize, y * cellSize, cellSize, cellSize);
                    }
                }
            }
        }
    }
}
