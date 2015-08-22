using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Project.Scripts.SpriteSheets;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets._Project.Scripts.Maps
{
    public class TileMapMeshGenerator
    {
        readonly List<Vector2> _vertices = new List<Vector2>();
        readonly List<Vector2> _uvCoords = new List<Vector2>();
        readonly List<int> _indices = new List<int>();
        private int _currentIndex;

        private readonly TileMap _tileMap;
        private readonly MeshFilter _meshFilter;
        private readonly SpriteSheet _spriteSheet;

        public TileMapMeshGenerator([NotNull] TileMap tileMap, [NotNull] MeshFilter meshFilter,
            [NotNull] SpriteSheet spriteSheet)
        {
            if (tileMap == null) throw new ArgumentNullException("tileMap");
            if (meshFilter == null) throw new ArgumentNullException("meshFilter");
            if (spriteSheet == null) throw new ArgumentNullException("spriteSheet");

            _tileMap = tileMap;
            _meshFilter = meshFilter;
            _spriteSheet = spriteSheet;
        }

        public void Generate(float unitSize = 1f)
        {
            _vertices.Clear();
            _uvCoords.Clear();
            _indices.Clear();
            _currentIndex = 0;

            var mesh = _meshFilter.sharedMesh ?? new Mesh();

            for (int i = 0; i < _tileMap.Width; i++)
            {
                for (int j = 0; j < _tileMap.Height; j++)
                {
                    var cell = _tileMap.GetCell(i, j);
                    if (cell > -1)
                    {
                        AddQuad(_vertices, _uvCoords, _indices, i, j, cell, unitSize);
                    }
                }
            }


            mesh.Clear();
            mesh.vertices = _vertices.Select(x => new Vector3(x.x, x.y, 0f)).ToArray();
            mesh.triangles = _indices.ToArray();
            mesh.uv = _uvCoords.ToArray();
            mesh.Optimize();
            mesh.RecalculateNormals();
            _meshFilter.mesh = mesh;
        }

        private void AddQuad(List<Vector2> vertices, List<Vector2> uvCoords, List<int> indices, int col, int row, int value, float unitSize)
        {
            var x = col*unitSize;
            var y = row*unitSize;
            const float z = 0f;

            vertices.Add(new Vector3(x, y, z));
            vertices.Add(new Vector3(x + unitSize, y, z));
            vertices.Add(new Vector3(x + unitSize, y - unitSize, z));
            vertices.Add(new Vector3(x, y - unitSize, z));

            var indexOffset = _currentIndex;

            indices.Add(indexOffset + 0);
            indices.Add(indexOffset + 1);
            indices.Add(indexOffset + 3);

            indices.Add(indexOffset + 1);
            indices.Add(indexOffset + 2);
            indices.Add(indexOffset + 3);

            _currentIndex += 4;

            var cellCoord = _spriteSheet.GetUvCoordsForCellValue(value);
            var unitCoordSize = _spriteSheet.UnitCoordSize;

            uvCoords.Add(new Vector2(unitCoordSize * cellCoord.x, unitCoordSize * cellCoord.y + unitCoordSize));
            uvCoords.Add(new Vector2(unitCoordSize * cellCoord.x + unitCoordSize, unitCoordSize * cellCoord.y + unitCoordSize));
            uvCoords.Add(new Vector2(unitCoordSize * cellCoord.x + unitCoordSize, unitCoordSize * cellCoord.y));
            uvCoords.Add(new Vector2(unitCoordSize * cellCoord.x, unitCoordSize * cellCoord.y));
        }

    }
}
