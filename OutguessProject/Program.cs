using System.Data.SqlTypes;

namespace OutguessProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //variables
            int guesses;
            double bet;
            bool win;
            int maxGuesses = 10;
            double winningAmt;
            int cpuNum;
            int playerNum = 0;
            int winsTracker = 0;
            double winPercent;
            double wager; 
            bool userInput = true;
            int totalRounds = 0;
            int gameNum = 1;

            //game intro & user enters initial bet amount
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Welcome to Outguess, gambler!");
            Console.ReadKey();
            Console.Write("First, do you want to place a bet?\nEnter the amount you're bringing to the table: $");
            bet = double.Parse(Console.ReadLine()); //player's bet

            if (bet <= 0)
            {
                Console.WriteLine("ERROR\nBet must be a valid amount.");
                Console.Write("Enter the amount you're bringing to the table: $");
                bet = double.Parse(Console.ReadLine());
            }//input validation for bet

            do
            {
                //create random number generator
                Random rnd_maker = new Random();
                cpuNum = rnd_maker.Next(1, 100); //using .next method to generate random num between 1 and 100

                win = false;

                do
                {
                    Console.WriteLine("------------------------------------------------------------");
                    Console.Write("Enter your wager amount: $");
                    wager = double.Parse(Console.ReadLine());

                    if (wager > bet || wager <= 0)
                    {
                        Console.WriteLine("ERROR\nWager cannot exceed bet placed or wager is too low.");
                        Console.Write("Enter wager amount: $");

                    }
                    bet = bet - wager;

                } while (wager > bet);

                do
                {
                    Console.Write("In how many guesses do you think you can guess my number? (10 max): ");
                    guesses = int.Parse(Console.ReadLine()); //getting number of guesses from user

                    if (guesses > maxGuesses || guesses < 0)
                    {
                        Console.WriteLine("ERROR\nPlease enter a valid number of guesses to wage.");
                    }//input validation for guesses wagered


                } while (guesses > 10 || guesses <= 0);

                Console.WriteLine("I've chosen a secret number between 1 & 100...now guess it!");

                //player turn loop
                while (guesses > 0 && playerNum != cpuNum)
                {
                    do
                    {
                        
                        Console.WriteLine();
                        Console.WriteLine("What's your guess?");
                        playerNum = int.Parse(Console.ReadLine());
                        totalRounds += 1;

                    } while (playerNum < 1 || playerNum > 100); //input validation w conditional operator for player guess

                    if (playerNum == cpuNum)
                    {
                        Console.WriteLine("You guessed my number! You win...this time!");
                        win = true;
                        winsTracker += 1;
                        winningAmt = wager * WagerMultiplier(totalRounds, win);
                        Console.WriteLine("You won {0:C} this round.", winningAmt);
                        bet += winningAmt;
                        
                        gameNum += 1;
                        
                        if (bet > 0)
                        {
                            Console.WriteLine("Your current bank: {0:C}", bet);
                            Console.WriteLine("Do you want to cash out? Press Y for YES or N for NO.", userInput);
                            userInput = Console.ReadKey(true).KeyChar.ToString().ToLower() == "y";

                            if (userInput)
                            {
                                Console.WriteLine("Thanks for playing!");
                                if (winsTracker >= 1)
                                {
                                    winPercent = winsTracker / gameNum * 100;
                                    Console.WriteLine("Your win percentage: {0}%", winPercent);
                                    Console.WriteLine("Your bank has: {0:C}", bet);
                                }

                            }
                            else if (userInput == false)
                            {
                                Console.WriteLine("Happy playing and good luck!");
                                if (winsTracker >= 1)
                                {
                                    winPercent = winsTracker / gameNum * 100;
                                    Console.WriteLine("Your win percentage: {0}%", winPercent);
                                    Console.WriteLine("Your bank has: {0:C}", bet);
                                }
                            }

                        }

                    }
                    else if (playerNum < cpuNum)
                    {
                        Console.WriteLine("Too low. Total guesses remaining: {0}", guesses - 1);
                        guesses -= 1;
                        

                    }
                    else if (playerNum > cpuNum)
                    {
                        Console.WriteLine("Too high. Total guesses remaining: {0}", guesses - 1);
                        guesses -= 1;
                        
                    }

                }

                if (playerNum != cpuNum && guesses == 0)
                {
                    Console.WriteLine("That's too bad! The correct number was {0}. ", cpuNum);
                    Console.WriteLine("Your wager is forfeit and this round of guessing is over.");
                    //lossesTracker += 1;

                    if (bet > 0)
                    {
                        Console.WriteLine("You have {0:C}\nDo you want to cash out? Press Y for YES or N for NO.", bet);
                        userInput = Console.ReadKey(true).KeyChar.ToString().ToLower() == "y";

                        if (userInput)
                        {
                            Console.WriteLine("Thanks for playing!");
                            Console.WriteLine("You went home with: {0:C}", bet);

                        }
                        else if (userInput == false)
                        {
                            Console.WriteLine("Happy playing and good luck!");
                            //win percent calculation
                            winPercent = winsTracker / totalRounds * 100;
                            Console.WriteLine("Your win percentage: {0}%", winPercent);
                            Console.WriteLine("Your bank has: {0:C}", bet);
                            

                        }
                    }
                }

            } while (userInput && bet > 0);


            if (bet <= 0)
            {
                Console.WriteLine("You're out of moolah! Add more to play again.");

            }//end if statement for zero money in bank 

        }//end main

        static int WagerMultiplier(int totalRounds, bool win)
        {
            if (totalRounds == 10 && win == true) return 1;
            if (totalRounds == 9 && win == true) return 2;
            if (totalRounds == 8 && win == true) return 3;
            if (totalRounds == 7 && win == true) return 4;
            if (totalRounds == 6 && win == true) return 5;
            if (totalRounds == 5 && win == true) return 6;
            if (totalRounds == 4 && win == true) return 7;
            if (totalRounds == 3 && win == true) return 8;
            if (totalRounds == 2 && win == true) return 9;
            if (totalRounds == 1 && win == true) return 10;
            else return 0;
        }

    }//end class
}//end namespace