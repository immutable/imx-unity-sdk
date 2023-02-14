using Imx.Sdk.Gen.Model;
using TMPro;
using UnityEngine;

namespace ImmutableSDK.Samples.PurchaseFlow
{
    /// <summary>
    /// List items for an asset owned by the user
    /// </summary>
    public class OwnedItem : MonoBehaviour
    {
        [SerializeField]
        public TMP_Text name = null;
        
        [SerializeField]
        public TMP_Text address = null;
        
        /// <summary>
        /// Update and populate UI with user asset info
        /// </summary>
        /// <param name="asset"> Asset used to populate </param>
        public void Initialise(AssetWithOrders asset)
        {
            name.text = asset.Name;
            address.text = asset.TokenAddress;
        }
    }
}
