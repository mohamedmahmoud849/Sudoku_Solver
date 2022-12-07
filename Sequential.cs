using System;
using System.Collections.Generic;

namespace Sudoku_Solver
{
    public class Sequential
    {
        
        int length;
        char EMPTY_ENTRY = '*';
        public Sequential(int length)
        {
         
            this.length = length;
            
        }

        public void  solveSudoku(char[,] board)
        {

            board[0, 0] = '9';
            canSolveSudokuFromCell(0, 0, board);
        }

        private bool canSolveSudokuFromCell(int row, int col, char[,] board)
        {
            if (col == length)
            {
                col = 0;
                row++;

                if (row == length)
                {
                    return true;
                }
            }

            // Skip entries already filled out. They already have a value in them.
            if (board[row,col] != EMPTY_ENTRY)
            {
                return canSolveSudokuFromCell(row, col + 1, board);
            }

            for (int value = 1; value <= length; value++)
            {
                char charToPlace = (char)(value + '0');

                if (canPlaceValue(board, row, col, charToPlace))
                {
                    board[row,col] = charToPlace;
                    if (canSolveSudokuFromCell(row, col + 1, board))
                    {
                        return true;
                    }
                    board[row,col] = EMPTY_ENTRY;
                }
            }

            return false;
        }

        private bool canPlaceValue(char[,] board, int row, int col, char charToPlace)
        {
            // Check column of the placement
            for (int i = 0; i < length; i++)
            {
                if (charToPlace == board[i,col])
                {
                    return false;
                }
            }


            // Check row of the placement
            for (int i = 0; i < length; i++)
            {
                if (charToPlace == board[row,i])
                {
                    return false;
                }
            }

            // Check region constraints - get the size of the sub-box
            int regionSize = (int) Math.Sqrt(length);

            int verticalBoxIndex = row / regionSize;
            int horizontalBoxIndex = col / regionSize;

            int topLeftOfSubBoxRow = regionSize * verticalBoxIndex;
            int topLeftOfSubBoxCol = regionSize * horizontalBoxIndex;

            for (int i = 0; i < regionSize; i++)
            {
                for (int j = 0; j < regionSize; j++)
                {
                    if (charToPlace == board[topLeftOfSubBoxRow + i,topLeftOfSubBoxCol + j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        private List<char[,]> prepareBoards(List<char> probabilitis, char[,] board, int row, int col)
        {

            List<char[,]> list = new List<char[,]>();

            for (int i = 0; i < probabilitis.Count; i++)
            {
                var new_board = board.Clone() as char[,];
                new_board[row, col] = probabilitis[i];
                list.Add(new_board);
            }
            return list;
        }

        public List<char> getCellProbabilities(int row, int col, char[,] board)
        {
            List<char> prob = new List<char>();
            for (int value = 1; value <= 9; value++)
            {
                char charToPlace = (char)(value + '0');
                if (canPlaceValue(board, row, col, charToPlace))
                    prob.Add(charToPlace);
            }
            return prob;
        }


    }
}
