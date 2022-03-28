using Data.Shop.TubeSkins;
using UnityEngine;

namespace Core
{
    public class TubePart : MonoBehaviour
    {
        private TubeMover tubeMover; 
        
        public void Initialize(TubeMover _tubeMover)
        {
            tubeMover = _tubeMover;
            ChangeTheme();
        }

        public void TryOnSkin(EnvironmentSkinData _environmentSkinData)
        {
            GetComponent<MeshRenderer>().material = _environmentSkinData.tubeMaterial;
            GetComponent<MeshFilter>().mesh = _environmentSkinData.tube;
        }
        
        public void ChangeTheme()
        {
            GetComponent<MeshRenderer>().material = tubeMover.platformMover.visualController.GetTubeMaterial();
            GetComponent<MeshFilter>().mesh = tubeMover.platformMover.visualController.GetTubeMesh();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("TubeTrigger")) return; 
            tubeMover.CreateNewTubePart();
            Destroy(gameObject);
        }
    }
}