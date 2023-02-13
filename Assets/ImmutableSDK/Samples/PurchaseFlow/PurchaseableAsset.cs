using UnityEngine;

namespace ImmutableSDK.Samples.PurchaseFlow
{
    public class PurchaseableAsset : MonoBehaviour
    {
        [SerializeField]
        private PurchaseAsset assetScreen = null;
        
        public void Purchase()
        {
            assetScreen.Purchase();
        }
    }
}
