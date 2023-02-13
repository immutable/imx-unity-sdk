using System;
using System.Collections;
using System.Threading.Tasks;
using Imx.Sdk;
using Imx.Sdk.Gen.Model;
using TMPro;
using UnityEngine;

namespace ImmutableSDK.Samples.PurchaseFlow
{
    public class GetWallet : MonoBehaviour
    {
        [SerializeField]
        private PurchaseFlowManager flowManager = null;
        
        [SerializeField]
        private TMP_InputField walletInput = null;
        
        private void Awake()
        {
            walletInput.text = flowManager.walletID;
        }

        public void GetWalletKeys()
        {
            StartCoroutine(GetKeys());
        }

        private IEnumerator GetKeys()
        {
            // Create a client for sandbox assets and fetch
            Client client = new Client(new Config() {
                Environment = EnvironmentSelector.Sandbox
            });
                
            Task<GetUsersApiResponse> result = client.GetUsersAsync(walletInput.text);

            while (!result.IsCompleted)
            {
                yield return null;
            }

            if (result.IsCompletedSuccessfully)
            {
                // success
                flowManager.l2StarkKey = result.Result.Accounts[0];
                flowManager.CompleteLogin("");
            }
            else
            {
                // fail
            }
        }
    }
}
