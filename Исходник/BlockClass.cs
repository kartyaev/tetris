//using System;
//using System.Data;
//using System.Diagnostics.Eventing.Reader;
//using System.Dynamic;
//using System.Security.Claims;
//using System.Security.Cryptography;

//namespace Game
//{
//    class BlockClass : BaseClass
//    {
//        public RotationEnum Angel
//        {
//            get
//            {
//                return m_block.angel;
//            }
//            set
//            {
//                m_block.angel = value;
//            }

//        }
//        public BlockTypeNum Type
//        {
//            get
//            {
//                return m_block.type;
//            }
//            set
//            {
//                m_block.type = value;
//            }
//        }
//        public Point Location
//        {
//            get
//            {
//                return new Point(m_blockpos.x, m_blockpos.y);

//            }
//            set
//            {
//                m_blockpos = value;
//            }
//        }
//        public int Size
//        {
//            get
//            {
//                return block_size;
//            }

//        }
//        public ConsoleColor Color(BlockTypeNum typBlock)
//        {
//            switch (typBlock)
//            {
//                case BlockTypeNum.block01:
//                    return ConsoleColor.Red;
//                case BlockTypeNum.block02:
//                    return ConsoleColor.Blue;
//                case BlockTypeNum.block03:
//                    return ConsoleColor.Cyan;
//                case BlockTypeNum.block04:
//                    return ConsoleColor.Yellow;
//                case BlockTypeNum.block05:
//                    return ConsoleColor.Green;
//                case BlockTypeNum.block06:
//                    return ConsoleColor.Magenta;
//                default:
//                    return ConsoleColor.DarkCyan;
//            }
//        }
//        public StructBlock Generate()
//        {
//            Random rnd = new Random();
//            return new StructBlock((RotationEnum)rnd.Next(0, Enum.GetNames(typeof(RotationEnum)).Length),
//                (BlockTypeNum)rnd.Next(0, Enum.GetNames(typeof(BlockTypeNum)).Length));
//        }
//        public WindowRect Rotate(RotationEnum newAngel)
//        {
//            WindowRect wrBlock = new WindowRect();
//            Angel = newAngel;
//            Build();
//            return wrBlock;
//        }
//        public void Build()
//        {
//            arrBlock = GetBlocData(new StructBlock(Angel, Type));
//        }
//        public bool[] GetBlocData(StructBlock structBlock)
//        {
//            bool[] arrData = new bool[block_size << 2];
//            switch (structBlock.type)
//            {
//                case BlockTypeNum.block01:
//                    if ((structBlock.angel.Equals(RotationEnum.deg0)) ||
//    (structBlock.angel.Equals(RotationEnum.deg180)))
//                    {
//                        arrData[2] = true;
//                        arrData[6] = true;
//                        arrData[10] = true;
//                        arrData[14] = true;
//                    }
//                    else
//                    {
//                        arrData[12] = true;
//                        arrData[13] = true;
//                        arrData[14] = true;
//                        arrData[15] = true;
//                    }
//                    break;
//                case BlockTypeNum.block02:
//                    arrData[0] = true;
//                    arrData[1] = true;
//                    arrData[4] = true;
//                    arrData[5] = true;
//                    break;
//                case BlockTypeNum.block03:
//                    if ((structBlock.angel.Equals(RotationEnum.deg0)) ||
//                        (structBlock.angel.Equals(RotationEnum.deg180)))
//                    {
//                        arrData[5] = true;
//                        arrData[6] = true;
//                        arrData[8] = true;
//                        arrData[9] = true;
//                    }
//                    else
//                    {
//                        arrData[1] = true;
//                        arrData[5] = true;
//                        arrData[6] = true;
//                        arrData[10] = true;
//                    }
//                    break;
//                case BlockTypeNum.block04:
//                    if ((structBlock.angel.Equals(RotationEnum.deg0)) ||
//                        (structBlock.angel.Equals(RotationEnum.deg180)))
//                    {

//                        arrData[4] = true;
//                        arrData[5] = true;
//                        arrData[9] = true;
//                        arrData[10] = true;
//                    }
//                    else
//                    {

