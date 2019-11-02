
using MineServer.Resources;
using System;
/**
* @(#) Map.cs
*/
namespace MineServer.Models
{
	public class Map
	{
		private Cell[,] _cells;
        private readonly CellFactory _factory = new CellFactory();
        private static readonly Object obj = new Object();

		 public Map(int index1, int index2)
        {
            lock (obj)
            {
                _cells = new Cell[index1, index2];
                for (int i = 0; i < index1; i++)
                {
                    for (int j = 0; j < index2; i++)
                    {
                        _cells[i, j] = _factory.Create("Empty");
                    }
                }
            }
        }

         
         /// <summary>
         /// Reveals a singular cell, and if it is not a bomb, reveals surrounding non Bomb cells
         /// </summary>
         /// <param name="index1"></param>
         /// <param name="index2"></param>
         /// <returns></returns>
         public Result RevealCell(int index1, int index2)
        {
            Result result = new Result();
            lock (obj)
            {
                if (!_cells[index1, index2].marked)
                {
                    if (_cells[index1, index2] is Tnt)
                    {
                        _cells[index1, index2] = _factory.Create("ExplodedTNT");
                    }
                    else
                    {
                        RevealMoreCells(index1, index2);
                    }
                    result.success = true;
                }
                return BuildMap(new Result());
            }
        }

        /// <summary>
        /// Reveals empty cells until finds cell near bomb
        /// </summary>
        /// <param name="i">Coordinate X</param>
        /// <param name="j">Coordinate Y</param>
        private void RevealMoreCells(int i, int j)
        {
            _cells[i, j] = _factory.Create("Revealed");
            int bombs = CalculateBombs(i, j);
            _cells[i, j].bombs = bombs;
            if (bombs != 0)
                return;
            if (i > 0)//if there are cells downwards
            {
                if (!(_cells[i - 1, j] is Revealed))//down
                {
                    RevealMoreCells(i - 1, j);
                }
                if (!(_cells[i - 1, j - 1] is Revealed))//down left
                {
                    RevealMoreCells(i - 1, j - 1);
                }
                if ((j < _cells.GetLength(1)) && !(_cells[i - 1, j + 1] is Revealed))//down right
                {
                    RevealMoreCells(i - 1, j + 1);
                }
            }
            if (i < _cells.GetLength(0))// if there are cells upwards
            {
                if (!(_cells[i + 1, j] is Revealed))//up
                {
                    RevealMoreCells(i + 1, j);
                }
                if (!(_cells[i + 1, j - 1] is Revealed))//up left
                {
                    RevealMoreCells(i + 1, j - 1);

                }
                if ((j < _cells.GetLength(1)) && !(_cells[i + 1, j + 1] is Revealed))//up right
                {
                    RevealMoreCells(i + 1, j + 1);
                }
            }
            if (j > 0 && !(_cells[i, j - 1] is Revealed))// if there are cells left
            {
                RevealMoreCells(i, j - 1);
            }
            if (j < _cells.GetLength(1) && !(_cells[i, j + 1] is Revealed))// if there are cells right
            {
                RevealMoreCells(i, j + 1);
            }
        }
        
        /// <summary>
        /// finds the number of bombs around cell
        /// </summary>
        /// <param name="i">Coordinate X</param>
        /// <param name="j">Coordinate Y</param>
        /// <returns></returns>
        private int CalculateBombs(int i, int j)
        {
            int bombs = 0;
            if (i > 0)//if there are cells downwards
            {
                if (_cells[i - 1, j] is Tnt)//down
                {
                    bombs++;
                }
                if (_cells[i - 1, j - 1] is Tnt)//down left
                {
                    bombs++;
                }
                if ( (j < _cells.GetLength(1)) && _cells[i - 1, j + 1] is Tnt)//down right
                {
                    bombs++;
                }
            }
            if(i < _cells.GetLength(0))// if there are cells upwards
            {
                if (_cells[i + 1, j] is Tnt)//up
                {
                    bombs++;
                }
                if (_cells[i + 1, j - 1] is Tnt)//up left
                {
                    bombs++;

                }
                if ((j < _cells.GetLength(1)) && _cells[i + 1, j + 1] is Tnt)//up right
                {
                    bombs++;
                }
            }
            if (j > 0 && _cells[i, j - 1] is Tnt)// if there are cells left
            {
                bombs++;

            }
            if (j < _cells.GetLength(1) && _cells[i, j + 1] is Tnt)// if there are cells right
            {
                bombs++;
            }
            _cells[i, j].bombs = bombs;
            return bombs;
        }

