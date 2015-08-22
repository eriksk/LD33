using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets._Project.Scripts.SpriteSheets
{
    public class SourceSelect : MonoBehaviour
    {
        public int CellSize = 32;
        public int Index = 0;

        void Start()
        {
            var renderer = GetComponent<Renderer>();
            if (renderer == null)
                return;

            var sheet = new SpriteSheet(renderer.material, 32);
            var cell = sheet.GetCellFromIndex(Index);
            var uv = sheet.GetUvCoordsForCell(cell);

            renderer.material.SetTextureScale("_MainTex", new Vector2(sheet.UnitCoordSize, sheet.UnitCoordSize));
            renderer.material.SetTextureScale("_BumpMap", new Vector2(sheet.UnitCoordSize, sheet.UnitCoordSize));
            renderer.material.SetTextureOffset("_MainTex", uv);
            renderer.material.SetTextureOffset("_BumpMap", uv);
        }
    }
}
