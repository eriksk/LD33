using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets._Project.Scripts.SpriteSheets
{
    public class Cell
    {
        public int Col { get; set; }
        public int Row { get; set; }

        public Cell()
        {
            
        }

        public Cell(int col, int row)
        {
            Col = col;
            Row = row;
        }

        public static Cell Convert(Vector3 worldPoint, float cellSize)
        {
            return new Cell((int)(worldPoint.x / cellSize), (int)(worldPoint.y / cellSize));
        }

        public override string ToString()
        {
            return string.Format("Col: {0}, Row: {1}", Col, Row);
        }
    }
}
