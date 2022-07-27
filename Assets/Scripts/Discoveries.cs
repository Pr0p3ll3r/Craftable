using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Discoveries : MonoBehaviour 
{
	public static Discoveries Instance;

	[SerializeField] private GameObject discoverUI;
	[SerializeField] private Animator discoverAnimator;
	[SerializeField] private Image icon;
	[SerializeField] private TextMeshProUGUI itemName;
	[SerializeField] private TextMeshProUGUI description;
	[SerializeField] private TextMeshProUGUI title;

	[SerializeField] private List<Item> discoveredItems;
	[SerializeField] private List<Item> nextToDiscover;

	private bool isDiscovering = false;
	private Item tempItem;

	private string currentText = "";
	private Coroutine co;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		discoveredItems = new List<Item>();
	}

	public bool HasDiscovered(Item item)
	{
		return discoveredItems.Contains(item);
	}

	public void Discover(Item item)
	{
		if (isDiscovering)
		{
			nextToDiscover.Add(item);
		} 
		else
		{
			OpenDiscoveryPanel(item);
		}

		discoveredItems.Add(item);
	}

	void OpenDiscoveryPanel(Item item)
	{
		if (!discoverUI.activeSelf)
			discoverUI.SetActive(true);

		Debug.Log(item.name);
		discoverAnimator.SetBool("isOpen", true);

		AudioManager.Instance.Play("Discovered");

		isDiscovering = true;

		icon.sprite = item.icon;
		itemName.text = item.name;

		description.text = "";

		if (co != null) StopCoroutine(co);

		if (nextToDiscover.Count > 0) co = StartCoroutine(ShowText(item.discoveryText, 0f));
		else co = StartCoroutine(ShowText(item.discoveryText, 1f));

		if (string.IsNullOrEmpty(item.discoveryTitle))
			title.text = "Discovered new item!";
		else title.text = item.discoveryTitle;

		if(PlayerPrefs.GetInt("Vibration") == 1) Handheld.Vibrate();
		tempItem = item;
	}

	public void CloseDiscoveryPanel()
	{
		AudioManager.Instance.Play("Click");

		if(tempItem.name == "Slot")
        {
			tempItem = null;
			Crafter.Instance.UnlockSlot();
			PlayerPrefs.SetInt("Slot", 1);
		}
		else
        {
			Inventory.Instance.AddItem(tempItem);
			tempItem = null;
		}

		if (nextToDiscover.Count > 0)
		{
			OpenDiscoveryPanel(nextToDiscover[0]);
			nextToDiscover.RemoveAt(0);
		} 
		else
		{
			isDiscovering = false;
			discoverAnimator.SetBool("isOpen", false);
			if(Inventory.Instance.items.Count % 30 == 0)
				InitializeAds.Instance.ShowInterstitialAd();
		}
	}

	IEnumerator ShowText(string fullText, float delay)
	{
		yield return new WaitForSeconds(delay);
		for (int i = 0; i <= fullText.Length; i++)
		{
			currentText = fullText.Substring(0, i);
			description.text = currentText;
			yield return new WaitForSeconds(0.05f);
		}
	}
}