//                        arrData[2] = true;
//                        arrData[5] = true;
//                        arrData[6] = true;
//                        arrData[9] = true;
//                    }
//                    break;
//                case BlockTypeNum.block05:
//                    if (structBlock.angel.Equals(RotationEnum.deg0))
//                    {
//                        arrData[4] = true;
//                        arrData[5] = true;
//                        arrData[6] = true;
//                        arrData[9] = true;
//                    }
//                    else if (structBlock.angel.Equals(RotationEnum.deg90))
//                    {
//                        arrData[1] = true;
//                        arrData[4] = true;
//                        arrData[5] = true;
//                        arrData[9] = true;
//                    }
//                    else if (structBlock.angel.Equals(RotationEnum.deg180))
//                    {
//                        arrData[5] = true;
//                        arrData[8] = true;
//                        arrData[9] = true;
//                        arrData[10] = true;
//                    }
//                    else
//                    {
//                        arrData[1] = true;
//                        arrData[5] = true;
//                        arrData[6] = true;
//                        arrData[9] = true;
//                    }
//                    break;
//                case BlockTypeNum.block06:
//                    if (structBlock.angel.Equals(RotationEnum.deg0))
//                    {
//                        arrData[4] = true;
//                        arrData[5] = true;
//                        arrData[6] = true;
//                        arrData[8] = true;
//                    }
//                    else if (structBlock.angel.Equals(RotationEnum.deg90))
//                    {
//                        arrData[0] = true;
//                        arrData[1] = true;
//                        arrData[5] = true;
//                        arrData[9] = true;
//                    }
//                    else if (structBlock.angel.Equals(RotationEnum.deg180))
//                    {
//                        arrData[6] = true;
//                        arrData[8] = true;
//                        arrData[9] = true;
//                        arrData[10] = true;
//                    }
//                    else
//                    {
//                        arrData[1] = true;
//                        arrData[5] = true;
//                        arrData[9] = true;
//                    }
//                    break;
//                case BlockTypeNum.block07:
//                    if (structBlock.angel.Equals(RotationEnum.deg0))
//                    {
//                        arrData[4] = true;
//                        arrData[5] = true;
//                        arrData[6] = true;
//                        arrData[10] = true;
//                    }
//                    else if (structBlock.angel.Equals(RotationEnum.deg90))
//                    {
//                        arrData[1] = true;
//                        arrData[5] = true;
//                        arrData[8] = true;
//                        arrData[9] = true;
//                    }
//                    else if (structBlock.angel.Equals(RotationEnum.deg180))
//                    {
//                        arrData[4] = true;
//                        arrData[8] = true;
//                        arrData[9] = true;
//                        arrData[10] = true;
//                    }
//                    else
//                    {
//                        arrData[1] = true;
//                        arrData[2] = true;
//                        arrData[5] = true;
//                        arrData[9] = true;
//                    }
//                    break;
//            }
//            return arrData;


//        }
//        public void Adjustment(ref WindowRect wrBlock)
//        {
//            Adjustment(ref wrBlock, arrBlock); //Перегрузка метода корректировки MH
//        }
//        public void Adjustment(ref WindowRect wrBlock, bool[] arrData)
//        {
//            wrBlock = new WindowRect();
//            int col;
//            int row;
//            bool isAdj;
//            isAdj = true;
//            for (col = 0; col < block_size; col++)
//            {
//                for (row = 0; row < block_size; row++)
//                {
//                    if (arrData[col + row * block_size])
//                    {
//                        isAdj = false;
//                        break;
//                    }
//                    if (isAdj)
//                    {
//                        wrBlock.left++;
//                    }
//                    else
//                        break; //еб*аная крайняя левая регулировка время 4 часа ночи сука
//                }
//            }
//            isAdj = true;
//            for (row = 0; row < block_size; row++)
//            {
//                for (col = 0; col < block_size; col++)
//                {
//                    if (arrData[col + row * block_size])
//                    {
//                        isAdj = false;
//                        break;
//                    }
//                    if (isAdj)
//                    {
//                        wrBlock.top++;
//                    }
//                    else
//                        break;
//                }
//            }
//            // время проверить пустные столпцы GPT В помочь
//            isAdj = true;
//            for (col = block_size - 1; col >= 0; col--)
//            {
//                for (row = 0; row < block_size; row++)
//                {
//                    if (arrData[col + row * block_size])
//                    {
//                        isAdj = false;
//                        break;
//                    }
//                    if (isAdj)
//                    {
//                        wrBlock.width++;
//                    }
//                    else
//                        break;
//                }

