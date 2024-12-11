using System;

namespace Game
{
    struct StructBlockStyle
    {
        public ConsoleColor color;
        public bool isBlock;
        public StructBlockStyle(ConsoleColor newColor, bool newIsBlock)
        {
            color = newColor;
            isBlock = newIsBlock;
        }    
    }
}