using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace suduko
{
    public class UIManager
    {
        private Canvas _canvas;
        sudukoBoard _sudukoSquare;
        Random _rand;
        public sudukoBoard SudukoSquare { get { return _sudukoSquare; } }
        public UIManager(Canvas cnvs)
        {
            _rand = new Random();
            _canvas = cnvs;
            TextBoxSudoku.Width = _canvas.ActualWidth / 9;
            TextBoxSudoku.Height = _canvas.ActualHeight / 9;
            _sudukoSquare = new sudukoBoard(0, 0);
            _sudukoSquare.AddKeyboardToElement(_canvas);
            InitColor();
        }

        public void InitColor()
        {
            MarkReadOnlyCells();
            for (int i = 0; i < _sudukoSquare.TexBoxes.GetLength(0); i++)
            {
                for (int j = 0; j < _sudukoSquare.TexBoxes.GetLength(1); j++)
                {
                    if (_sudukoSquare.TexBoxes[i, j].SquareIndex % 2 == 1)
                        _sudukoSquare.TexBoxes[i, j].TexBox.Background = new SolidColorBrush(Windows.UI.Colors.LightGray);
                    else
                        _sudukoSquare.TexBoxes[i, j].TexBox.Background = new SolidColorBrush(Windows.UI.Colors.White);
                }
            }
        }

        public void MarkReadOnlyCells()
        {
            for (int i = 0; i < _sudukoSquare.TexBoxes.GetLength(0); i++)
            {
                for (int j = 0; j < _sudukoSquare.TexBoxes.GetLength(1); j++)
                {
                    if (_sudukoSquare.TexBoxes[i, j].TexBox.IsReadOnly == true)
                        _sudukoSquare.TexBoxes[i, j].TexBox.Foreground = new SolidColorBrush(Windows.UI.Colors.DarkRed);
                }
            }
        }

        public void SetColors()
        {
            for (int i = 0; i < _sudukoSquare.TexBoxes.GetLength(0); i++)
            {
                for (int j = 0; j < _sudukoSquare.TexBoxes.GetLength(1); j++)
                {
                    byte r = (byte)_rand.Next(40, 255);
                    byte g = (byte)_rand.Next(40, 255);
                    byte b = (byte)_rand.Next(40, 255);
                    _sudukoSquare.TexBoxes[i, j].TexBox.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(200, r, g, b));
                }
            }

        }
    }
}
