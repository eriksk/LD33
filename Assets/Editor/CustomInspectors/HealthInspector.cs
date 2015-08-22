using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Project.Scripts.Characters;
using UnityEditor;

namespace Assets.Editor.CustomInspectors
{
    [CustomEditor(typeof(Health))]
    public class HealthInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Current Health: " + (target as Health).Value);
            base.OnInspectorGUI();
        }
    }
}
