
using MineServer.Resources;
using System;
using System.Collections.Generic;

/**
* @(#) Map.cs
*/
namespace MineServer.Models
{
	public class Map : ModelClass
    {
		public  List<Cell> Cells { get; set; }
        //public Game game { get; set; }
        private readonly Factory _factory = new CellFactory();
        private static readonly object Obj = new object();

		 public Map()
        {
            Cells = new List<Cell>();
            for (int i = 0; i < 10 * 10; i++)
            {
                Cells.Add(_factory.Create("Unknown"));
                Cells[i].number = i;
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
            lock (Obj)
            {
                if (!Cells[Index(index1,index2)].marked)
                {
                    if (Cells[Index(index1, index2)] is Tnt)
                    {
                        Cells[Index(index1, index2)] = _factory.Create("ExplodedTNT");
                        Cells[Index(index1,index2)].number = Index(index1, index2);
                        BombExploded();
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

         private void BombExploded()
         {
             for (int i = 0; i < Cells.Count; i++)
             {
                 switch (Cells[i].marked)
                 {
                    case true:
                        if (!(Cells[i] is Tnt))
                        {
                            int number = Cells[i].number;
                            Cells[i] = _factory.Create("WrongTNT");
                            Cells[i].number = number;
                        }
                        break;
                    case false:
                        if (Cells[i] is Tnt)
                        {
                            int number = Cells[i].number;
                            Cells[i] = _factory.Create("ExplodedTNT");
                            Cells[i].number = number;
                        }
                        break;
                 }
             }
         }

        /// <summary>
        /// Reveals empty cells until finds cell near bomb
        /// </summary>
        /// <param name="i">Coordinate X</param>
        /// <param name="j">Coordinate Y</param>
        private void RevealMoreCells(int i, int j)
        {
            Cells[Index(i, j)] = _factory.Create("Revealed");
            Cells[Index(i, j)].number = Index(i, j);

            int bombs = CalculateBombs(i, j);
            Cells[Index(i, j)].bombs = bombs;
            if (bombs != 0)
                return;
            if (i > 0)//if there are cells downwards
            {
                if (!(Cells[Index(i-1, j)] is Revealed))//down
                {
                    RevealMoreCells(i - 1, j);
                }
                if ( j > 0 && !(Cells[Index(i-1, j-1)] is Revealed))//down left
                {
                    RevealMoreCells(i - 1, j - 1);
                }
                if ((j < 10-1) && !(Cells[Index(i-1, j+1)] is Revealed))//down right
                {
                    RevealMoreCells(i - 1, j + 1);
                }
            }
            if (i < 10-1)// if there are cells upwards
            {
                if (!(Cells[Index(i+1, j)] is Revealed))//up
                {
                    RevealMoreCells(i + 1, j);
                }
                if (j > 0 && !(Cells[Index(i+1, j-1)] is Revealed))//up left
                {
                    RevealMoreCells(i + 1, j - 1);

                }
                if ((j < 10-1) && !(Cells[Index(i+1, j+1)] is Revealed))//up right
                {
                    RevealMoreCells(i + 1, j + 1);
                }
            }
            if (j > 0 && !(Cells[Index(i, j-1)] is Revealed))// if there are cells left
            {
                RevealMoreCells(i, j - 1);
            }
            if (j < 10-1 && !(Cells[Index(i, j+1)] is Revealed))// if there are cells right
            {
                RevealMoreCells(i, j + 1);
            }
        }

        public Result Surrender(bool mineSweeper)
        {
            for(int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (mineSweeper)
                    {
                        lock (Obj)
                        {
                            Cells[Index(i, j)] = _factory.Create("ExplodedTNT");
                        }

                        Cells[Index(i, j)].number = Index(i, j);
                    }
                    else if(Cells[Index(i, j)] is Unknown)
                    {
                        lock (Obj)
                        {
                            Cells[Index(i, j)] = _factory.Create("Revealed");
                        }

                        Cells[Index(i, j)].number = Index(i, j);
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
                if (Cells[Index(i-1, j)] is Tnt)//down
                {
                    bombs++;
                }
                if (j > 0 && Cells[Index(i-1, j-1)] is Tnt)//down left
                {
                    bombs++;
                }
                if ( (j < 10-1) && Cells[Index(i-1, j+1)] is Tnt)//down right
                {
                    bombs++;
                }
            }
            if(i < 10-1)// if there are cells upwards
            {
                if (Cells[Index(i+1, j)] is Tnt)//up
                {
                    bombs++;
                }
                if (j > 0 && Cells[Index(i+1, j-1)] is Tnt)//up left
                {
                    bombs++;

                }
                if ((j < 10-1) && Cells[Index(i+1, j+1)] is Tnt)//up right
                {
                    bombs++;
                }
            }
            if (j > 0 && Cells[Index(i, j-1)] is Tnt)// if there are cells left
            {
                bombs++;

            }
            if (j < (10-1) && Cells[Index(i, j+1)] is Tnt)// if there are cells right
            {
                bombs++;
            }
            Cells[Index(i, j)].bombs = bombs;
            return bombs;
        }

        /// <summary>
        /// Marks the cell  as bomb
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>Map with marked cell</returns>
        public Result MarkCell(int i, int j)
        {
            lock (Obj)
            {
                Cells[Index(i, j)].marked = !Cells[Index(i, j)].marked;
                return BuildMap(new Result());//Returns new result
            }
        }

        /// <summary>
        /// Sets mine in a cell
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public Result SetMine(int i, int j)
        {
            Result result = new Result();
            lock (Obj)
            {
                if (!(Cells[Index(i, j)] is Tnt))
                {//Check if not already set
                    Cells[Index(i, j)] = _factory.Create("TNT");
                    Cells[Index(i, j)].number = Index(i, j);

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
        /// <param name="i">Coordinate X</param>
        /// <param name="j">Coordinate Y</param>
        /// <returns>Updated map if success, failure if cell wasn't a mine</returns>
        public Result UnsetMine(int i, int j)
        {
            Result result = new Result();
            lock (Obj)
            {
                if (Cells[Index(i, j)] is Tnt)//Check if already set
                {
                    var marked = Cells[Index(i, j)].marked;
                    Cells[Index(i, j)] = _factory.Create("Unknown");
                    Cells[Index(i, j)].number = Index(i, j);
                    Cells[Index(i, j)].marked = marked;
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
//            for(int i = 0; i < _cells.Count; i++)
//            {
//                var cell = _cells[i];
//                if (_cells[i] is Tnt)
//                {
//                    _cells[i] = _factory.Create("TNT");
//                    _cells[i].number = i;
//                }
//                else if (_cells[i] is Revealed)
//                {
//                    _cells[i] = _factory.Create("Revealed");
//                    _cells[i].number = i;
//                    _cells[i].bombs = CalculateBombs(i/10, i%10);
//                }
//                else if (_cells[i] is ExplodedTnt)
//                {
//                    _cells[i] = _factory.Create("ExplodedTNT");
//                    _cells[i].number = i;
//                }
//                else if (_cells[i] is Unknown)
//                {
//                    _cells[i] = _factory.Create("Unknown");
//                    _cells[i].number = i;
//                }
//                else
//                {
//                    _cells[i] = _factory.Create("Unknown");
//                    _cells[i].number = i;
//                }
//                _cells[i].marked = cell.marked;
//            }
            return GetMapStatus(result, mineSweeper);
        }

        /// <summary>
        /// Only gets the status of the map
        /// </summary>
        /// <param name="result"></param>
        /// <param name="mineSweeper"></param>
        /// <returns></returns>
        private Result GetMapStatus(Result result, bool mineSweeper = true)
        {
            //The default game status is 0 aka Ongoing
            //result.status = GameStatus.Ongoing;
            result.success = true;
            bool finished = true;
            lock (Obj)
            {
                result.map = new char[10, 10];
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        var cell = Cells[Index(i, j)];
                        if (cell is Tnt)
                        {
                            result.map[i, j] = mineSweeper ? 'u' : 't';// unknown or TNT
                        }
                        else if (cell is Revealed)
                        {
                            result.map[i, j] = cell.bombs.ToString()[0];// max number is 6
                        }
                        else if (cell is ExplodedTnt)
                        {
                            result.map[i, j] = 'e';// Exploded
                            finished = false;
                            result.status = mineSweeper ? GameStatus.Lost : GameStatus.Won;//status changed to lost or won
                        }
                        else if (cell is Unknown)
                        {
                            result.map[i, j] = 'u'; // empty cell
                            finished = false;
                        }else if (cell is WrongTnt)
                        {
                            result.map[i, j] = 'w'; // empty cell
                        }
                        if (cell.marked && !(cell is Revealed))
                            result.map[i, j] = 'm'; // empty cell
                        Cells[Index(i, j)].marked = cell.marked;
                        Cells[Index(i, j)].bombs = cell.bombs;
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
            return GetMapStatus(new Result(), mineSweeper);
        }
    }
	
}
