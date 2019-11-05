
using MineServer.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
/**
* @(#) Map.cs
*/
namespace MineServer.Models
{
	public class Map : ModelClass
    {
		public  List<Cell> _cells { get; set; }
        //public Game game { get; set; }
        private readonly Factory _factory = new CellFactory();
        private static readonly Object obj = new Object();

		 public Map()
        {
            _cells = new List<Cell>();
            for (int i = 0; i < 10 * 10; i++)
            {
                _cells.Add(_factory.Create("Unknown"));
                _cells[i].number = i;
            }
        }

        private int Index(int i, int j = 0)
        {
            return i * 10 + j;
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
                if (!_cells[Index(index1,index2)].marked)
                {
                    if (_cells[Index(index1, index2)] is Tnt)
                    {
                        _cells[Index(index1, index2)] = _factory.Create("ExplodedTNT");
                        _cells[Index(index1,index2)].number = Index(index1, index2);
                    }
                    else
                    {
                        try
                        {
                            RevealMoreCells(index1, index2);
                        }
                        catch(Exception ex)
                        {
                            throw ex;
                        }
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
            _cells[Index(i, j)] = _factory.Create("Revealed");
            _cells[Index(i, j)].number = Index(i, j);

            int bombs = CalculateBombs(i, j);
            _cells[Index(i, j)].bombs = bombs;
            if (bombs != 0)
                return;
            if (i > 0)//if there are cells downwards
            {
                if (!(_cells[Index(i-1, j)] is Revealed))//down
                {
                    RevealMoreCells(i - 1, j);
                }
                if ( j > 0 && !(_cells[Index(i-1, j-1)] is Revealed))//down left
                {
                    RevealMoreCells(i - 1, j - 1);
                }
                if ((j < 10-1) && !(_cells[Index(i-1, j+1)] is Revealed))//down right
                {
                    RevealMoreCells(i - 1, j + 1);
                }
            }
            if (i < 10-1)// if there are cells upwards
            {
                if (!(_cells[Index(i+1, j)] is Revealed))//up
                {
                    RevealMoreCells(i + 1, j);
                }
                if (j > 0 && !(_cells[Index(i+1, j-1)] is Revealed))//up left
                {
                    RevealMoreCells(i + 1, j - 1);

                }
                if ((j < 10-1) && !(_cells[Index(i+1, j+1)] is Revealed))//up right
                {
                    RevealMoreCells(i + 1, j + 1);
                }
            }
            if (j > 0 && !(_cells[Index(i, j-1)] is Revealed))// if there are cells left
            {
                RevealMoreCells(i, j - 1);
            }
            if (j < 10-1 && !(_cells[Index(i, j+1)] is Revealed))// if there are cells right
            {
                RevealMoreCells(i, j + 1);
            }
        }

        internal Result Surrender(bool mineSweeper)
        {
            for(int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (mineSweeper)
                    {
                        _cells[Index(i, j)] = _factory.Create("ExplodedTNT");
                        _cells[Index(i, j)].number = Index(i, j);

                    }
                    else if(!mineSweeper && _cells[Index(i, j)] is Unknown)
                    {
                        _cells[Index(i, j)] = _factory.Create("Revealed");
                        _cells[Index(i, j)].number = Index(i, j);

                    }
                }
            }
            return BuildMap(new Result(), mineSweeper);
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
                if (_cells[Index(i-1, j)] is Tnt)//down
                {
                    bombs++;
                }
                if (j > 0 && _cells[Index(i-1, j-1)] is Tnt)//down left
                {
                    bombs++;
                }
                if ( (j < 10-1) && _cells[Index(i-1, j+1)] is Tnt)//down right
                {
                    bombs++;
                }
            }
            if(i < 10-1)// if there are cells upwards
            {
                if (_cells[Index(i+1, j)] is Tnt)//up
                {
                    bombs++;
                }
                if (j > 0 && _cells[Index(i+1, j-1)] is Tnt)//up left
                {
                    bombs++;

                }
                if ((j < 10-1) && _cells[Index(i+1, j+1)] is Tnt)//up right
                {
                    bombs++;
                }
            }
            if (j > 0 && _cells[Index(i, j-1)] is Tnt)// if there are cells left
            {
                bombs++;

            }
            if (j < (10-1) && _cells[Index(i, j+1)] is Tnt)// if there are cells right
            {
                bombs++;
            }
            _cells[Index(i, j)].bombs = bombs;
            return bombs;
        }

        /// <summary>
        /// Marks the cell  as bomb
        /// </summary>
        /// <param name="index1">Coordinate X</param>
        /// <param name="index2">Coordinate Y</param>
        /// <returns>Map with marked cell</returns>
        public Result MarkCell(int i, int j)
        {
            lock (obj)
            {
                _cells[Index(i, j)].marked = !_cells[Index(i, j)].marked;
                return BuildMap(new Result());//Returns new result
            }
        }

        /// <summary>
        /// Sets mine in a cell
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public Result SetMine(int i, int j)
        {
            int id;
            Result result = new Result();
            lock (obj)
            {
                if (!(_cells[Index(i, j)] is Tnt))
                {//Check if not already set
                    _cells[Index(i, j)] = _factory.Create("TNT");
                    _cells[Index(i, j)].number = Index(i, j);

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
        /// Unsets mine
        /// </summary>
        /// <param name="X">Coordinate X</param>
        /// <param name="Y">Coordinate Y</param>
        /// <returns>Updated map if success, failure if cell wasn't a mine</returns>
        public Result UnsetMine(int i, int j)
        {
            Result result = new Result();
            lock (obj)
            {
                if (_cells[Index(i, j)] is Tnt)//Check if already set
                {
                    var marked = _cells[Index(i, j)].marked;
                    _cells[Index(i, j)] = _factory.Create("Unknown");
                    _cells[Index(i, j)].number = Index(i, j);
                    _cells[Index(i, j)].marked = marked;
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
            //The default game status is 0 aka Ongoing
            //result.status = GameStatus.Ongoing;
            result.success = true;
            bool finished = true;
            lock (obj)
            {
                result.map = new char[10, 10];
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        var cell = _cells[Index(i, j)];
                        if (cell is Tnt)
                        {
                            result.map[i, j] = mineSweeper ? 'u' : 't';// unknown or TNT
                            _cells[Index(i, j)] = _factory.Create("TNT");
                            _cells[Index(i, j)].number = Index(i, j);
                        }
                        else if (cell is Revealed)
                        {
                            result.map[i, j] = cell.bombs.ToString()[0];// max number is 6
                            _cells[Index(i, j)] = _factory.Create("Revealed");
                            _cells[Index(i, j)].number = Index(i, j);
                        }
                        else if (cell is ExplodedTnt)
                        {
                            result.map[i, j] = 'e';// Exploded
                            finished = false;
                            result.status = mineSweeper ? GameStatus.Lost : GameStatus.Won;//status changed to lost or won
                            _cells[Index(i, j)] = _factory.Create("ExplodedTNT");
                            _cells[Index(i, j)].number = Index(i, j);
                        }
                        else if (cell is Unknown)
                        {
                            result.map[i, j] = 'u'; // empty cell
                            finished = false;
                            _cells[Index(i, j)] = _factory.Create("Unknown");
                            _cells[Index(i, j)].number = Index(i, j);
                        }
                        if (cell.marked && !(cell is Revealed))
                            result.map[i, j] = 'm'; // empty cell
                        _cells[Index(i, j)].marked = cell.marked;
                        _cells[Index(i, j)].bombs = cell.bombs;
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
