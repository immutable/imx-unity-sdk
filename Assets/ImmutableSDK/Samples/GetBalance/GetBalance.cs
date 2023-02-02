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
    
    [SerializeField]
    private TMP_Text addressText = null;

    private List<string> tokenAddresses = new List<string>();

    private void Start()
    {
        UpdateTokenDropdown();
        environmentDropdown.onValueChanged.AddListener((T0) => UpdateTokenDropdown());
    }

    private void UpdateTokenDropdown()
    {
        Environment env = environmentDropdown.value == 0
            ? EnvironmentSelector.Sandbox
            : EnvironmentSelector.Mainnet;
        
        tokenAddresses.Clear();
    
        // Create a client
        Client client = new Client(new Config() {
            Environment = env
        });

        var result = client.ListTokens();
        
        // Update ui
        string resultString = "";
        List<TMP_Dropdown.OptionData> tokenOptions = new List<TMP_Dropdown.OptionData>();
        
        for (int i = 0; i < result.Result.Count; i++)
        {
            resultString += $"{result.Result[i].Name}: {result.Result[i].TokenAddress}\n";
            tokenOptions.Add(new TMP_Dropdown.OptionData(result.Result[i].Name));
            tokenAddresses.Add(result.Result[i].TokenAddress);
        }
        
        Debug.Log(resultString);

        tokenDropdown.options = tokenOptions;
    }

    public void FetchBalance()
    {
        StartCoroutine(GetBalanceAsync());
    }

    private IEnumerator GetBalanceAsync()
    {
        Environment env = environmentDropdown.value == 0
            ? EnvironmentSelector.Sandbox
            : EnvironmentSelector.Mainnet;
    
        // Create a client
        Client client = new Client(new Config() {
            Environment = env
        });

        Task<Balance> balanceTask = client.GetBalanceAsync(ownerInput.text, tokenAddresses[tokenDropdown.value]);

        // Show loading state
        loadingText.gameObject.SetActive(true);

        // Wait on method to complete
        while (!balanceTask.IsCompleted)
        {
            yield return null;
        }
        
        // End loading state
        loadingText.gameObject.SetActive(false);
        
        Debug.Log(balanceTask.Result.ToJson());
        
        // Update ui
        resultText.text = balanceTask.Result.ToString();

        yield return null;
    }
}
