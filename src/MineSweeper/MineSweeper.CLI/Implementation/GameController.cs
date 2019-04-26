using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineSweeper.CLI.Implementation;
using MineSweeper.Implementation.Boards;
using MineSweeper.Implementation.Enum;
using MineSweeper.Interfaces;

namespace MineSweeper.CLI.Implementation
{
    internal class GameController
    {
        MineSweeperView GameView { get; set; }
        MineSweeperBoard GameBoard { get; set; }

        public GameController()
        {
            this.GameView = new MineSweeperView();
            this.GameBoard = new MineSweeperBoard();
        }

        public void Start()
        {
            var size = DetermineBoardSize();
            this.GameBoard.Start(size);

            Run();
        }

        void Run()
        {
            bool gameOver = false;
            while (!gameOver)
            {
                DrawBoard(this.GameBoard.BoardTiles);
                var pos = GetInput();
                var response = this.GameBoard.OnClick(pos.Item1, pos.Item2);

                if (response.Status == GameStatus.Lost)
                {
                    DrawBoard(response.UpdatedBoard);
                    Console.WriteLine("Game over!");
                    gameOver = true;
                } else if (response.Status == GameStatus.Won)
                {
                    DrawBoard(response.UpdatedBoard);
                    Console.WriteLine("You won!");
                    gameOver = true;
                } else if (!String.IsNullOrWhiteSpace(response.Message))
                {
                    Console.WriteLine(response.Message);
                    Console.ReadLine();
                }
            }

            Console.ReadLine();
        }

        Tuple<int, int> GetInput()
        {
            int y = 0;
            int x = 0;

            bool finished = false;
            while (!finished)
            {
                Console.WriteLine("Which tile would you like to choose?");
                Console.WriteLine("y: ");

                var yIn = Console.ReadLine();
                
                if (!Int32.TryParse(yIn, out y))
                {
                    Console.WriteLine("Please enter a valid integer.");
                    continue;
                }

                Console.WriteLine("x: ");
                var xIn = Console.ReadLine();
               
                if (!Int32.TryParse(xIn, out x))
                {
                    Console.WriteLine("Please enter a valid integer.");
                    continue;
                }

                finished = true;
            }

            return new Tuple<int, int>(y, x);
        }

        void DrawBoard(ITile[,] board)
        {
            var formattedBoard = GetConsoleFormattedBoard(board);
            this.GameView.Write(formattedBoard);
        }

        string GetConsoleFormattedBoard(ITile[,] board)
        {
            StringBuilder sb = new StringBuilder();

            for (int y = 0; y < board.GetLength(0); y++)
            {
                for (int x = 0; x < board.GetLength(1); x++)
                {
                    var elementAtPos = board[y, x];

                    if (!elementAtPos.IsHidden)
                    {
                        if (!elementAtPos.IsEmpty)
                            sb.Append("X");
                        else if (elementAtPos.AdjacentTileCount != 0)
                            sb.Append(elementAtPos.AdjacentTileCount);
                        else
                            sb.Append("_");
                    }
                    else
                    {
                        sb.Append("\u25A1");
                    }
                    sb.Append("\t");
                }
                sb.Append("\n");
            }
            sb.AppendLine("");

            return sb.ToString();
        }

        BoardSize DetermineBoardSize()
        {
            BoardSize size = BoardSize.Undetermined;

            bool boardSelected = false;
            while (!boardSelected)
            {
                Console.WriteLine("Which board would you like to start with?");
                Console.WriteLine("small | medium | large");

                var response = Console.ReadLine();

                switch (response.ToLower())
                {
                    case "small":
                        size = BoardSize.Small;
                        boardSelected = true;
                        break;
                    case "medium":
                        size = BoardSize.Medium;
                        boardSelected = true;
                        break;
                    case "large":
                        size = BoardSize.Large;
                        boardSelected = true;
                        break;
                    default:
                        Console.WriteLine("Please select a valid option.");
                        break;
                }
            }

            return size;
        }
    }
}
