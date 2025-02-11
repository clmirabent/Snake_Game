using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake_Game_V2
{
    public class Board
    {
        // ATRIBUTES
        private char[,] _gameBoard;
        private int _boardSize; // Dimensión del tablero
        private Snake _snake; //instacia de la clase serpiente
        private List<int[]> _apples;
        private List<int[]> _bombs;
        private List<int[]> _snakeBodyPositionCoordinates => _snake.SnakeBodyPositionCoordinates;

        public int BoardSize => _boardSize;

        // CONSTRUCTOR
        public Board(int boardSize, Snake snake, List<int[]> apples, List<int[]> bombs)
        {
            // Validate boardSize
            if (boardSize <= 0)
            {
                throw new ArgumentException("Board size must be greater than zero.", nameof(boardSize));
            }

            _boardSize = boardSize;
            _gameBoard = new char[_boardSize, _boardSize];
            _snake = snake;
            _apples = apples;
            _bombs = bombs;
            UpdateBoard();
        }

        public void UpdateBoard()
        {
            for (int i = 0; i < _boardSize; i++)
            {
                for (int j = 0; j < _boardSize; j++)
                {
                    _gameBoard[i, j] = ' ';
                }
            }

            // _snakeBodyPositionCoordinates is a list of array of 2 elements
            foreach (int[] snakeBodyPosition in _snakeBodyPositionCoordinates)
            {
                // snakeBodyPosition is array of 2 integer: row and column
                // snakeBodyPosition[0] is the row number
                // snakeBodyPosition[1] is the column number
                _gameBoard[snakeBodyPosition[0], snakeBodyPosition[1]] = 'O';
            }

            _gameBoard[_snake.HeadPosition[0], _snake.HeadPosition[1]] = '*';

            foreach (int[] apple in _apples)
            {
                _gameBoard[apple[0], apple[1]] = '@';
            }

            foreach (int[] bomb in _bombs)
            {
                _gameBoard[bomb[0], bomb[1]] = 'B';
            }
        }

        public void PrintBoard()
        {
            UpdateBoard();
            Console.Clear();
            Console.WriteLine(new string('-', _boardSize * 4 + 1)); // Horizontal line separator
            for (int i = 0; i < _boardSize; i++)
            {
                Console.Write("| ");
                for (int j = 0; j < _boardSize; j++)
                {
                    Console.Write(_gameBoard[i, j] + " | ");
                }

                Console.WriteLine(); // New line for the next row
                Console.WriteLine(new string('-', _boardSize * 4 + 1)); // Horizontal line separator
            }

            Console.WriteLine("Use the arrow keys to move the snake. Press ESC to exit the game.");
        }

        public List<int[]> GetEmptyPositions()
        {
            List<int[]> emptyCoordinates = new List<int[]>();
            for (int i = 0; i < _boardSize; i++)
            {
                for (int j = 0; j < _boardSize; j++)
                {
                    if (_gameBoard[i, j] == ' ')
                    {
                        emptyCoordinates.Add(new[] { i, j });
                    }
                }
            }

            return emptyCoordinates;
        }

    }
}

