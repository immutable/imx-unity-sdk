using System.Collections;
using System.Threading.Tasks;
using Imx.Sdk;
using Imx.Sdk.Gen.Model;
using TMPro;
using UnityEngine;

namespace ImmutableSDK.Samples.PurchaseFlow
{
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
            
            StartCoroutine(GetBalance());
            
            // ListAssetsResponse result = client.ListAssets(50, null, null, null, null, 
            //     "withdrawable", null, null, true, null, null, null);

            var result = client.ListOrders();

            for (int i = 0; i < result.Result.Count; i++)
            {
                //Debug.Log(result.Result[i]);

            }
        }

        public void Purchase()
        {
            flowManager.CompletePurchase();
        }

        private IEnumerator GetBalance()
        {
            Task<ListBalancesResponse> listBalances = client.ListBalancesAsync(flowManager.walletID);
            
            while (!listBalances.IsCompleted)
            {
                yield return null;
            }

            Balance ethBalance = null;

            for (int i = 0; i < listBalances.Result.Result.Count; i++)
            {
                //Debug.Log(listBalances.Result.Result[i].ToJson());
                if (listBalances.Result.Result[i].Symbol == "ETH")
                {
                    ethBalance = listBalances.Result.Result[i];
                    break;
                }
            }

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
