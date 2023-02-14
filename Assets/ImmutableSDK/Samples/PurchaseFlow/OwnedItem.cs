using Imx.Sdk.Gen.Model;
using TMPro;
using UnityEngine;

namespace ImmutableSDK.Samples.PurchaseFlow
{
    public class OwnedItem : MonoBehaviour
    {
        [SerializeField]
        public TMP_Text name = null;
        
        [SerializeField]
        public TMP_Text address = null;
        
        public void Initialise(AssetWithOrders asset)
        {
            name.text = asset.Name;
            address.text = asset.TokenAddress;
        }
    }
}
