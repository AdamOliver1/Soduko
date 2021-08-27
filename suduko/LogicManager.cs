using System;
using System.Collections.Generic;

namespace suduko
{
    public class LogicManager
    {
        Random _rand;
        sudukoBoard _sodukoBoard;
        public LogicManager(sudukoBoard board)
        {
            _rand = new Random();
            _sodukoBoard = board;
        }

        public void NewGame(Level parameter)
        {
            SetRandomNum();

            //choose your favorite:

            CreateBoardWithoutRecursion();
             //CreateBoardWithRecursion(0, 0);

            SaveNums();
             InitBoard(parameter);
        }

        public int GetIndex(int countSmallRounds, int countBigRounds, int firstSecondOrThirdRow)
        {
            int FirstComparer = 0;
            int SecondComparer = 0;

            int index = 0;

            if (countBigRounds < 3)
            {
                FirstComparer = 1;
                SecondComparer = 2;
            }
            else if (countBigRounds < 6)
            {
                FirstComparer = 3;
                SecondComparer = 1;
            }
            else
            {
                FirstComparer = 2;
                SecondComparer = 3;
            }

            if (firstSecondOrThirdRow == FirstComparer)
            {
                if (countSmallRounds == 1) index = 0;
                else if (countSmallRounds == 2) index = 1;
                else if (countSmallRounds == 3) index = 2;
            }
            else if (firstSecondOrThirdRow == SecondComparer)
            {
                if (countSmallRounds == 1) index = 2;
                else if (countSmallRounds == 2) index = 0;
                else if (countSmallRounds == 3) index = 1;
            }
            else
            {
                if (countSmallRounds == 1) index = 1;
                else if (countSmallRounds == 2) index = 2;
                else if (countSmallRounds == 3) index = 0;
            }

            return index;



        }

        public void CreateBoardWithoutRecursion()
        {
            //An array of arrays to hold the triplets
            string[][] triplets = new string[3][];

            //init the arrays
            for (int i = 0; i < 3; i++)
            {
                triplets[i] = new string[3];
            }

           
            int index = 0;

            List<int> numsRange = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            // input first random numbers
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    index = _rand.Next(numsRange.Count);
                    triplets[i][j] = numsRange[index].ToString();
                    numsRange.RemoveAt(index);
                }
            }

            int counter = 0;

