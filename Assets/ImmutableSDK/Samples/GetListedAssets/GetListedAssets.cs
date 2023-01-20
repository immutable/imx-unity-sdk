using System.Collections.Generic;
using api.Api;
using api.Client;
using api.Model;
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
         private TMP_InputField pageInput = null;
         
         [SerializeField]
         private TMP_InputField userInput = null;

         private List<AssetListObject> listedAssets = new List<AssetListObject>();

         private void Awake()
         {
             assetObj.gameObject.SetActive(false);
         }

         /// <summary>
         /// Fetches assets and populates the AssetListObject ui list
         /// </summary>
         public void FetchAssets()
         {
             // Sandbox config
             Configuration config = new Configuration();
             config.BasePath = "https://api.sandbox.x.immutable.com";
             
             AssetsApi apiInstance = new AssetsApi(config);
                 
             try
             {
                 // Get a list of assets
                 int pageSize = 50;
                 int.TryParse(pageInput.text, out pageSize);
                 
                 ListAssetsResponse result = apiInstance.ListAssets(pageSize, null, null, null, userInput.text);
                 Debug.Log(result.ToJson());
                 
                 // Delete existing list
                 foreach (AssetListObject uiAsset in listedAssets)
                 {
                     Destroy(uiAsset.gameObject);
                 }
                 
                 listedAssets.Clear();

                 // Populate UI list elements
                 foreach (AssetWithOrders asset in result.Result)
                 {
                     AssetListObject newAsset = GameObject.Instantiate(assetObj, listParent);
                     newAsset.gameObject.SetActive(true);
                     
                     newAsset.Initialise(asset);
                     listedAssets.Add(newAsset);
                 }
             }
             catch (ApiException  e)
             {
                 Debug.Log("Exception when calling AssetsApi.ListAssets: " + e.Message);
                 Debug.Log("Status Code: " + e.ErrorCode);
                 Debug.Log(e.StackTrace);
             }
         }

         public void FetchDeposits()
         {
             ApiClient client = new ApiClient();
         }
    }
}
