using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets._Project.Scripts.Characters.Collision
{
    [ExecuteInEditMode]
    public class CharacterSurroundings : MonoBehaviour
    {
        public bool Left, Right, Up, Down;
        public LayerMask LayerMask;

        public float DownOffset;
        public float DownDistance = 1f;

        public float HorizontalOffset = 0f;
        public float HorizontalDistance = 1f;

        public bool DrawDebug = false;

        void Update()
        {
            Left = RayCast(new Vector2(-HorizontalOffset, 0f), Vector2.left, HorizontalDistance * transform.localScale.x);
            Right = RayCast(new Vector2(HorizontalOffset, 0f), Vector2.right, HorizontalDistance * transform.localScale.x);

            Down = RayCast(new Vector2(0f, DownOffset * transform.localScale.x), Vector2.down, DownDistance * transform.localScale.y);
        }

        private bool RayCast(Vector2 offset, Vector2 direction, float distance = 1f)
        {
            var position = transform.position + new Vector3(offset.x, offset.y, 0f);

            var hit = Physics2D.Raycast(position, direction, distance, LayerMask);

            if(DrawDebug)
            {
                Debug.DrawRay(position, new Vector3(direction.x, direction.y, 0f) * distance, hit.collider == null ? Color.green : Color.red, 0.01f);
            }
            return hit.collider != null;
        }
    }
}
