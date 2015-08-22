using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Project.Scripts.Physics;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.CustomInspectors
{
    [CustomEditor(typeof(Explosion))]
    public class ExplosionInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Explode"))
            {
                (target as Explosion).Explode();
            }
            base.OnInspectorGUI();
        }
    }
}
