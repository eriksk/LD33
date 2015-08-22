using System;
using System.Collections.Generic;
using Assets._Project.Scripts.SpriteSheets;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets._Project.Scripts.Maps.Collision
{
    public class TileMapColliderGenerator
    {
        private readonly TileMap _tileMap;
        private readonly SpriteSheet _spriteSheet;

        public TileMapColliderGenerator([NotNull] TileMap tileMap, [NotNull] SpriteSheet spriteSheet)
        {
            if (tileMap == null) throw new ArgumentNullException("tileMap");
            if (spriteSheet == null) throw new ArgumentNullException("spriteSheet");

            _tileMap = tileMap;
            _spriteSheet = spriteSheet;
        }

        public void GenerateColliders()
        {
            CreateOrClearCollider();

            var collidableCells = GetCollidableCells();
            foreach (var cell in collidableCells)
            {
                AddQuad(cell.Col, cell.Row);
            }
        }

        private void AddQuad(int col, int row)
        {
            var gameObject = new GameObject("Collider");
            gameObject.transform.parent = _tileMap.gameObject.transform;
            gameObject.transform.localPosition = new Vector3(col * _tileMap.UnitSize + _tileMap.UnitSize * 0.5f, row * _tileMap.UnitSize - _tileMap.UnitSize * 0.5f, 0f);
            var collider = gameObject.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(_tileMap.UnitSize, _tileMap.UnitSize);
        }

        private Vector2[] CreateVerts(Cell cell)
        {
            var size = _tileMap.UnitSize;

            return new []
            {
                new Vector2(cell.Col * size, cell.Row*size), 
                new Vector2(cell.Col* size + size, cell.Row*size), 
                new Vector2(cell.Col *size + size, cell.Row*size - size), 
                new Vector2(cell.Col*size, cell.Row*size - size), 
            };
        }

        private void CreateOrClearCollider()
        {
            var children = _tileMap.gameObject.GetComponentsInChildren<BoxCollider2D>();

            foreach (var child in children)
                GameObject.DestroyImmediate(child.gameObject);
        }

        private List<Cell> GetCollidableCells()
        {
            var collidableCells = new List<Cell>();

            for (int i = 0; i < _tileMap.Width; i++)
            {
                for (int j = 0; j < _tileMap.Height; j++)
                {
                    var cell = _tileMap.GetCell(i, j);
                    if (cell > -1)
                    {
                        collidableCells.Add(new Cell(i, j));
                    }
                }
            }
            return collidableCells;
        }
    }
}
