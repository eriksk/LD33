using UnityEngine;

namespace Assets._Project.Scripts.Cameras
{
    [RequireComponent(typeof(Camera))]
    [ExecuteInEditMode]
    public class CustomSortMode : MonoBehaviour
    {
        public TransparencySortMode SortMode = TransparencySortMode.Orthographic;

        void Start()
        {
            var cam = GetComponent<Camera>();
            if(cam != null)
                cam.transparencySortMode = SortMode;
        }
    }
}
