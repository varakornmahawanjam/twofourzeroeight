using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twozerofoureight
{
    class TwoZeroFourEightModel : Model
    {
        protected int boardSize; // default is 4
        protected int[,] board;
        protected Random rand;

        public TwoZeroFourEightModel() : this(4)
        {
            // default board size is 4 
        }

        public TwoZeroFourEightModel(int size)
        {
            boardSize = size;
            board = new int[boardSize, boardSize];
            var range = Enumerable.Range(0, boardSize);
            foreach (int i in range)
            {
                foreach (int j in range)
                {
                    board[i, j] = 0;
                }
            }
            rand = new Random();
            board = Random(board);
            NotifyAll();
        }

        public int[,] GetBoard()
        {
            return board;
        }

        private int[,] Random(int[,] input)
        {
            while (true)
            {
                int x = rand.Next(boardSize);
                int y = rand.Next(boardSize);
                if (board[x, y] == 0)
                {
                    board[x, y] = 2;
                    break;
                }
            }
            return input;
        }

        // Perform shift and merge to the left of the given array.
        protected bool ShiftAndMerge(int[] buffer)
        {
            bool changed = false; // whether the array has changed
            int pos = 0; // next available slot index
            int lastMergedSlot = -1; // last slot that resulted from merging
            for (int k = 0; k < boardSize; k++)
            {
                if (buffer[k] != 0) // nonempty slot
                {
                    // check if we can merge with the previous slot
                    if (pos > 0 && pos - 1 > lastMergedSlot && buffer[pos - 1] == buffer[k])
                    {
                        // merge
                        buffer[pos - 1] *= 2;
                        buffer[k] = 0;
                        lastMergedSlot = pos - 1;
                        changed = true;
                    }
                    else
                    {
                        // shift to the next available slot
                        buffer[pos] = buffer[k];
                        if (pos != k)
                        {
                            buffer[k] = 0;
                            changed = true;
                        }
                        // move the next available slot
                        pos++;
                    }
                }
            }
            return changed;
        }

        public void PerformDown()
        {
            bool changed = false; // whether the board has changed
            int[] rangeX = Enumerable.Range(0, boardSize).ToArray();
            int[] rangeY = Enumerable.Range(0, boardSize).ToArray();
            Array.Reverse(rangeY);
            foreach (int i in rangeX)
            {
                int[] buffer = new int[boardSize];
                int pos = 0;
                // extract the current column from bottom to top
                foreach (int j in rangeY)
                {
                    buffer[pos] = board[j, i];
                    pos++;
                }
                // process the extracted array
                // also track changes
                changed = ShiftAndMerge(buffer) || changed;
                // copy back
                pos = 0;
                foreach (int j in rangeY)
                {
                    board[j, i] = buffer[pos];
                    pos++;
                }
            }
            if (changed)
            {
                board = Random(board);
                NotifyAll();
            }
        }

        public void PerformUp()
        {
            bool changed = false; // whether the board has changed
            int[] range = Enumerable.Range(0, boardSize).ToArray();
            foreach (int i in range)
            {
                int[] buffer = new int[boardSize];
                int pos = 0;
                // extract the current column from top to bottom
                foreach (int j in range)
                {
                    buffer[pos] = board[j, i];
                    pos++;
                }
                // process the extracted array
                // also track changes
                changed = ShiftAndMerge(buffer) || changed;
                // copy back
                pos = 0;
                foreach (int j in range)
                {
                    board[j, i] = buffer[pos];
                    pos++;
                }
            }
            if (changed)
            {
                board = Random(board);
                NotifyAll();
            }
        }

        public void PerformRight()
        {
            bool changed = false; // whether the board has changed
            int[] rangeX = Enumerable.Range(0, boardSize).ToArray();
            int[] rangeY = Enumerable.Range(0, boardSize).ToArray();
            Array.Reverse(rangeX);
            foreach (int i in rangeY)
            {
                int[] buffer = new int[boardSize];
                int pos = 0;
                // extract the current column from bottom to top
                foreach (int j in rangeX)
                {
                    buffer[pos] = board[i, j];
                    pos++;
                }
                // process the extracted array
                // also track changes
                changed = ShiftAndMerge(buffer) || changed;
                // copy back
                pos = 0;
                foreach (int j in rangeX)
                {
                    board[i, j] = buffer[pos];
                    pos++;
                }
            }
            if (changed)
            {
                board = Random(board);
                NotifyAll();
            }
        }

        public void PerformLeft()
        {
            bool changed = false; // whether the board has changed
            int[] range = Enumerable.Range(0, boardSize).ToArray();
            foreach (int i in range)
            {
                int[] buffer = new int[boardSize];
                int pos = 0;
                // extract the current column from bottom to top
                foreach (int j in range)
                {
                    buffer[pos] = board[i, j];
                    pos++;
                }
                // process the extracted array
                // also track changes
                changed = ShiftAndMerge(buffer) || changed;
                // copy back
                pos = 0;
                foreach (int j in range)
                {
                    board[i, j] = buffer[pos];
                    pos++;
                }
            }
            if (changed)
            {
                board = Random(board);
                NotifyAll();
            }
        }
    }
}
