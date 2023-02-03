using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Imx.Sdk;
using Imx.Sdk.Gen.Model;
using TMPro;
using UnityEngine;
using Environment = Imx.Sdk.Environment;

public class GetBalance : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown environmentDropdown = null;
    
    [SerializeField]
    private TMP_Dropdown tokenDropdown = null;
    
    [SerializeField]
    private TMP_InputField ownerInput = null;
    
    [SerializeField]
    private TMP_InputField addressInput = null;

    [SerializeField] 
    private TMP_Text loadingText = null;

    [SerializeField]
    private TMP_Text resultText = null;
    
    private List<Balance> userBalances = new List<Balance>();

    private void Awake()
    {
        tokenDropdown.onValueChanged.AddListener((T0) => DisplaySelectedBalance());
    }

    private void UpdateTokenDropdown()
    {
        if (userBalances == null || userBalances.Count == 0)
        {
            // No tokens owned, clear list
            tokenDropdown.options = new List<TMP_Dropdown.OptionData>();
            DisplaySelectedBalance();
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

    private void DisplaySelectedBalance()
    {
        if (tokenDropdown.value < 0 || userBalances.Count == 0)
        {
            resultText.text = $"No balances to display";
            return;
        }
        
        Balance selectedBalance = userBalances[tokenDropdown.value];
        
        // Update ui
        resultText.text = $"Current wallet balance for {selectedBalance.Symbol}:\n" +
                          $"Wei Balance: {selectedBalance._Balance}\n" +
                          $"Withdrawable: {selectedBalance.Withdrawable}";
    }

    public void FetchBalance()
    {
        StartCoroutine(GetBalanceAsync());
    }

    private IEnumerator GetBalanceAsync()
    {
        userBalances.Clear();
        
        Environment env = environmentDropdown.value == 0
            ? EnvironmentSelector.Sandbox
            : EnvironmentSelector.Mainnet;
    
        // Create a client
        Client client = new Client(new Config() {
            Environment = env
        });
        
        Task<ListBalancesResponse> listBalances = client.ListBalancesAsync(ownerInput.text);
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
