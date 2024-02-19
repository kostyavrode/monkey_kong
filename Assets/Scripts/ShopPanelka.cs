using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanelka : MonoBehaviour
{
    public int cost1;
    public int cost2;
    public void Buy1()
    {
        if (PlayerPrefs.GetInt("Money")>=cost1)
        {
            PlayerPrefs.SetInt("Buy1", 1);
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - cost1);
            PlayerPrefs.Save();
        }
        GameManager.instance.CheckBuy();
    }
    public void Buy2()
    {
        if (PlayerPrefs.GetInt("Money") >= cost2)
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - cost2);
            PlayerPrefs.SetInt("Buy2", 1);
            PlayerPrefs.Save();
        }
        GameManager.instance.CheckBuy();
    }
}
