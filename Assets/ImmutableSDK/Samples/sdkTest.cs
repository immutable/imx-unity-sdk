using System;
using System.Collections;
using System.Collections.Generic;
using api.Api;
using api.Client;
using api.Model;
using TMPro;
using UnityEngine;

namespace imx.sdk
{
    public class sdkTest : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI assetsText = null;
        
        [SerializeField]
        private TextMeshProUGUI accountText = null;
        
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Checking Assets");
            CheckAssets();
            
            Debug.Log("Fetching User");
            GetUser();
        }

        private void RegisterOffChain()
        {
            // Testing registering off chain
            var usersInstance = new UsersApi();
            
            try
            {
                string email = "pascal.cross@immutable.com";
                string ethSignature = "";
                string ethKey = "";
                string starkKey = "";
                string starkSignature = "";
                
                RegisterUserRequest request = new RegisterUserRequest(email,ethSignature, ethKey, starkKey, starkSignature);
                
                RegisterUserResponse result = usersInstance.RegisterUser(request);


                GetSignableRegistrationRequest signRequest = new GetSignableRegistrationRequest("", "");
                //usersInstance.stark
                Debug.Log(result.ToJson());
            }
            catch (ApiException e)
            {
                Debug.Log("Exception when calling UsersApi.RegisterUser: " + e.Message);
                Debug.Log("Status Code: " + e.ErrorCode);
                Debug.Log(e.StackTrace);
            }
        }

        private void CheckAssets()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.sandbox.x.immutable.com";
            
            // Testing asset lists
            var apiInstance = new AssetsApi(config);
            
            try
            {
                // Get a list of assets
                ListAssetsResponse result = apiInstance.ListAssets();
                Debug.Log(result.ToJson());
                assetsText.text = result.ToJson();
            }
            catch (ApiException  e)
            {
                Debug.Log("Exception when calling AssetsApi.ListAssets: " + e.Message);
                Debug.Log("Status Code: " + e.ErrorCode);
                Debug.Log(e.StackTrace);
            }
        }

        private void GetUser()
        {
            // Testing getting users
            var usersInstance = new UsersApi();
            
            try
            {
                GetUsersApiResponse result = usersInstance.GetUsers("0x7b680319568a55bf60e94291cDf4E939d603cE96");
                Debug.Log(result.ToJson());
                accountText.text = result.ToJson();
            }
            catch (ApiException e)
            {
                Debug.Log("Exception when calling UsersApi.GetUsers: " + e.Message);
                Debug.Log("Status Code: " + e.ErrorCode);
                Debug.Log(e.StackTrace);
            }
        }
    }
}
