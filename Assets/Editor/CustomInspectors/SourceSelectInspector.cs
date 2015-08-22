using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Project.Scripts.SpriteSheets;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.CustomInspectors
{
    [CustomEditor(typeof(SourceSelect))]
    public class SourceSelectInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var t = (SourceSelect) target;
            if (GUILayout.Button("Refresh"))
            {
                t.Refresh();
            }
            base.OnInspectorGUI();
        }
    }
}
