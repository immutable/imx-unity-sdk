using System;
using ImmutableSDK.Samples.GetListedAssets;
using Imx.Sdk;
using Imx.Sdk.Gen.Model;
using UnityEngine;

namespace ImmutableSDK.Samples.PurchaseFlow
{
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
