using System;
using System.Drawing;

namespace Tetris
{
    public class Tetrus
    {
        public Point[] Blocks { get; private set; }
        public Color Color { get; private set; }

        public Tetrus(Point[] blocks, Color color)
        {
            Blocks = blocks;
            Color = color;
        }

        public void Rotate()
        {
            Point center = Blocks[1]; // Центр вращения
            for (int i = 0; i < Blocks.Length; i++)
            {
                int x = Blocks[i].X - center.X;
                int y = Blocks[i].Y - center.Y;
                Blocks[i].X = center.X - y;
                Blocks[i].Y = center.Y + x;
            }
        }

        public void Move(int dx, int dy)
        {
            for (int i = 0; i < Blocks.Length; i++)
            {
                Blocks[i].X += dx;
                Blocks[i].Y += dy;
            }
        }

        public static Tetrus GenerateRandomTetromino()
        {
            Random rand = new Random();
            int type = rand.Next(0, 7);
            switch (type)
            {
                case 0: return new Tetrus(new Point[] { new Point(4, 0), new Point(5, 0), new Point(6, 0), new Point(7, 0) }, Color.Cyan); // I
                case 1: return new Tetrus(new Point[] { new Point(4, 0), new Point(5, 0), new Point(4, 1), new Point(5, 1) }, Color.Yellow); // O
                case 2: return new Tetrus(new Point[] { new Point(4, 0), new Point(5, 0), new Point(6, 0), new Point(5, 1) }, Color.Purple); // T
                case 3: return new Tetrus(new Point[] { new Point(4, 0), new Point(5, 0), new Point(5, 1), new Point(6, 1) }, Color.Green); // S
                case 4: return new Tetrus(new Point[] { new Point(5, 0), new Point(6, 0), new Point(4, 1), new Point(5, 1) }, Color.Red); // Z
                case 5: return new Tetrus(new Point[] { new Point(4, 0), new Point(5, 0), new Point(6, 0), new Point(4, 1) }, Color.Blue); // J
                case 6: return new Tetrus(new Point[] { new Point(4, 0), new Point(5, 0), new Point(6, 0), new Point(6, 1) }, Color.Orange); // L
                default: return null;
            }
        }
    }
}
