using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanelka : MonoBehaviour
{
    public int cost1;
    public int cost2;
    public int cost3;
    public int cost4;
    public GameObject[] notBrought;
    public GameObject[] brought;
    
    private void OnEnable()
    {
        Check();
    }
    private void Check()
    {
        if (PlayerPrefs.HasKey("Buy1"))
        {
            brought[0].SetActive(true);
            notBrought[0].SetActive(false);
        }
        if (PlayerPrefs.HasKey("Buy2"))
        {
            brought[1].SetActive(true);
            notBrought[1].SetActive(false);
        }
        if (PlayerPrefs.HasKey("Buy3"))
        {
            brought[2].SetActive(true);
            notBrought[2].SetActive(false);
        }
        if (PlayerPrefs.HasKey("Buy4"))
        {
            brought[3].SetActive(true);
            notBrought[3].SetActive(false);
        }
    }
    public void Buy1()
    {
        if (PlayerPrefs.GetInt("Money")>=cost1)
        {
            PlayerPrefs.SetInt("Buy1", 1);
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - cost1);
            PlayerPrefs.Save();
        }
        GameManager.instance.CheckBuy();
        Check();
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
        Check();
    }
    public void Buy3()
    {
        if (PlayerPrefs.GetInt("Money") >= cost3)
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - cost3);
            PlayerPrefs.SetInt("Buy3", 1);
            PlayerPrefs.Save();
        }
        GameManager.instance.CheckBuy();
        Check();
    }
    public void Buy4()
    {
        if (PlayerPrefs.GetInt("Money") >= cost4)
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - cost4);
            PlayerPrefs.SetInt("Buy4", 1);
            PlayerPrefs.Save();
        }
        GameManager.instance.CheckBuy();
        Check();
    }
}
