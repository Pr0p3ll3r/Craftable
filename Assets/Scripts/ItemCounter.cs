using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemCounter : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    public SlicedFilledImage progressBar;
    private int totalItems;

    void Start()
    {
        totalItems = Resources.LoadAll<Item>("Items").Length;
    }

    void Update()
    {
        int currentItems = Inventory.Instance.items.Count;
        counterText.text = currentItems + "/" + totalItems;
        progressBar.fillAmount = currentItems / (float)totalItems;
    }
}
