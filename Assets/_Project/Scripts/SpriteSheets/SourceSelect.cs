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
            Refresh();
        }

        public void Refresh()
        {
            var renderer = GetComponent<Renderer>();
            if (renderer == null)
                return;

            var sheet = new SpriteSheet(renderer.sharedMaterial, 32);
            var cell = sheet.GetCellFromIndex(Index);
            var uv = sheet.GetUvCoordsForCell(cell);

            renderer.sharedMaterial.SetTextureScale("_MainTex", new Vector2(sheet.UnitCoordSize, sheet.UnitCoordSize));
            renderer.sharedMaterial.SetTextureScale("_BumpMap", new Vector2(sheet.UnitCoordSize, sheet.UnitCoordSize));
            renderer.sharedMaterial.SetTextureOffset("_MainTex", uv);
            renderer.sharedMaterial.SetTextureOffset("_BumpMap", uv);
        }
    }
}
