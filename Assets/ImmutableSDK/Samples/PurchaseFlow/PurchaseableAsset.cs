using Imx.Sdk.Gen.Model;
using TMPro;
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
        
        [SerializeField]
        private TMP_Text assetName = null;
        
        [SerializeField]
        private TMP_Text assetPrice = null;

        public void Initialise(AssetWithOrders asset)
        {
            assetName.text = string.IsNullOrEmpty(asset.Name) ? "~name missing~" : asset.Name;
            // TODO: price data missing
        }
        
        /// <summary>
        /// Start purchase flow and screen transition on success
        /// </summary>
        public void Purchase()
        {
            assetScreen.Purchase();
        }
    }
}