//            }
//            wrBlock.width = block_size - (wrBlock.left + wrBlock.width); // ширина проверка
//            isAdj = true;
//            for (row = block_size - 1; row >= 0; row--)
//            {
//                for (col = 0; col < block_size; col++)
//                {
//                    if (arrData[col + row * block_size])
//                    {
//                        isAdj = false;
//                        break;
//                    }
//                    if (isAdj)
//                    {
//                        wrBlock.height++;
//                    }
//                    else
//                        break; //я забыл написать еще один for Kill me pls 
//                }
//            }
//            wrBlock.height = block_size - (wrBlock.top + wrBlock.height); // высота емае
//        }
//        public void Draw(Point pt, WindowRect wrBlockAdj,bool isRotateUpdate)
//        {
//            if (!Location.x.Equals(pt.x) || !Location.y.Equals(pt.y) || isRotateUpdate) //ша нарисуем блоки и поле в тетрискласс 
//            {
//                TerisClass.DrawFiead(pt, wrBlockAdj);
//                Console.ForegroundColor = Color(Type);
//                for(int row = wrBlockAdj.top; row <wrBlockAdj.top+wrBlockAdj.height;row++) // ряды и по высоте
//                {
//                    for(int col = wrBlockAdj.left;col < wrBlockAdj.left +wrBlockAdj.width;col++ )
//                    {
//                        if (arrBlock[col+row * block_size])
//                        {
//                            Console.SetCursorPosition(pt.x+ col - wrBlockAdj.left,pt.y +row -wrBlockAdj.top);
//                            Console.WriteLine("#"); // фигура
//                        }
//                    }
//                    Console.ResetColor();
//                    Location = pt;
//                }
//            }
//        }
//        public void Preview(Point pt, StructBlock structBlock) //превьюшка жесткая йщу
//        {
//            WindowRect wrBlockAdj = new WindowRect();
//            bool[] arrDate = GetBlocData(structBlock);

//            // отресовка правельного положенгиЯ блока
//            Adjustment(ref wrBlockAdj, arrDate);

//            Console.ForegroundColor= Color(structBlock.type);
//            for (int row = wrBlockAdj.top; row < wrBlockAdj.top+wrBlockAdj.height;row++)
//            {
//                for (int col = wrBlockAdj.left;col < wrBlockAdj.left + wrBlockAdj.width; col++)
//                {
//                    if (arrDate[col+row+block_size])
//                    {
//                        Console.SetCursorPosition(pt.x + col - wrBlockAdj.left - wrBlockAdj.width / 2, pt.y + row - wrBlockAdj.top - wrBlockAdj.height / 2);
//                        Console.WriteLine("#");
//                    }
//                }
//                Console.ResetColor();
//            }
//        }
//        //устал(
//        public RotationEnum getNextAngel(int rotateOption)   // метод отвечающий за угол щщщ
//        {
//            if (rotateOption.Equals(0))
//            {

//                switch (Angel)
//                {
//                    case RotationEnum.deg0:
//                        return RotationEnum.deg90;
//                    case RotationEnum.deg90:
//                        return RotationEnum.deg180;
//                    case RotationEnum.deg180:
//                        return RotationEnum.deg270;
//                    default:
//                        return RotationEnum.deg0;
//                }
//            }
//            else
//                switch (Angel)
//                { 
//                    case RotationEnum.deg0:
//                        return RotationEnum.deg270;
//                    case RotationEnum.deg270:
//                        return RotationEnum.deg180;
//                    case RotationEnum.deg180:
//                        return RotationEnum.deg90;
//                        default: 
//                        return RotationEnum.deg0;
//                }
//        }
//        public void Assign(StructBlock sbNew)
//        {
//            Angel = sbNew.angel;
//            Type = sbNew.type;
//        }
//    }

//}

