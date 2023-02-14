using UnityEngine;

namespace ImmutableSDK.Samples.PurchaseFlow
{
    /// <summary>
    /// List asset to show NFTs that can be bought with a buy button
    /// </summary>
    public class PurchaseableAsset : MonoBehaviour
    {
        [SerializeField]
        private PurchaseAsset assetScreen = null;
        
        /// <summary>
        /// 
        /// </summary>
        public void Purchase()
        {
            assetScreen.Purchase();
        }
    }
}
