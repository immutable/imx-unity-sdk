using UnityEngine;

namespace ImmutableSDK.Samples.PurchaseFlow
{
    /// <summary>
    /// Handles transitioning between states of the purchase flow demo
    /// </summary>
    public class PurchaseFlowManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject loginScreen = null;
    
        [SerializeField]
        private GameObject purchaseScreen = null;
    
        [SerializeField]
        private GameObject inventoryScreen = null;

        [HideInInspector]
        public string walletAddress = "0xF652A8bCb0Df65AE5fEd91DECaA8B591caeD1e1c";
        
        [HideInInspector]
        public string l2StarkKey = "";
        
        [HideInInspector]
        public string collectionAddress = "0x4344b6e12f910b32f63c74b2937c49c0d2aab513";
    
        /// <summary>
        /// Caches data from login phase, transitions to the purchase screen phase
        /// </summary>
        /// <param name="l2Keys"></param>
        public void CompleteLogin(string l2Keys)
        {
            loginScreen.SetActive(false);
            purchaseScreen.SetActive(true);
            inventoryScreen.SetActive(false);
        }

        /// <summary>
        /// Transitions from successful purchase to owned items screen
        /// </summary>
        public void CompletePurchase()
        {
            loginScreen.SetActive(false);
            purchaseScreen.SetActive(false);
            inventoryScreen.SetActive(true);
        }
    }
}
