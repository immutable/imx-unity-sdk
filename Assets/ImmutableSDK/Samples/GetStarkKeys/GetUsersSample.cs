using Imx.Sdk;
using Imx.Sdk.Gen.Client;
using Imx.Sdk.Gen.Model;
using TMPro;
using UnityEngine;

namespace ImmutableSDK.Samples.GetStarkKeys
{
    /// <summary>
    /// Demo sample to get user information from Immutable API
    /// </summary>
    public class GetUsersSample : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField userInputField = null;

        [SerializeField]
        private TextMeshProUGUI userData = null;
        
        [SerializeField]
        private TMP_Dropdown environmentDropdown = null;

        /// <summary>
        /// Looks up a user via a UserID and returns account information containing Stark Keys
        /// </summary>
        public void GetUser()
        {
            if (string.IsNullOrEmpty(userInputField.text))
            {
                return; // No id input
            }
            
            try
            {
                Environment env = environmentDropdown.value == 0
                    ? EnvironmentSelector.Sandbox
                    : EnvironmentSelector.Mainnet;
    
                // Create a client for sandbox assets and fetch
                Client client = new Client(new Config() {
                    Environment = env
                });
                
                GetUsersApiResponse result = client.GetUsers(userInputField.text);
                Debug.Log(result.ToJson());

                string accounts = "";
                for (int i = 0; i < result.Accounts.Count; i++)
                {
                    accounts += result.Accounts[i] + "\n";
                }
                
                userData.text = accounts;
            }
            catch (ApiException e)
            {
                Debug.Log("Exception when calling UsersApi.GetUsers: " + e.Message);
                Debug.Log("Status Code: " + e.ErrorCode);
                Debug.Log(e.StackTrace);
                
                userData.text = "Status Code: " + e.ErrorCode;
            }
        }
    }
}
