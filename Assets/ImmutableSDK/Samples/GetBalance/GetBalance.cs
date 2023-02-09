using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Imx.Sdk;
using Imx.Sdk.Gen.Model;
using TMPro;
using UnityEngine;
using Environment = Imx.Sdk.Environment;

namespace ImmutableSDK.Samples.GetBalance
{
    /// <summary>
    /// Fetches the balances for a user wallet and displays the available tokens and balances
    /// </summary>
    public class GetBalance : MonoBehaviour
    {
        [SerializeField]
        private TMP_Dropdown environmentDropdown = null;
    
        [SerializeField]
        private TMP_Dropdown tokenDropdown = null;
    
        [SerializeField]
        private TMP_InputField ownerInput = null;

        [SerializeField]
        private TMP_Text resultText = null;
    
        private List<Balance> userBalances = new List<Balance>();
        
        private const string defaultWalletAddress = "0x2d0ad946788938B9044ed72b1C464e1e9bb9d401"; // Default address for a wallet

        private void Awake()
        {
            // Update balance text whenever user changes token type
            tokenDropdown.onValueChanged.AddListener((T0) => DisplaySelectedBalance());

            // Populate Defaults
            ownerInput.text = defaultWalletAddress;
        }

        /// <summary>
        /// Updates the token options based on available tokens in the user wallet
        /// </summary>
        private void UpdateTokenDropdown()
        {
            if (userBalances == null || userBalances.Count == 0)
            {
                // No tokens owned, clear list
                tokenDropdown.options = new List<TMP_Dropdown.OptionData>();
                resultText.text = "No balances to display";
                return;
            }

            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

            for (int i = 0; i < userBalances.Count; i++)
            {
                options.Add(new TMP_Dropdown.OptionData(userBalances[i].Symbol));
            }

            tokenDropdown.options = options;
            DisplaySelectedBalance();
        }

        /// <summary>
        /// Display the current balance for a token, or notify that there are no balances are available
        /// </summary>
        private void DisplaySelectedBalance()
        {
            if (tokenDropdown.value < 0 || userBalances.Count == 0)
            {
                resultText.text = "No balances to display";
                return;
            }
        
            Balance selectedBalance = userBalances[tokenDropdown.value];
        
            // Update ui
            resultText.text = $"Current wallet balance for {selectedBalance.Symbol}:\n" +
                              $"Wei Balance: {selectedBalance._Balance}\n" +
                              $"Withdrawable: {selectedBalance.Withdrawable}";
        }

        /// <summary>
        /// Public entry point to fetch and update balances to ui
        /// </summary>
        public void FetchBalance()
        {
            StartCoroutine(GetBalanceAsync());
        }

        /// <summary>
        /// Fetches balances async and triggers a ui update
        /// </summary>
        /// <returns></returns>
        private IEnumerator GetBalanceAsync()
        {
            if (string.IsNullOrEmpty(ownerInput.text))
            {
                yield break;
            }
            
            userBalances.Clear();
        
            Environment env = environmentDropdown.value == 0
                ? EnvironmentSelector.Sandbox
                : EnvironmentSelector.Mainnet;
    
            // Create a client
            Client client = new Client(new Config() {
                Environment = env
            });
        
            Task<ListBalancesResponse> listBalances = client.ListBalancesAsync(ownerInput.text);
            
            // Display async loading to user while operation finishes
            resultText.text = "Fetching Balances...";
        
            while (!listBalances.IsCompleted)
            {
                yield return null;
            }
        
            resultText.text = "";
        
            if (listBalances.Result != null)
            {
                userBalances = listBalances.Result.Result;
            }

            UpdateTokenDropdown();
        }
    }
}
