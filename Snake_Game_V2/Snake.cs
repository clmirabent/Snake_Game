using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake_Game_V2
{
    public enum MovementDirection
    {
        up,
        down,
        right,
        left
    }
    
    public class Snake
    {
        // ATRIBUTES 
        private List<int[]> _snakeBodyPositionCoordinates;
        private MovementDirection _direction;
        private Board _board;

        
        // PROPERTY
        public List<int[]> SnakeBodyPositionCoordinates 
        { 
            get { return _snakeBodyPositionCoordinates; } 
            private set { _snakeBodyPositionCoordinates = value; }
        }
        public int[] HeadPosition => _snakeBodyPositionCoordinates.Last(); 
        public MovementDirection Direction { get => _direction;  set => _direction = value; }
        
        // CONSTRUCTOR 
        public Snake()
        {
            _direction = MovementDirection.right;
            _snakeBodyPositionCoordinates = new List<int[]>();
            // Initiate snake with body and head
            int[] head = { 0, 1 };
            int[] body = { 0, 0 };
            _snakeBodyPositionCoordinates.Add(body);
            _snakeBodyPositionCoordinates.Add(head);
        }
        
        public void CutTail()
        {
            _snakeBodyPositionCoordinates.RemoveAt(0); //delete the tail 
        }

        //snake grow and move
        public bool SnakeGrowthDirection(int boardSize)
        {
            int[] currentHead = HeadPosition;
            int newHeadRow = currentHead[0];
            int newHeadColumn = currentHead[1];

            switch (_direction)
            {
                case MovementDirection.up:
                    newHeadRow--;
                    break;
                case MovementDirection.down:
                    newHeadRow++;
                    break;
                case MovementDirection.right:
                    newHeadColumn++;
                    break;
                case MovementDirection.left:
                    newHeadColumn--;
                    break;
                default:
                    return false; // Exit the function
            }

            // Check if the new position is within the board bounds
            if (newHeadColumn < 0 || newHeadColumn >= boardSize || newHeadRow < 0 || newHeadRow >= boardSize)
            {
                Console.WriteLine("---GAME OVER--- Snake is out of bounds");
                return false;
            }
            
            // Self-collision check
            foreach (var segment in _snakeBodyPositionCoordinates)
            {
                if (segment[0] == newHeadRow && segment[1] == newHeadColumn)
                {
                    Console.WriteLine("---GAME OVER--- Snake hit itself");
                    return false;
                }
            }
            
            _snakeBodyPositionCoordinates.Add(new int[] { newHeadRow, newHeadColumn }); //add the new head position
            return true;
        }
    }
}