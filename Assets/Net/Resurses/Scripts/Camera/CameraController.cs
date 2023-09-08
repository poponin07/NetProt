using UnityEngine;

namespace Net
{
    public class CameraController : MonoBehaviour
    {
        public Vector3 positionOffset;
        private GameObject gameobjectToTrack;

        public void Initialization(GameObject target)
        {
            gameobjectToTrack = target;
        }


        private void LateUpdate()
        {
            gameObject.transform.position = gameobjectToTrack.transform.position + positionOffset;
        }
    }
}