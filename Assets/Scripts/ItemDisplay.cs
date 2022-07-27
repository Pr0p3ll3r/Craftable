using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDisplay : MonoBehaviour
{
	public Item item;
	[SerializeField] private Image icon;
	[SerializeField] private TextMeshProUGUI nameText;

	public void Setup (Item _item)
	{
		item = _item;
		icon.sprite = item.icon;
		nameText.text = item.name;
	}
}
