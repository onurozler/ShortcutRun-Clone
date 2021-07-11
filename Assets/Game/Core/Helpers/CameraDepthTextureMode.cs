using UnityEngine;

namespace Game.Core.Helpers
{
    [ExecuteInEditMode]
    public class CameraDepthTextureMode : MonoBehaviour {

        private Camera cam;
        
        void Update()
        {
            if (cam == null)
            {
                cam = GetComponent<Camera>();
            }

            if (cam.depthTextureMode == DepthTextureMode.None)
            {
                cam.depthTextureMode = DepthTextureMode.Depth;
            }
        }
        
    }
}