using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
     class TerisClass : BaseClass // налследован гандон
    {
        public event TetrisisHandler ProcessEvent;
        public BlockClass Block = new BlockClass(); // Создавал поле 

        public TerisClass(WindowRect wrField)
        {
            TetrisField = wrField;
            BuildField(); //отрисовка поля
        }
        public void BuildField()
        {
            arrField = new StructBlockStyle[TetrisField.width * TetrisField.height];
        }

        public static void DrawFiead(Point pt, WindowRect wrBlockAdj)
        {
            int w = TetrisField.width; // длина самого ебаного поля
            int h = TetrisField.height; // ширина самого ебаного поля
            for (int row = 0; row < h; row++)
            {
                for(int col = 0; col < w;col++)
                {
                    if (((StructBlockStyle)arrField[col+row*w]).isBlock)
                    {
                        Console.ForegroundColor = ((StructBlockStyle)arrField[col + row * w]).color;
                        Console.SetCursorPosition(TetrisField.left + col, TetrisField.top + row);// где отображаться фигуры
                        Console.Write("╝");
                    }
                    else
                    {
                        Console.SetCursorPosition(TetrisField.left + col, TetrisField.top + row);
                        Console.Write("");
                    }                
                }
                
            }
            Console.ResetColor();
        }
        public bool IsCollided(Point pt, WindowRect wrBlockAdj)
        {
            int sx = pt.x - TetrisField.left;
            int sy = pt.y - TetrisField.top;
            int w = TetrisField.width;

            int blockIndex;
            int fieldIndex;
            for (int row = 0; row < wrBlockAdj.height; row++)
            {
                for(int col = 0; col <wrBlockAdj.width; col++)
                {
                    blockIndex = (wrBlockAdj.left + col) + ((wrBlockAdj.top + row) * block_size);
                    fieldIndex = ((sx + sy * w) + col) + row * w;
                    if (arrBlock[blockIndex] && ((StructBlockStyle)arrField[fieldIndex]).isBlock);
                    {
                        return true;
                    }
                }
            }
            return false;
        } // столкновения
        public void SendToField(Point pt, WindowRect wrBlockAdj)
        {
            int blockIndex;
            int fieldIndex; // отпрака данных в поле как и выше
            for (int row = 0; row < wrBlockAdj.height; row++)
            {
                for (int col = 0; col < wrBlockAdj.width; col++)
                {
                    blockIndex = (wrBlockAdj.left + col + col) + (wrBlockAdj.top + row) * Block.Size;
                    fieldIndex = (pt.x - TetrisField.left + col) + (pt.y - TetrisField.top + row) * TetrisField.width;

                    if (arrBlock[blockIndex])
                        arrField[fieldIndex] = new StructBlockStyle(Block.Color(Block.Type), true);
                }
           }
            ProcessRows();
        }
    public void ProcessRows() // что бы строки исчезали
        {
            int w = TetrisField.width;
            int h = TetrisField.height;
            int rowCounter = h - 1;
            int rowTotal = 0; // очки
            bool isFullLine = true; //проверка
            StructBlockStyle[] arrData = new StructBlockStyle[TetrisField.width * TetrisField.height];
            for( int row = h - 1; row >= 0; row--)
            {
                 for(int col = w - 1;(col>=0) && isFullLine; col--)
                {
                    if (!((StructBlockStyle)arrField[col + row * w]).isBlock)
                    {
                        isFullLine = false;
                    }
                }
            if (!isFullLine)
                {
                    for(int col = w -1; col >= 0; col --) // копия строки
                    {
                        arrData[col + rowCounter * w] = arrField[col + row * w];
                    }
                    rowCounter--;
                    isFullLine = true;
                }
            else
                {
                    rowTotal++;
                }
            }
            arrField = arrData;
            EventArgs e = new EventArgs(rowTotal);
            ReiseEvent((object)this, e);

        }
    private void ReiseEvent(object o, EventArgs e)
        {
            if(ProcessEvent != null)
                ProcessEvent(o, e);
        }
    }
}
