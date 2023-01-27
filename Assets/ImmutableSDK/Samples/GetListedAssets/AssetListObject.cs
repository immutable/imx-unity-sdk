using System.Collections;
using Imx.Sdk.Gen.Model;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace ImmutableSDK.Samples.GetListedAssets
{
    /// <summary>
    /// UI object representation of AssetWithOrders object returned from getting listed assets on Immutable X
    /// </summary>
    public class AssetListObject : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI collectionText = null;
        
        [SerializeField]
        private TextMeshProUGUI nameText = null;
        
        [SerializeField]
        private TextMeshProUGUI descText = null;
        
        [SerializeField]
        private TextMeshProUGUI createdText = null;
        
        [SerializeField]
        private TextMeshProUGUI addressText = null;
    
        [SerializeField]
        private RawImage collectionImage = null;
    
        
        /// <summary>
        /// Initialises the ui asset based on AssetWithOrders object
        /// </summary>
        /// <param name="asset">Immutable X asset</param>
        public void Initialise(AssetWithOrders asset)
        {
            collectionText.text = $"Collection: {asset.Collection.Name}";
            nameText.text = $"Name: {asset.Name}";
            descText.text = $"Description: {asset.Description}";
            createdText.text = $"Created: {asset.CreatedAt}";
            addressText.text = $"Address: {asset.TokenAddress}";
    
            // Lookup collection image
            if (!string.IsNullOrEmpty(asset.Collection.IconUrl))
            {
                StartCoroutine(DownloadImage(asset.Collection.IconUrl));
            }
            else
            {
                collectionImage.gameObject.SetActive(false);
            }
        }
        
        /// <summary>
        /// Downloads a web image and displays it in a unity raw image if successful, or a text override if not
        /// </summary>
        /// <param name="MediaUrl"> Image URL to resolve</param>
        IEnumerator DownloadImage(string MediaUrl)
        {   
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
                collectionText.gameObject.SetActive(true);
                collectionImage.gameObject.SetActive(false);
            }
            else
            {
                collectionText.gameObject.SetActive(false);
                collectionImage.texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
            }
        } 
    }
}