

using System;
/**
* @(#) Map.cs
*/
namespace MineServer.Models
{
	public class Map
	{
		Cell[,] cells;
        CellFactory factory;

		int size;

		 public Map(int index1, int index2)
        {
            cells = new Cell[index1, index2];
            for(int i = 0; i < index1; i++)
            {
                for(int j = 0; j < index2; i++)
                {
                    cells[i, j] = factory.Create("Empty");
                }
            }
        }

         public string RevealCell(int index1, int index2)
        {
            if (cells[index1, index2].marked == false)
            {
                if (cells[index1, index2] is TNT)
                {

                }
                else
                {
                    RevealMoreCells(index1, index2);
                }
                return "";//TODO return
            }
            return "";//TODO return
        }

        /// <summary>
        /// Reveals empty cells until finds cell near bomb
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void RevealMoreCells(int i, int j)
        {
            cells[i, j] = factory.Create("Revealed");
            int bombs = CalculateBombs(i, j);
            cells[i, j].bombs = bombs;
            if (bombs != 0)
                return;
            if (i > 0)//if there are cells downwards
            {
                if (!(cells[i - 1, j] is Revealed))//down
                {
                    RevealMoreCells(i - 1, j);
                }
                if (!(cells[i - 1, j - 1] is Revealed))//down left
                {
                    RevealMoreCells(i - 1, j - 1);
                }
                if ((j < cells.GetLength(1)) && !(cells[i - 1, j + 1] is Revealed))//down right
                {
                    RevealMoreCells(i - 1, j + 1);
                }
            }
            if (i < cells.GetLength(0))// if there are cells upwards
            {
                if (!(cells[i + 1, j] is Revealed))//up
                {
                    RevealMoreCells(i + 1, j);
                }
                if (!(cells[i + 1, j - 1] is Revealed))//up left
                {
                    RevealMoreCells(i + 1, j - 1);

                }
                if ((j < cells.GetLength(1)) && !(cells[i + 1, j + 1] is Revealed))//up right
                {
                    RevealMoreCells(i + 1, j + 1);
                }
            }
            if (j > 0 && !(cells[i, j - 1] is Revealed))// if there are cells left
            {
                RevealMoreCells(i, j - 1);
            }
            if (j < cells.GetLength(1) && !(cells[i, j + 1] is Revealed))// if there are cells right
            {
                RevealMoreCells(i, j + 1);
            }
        }
        
        /// <summary>
        /// finds the number of bombs around cell
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private int CalculateBombs(int i, int j)
        {
            int bombs = 0;
            if (i > 0)//if there are cells downwards
            {
                if (cells[i - 1, j] is TNT)//down
                {
                    bombs++;
                }
                if (cells[i - 1, j - 1] is TNT)//down left
                {
                    bombs++;
                }
                if ( (j < cells.GetLength(1)) && cells[i - 1, j + 1] is TNT)//down right
                {
                    bombs++;
                }
            }
            if(i < cells.GetLength(0))// if there are cells upwards
            {
                if (cells[i + 1, j] is TNT)//up
                {
                    bombs++;
                }
                if (cells[i + 1, j - 1] is TNT)//up left
                {
                    bombs++;

                }
                if ((j < cells.GetLength(1)) && cells[i + 1, j + 1] is TNT)//up right
                {
                    bombs++;
                }
            }
            if (j > 0 && cells[i, j - 1] is TNT)// if there are cells left
            {
                bombs++;

            }
            if (j < cells.GetLength(1) && cells[i, j + 1] is TNT)// if there are cells right
            {
                bombs++;
            }
            cells[i, j].bombs = bombs;
            return bombs;
        }

        /// <summary>
        /// marks the cell  as bomb
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <returns></returns>
        public string MarkCell(int index1, int index2)
        {
            if(!cells[index1, index2].marked)
                cells[index1, index2].marked = true;
            else
                cells[index1, index2].marked = false;
            return "Marked";
        }



		public void ToJSON(  )
		{
			
		}
		
	}
	
}