            // initializes every row, and every three rows changes the order of the triples,
            // of inside the triples, and the order of there posting
            for (int i = 0; i < 9; i += 3)
            {
                int firstSecondOrThirdRow = 0;             
                for (int j = 0; j < 3; j++)
                {
                    counter = 0;
                    int countSmallRounds = 1;
                    firstSecondOrThirdRow++;
                    while (counter < 9)
                    {
                        index = GetIndex(countSmallRounds, i, firstSecondOrThirdRow);
                        _sodukoBoard.TexBoxes[i + firstSecondOrThirdRow - 1, counter].TexBox.Text = triplets[index][0];
                        counter++;
                        _sodukoBoard.TexBoxes[i + firstSecondOrThirdRow - 1, counter].TexBox.Text = triplets[index][1];
                        counter++;
                        _sodukoBoard.TexBoxes[i + firstSecondOrThirdRow - 1, counter].TexBox.Text = triplets[index][2];
                        counter++;
                        countSmallRounds++;
                    }
                }
              
                // to every three rows change the order of the triples, and the order of themselves
                ChangeTripleOrder(triplets);
            }
        }

        private bool CreateBoardWithRecursion(int row, int col)
        {
            // exit recursion
            if (row > 8) return true;

            int nextRow = row;
            int nextCol = col + 1;

            if (col == 8)
            {
                nextRow = row + 1;
                nextCol = 0;
            }

            if (!(_sodukoBoard.TexBoxes[row, col].TexBox.Text == string.Empty))
            {
                return CreateBoardWithRecursion(nextRow, nextCol);
            }

            // assume that it is digit number 
            List<string> availableNumbers = getAvailableNumbers(_sodukoBoard.TexBoxes, row, col);

            foreach (var num in availableNumbers)
            {
                _sodukoBoard.TexBoxes[row, col].TexBox.Text = num;

                bool result = CreateBoardWithRecursion(nextRow, nextCol);

                if (result)
                {
                    return true;
                }
                _sodukoBoard.TexBoxes[row, col].TexBox.Text = string.Empty;
            }
            return false;
        }

        public void ChangeTripleOrder(string[][] threes)
        {
            for (int i = 0; i < 3; i++)
            {
                string temp = threes[i][0];
                threes[i][0] = threes[i][1];
                threes[i][1] = threes[i][2];
                threes[i][2] = temp;
            }
        }

        public bool CheckNullCells()
        {
            for (int i = 0; i < _sodukoBoard.TexBoxes.GetLength(0); i++)
            {
                for (int j = 0; j < _sodukoBoard.TexBoxes.GetLength(1); j++)
                {
                    if (_sodukoBoard.TexBoxes[i, j].TexBox.Text == string.Empty)
                        return false;
                }
            }
            return true;
        }

        public bool CheckBoard()
        {
            if (!CheckNullCells()) return false;
            bool isOk = true;
            for (int i = 0; i < _sodukoBoard.TexBoxes.GetLength(0); i++)
            {
                isOk = Check(CheckParams.Column, i);
                if (!isOk) return isOk;
                isOk = Check(CheckParams.Row, i);
                if (!isOk) return isOk;
                isOk = Check(CheckParams.Square, i);
                if (!isOk) return isOk;
            }
            return isOk;

        }

        public bool CheckTextBoxInputValidation(TextBoxSudoku tbs)
        {
            string nums = "123456789";
            if (!nums.Contains(tbs.TexBox.Text)) return false;
            return Check(CheckParams.Column, tbs.ColumnIndex) && Check(CheckParams.Row, tbs.RowIndex) && Check(CheckParams.Square, tbs.SquareIndex);
        }

        public void ShowSolution()
        {
            for (int i = 0; i < _sodukoBoard.TexBoxes.GetLength(0); i++)
            {
                for (int j = 0; j < _sodukoBoard.TexBoxes.GetLength(1); j++)
                {
                    _sodukoBoard.TexBoxes[i, j].TexBox.Text = _sodukoBoard.TexBoxes[i, j].CorrectNum;
                }
            }
        }

        public void SaveNums()
        {
            for (int i = 0; i < _sodukoBoard.TexBoxes.GetLength(0); i++)
            {
                for (int j = 0; j < _sodukoBoard.TexBoxes.GetLength(1); j++)
                {
                    _sodukoBoard.TexBoxes[i, j].CorrectNum = _sodukoBoard.TexBoxes[i, j].TexBox.Text;
                }
            }
        }

        private void InitBoard(Level parameter)
        {
            int num = 0;
            if (parameter == Level.Easy) num = 36;
            else if (parameter == Level.Medium) num = 31;
            else if (parameter == Level.Hard) num = 28;

            for (int i = 0; i < 81 - num; i++)
            {
                int row = _rand.Next(9);
                int col = _rand.Next(9);
                if (_sodukoBoard.TexBoxes[row, col].TexBox.Text == string.Empty)
                {
                    i--;
                    continue;
                }
                else _sodukoBoard.TexBoxes[row, col].TexBox.Text = string.Empty;
            }

            for (int i = 0; i < _sodukoBoard.TexBoxes.GetLength(0); i++)
            {
                for (int j = 0; j < _sodukoBoard.TexBoxes.GetLength(1); j++)
                {
                    if (_sodukoBoard.TexBoxes[i, j].TexBox.Text != string.Empty)
                        _sodukoBoard.TexBoxes[i, j].TexBox.IsReadOnly = true;
                    else
                        _sodukoBoard.TexBoxes[i, j].TexBox.IsReadOnly = false;
                }
            }
        }

        public void SetRandomNum()
        {
            for (int i = 0; i < _sodukoBoard.TexBoxes.GetLength(0); i++)
            {
                for (int j = 0; j < _sodukoBoard.TexBoxes.GetLength(1); j++)
                {
                    _sodukoBoard.TexBoxes[i, j].TexBox.Text = string.Empty;
                }
            }
            for (int i = 0; i < 2; i++)
            {
                _sodukoBoard.TexBoxes[_rand.Next(9), _rand.Next(9)].TexBox.Text = _rand.Next(1, 10).ToString();
            }
        }

        private List<string> getAvailableNumbers(TextBoxSudoku[,] board, int currentRow, int currentCol)
        {
            List<string> available = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            // check by row
            for (int col = 0; col < 9; col++)
            {
                if (board[currentRow, col].TexBox.Text != string.Empty)
                {
                    available.Remove(board[currentRow, col].TexBox.Text);
                }
            }

            // check by col
            for (int row = 0; row < 9; row++)
            {
                if (board[row, currentCol].TexBox.Text != string.Empty)
                {
                    available.Remove(board[row, currentCol].TexBox.Text);
                }
            }

            // check by 3 * 3 matrix 
            int startRow = currentRow / 3 * 3;
            int startCol = currentCol / 3 * 3;
            for (int row = startRow; row < startRow + 3; row++)
            {
                for (int col = startCol; col < startCol + 3; col++)
                {
                    if (board[row, col].TexBox.Text != string.Empty)
                    {
                        available.Remove(board[row, col].TexBox.Text);
                    }
                }
            }
            return available;
        }

        public bool Check(CheckParams parameter, int index)
        {
            List<string> values = new List<string>();
            bool isOk = true;
            if (parameter == CheckParams.Row)
            {
                for (int i = 0; i < _sodukoBoard.TexBoxes.GetLength(1); i++)
                {
                    if (!string.IsNullOrEmpty(_sodukoBoard.TexBoxes[index, i].TexBox.Text))
                        values.Add(_sodukoBoard.TexBoxes[index, i].TexBox.Text);
                }
            }

            if (parameter == CheckParams.Column)
            {
                for (int i = 0; i < _sodukoBoard.TexBoxes.GetLength(0); i++)
                {
                    if (!string.IsNullOrEmpty(_sodukoBoard.TexBoxes[i, index].TexBox.Text))
                        values.Add(_sodukoBoard.TexBoxes[i, index].TexBox.Text);
                }
            }
            if (parameter == CheckParams.Square)
            {
                for (int i = 0; i < _sodukoBoard.TexBoxes.GetLength(0); i++)
                {
                    for (int j = 0; j < _sodukoBoard.TexBoxes.GetLength(1); j++)
                    {
                        if (!string.IsNullOrEmpty(_sodukoBoard.TexBoxes[i, j].TexBox.Text))
                        {
                            if (_sodukoBoard.TexBoxes[i, j].SquareIndex == index)
                                values.Add(_sodukoBoard.TexBoxes[i, j].TexBox.Text);
                        }
                    }
                }

            }

            for (int i = 0; i < values.Count; i++)
            {
                for (int j = i + 1; j < values.Count; j++)
                {
                    if (values[i] == values[j]) isOk = false;
                }
            }
            return isOk;
        }


    }
}
