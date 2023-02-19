using System.Collections;
using System.Threading.Tasks;
using Imx.Sdk;
using Imx.Sdk.Gen.Model;
using TMPro;
using UnityEngine;

namespace ImmutableSDK.Samples.PurchaseFlow
{
    /// <summary>
    /// Purchase screen to view assets, balance and purchase an asset
    /// </summary>
    public class PurchaseAsset : MonoBehaviour
    {
        [SerializeField]
        private PurchaseFlowManager flowManager = null;
        
        [SerializeField]
        private TMP_Text balanceText = null;
        
        [SerializeField]
        private TMP_Text walletIDText = null;
        
        [SerializeField]
        private PurchaseableAsset asset = null;
        
        [SerializeField]
        private Transform listParent = null;

        private Client client = null;
    
        // Start is called before the first frame update
        void Start()
        {
            walletIDText.text = flowManager.walletAddress;
            
            // Init client
            client = new Client(new Config() {
                Environment = EnvironmentSelector.Sandbox
            });
            
            // Populate balance data
            StartCoroutine(GetBalance());
            
            ListAssetsResponse result = client.ListAssets(50, null, null, null, null, 
                null, null, null, true, null, null, flowManager.collectionAddress);
            
            Debug.Log($"COllection: {flowManager.collectionAddress}");
            
            for (int i = 0; i < result.Result.Count; i++)
            {
                var newAsset = Instantiate(asset, listParent);
                newAsset.Initialise(result.Result[i]);
            }
            
            asset.gameObject.SetActive(false);
        }

        /// <summary>
        /// Complete purchase flow and transfer to owned item screen
        /// </summary>
        public void Purchase()
        {
            // TODO: Purchasing asset missing
            flowManager.CompletePurchase();
        }

        /// <summary>
        /// Fetches a user balance and populates a ui element
        /// </summary>
        private IEnumerator GetBalance()
        {
            Task<ListBalancesResponse> listBalances = client.ListBalancesAsync(flowManager.walletAddress);
            
            while (!listBalances.IsCompleted)
            {
                yield return null;
            }

            // Search specifically for ETH
            Balance ethBalance = null;

            for (int i = 0; i < listBalances.Result.Result.Count; i++)
            {
                if (listBalances.Result.Result[i].Symbol == "ETH")
                {
                    ethBalance = listBalances.Result.Result[i];
                    break;
                }
            }

            // Populate existing ETH balance and convert from wei
            if (ethBalance == null)
            {
                balanceText.text = "No Balances";
            }
            else
            {
                balanceText.text = $"ETH: {(double.Parse(ethBalance._Balance) / 1000000000000000000.0)}";
            }
        }
    }
}
