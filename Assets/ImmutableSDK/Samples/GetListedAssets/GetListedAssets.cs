using System.Collections.Generic;
using Imx.Sdk;
using Imx.Sdk.Gen.Client;
using Imx.Sdk.Gen.Model;
using TMPro;
using UnityEngine;

namespace ImmutableSDK.Samples.GetListedAssets
{
    /// <summary>
    /// Fetches listed assets on Immutable X sandbox to display
    /// </summary>
	public class GetListedAssets : MonoBehaviour
    {
         [SerializeField]
         private AssetListObject assetObj = null;

         [SerializeField]
         private Transform listParent = null;
         
         [SerializeField]
         private TMP_Text noResultObject = null;

         [SerializeField]
         private TMP_InputField nameInput = null;
         
         [SerializeField]
         private TMP_InputField collectionInput = null;
         
         [SerializeField]
         private TMP_InputField pageInput = null;
         
         [SerializeField]
         private TMP_InputField userInput = null;

         [SerializeField]
         private TMP_Dropdown environmentDropdown = null;

         private List<AssetListObject> listedAssets = new List<AssetListObject>();

         private const string defaultCollectionAddress = "0x6ac5097f3fab829bae5462cb217133bc3c2a096e"; // Default address to a collection
         private const string defaultWalletAddress = "0x5A7CB0ba4D94b6B08B837E4e97A90b9F2400C80D"; // Default address for a wallet

         private void Awake()
         {
             assetObj.gameObject.SetActive(false);
             
             // Populate defaults
             collectionInput.text = defaultCollectionAddress;
             userInput.text = defaultWalletAddress;
         }

         /// <summary>
         /// Fetches assets and populates the AssetListObject ui list
         /// </summary>
         public void FetchAssets()
         {
             try
             {
                 // Get a list of assets
                 int pageSize = 50;
                 int.TryParse(pageInput.text, out pageSize);

                 Environment env = environmentDropdown.value == 0
                     ? EnvironmentSelector.Sandbox
                     : EnvironmentSelector.Mainnet;
    
                 // Create a client for sandbox assets and fetch
                 Client client = new Client(new Config() {
                     Environment = env
                 });
                 ListAssetsResponse result = client.ListAssets(pageSize, null, null, null, userInput.text, 
                     null, nameInput.text, null, null, null, null, collectionInput.text);
                 
                 Debug.Log(result.ToJson());
                 
                 // Delete existing list
                 foreach (AssetListObject uiAsset in listedAssets)
                 {
                     Destroy(uiAsset.gameObject);
                 }
                 
                 listedAssets.Clear();

                 // Handle no result states
                 noResultObject.gameObject.SetActive(result.Result.Count == 0);
                 
                 if (result.Result.Count == 0)
                 {
                     string message = "No Results Found";

                     if (!string.IsNullOrEmpty(userInput.text))
                     {
                         message += "\nCheck wallet address";
                     }
                     
                     if (!string.IsNullOrEmpty(collectionInput.text))
                     {
                         message += "\nCheck collection ID";
                     }

                     noResultObject.text = message;
                 }
                 else
                 {
                     // Populate UI list elements
                     foreach (AssetWithOrders asset in result.Result)
                     {
                         AssetListObject newAsset = GameObject.Instantiate(assetObj, listParent);
                         newAsset.gameObject.SetActive(true);
                     
                         newAsset.Initialise(asset);
                         listedAssets.Add(newAsset);
                     }
                 }
             }
             catch (ApiException  e)
             {
                 Debug.Log("Exception when calling AssetsApi.ListAssets: " + e.Message);
                 Debug.Log("Status Code: " + e.ErrorCode);
                 Debug.Log(e.StackTrace);
             }
         }
    }
}
