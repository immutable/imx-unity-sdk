using Imx.Sdk;
using Imx.Sdk.Gen.Model;
using UnityEngine;

namespace ImmutableSDK.Samples.PurchaseFlow
{
    /// <summary>
    /// Screen to show owned items in user wallet in a list
    /// </summary>
    public class ViewOwnership : MonoBehaviour
    {
        [SerializeField]
        private PurchaseFlowManager flowManager = null;
        
        [SerializeField]
        private OwnedItem templateItem = null;

        [SerializeField]
        private Transform listParent = null;

        private void Start()
        {
            Initialise();
        }

        /// <summary>
        /// Fetches assets for the user and populates a grid list of the owned items
        /// </summary>
        public void Initialise()
        {
            // Create a client for sandbox assets and fetch
            Client client = new Client(new Config() {
                Environment = EnvironmentSelector.Sandbox
            });
            
            ListAssetsResponse result = client.ListAssets(null, null, null, null, flowManager.walletID, 
                null, null, null, null, null, null, null);

            for (int i = 0; i < result.Result.Count; i++)
            {
                OwnedItem newItem = Instantiate(templateItem, listParent);
                newItem.Initialise(result.Result[i]);
            }
                 
            templateItem.gameObject.SetActive(false);
        }
    }
}
