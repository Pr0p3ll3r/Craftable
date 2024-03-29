﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hint : MonoBehaviour
{
    private AnimationController animationController;
    [SerializeField] private TextMeshProUGUI errorText;
    [SerializeField] private GameObject adImage;
    [SerializeField] private GameObject text;

    private bool clicked = false;
    private int totalItems;

    void Start()
    {
        totalItems = Resources.LoadAll<Item>("Items").Length;
        animationController = GetComponent<AnimationController>();
    }

    public void GetHint()
    {
        if(Inventory.Instance.items.Count == totalItems)
        {
            errorText.text = "You created all items.";
            errorText.gameObject.GetComponent<Animator>().Play("Show");
            return;
        }
    
        if(Wallet.keys > 0)
        {
            animationController.CloseWindow();
            Crafter.Instance.Hint();
        }
        else if(clicked == false)
        {
            errorText.text = "You don't have enough keys";
            errorText.gameObject.GetComponent<Animator>().Play("Show");
            adImage.SetActive(true);
            text.SetActive(false);
            clicked = true;
        }
        else
        {
            InitializeAds.Instance.ShowRewardedVideo();
            animationController.CloseWindow();
            adImage.SetActive(false);
            text.SetActive(true);
            clicked = false;
        }
    }

    public void ResetMenu()
    {
        clicked = false;
        adImage.SetActive(false);
        text.SetActive(true);       
    }
}
