using Windows.UI.Xaml.Controls;

namespace suduko
{
    public class TextBoxSudoku
    {
        private static double _width = 0;
        private static double _height = 0;
        private TextBox texBox;
        public TextBoxSudoku(double x, double y, string number = null)
        {
            if (number != null) texBox.Text = number;
            texBox = new TextBox();
            X = x;
            Y = y;
            texBox.TextAlignment = Windows.UI.Xaml.TextAlignment.Center;
            texBox.TextAlignment = Windows.UI.Xaml.TextAlignment.Center;
            texBox.FontSize = 40;
            texBox.Width = _width;
            texBox.Height = _height;
        }

        public string CorrectNum { get; set; }
        public bool IsSorted { get; set; }
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public int SquareIndex { get; set; }
        public TextBox TexBox
        {
            get
            {
                return texBox;
            }
        }
        public static double Width
        {
            get
            {
                return _width;
            }
            set
            {
                if (value > 0)
                {
                    _width = value;
                }
            }
        }
        public static double Height
        {
            get
            {
                return _height;
            }
            set
            {
                if (value > 0)
                    _height = value;
            }
        }
        public double X
        {
            get
            {
                return Canvas.GetLeft(texBox);
            }
            set
            {
                Canvas.SetLeft(texBox, value);
            }
        }
        public double Y
        {
            get
            {
                return Canvas.GetTop(texBox);
            }
            set
            {
                Canvas.SetTop(texBox, value);
            }
        }

        public override bool Equals(object obj)
        {
            TextBoxSudoku tb = obj as TextBoxSudoku;
            if (tb == null) return false;
            return X.Equals(tb.X) && Y.Equals(tb.Y);
        }
    }
}
