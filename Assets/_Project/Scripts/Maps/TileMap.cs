using Assets._Project.Scripts.SpriteSheets;
using UnityEngine;

namespace Assets._Project.Scripts.Maps
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(Material))]
    [RequireComponent(typeof(MeshRenderer))]
    public class TileMap : MonoBehaviour
    {
        public int Width;
        public int Height;

        [SerializeField]
        private int[] _data;

        public float UnitSize = 1f;
        public int SpriteSheetCellSize = 32;

        public bool Dirty { get; set; }

        public TileMap()
        {
            _data = new int[Width * Height];
        }

        public void GenerateMesh()
        {
            Debug.Log(string.Format("Generating Mesh for '{0}'", gameObject.name));
            RefreshMesh();
        }

        private void Ensure()
        {
            if (Dirty)
            {
                // TODO: copy current data, lose if less size
                _data = new int[Width * Height];
                Dirty = false;
            }
        }

        private void RefreshMesh()
        {
            Ensure();
            new TileMapMeshGenerator(this, GetComponent<MeshFilter>(), new SpriteSheet(GetComponent<MeshRenderer>().sharedMaterial, SpriteSheetCellSize))
                .Generate(UnitSize);
        }

        public void RandomizeData()
        {
            Ensure();
            for (int i = 0; i < _data.Length; i++)
            {
                _data[i] = Random.Range(0, 2) > 0 ? 15 : -1;
            }
        }

        public int GetCell(int col, int row)
        {
            return _data[col + row*Width];
        }

        public bool IsOccupied(int col, int row)
        {
            return GetCell(col, row) > -1;
        }

        public void SetCell(int col, int row, int value)
        {
            _data[col + row*Width] = value;
        }

        public int GetCellOrDefault(int col, int row)
        {
            if (col < 0 || col > Width - 1)
                return -1;
            if (row < 0 || row > Height - 1)
                return -1;

            return GetCell(col, row);
        }

        public void SetAllCells(int value)
        {
            for (int i = 0; i < _data.Length; i++)
            {
                _data[i] = value;
            }
        }
    }
}