        /// <summary>
        /// Marks the cell  as bomb
        /// </summary>
        /// <param name="index1">Coordinate X</param>
        /// <param name="index2">Coordinate Y</param>
        /// <returns>Map with marked cell</returns>
        public Result MarkCell(int index1, int index2)
        {
            lock (obj)
            {
<<<<<<< Updated upstream
                _cells[index1, index2].marked = !_cells[index1, index2].marked;
                return BuildMap(new Result());//Returns new result
=======
                if (!cells[index1, index2].marked)
                    cells[index1, index2].marked = true;
                else
                    cells[index1, index2].marked = false;
                return BuildMap(BuildMap(new Result()));
>>>>>>> Stashed changes
            }
        }

        /// <summary>
        /// Sets mine in a cell
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public Result SetMine(int X, int Y)
        {
            Result result = new Result();
            lock (obj)
            {
                if(!(_cells[X, Y] is Tnt))//Check if not already set
                    _cells[X, Y] = _factory.Create("TNT");
                else
                {
                    result.success = false;
                    return result;
                }
            }
            return BuildMap(result, false);
        }

        
        /// <summary>
        /// Unsets mine
        /// </summary>
        /// <param name="X">Coordinate X</param>
        /// <param name="Y">Coordinate Y</param>
        /// <returns>Updated map if success, failure if cell wasn't a mine</returns>
        public Result UnsetMine(int X, int Y)
        {
            Result result = new Result();
            lock (obj)
            {
                if (_cells[X, Y] is Tnt)//Check if already set
                {
                    var marked = _cells[X, Y].marked;
                    _cells[X, Y] = _factory.Create("Unknown");
                    _cells[X, Y].marked = marked;
                }
                else
                {
                    result.success = false;
                    return result;
                }
            }
            return BuildMap(result, false);
        }

        /// <summary>
        /// Builds a map to return for a player
        /// Returns a different type of map depending on player role
        /// Also returns if the player lost or won
        /// </summary>
        /// <param name="result">Final result to send</param>
        /// <param name="mineSweeper">true if the player is a minesweeper</param>
        /// <returns></returns>
        private Result BuildMap(Result result, bool mineSweeper = true)
        {
<<<<<<< Updated upstream
            //The default game status is 0 aka Ongoing
            //result.status = GameStatus.Ongoing;
            result.success = true;
            bool finished = true;
            lock (obj)
=======
            result.map = new char[cells.GetLength(0), cells.GetLength(1)];
            result.status = GameStatus.Ongoing;
            result.success = true;
            for (int i = 0; i < cells.GetLength(0); i++)
>>>>>>> Stashed changes
            {
                result.map = new char[_cells.GetLength(0), _cells.GetLength(1)];
                for (int i = 0; i < _cells.GetLength(0); i++)
                {
                    for (int j = 0; j < _cells.GetLength(1); j++)
                    {
                        var cell = _cells[i, j];
                        if (cell is Tnt)
                            result.map[i, j] = mineSweeper ? 'u' : 't';// unknown or TNT
                        else if (cell is Revealed)
                            result.map[i, j] = cell.bombs.ToString()[0];// max number is 6
                        else if (cell is ExplodedTnt)
                        {
                            result.map[i, j] = 'e';// Exploded
                            result.status = mineSweeper ? GameStatus.Lost : GameStatus.Won;//status changed to lost or won
                        }else if (cell is Unknown)
                        {
                            result.map[i, j] = 'u'; // empty cell
                            finished = false;
                        }

                    }
                }
            }
            //If all empty cells are revealed and the game is not finished yet, the minesweeper wins
            if (result.status.Equals(GameStatus.Ongoing) && finished)
                result.status = mineSweeper ? GameStatus.Won : GameStatus.Lost;
            return result;
        }

        public Result Update(bool mineSweeper)
        {
            return BuildMap(new Result());
        }
    }
	
}
