using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets._Project.Scripts.SpriteSheets
{
    public class SpriteSheet
    {
        private readonly Material _material;
        private readonly int _cellSize;

        public int Columns { get; private set; }
        public int Rows { get; private set; }
        public int CellSize
        {
            get { return _cellSize; }
        }

        public SpriteSheet([NotNull] Material material, int cellSize)
        {
            if (material == null) throw new ArgumentNullException("material");
            _material = material;
            _cellSize = cellSize;

            var width = _material.mainTexture.width;
            var height = _material.mainTexture.height;

            Columns = width / cellSize;
            Rows = height / cellSize;
        }

        public float UnitCoordSize
        {
            get { return 1f / (_material.mainTexture.width / (float)_cellSize); }
    
        }

        public Cell GetCellFromIndex(int index)
        {
            var cellSize = _cellSize;

            var width = _material.mainTexture.width;
            var height = _material.mainTexture.height;

            int columns = width / cellSize;
            int rows = height / cellSize;

            int col = index % columns;
            int row = index / rows;

            return new Cell(col, row);
        }

        public Vector2 GetUvCoordsForCell(Cell value)
        {
            var height = _material.mainTexture.height;

            int rows = height / _cellSize;

            return new Vector2(value.Col, rows - (value.Row + 1)) * UnitCoordSize;
        }

        public Vector2 GetUvCoordsForCellValue(int value)
        {
            var cellSize = _cellSize;

            var width = _material.mainTexture.width;
            var height = _material.mainTexture.height;

            int columns = width / cellSize;
            int rows = height / cellSize;

            int col = value % columns;
            int row = value / rows;

            return new Vector2(col, rows - (row + 1));
        }

        public int GetIndexFromCell(Cell cell)
        {
            return cell.Col + cell.Row*Columns;
        }
    }
}
