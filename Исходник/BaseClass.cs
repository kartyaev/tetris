namespace Game
{
    class BaseClass
    {
        protected static int block_size = 10;
        protected static bool[] arrBlock = new bool[block_size << 2];
        protected static WindowRect TetrisField = new WindowRect();
        protected static Point m_blockpos = new Point();
        protected static StructBlock m_block = new StructBlock();
        protected static StructBlockStyle[] arrField;
    }
}
