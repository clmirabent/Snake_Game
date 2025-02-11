using System.Collections.Generic;
using System;
using System.Threading;

namespace Snake_Game_V2
{
    public class Game
    {
        private Board _board;
        private Snake _snake;
        private List<int[]> _apple;
        private List<int[]> _bomb;
        
        public Game()
        {
            Console.Write("Enter the board size (N x N): ");
            int size;

            while (!(int.TryParse(Console.ReadLine(), out size) && size > 0))
            {
                Console.WriteLine("This number is invalid.Please enter a positive integer.");
                
            }
            _bomb = new List<int[]>();
            _apple = new List<int[]>();
            _snake = new Snake();
            _board = new Board(size, _snake, _apple, _bomb);
        }

        public MovementDirection MovementDirection { get; }

        public void Run()
        {
            int bombCounter = 0;
            bool keepPlaying = true;
            _board.PrintBoard();
            AddRandomApple();
            
            while (keepPlaying) {
                Thread.Sleep(500);
                ChangeSnakeDirection();
                keepPlaying = TryMoveSnake();
                _board.PrintBoard();
                
                bombCounter++;
                if (bombCounter % 8 == 0) // Add a bomb every 8 moves
                {
                    AddRandomBomb();
                }
            }
        }

        public void AddRandomApple()
        {
            Random rnd = new Random();
            var emptyPositions = _board.GetEmptyPositions();
            int[] newApplePosition = emptyPositions[rnd.Next(0, emptyPositions.Count - 1)];
            _apple.Add(newApplePosition);
        }

        public void AddRandomBomb()
        {
            Random rnd = new Random();
            var bombEmptyPositions = _board.GetEmptyPositions();
            int [] newBombPosition = bombEmptyPositions[rnd.Next(0, bombEmptyPositions.Count - 1)];
            _bomb.Add(newBombPosition);
        }

       void ChangeSnakeDirection()
       {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(false).Key;
                switch (key)
                {
                    case ConsoleKey.DownArrow when key != ConsoleKey.UpArrow:
                        _snake.Direction = MovementDirection.down;
                        break;
                    case ConsoleKey.UpArrow when key != ConsoleKey.DownArrow:
                        _snake.Direction = MovementDirection.up;
                        break;
                    case ConsoleKey.LeftArrow when key != ConsoleKey.RightArrow:
                        _snake.Direction = MovementDirection.left;
                        break;
                    case ConsoleKey.RightArrow when key != ConsoleKey.LeftArrow:
                        _snake.Direction = MovementDirection.right;
                        break;
                    default:
                        break;
                }
            }
       } 
       public bool TryMoveSnake()
       {
           bool moved = _snake.SnakeGrowthDirection(_board.BoardSize);
           
           int[] currentHeadPosition = _snake.HeadPosition; // Get snake's head position

           int[] eaten = null;
           
           int [] bombAte = null;
           
           
           foreach (var applePosition in _apple)
           {
               if (currentHeadPosition[0] == applePosition[0] && currentHeadPosition[1] == applePosition[1])
               {
                   eaten = applePosition;
               }
           }

           if (eaten == null)
           {
               _snake.CutTail();
           }
           else
           {
               _apple.Remove(eaten);
               AddRandomApple();
           }
           
           foreach (var bombPosition in _bomb)
           {
               if (currentHeadPosition[0] == bombPosition[0] && currentHeadPosition[1] == bombPosition[1])
               {
                  Console.WriteLine("---GAME OVER--- You ate a bomb!");
                  _bomb.Remove(bombAte);
                  return false; // End the game
               }
           }

           return moved;

         
          
       }
    }

}