using Data.Shop.TubeSkins;
using UnityEngine;

namespace Core
{
    public class TubePart : MonoBehaviour
    {
        private PlatformMover platformMover; 
        
        public void Initialize(PlatformMover _platformMover)
        {
            platformMover = _platformMover;
            ChangeTheme();
        }

        public void TryOnSkin(EnvironmentSkinData _environmentSkinData)
        {
            GetComponent<MeshRenderer>().material = _environmentSkinData.tubeMaterial;
            GetComponent<MeshFilter>().mesh = _environmentSkinData.tube;
        }
        
        public void ChangeTheme()
        {
            GetComponent<MeshRenderer>().material = platformMover.visualController.GetTubeMaterial();
            GetComponent<MeshFilter>().mesh = platformMover.visualController.GetTubeMesh();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("TubeTrigger")) return;
            platformMover.CreateNewTubePart();
            Destroy(gameObject);
        }
    }
}