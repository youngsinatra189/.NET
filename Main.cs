using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace A1_NonIntelligent_Version2
{
    class Program

    {
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

                // ArrayList should be shuffled here?
                availableMoves = Shuffle(availableMoves);

                int[] newCoordinates = moveKnight(listOfKnightMoves[counter], availableMoves[moveNumber]);

                listOfKnightMoves.Add(newCoordinates);

                bool repeatSquare = checkIfAlreadyBeenThere(listOfKnightMoves, newCoordinates);

                //*****************TESTING PURPOSES ONLY******************//

                Console.WriteLine("The knight has travelled to X: " + newCoordinates[0] + " Y: " + newCoordinates[1]);

                //*****************TESTING PURPOSES ONLY******************//

                if (repeatSquare == true)
                {
                    Console.WriteLine(" The knight has travelled to a repeat square, game over!");
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

            if (((currentPosition[0] + move1[0]) >= 0) && ((currentPosition[0] + move1[0]) < 8) && ((currentPosition[1] + move1[1]) >= 0) && ((currentPosition[1] + move1[1]) < 8)) 
            {
                availableMoves.Add(move1);
            }

            int[] move2 = new int[] { 1, -2 };

            if (((currentPosition[0] + move2[0]) >= 0) && ((currentPosition[0] + move2[0]) < 8) && ((currentPosition[1] + move2[1]) >= 0) && ((currentPosition[1] + move2[1]) < 8))
            {
                availableMoves.Add(move2);
            }

            int[] move3 = new int[] { 2, -1 };

            if (((currentPosition[0] + move3[0]) >= 0) && ((currentPosition[0] + move3[0]) < 8) && ((currentPosition[1] + move3[1]) >= 0) && ((currentPosition[1] + move3[1]) < 8))
            {
                availableMoves.Add(move3);
            }

            int[] move4 = new int[] { 2, 1 };

            if (((currentPosition[0] + move4[0]) >= 0) && ((currentPosition[0] + move4[0]) < 8) && ((currentPosition[1] + move4[1]) >= 0) && ((currentPosition[1] + move4[1]) < 8))
            {
                availableMoves.Add(move4);
            }

            int[] move5 = new int[] { 1, 2 };

            if (((currentPosition[0] + move5[0]) >= 0) && ((currentPosition[0] + move5[0]) < 8) && ((currentPosition[1] + move5[1]) >= 0) && ((currentPosition[1] + move5[1]) < 8))
            {
                availableMoves.Add(move5);
            }

            int[] move6 = new int[] { -1, 2 };

            if (((currentPosition[0] + move6[0]) >= 0) && ((currentPosition[0] + move6[0]) < 8) && ((currentPosition[1] + move6[1]) >= 0) && ((currentPosition[1] + move6[1]) < 8))
            {
                availableMoves.Add(move6);
            }

            int[] move7 = new int[] { -2, 1 };


            if (((currentPosition[0] + move7[0]) >= 0) && ((currentPosition[0] + move7[0]) < 8) && ((currentPosition[1] + move7[1]) >= 0) && ((currentPosition[1] + move7[1]) < 8))
            {
                availableMoves.Add(move7);
            }

            int[] move8 = new int[] { -2, -1 };


            if (((currentPosition[0] + move8[0]) >= 0) && ((currentPosition[0] + move8[0]) < 8) && ((currentPosition[1] + move8[1]) >= 0) && ((currentPosition[1] + move8[1]) < 8))
            {
                availableMoves.Add(move8);
            }
            return availableMoves;
        }

        // A method that, based on the size of the list of the available moves, selects a random move based on a randomly
        // Generated number

        public static int pickNextMove(List<int[]> availableMoves)
        {
            // At this point, there could be anywhere from 1-8 moves available
            int moveNumber = RandomInteger(0, availableMoves.Count());

            return moveNumber;
        }

        // A method to move the knight and return the coordinates where it currently is
        public static int[] moveKnight(int[] currentPosition, int[] nextMove)
        {
            int[] newCoordinates = new int[] { (currentPosition[0] + nextMove[0]), (currentPosition[1] + nextMove[1]) };

            return newCoordinates;
        }

        /**
        * This method will loop teach array in the ArrayList and check if the move has already been logged. If it has, the program will know 
        * to stop that iteration of the game and bring the results to the text file. If the knight hasn't been to that square,
        * - the program knows it can keep moving.
        **/
        public static bool checkIfAlreadyBeenThere(List<int[]> listOfKnightsMove, int[] nextMoveXY)
        {
            bool hasBeenThere = false;
            for (int x = 0; x < (listOfKnightsMove.Count() - 1); x++)
            {
                if (nextMoveXY[0] == listOfKnightsMove[x][0] && nextMoveXY[1] == listOfKnightsMove[x][1])
                {
                    hasBeenThere = true;
                } 
            }
            return hasBeenThere;
        }

        //Function to get a random number 
        private static int RandomInteger(int min, int max)
        {
            RNGCryptoServiceProvider Rand = new RNGCryptoServiceProvider();

            uint scale = uint.MaxValue;
            while (scale == uint.MaxValue)
            {
                // Get four random bytes.
                byte[] four_bytes = new byte[4];
                Rand.GetBytes(four_bytes);

                // Convert that into an uint.
                scale = BitConverter.ToUInt32(four_bytes, 0);
            }

            // Add min to the scaled difference between max and min.
            return (int)(min + (max - min) *
                (scale / (double)uint.MaxValue));
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

        //**********TESTING PURPOSES ONLY*******//
        public static List<T> Shuffle<T>(List<T> list)
        {
            Random rnd = new Random();
            for (int i = 0; i < list.Count; i++)
            {
                int k = rnd.Next(0, i);
                T value = list[k];
                list[k] = list[i];
                list[i] = value;
            }
            return list;
        }

    }
}
