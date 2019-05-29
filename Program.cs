using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace con4
{
    class Game
    {
        static void Main(string[] args)
        {
            Board current_board = new Board();
            current_board.print_state();

            bool win = false;
            int current_player = 1;



            while (!win)
            {
                Console.Write("Please enter column Player " + current_player + "\n enter '-1' to skip \n enter '-2' to flip board." + "\n");
                string scol = Console.ReadLine();
                int col;
                while (!int.TryParse(scol.ToString(), out col))
                {
                    Console.Write("Please enter column Player " + current_player + "\n enter '-1' to skip \n enter '-2' to flip board." + "\n");

                    scol = Console.ReadLine();
                }

                

                if (int.Parse(scol) > 0)
                {
                    int row = current_board.drop(col, current_player);
                    if (row == -1) continue;
                    win = current_board.check_win(col, row, current_player);


                }

                else if (int.Parse(scol) == -1)
                {
                    int row2 = current_board.skip(current_player);
                }

                else if (int.Parse(scol) == -2)
                {
                    int row3 = current_board.turn(current_player);              //return multidimension array
                                                                                //array.drop
                                                                                //check_win
                }


                current_board.print_state();

                current_player = (current_player == 1) ? 2 : 1;
            }

            current_board.print_state();
            Console.ReadLine();
        }
    }

    public class Board
    {
        int size = 8;
        int to_win = 4;
        public int[,] state;
        string win_condition = "No Winner";
        int win_player = -1;
       

        public Board()
        {
            state = new int[size, size];
            reset();
        }

        public int drop(int col, int player)
        {
            // drop into col, return row or -1 if fail

            col -= 1;
            int row;
            int success = -1;

            if (col > size)
            {
                Console.Write("Board only has " + size + " columns \n");
                return -1;
            }

            if (col < 0)
            {
                Console.Write("Really? Don't be so negative \n");
                return -1;
            }

            for (row = 0; row < size; row++)
            {
                if (state[row, col] == 0)
                {
                    state[row, col] = player;
                    success = row;
                    break;
                }
            }
            if (row == size)
            {
                Console.Write("Column Full \n");
                success = -1;
            }
            return success;

        }

        public int skip(int player)
        {

            int success = -1;

            return success;

        }

        public int turn(int player)
        {
            int success = -1;


            int boardLength = size * size;
            int[] tempArray = new int[boardLength];
            int counterA = 0;
            int counterB = 0;

            for (int i = 0; i < size; i++)                                      //i = row
            {
                for (int j = 0; j < size; j++)                                  //j = column
                {
                    int tempInt = state[j, i];                                  //[j,i] = new column every 8
                    tempArray[counterA] = tempInt;
                    counterA++;
                }
            }


            Array.Reverse(tempArray);

            reset();

            for (int a = 1; a < size + 1; a++)
            {
                for (int b = 0; b < size; b++)
                {
                    if (tempArray[counterB] == 1)
                    {
                        player = 1;
                        drop(a, player);
                        counterB++;
                    }

                    else if (tempArray[counterB] == 2)
                    {
                        player = 2;
                        drop(a, player);
                        counterB++;
                    }

                    else
                    {
                        counterB++;
                        continue;
                    }
                }

            }

            return success;

        }


        public bool check_win(int col, int row, int player)
        {

            col -= 1;
            int[] check_flags = { -1, 1, -2, 2, -3, 3 };

            //win count along each direction, forward and backward flags 
            int win_count, fflag, bflag;

            //4 directions (h,v,d1,d2)
            for (int direction = 0; direction < 4; direction++)
            {
                switch (direction)
                {
                    case 0: //Horizontal
                        fflag = 0;
                        bflag = 0;
                        win_count = 1;
                        for (int i = 0; i < 6; i++)
                        {
                            int this_col = col + check_flags[i];

                            if (this_col < 0 || this_col >= size) continue;

                            if (state[row, this_col] == player)
                            {
                                win_count++;
                            }
                            else
                            {
                                if (check_flags[i] < 0) bflag = 1;
                                else fflag = 1;
                            }
                            if (bflag == 1 && fflag == 1) break;

                        }

                        if (win_count == to_win)
                        {
                            Console.Write("WINNER!! - Horizontal \n");
                            win_condition = "Horizontal";
                            win_player = player;
                            return true;
                        }
                        break;

                    case 1: //Vertical
                        fflag = 0;
                        bflag = 0;
                        win_count = 1;

                        for (int i = 0; i < 6; i++)
                        {
                            int this_row = row + check_flags[i];

                            if (this_row < 0 || this_row >= size) continue;

                            if (state[this_row, col] == player)
                            {
                                win_count++;
                            }
                            else
                            {
                                if (check_flags[i] < 0) bflag = 1;
                                else fflag = 1;
                            }
                            if (bflag == 1 && fflag == 1) break;

                        }

                        if (win_count == to_win)
                        {
                            Console.Write("WINNER!! - Vertical \n");
                            win_condition = "Vertical";
                            win_player = player;
                            return true;
                        }
                        break;


                    case 2: // == Diagonal

                        fflag = 0;
                        bflag = 0;
                        win_count = 1;

                        for (int i = 0; i < 6; i++)
                        {
                            int this_row = row + check_flags[i];
                            int this_col = col + check_flags[i];

                            if (this_row < 0 || this_row >= size) continue;
                            if (this_col < 0 || this_col >= size) continue;

                            if (state[this_row, this_col] == player)
                            {
                                win_count++;
                            }
                            else
                            {
                                if (check_flags[i] < 0) bflag = 1;
                                else fflag = 1;
                            }
                            if (bflag == 1 && fflag == 1) break;

                        }

                        if (win_count == to_win)
                        {
                            Console.Write("WINNER!! - Diagonal \n");
                            win_condition = "Diagonal (positive)";
                            win_player = player;
                            return true;
                        }
                        break;

                    case 3: // != Diagonal

                        fflag = 0;
                        bflag = 0;
                        win_count = 1;

                        for (int i = 0; i < 6; i++)
                        {
                            int this_row = row - check_flags[i];
                            int this_col = col + check_flags[i];

                            if (this_row < 0 || this_row >= size) continue;
                            if (this_col < 0 || this_col >= size) continue;

                            if (state[this_row, this_col] == player)
                            {
                                win_count++;
                            }
                            else
                            {
                                if (check_flags[i] < 0) bflag = 1;
                                else { fflag = 1; }
                            }
                            if (bflag == 1 && fflag == 1) break;

                        }

                        if (win_count == to_win)
                        {
                            Console.Write("WINNER!! - Diagonal \n");
                            win_condition = "Diagonal (negative)";
                            win_player = player;
                            return true;
                        }
                        break;
                }
            }
            return false;
        }

        public void reset()
        {
            win_player = -1;
            win_condition = "No Winner";
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    state[row, col] = 0;
                }
            }
        }

        public void print_state()
        {
            Console.Write("---------------------------\n");
            Console.Write("---------------------------\n");
            for (int row = size - 1; row >= 0; row--)
            {
                string rstring = "";

                for (int col = 0; col < size; col++)
                {
                    rstring += " " + state[row, col];
                }

                Console.Write(rstring + "\n");

            }
            Console.Write("---------------------------\n");
            Console.Write("---------------------------\n");

            Console.Write("Winning condition: " + win_condition);
            if (win_player != -1) { Console.Write(" ** WINNER **: " + win_player); }
            Console.Write("\n\n\n");
        }

    }



}
