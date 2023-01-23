using api.Api;
using api.Client;
using api.Model;
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

        /// <summary>
        /// Looks up a user via a UserID and returns account information containing Stark Keys
        /// </summary>
        public void GetUser()
        {
            if (string.IsNullOrEmpty(userInputField.text))
            {
                return; // No id input
            }
            
            UsersApi usersInstance = new UsersApi();
            
            try
            {
                GetUsersApiResponse result = usersInstance.GetUsers(userInputField.text);
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
