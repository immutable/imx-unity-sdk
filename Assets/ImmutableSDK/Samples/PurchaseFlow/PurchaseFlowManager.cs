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

        public string walletID = "0xF652A8bCb0Df65AE5fEd91DECaA8B591caeD1e1c";
        public string l2StarkKey = "";
    
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
