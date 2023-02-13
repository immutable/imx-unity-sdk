using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    public void CompleteLogin(string l2Keys)
    {
        loginScreen.SetActive(false);
        purchaseScreen.SetActive(true);
        inventoryScreen.SetActive(false);
    }

    public void CompletePurchase()
    {
        loginScreen.SetActive(false);
        purchaseScreen.SetActive(false);
        inventoryScreen.SetActive(true);
    }
}
