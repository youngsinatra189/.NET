using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1_NonIntelligent_Version2
{
    class Program

    {
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        
        static void Main(string[] args)
        {
            // A dynamic list to keep track of the knights moves
            List<int[]> listOfKnightMoves = new List<int[]>();
            // Putting the knight in its starting place
            int[] defaultStart = new int[] { 0, 0 };
            listOfKnightMoves.Add(defaultStart);

            // Keeping track of the move number
            int counter = 0;

            // While loop to keep the knight moving
            bool gameStatus = true;
            
            while (gameStatus == true)
            {
                // Determining the available moves based on where the kngiht already is
                List<int[]> availableMoves = determineAvailableMoves(listOfKnightMoves[counter]);

                int moveNumber = pickNextMove(availableMoves);

                int[] newCoordinates = moveKnight(listOfKnightMoves[counter], availableMoves[moveNumber]);

                listOfKnightMoves.Add(newCoordinates);

                bool repeatSquare = checkIfAlreadyBeenThere(listOfKnightMoves, newCoordinates);

                //*****************TESTING PURPOSES ONLY******************//

                Console.WriteLine("The knight has travelled to X: " + newCoordinates[0] + " Y: " + newCoordinates[1]);

                //*****************TESTING PURPOSES ONLY******************//

                if (repeatSquare == true)
                {
                    Console.WriteLine("At this point the game would be over! The knight has travelled to a repeat square");
                    nonIntelligentEndGamePrint(listOfKnightMoves);
                    gameStatus = false;

                } else
                {
                    counter++;
                }

            }
            Console.ReadKey();
        }

        // A method that calculates the available moves for the knight. 
        // The method assess the current position of the knight
        public static List<int[]> determineAvailableMoves(int[] currentPosition)
        {
            // List of available moves
            List<int[]> availableMoves = new List<int[]>();

            // 8 potential, not necessarily available moves the knight could undergo
            int[] move1 = new int[] {-1, -2 };

            if (((currentPosition[0] + move1[0]) >= 0) && ((currentPosition[0] + move1[0]) <= 7) && ((currentPosition[1] + move1[1]) >= 0) && ((currentPosition[1] + move1[1]) <= 7)) 
            {
                availableMoves.Add(move1);
            }

            int[] move2 = new int[] { 1, -2 };

            if (((currentPosition[0] + move2[0]) >= 0) && ((currentPosition[0] + move2[0]) <= 7) && ((currentPosition[1] + move2[1]) >= 0) && ((currentPosition[1] + move2[1]) <= 7))
            {
                availableMoves.Add(move2);
            }

            int[] move3 = new int[] { 2, -1 };

            if (((currentPosition[0] + move3[0]) >= 0) && ((currentPosition[0] + move3[0]) <= 7) && ((currentPosition[1] + move3[1]) >= 0) && ((currentPosition[1] + move3[1]) <= 7))
            {
                availableMoves.Add(move3);
            }

            int[] move4 = new int[] { 2, 1 };

            if (((currentPosition[0] + move4[0]) >= 0) && ((currentPosition[0] + move4[0]) <= 7) && ((currentPosition[1] + move4[1]) >= 0) && ((currentPosition[1] + move4[1]) <= 7))
            {
                availableMoves.Add(move4);
            }

            int[] move5 = new int[] { 1, 2 };

            if (((currentPosition[0] + move5[0]) >= 0) && ((currentPosition[0] + move5[0]) <= 7) && ((currentPosition[1] + move5[1]) >= 0) && ((currentPosition[1] + move5[1]) <= 7))
            {
                availableMoves.Add(move5);
            }

            int[] move6 = new int[] { -1, 2 };

            if (((currentPosition[0] + move6[0]) >= 0) && ((currentPosition[0] + move6[0]) <= 7) && ((currentPosition[1] + move6[1]) >= 0) && ((currentPosition[1] + move6[1]) <= 7))
            {
                availableMoves.Add(move6);
            }

            int[] move7 = new int[] { -2, 1 };


            if (((currentPosition[0] + move7[0]) >= 0) && ((currentPosition[0] + move7[0]) <= 7) && ((currentPosition[1] + move7[1]) >= 0) && ((currentPosition[1] + move7[1]) <= 7))
            {
                availableMoves.Add(move7);
            }

            int[] move8 = new int[] { -2, -1 };


            if (((currentPosition[0] + move8[0]) >= 0) && ((currentPosition[0] + move8[0]) <= 7) && ((currentPosition[1] + move8[1]) >= 0) && ((currentPosition[1] + move8[1]) <= 7))
            {
                availableMoves.Add(move8);
            }

            //*****************TESTING PURPOSES ONLY******************//

            Console.WriteLine("The available moves are ");
            for (int x = 0; x < availableMoves.Count(); x++)
            {
                Console.WriteLine("X: " + availableMoves[x][0] + "Y: " + availableMoves[x][1]);
            }

            //*****************TESTING PURPOSES ONLY******************//


            return availableMoves;
        }

        // A method that, based on the size of the list of the available moves, selects a random move based on a randomly
        // Generated number

        public static int pickNextMove(List<int[]> availableMoves)
        {
            // At this point, there could be anywhere from 1-8 moves available
            int moveNumber = RandomNumber(0, availableMoves.Count());

            //*****************TESTING PURPOSES ONLY******************//

            Console.WriteLine("The move that is picked is number " + moveNumber);

            //*****************TESTING PURPOSES ONLY******************//
            return moveNumber;
        }

        // A method to move the knight and return the coordinates where it currently is

        public static int[] moveKnight(int[] currentPosition, int[] nextMove)
        {
            int[] newCoordinates = new int[] { (currentPosition[0] + nextMove[0]), (currentPosition[1] + nextMove[1]) };

            //*****************TESTING PURPOSES ONLY******************//

            Console.WriteLine("The new coordinates of the knight would be X: " + newCoordinates[0] + " Y: " + newCoordinates[1]);

            //*****************TESTING PURPOSES ONLY******************//

            return newCoordinates;
        }

        /**
        * Before the knight can undergo a move that is confirmed to not knock if off the board. This method will loop though
        * each array in the ArrayList and check if the move has already been logged. If it has, the program will know 
        * to stop that iteration of the game and bring the results to the text file. If the knight hasn't been to that square,
        * - the program knows it can move the knight and go to the next move
        * 
        **/
        public static bool checkIfAlreadyBeenThere(List<int[]> listOfKnightsMove, int[] nextMoveXY)
        {
            bool hasBeenThere = false;
            for (int x = 0; x < (listOfKnightsMove.Count() - 1); x++)
            {
                if (nextMoveXY[0] == listOfKnightsMove[x][0] && nextMoveXY[1] == listOfKnightsMove[x][1])
                {
                    hasBeenThere = true;
                    Console.WriteLine("The knight has arleady beeen here, so we're done");
                } else
                {
                    Console.WriteLine("The knight hasn't been here, let's keep moving");
                }
            }
            return hasBeenThere;
        }

        //Function to get a random number 
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        }
        /**
         * When the game has ended, the number of moves has to be written to the text doc
        **/
        public static void nonIntelligentEndGamePrint(List<int[]> listOfKnightsMove)
        {
            using (StreamWriter writer = new StreamWriter("roshansahuNonIntelligentMethod.txt"))
            {
                writer.WriteLine(" Trial x: The knight was able to successfully touch " + listOfKnightsMove.Count() + " squares.");
            }
        }
    }
}
