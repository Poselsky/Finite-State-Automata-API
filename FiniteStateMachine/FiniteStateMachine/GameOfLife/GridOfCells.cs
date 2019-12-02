﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine.GameOfLife
{
    class GridOfCells
    {
        public Cell[,] cells { get; private set; }
        //So we can better work with it
        public List<Cell> cellsInRow { get; private set; }
        public int width { get; private set; }
        public int height { get; private set; }

        static Random gen = new Random();

        public GridOfCells(int numberOfCellsOnX, int numberOfCellsOnY, Cell alive, Cell dead,bool blank = false)
        {
            if (!alive.alive || dead.alive)
            {
                throw new ArgumentException("Please provide valid cells.");
            }

            width = numberOfCellsOnX -1;
            height = numberOfCellsOnY - 1;
            cellsInRow = new List<Cell>();

            cells = new Cell[numberOfCellsOnX, numberOfCellsOnY];
            for (int i = 0; i < numberOfCellsOnX; i++)
                for (int j = 0; j < numberOfCellsOnX; j++)
                {
                    if (!blank)
                    {
                        int rand = gen.Next(0, 2);
                        Cell temp = rand == 1 ? alive : dead;
                        cells[i, j] = temp;
                        cellsInRow.Add(temp);
                    }else
                    {
                        cells[i, j] = dead;
                        cellsInRow.Add(dead);
                    }
                }
        }

        public GridOfCells(int numberOfCellsOnX, int numberOfCellsOnY)
        {
            width = numberOfCellsOnX - 1;
            height = numberOfCellsOnY - 1;
            cellsInRow = new List<Cell>();

            cells = new Cell[numberOfCellsOnX, numberOfCellsOnY];
            for (int i = 0; i < numberOfCellsOnX; i++)
                for (int j = 0; j < numberOfCellsOnX; j++)
                {
                    int rand = gen.Next(0, 2);
                    Cell temp = rand == 1 ? new Cell(true) : new Cell(false);
                    cells[i, j] = temp;
                    cellsInRow.Add(temp);
                }
        }


        public GridOfCells(List<Cell> cells,int numberOfCellsOnX, int numberOfCellsOnY)
        {
            if (numberOfCellsOnX + numberOfCellsOnY == cells.Count)
            {
                cellsInRow = cells;
                for (int i = 0; i < numberOfCellsOnX; i++)
                    for (int j = 0; j < numberOfCellsOnY; j++)
                    {
                        this.cells[i, j] = cells[i + j];
                    }
            }
        }

        private Tuple<int,int> NumberOfNeighbours(int i, int j)
        {
            int alive = 0;
            int dead = 0;
            if (width > 0 && height > 0)
                for (int x = Math.Max(0, i - 1); x <= Math.Min(i + 1, width); x++)
                    for (int y = Math.Max(0, j - 1); y <= Math.Min(j + 1, height); y++)
                        if (x != i || y != j)
                            if (cells[y, x].alive)
                                alive++;
                            else
                                dead++;

            return new Tuple<int, int>( alive, dead );
        }


        public int[] SearchCellPosition(Cell cell)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (cells[i, j] == cell)
                        return new int[] { i, j };
                }
            }
            throw new ArgumentException("Cell is not in this grid");
        }


        public override string ToString()
        {
            var s = new StringBuilder();

            for (var i = 0; i < cells.GetLength(0); i++)
            {
                for (var j = 0; j < cells.GetLength(1); j++)
                {

                    //s.Append(cells[i, j].alive == true ? 1 : 0).Append('|');
                    s.Append(NumberOfNeighbours(i,j).Item1).Append('|');
                }

                s.Append('\n');
            }

            return s.ToString();
        }


        //Number of alive,dead neighbours
        public Tuple<int,int> this[int x, int y]
        {
            get => NumberOfNeighbours(y, x);
        }

        //If we don't know the position of the cell
        public Tuple<int,int> this[Cell searchedCell]
        {
            get {
                int[] position = SearchCellPosition(searchedCell);
                return NumberOfNeighbours(position[0], position[1]);
            }
        }
        

    }
}
