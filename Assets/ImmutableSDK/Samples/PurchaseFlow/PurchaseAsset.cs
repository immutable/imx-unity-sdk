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

        private const string collectionAddress = "0x4344b6E12F910B32f63c74B2937c49c0D2AAB513";

        private Client client = null;
    
        // Start is called before the first frame update
        void Start()
        {
            // Init client
            client = new Client(new Config() {
                Environment = EnvironmentSelector.Sandbox
            });
            
            // Populate balance data
            StartCoroutine(GetBalance());
            
            ListAssetsResponse result = client.ListAssets(50, null, null, null, null, 
                null, null, null, true, null, null, null);

            //var result = client.ListOrders();

            for (int i = 0; i < result.Result.Count; i++)
            {
                Debug.Log(result.Result[i]);

            }
        }

        /// <summary>
        /// Complete purchase flow and transfer to owned item screen
        /// </summary>
        public void Purchase()
        {
            flowManager.CompletePurchase();
        }

        /// <summary>
        /// Fetches a user balance and populates a ui element
        /// </summary>
        private IEnumerator GetBalance()
        {
            Task<ListBalancesResponse> listBalances = client.ListBalancesAsync(flowManager.walletID);
            
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
