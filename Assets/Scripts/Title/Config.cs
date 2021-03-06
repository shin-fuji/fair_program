﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Neuron;

public class Config : MonoBehaviour
{
    public GameObject OnPanel;
    public bool configMenu = false;
    Text newAddress;
    

    void Start()
    {
        OnUnConfig();
    }

    public void OnConfig()
    {
        OnPanel.SetActive(true);        // ConfigMenuを開く
        configMenu = true;
    }

    public void OnUnConfig()
    {
        OnPanel.SetActive(false);       // ConfigMenuを閉じる
        configMenu = false;
    }

    public void OnResume()
    {
        OnUnConfig();
    }
}
