using UnityEngine;

namespace Assets._Project.Scripts.Physics
{
    public class PsxExt
    {
        public static RaycastHit2D RayCastWithDebug(Vector3 position, Vector2 direction, float distance, LayerMask layerMask, bool drawDebug = true)
        { 
            var hit = Physics2D.Raycast(position, direction, distance, layerMask);
            if (drawDebug)
                Debug.DrawRay(position, new Vector3(direction.x, direction.y, 0f) * distance, hit.collider == null ? Color.green : Color.red, 0.01f);
            return hit;
        }
    }
}
