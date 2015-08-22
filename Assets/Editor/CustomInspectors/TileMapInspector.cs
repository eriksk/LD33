using Assets._Project.Scripts.Maps;
using Assets._Project.Scripts.Maps.Collision;
using Assets._Project.Scripts.SpriteSheets;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Editor.CustomInspectors
{
    [CustomEditor(typeof(TileMap))]
    public class TileMapInspector : UnityEditor.Editor
    {
        private float _treshold = 0.4f;
        private bool _editing = false;
        private int _selectedStampCell = 0;

        public override void OnInspectorGUI()
        {
            var tilemap = (TileMap)target;

            int width = EditorGUILayout.IntField("Width", tilemap.Width);
            int height = EditorGUILayout.IntField("Height", tilemap.Height);
            tilemap.UnitSize = EditorGUILayout.FloatField("UnitSize", tilemap.UnitSize);

            if (width != tilemap.Width || height != tilemap.Height)
            {
                tilemap.Width = width;
                tilemap.Height = height;
                tilemap.Dirty = true;
            }

            if (GUILayout.Button(_editing ? "Done Editing" : "Edit"))
            {
                _editing = !_editing;
            }

            if (_editing)
            {
                if (GUILayout.Button("Select Empty Cell"))
                {
                    _selectedStampCell = -1;
                }
            }

            if (!_editing)
            {

                if (GUILayout.Button("Clear Data"))
                {
                    if (EditorUtility.DisplayDialog("Are you sure?", "Are you sure?", "Yes", "Omg no!"))
                    {
                        tilemap.SetAllCells(-1);
                        tilemap.GenerateMesh();
                    }
                }

                if (GUILayout.Button("Randomize Data"))
                {
                    if (EditorUtility.DisplayDialog("Are you sure?", "Are you sure?", "Yes", "Omg no!"))
                    {
                        tilemap.RandomizeData();
                        tilemap.GenerateMesh();
                    }
                }

                if (GUILayout.Button("Generate Mesh"))
                {
                    tilemap.GenerateMesh();
                }


                if (GUILayout.Button("Generate Colliders"))
                {
                    new TileMapColliderGenerator(tilemap,
                        new SpriteSheet(tilemap.GetComponent<MeshRenderer>().sharedMaterial, tilemap.SpriteSheetCellSize))
                        .GenerateColliders();
                }
            }


            base.OnInspectorGUI();
        }

        void OnSceneGUI()
        {
            if (!_editing)
                return;

            var tilemap = (TileMap)target;

            var sheet = new SpriteSheet(tilemap.GetComponent<Renderer>().sharedMaterial, tilemap.SpriteSheetCellSize);

            var sourceTextureRect = new Rect(0, 0, 256, 256);
            int sourceTexCellSize = (int)(sourceTextureRect.width / sheet.Columns);

            var controlID = GUIUtility.GetControlID(FocusType.Passive);


            var screenPoint = Event.current.mousePosition;
            //var worldPoint = HandleUtility.GUIPointToWorldRay(screenPoint).origin;
            //worldPoint.z = tilemap.transform.position.z;
            var worldPoint = ScreenToWorld(new Vector2(screenPoint.x, screenPoint.y), tilemap.transform.position.z);
            var cellPoint = new Vector3((int)(worldPoint.x / tilemap.UnitSize), (int)(worldPoint.y / tilemap.UnitSize), tilemap.transform.position.z);

            bool isInSourceSelectTexture = sourceTextureRect.Contains(screenPoint);

            if (Event.current.type == EventType.mouseDown)
            {
                if (Event.current.button == 1) // right click
                {
                    if (isInSourceSelectTexture)
                    {
                        var mouseCell = Cell.Convert(screenPoint, sourceTexCellSize);
                        if (mouseCell.Col > -1 && mouseCell.Col < sheet.Columns && mouseCell.Row > -1 &&
                            mouseCell.Row < sheet.Rows)
                        {
                            var newIndex = sheet.GetIndexFromCell(mouseCell);
                            _selectedStampCell = newIndex;
                        }
                        else if (mouseCell.Row == -1 && mouseCell.Col == 0)
                        {
                            _selectedStampCell = -1;
                        }
                    }
                    else
                    {
                        // TODO: dont do this here if we actually are using the toolbox
                        TryCloneCellType(tilemap, cellPoint);
                    }
                }
                if (Event.current.button == 0)
                {
                    if (!isInSourceSelectTexture)
                    {
                        TryInsertCell(tilemap, cellPoint);
                    }
                }
            }
            if (Event.current.type == EventType.MouseDrag)
            {
                if (Event.current.button == 0)
                {
                    if (!isInSourceSelectTexture)
                    {
                        TryInsertCell(tilemap, cellPoint);
                    }
                }
            }

            {
                // TODO: check if cursor is in here, if it is, return and dont draw rest of editor
                Handles.BeginGUI();
                EditorGUI.DrawRect(sourceTextureRect, Color.magenta);
                GUI.DrawTextureWithTexCoords(sourceTextureRect, tilemap.GetComponent<Renderer>().sharedMaterial.mainTexture, new Rect(0,0,1,1), true);


                for (int i = 0; i < sheet.Columns; i++)
                {
                    for (int j = 0; j < sheet.Rows; j++)
                    {
                        int x = (i * sourceTexCellSize);
                        int y = (j * sourceTexCellSize);

                        if (sheet.GetIndexFromCell(new Cell(i, j)) == _selectedStampCell)
                        {
                            EditorGUI.DrawRect(new Rect(sourceTextureRect.x + x, sourceTextureRect.y + y, sourceTexCellSize, sourceTexCellSize), new Color(0f, 1f, 0f, 0.2f));
                        }
                        else
                        {
                            Handles.RectangleCap(controlID, new Vector3(sourceTextureRect.x + x, sourceTextureRect.y + y, 0f), Quaternion.identity, 32);
                        }
                    }
                }
                if (_selectedStampCell == -1)
                {
                    EditorGUI.DrawRect(new Rect(0, sourceTextureRect.y + sourceTextureRect.height, 32, 32), new Color(1f, 0f, 0f, 0.2f));
                }
                Handles.EndGUI();
            }

            var cell = sheet.GetCellFromIndex(_selectedStampCell);
            var uv = sheet.GetUvCoordsForCell(cell);

            Handles.BeginGUI();
            // selected tile
            GUI.DrawTextureWithTexCoords(new Rect(0, sourceTextureRect.y + sourceTextureRect.height, 32, 32), tilemap.GetComponent<Renderer>().sharedMaterial.mainTexture, new Rect(uv.x, uv.y, sheet.UnitCoordSize, sheet.UnitCoordSize));

            Handles.EndGUI();


            // selection
            if (!isInSourceSelectTexture)
            {
                float size = tilemap.UnitSize;
                var quad = new Vector3[]
                {
                    new Vector3(cellPoint.x, cellPoint.y, 0f),
                    new Vector3(cellPoint.x, cellPoint.y + size, 0f),
                    new Vector3(cellPoint.x + size, cellPoint.y + size, 0f),
                    new Vector3(cellPoint.x + size, cellPoint.y, 0f),
                };
                Handles.DrawSolidRectangleWithOutline(quad, new Color(1f, 1f, 1f, 0.2f), Color.red);
            }

            if (Event.current.type == EventType.layout)
            {
                HandleUtility.AddDefaultControl(controlID);
            }

            SceneView.RepaintAll();

            //if (GUI.changed)
            //{
            //    EditorUtility.SetDirty(target);
            //}
        }

        private void TryCloneCellType(TileMap tilemap, Vector3 cellPoint)
        {

            int col = (int)cellPoint.x;
            int row = (int)cellPoint.y + 1;

            if (col < 0) return;
            if (col > tilemap.Width - 1) return;
            if (row < 0) return;
            if (row > tilemap.Height - 1) return;

            _selectedStampCell = tilemap.GetCell(col, row);
        }

        private void TryInsertCell(TileMap tileMap, Vector3 cellPoint)
        {
            int col = (int)cellPoint.x;
            int row = (int)cellPoint.y + 1;

            if (col < 0) return;
            if (col > tileMap.Width - 1) return;
            if (row < 0) return;
            if (row > tileMap.Height - 1) return;

            tileMap.SetCell(col, row, _selectedStampCell);
            tileMap.GenerateMesh();
        }

        Vector3 ScreenToWorld(Vector2 screen, float Z)
        {
            // Z is Z position
            Handles.SetCamera(Camera.current);
            if (Camera.current != null)
            {
                Ray ray = HandleUtility.GUIPointToWorldRay(screen);
                return new Vector3(ray.origin.x, ray.origin.y, Z);
            }
            return new Vector3(0f, 0f, 0f);
        }
    }
}
