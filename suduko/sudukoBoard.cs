using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace suduko
{
    public class sudukoBoard
    {
        TextBoxSudoku[,] texboxes;
        public sudukoBoard(double x, double y)
        {
            texboxes = new TextBoxSudoku[9, 9];

            for (int i = 0; i < texboxes.GetLength(0); i++)
            {
                for (int j = 0; j < texboxes.GetLength(1); j++)
                {
                    texboxes[i, j] = new TextBoxSudoku(x + j * TextBoxSudoku.Width, y + i * TextBoxSudoku.Height);

                    texboxes[i, j].RowIndex = i;
                    texboxes[i, j].ColumnIndex = j;
                   
                    if ((i >= 0 && i <= 2) && (j >= 0 && j <= 2)) texboxes[i, j].SquareIndex = 1;
                    else if ((i >= 0 && i <= 2) && (j >= 3 && j <= 5)) texboxes[i, j].SquareIndex = 2;
                    else if ((i >= 0 && i <= 2) && (j >= 6 && j <= 8)) texboxes[i, j].SquareIndex = 3;
                    else if ((i >= 3 && i <= 5) && (j >= 0 && j <= 2)) texboxes[i, j].SquareIndex = 4;
                    else if ((i >= 3 && i <= 5) && (j >= 3 && j <= 5)) texboxes[i, j].SquareIndex = 5;
                    else if ((i >= 3 && i <= 5) && (j >= 6 && j <= 8)) texboxes[i, j].SquareIndex = 6;
                    else if ((i >= 6 && i <= 8) && (j >= 0 && j <= 2)) texboxes[i, j].SquareIndex = 7;
                    else if ((i >= 6 && i <= 8) && (j >= 3 && j <= 5)) texboxes[i, j].SquareIndex = 8;
                    else if ((i >= 6 && i <= 8) && (j >= 6 && j <= 8)) texboxes[i, j].SquareIndex = 9;
                }
            }
        }

        public TextBoxSudoku[,] TexBoxes { get { return texboxes; } }

        public List<TextBoxSudoku> GetTexboxesAsList()
        {
            List<TextBoxSudoku> list = new List<TextBoxSudoku>();
            for (int i = 0; i < texboxes.GetLength(0); i++)
            {
                for (int j = 0; j < texboxes.GetLength(1); j++)
                {
                    list.Add(texboxes[i, j]);
                }
            }
            return list;
        }

        public void AddKeyboardToElement(Canvas cnvs)
        {
            for (int i = 0; i < texboxes.GetLength(0); i++)
            {
                for (int j = 0; j < texboxes.GetLength(1); j++)
                {
                    cnvs.Children.Add(texboxes[i, j].TexBox);
                }
            }
        }
    }
}
