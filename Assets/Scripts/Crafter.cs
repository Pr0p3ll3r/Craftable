using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafter : MonoBehaviour
{
    public static Crafter Instance;

	public Item slot01;
	public Item slot02;
	public Item slot03;

	public Image slot01Icon;
	public Image slot02Icon;
	public Image slot03Icon;

	public Recipe[] recipes;

	private int currentSlot = 1;
	public int numberOfSlots = 2;

	private Recipe tempRecipe;

	void Awake()
    {
        Instance = this;
    }

	private void Start()
	{
		recipes = Resources.LoadAll<Recipe>("Recipes");
	}

	public void AddItem(Item item)
	{
		Debug.Log("Add item to Crafter: " + item.name + " at slot " + currentSlot);

		switch(numberOfSlots)
        {
			case 2:
                {
					if (currentSlot == 1 && slot01 == null)
					{
						slot01 = item;
						ShowItem(slot01Icon, item);
					}
					else if (currentSlot == 2 && slot02 == null)
					{
						slot02 = item;
						ShowItem(slot02Icon, item);
					}
					else if (currentSlot == 1 && slot01 != null)
					{
						RemoveItem(currentSlot);
						slot01 = item;
						ShowItem(slot01Icon, item);
					}
					else
					{
						RemoveItem(currentSlot);
						slot02 = item;
						ShowItem(slot02Icon, item);
					}

					if (currentSlot == 1) currentSlot++;
					else currentSlot--;

					break;
				}
			case 3:
                {
					if (currentSlot == 1 && slot01 == null)
					{
						slot01 = item;
						ShowItem(slot01Icon, item);
					}
					else if (currentSlot == 2 && slot02 == null)
					{
						slot02 = item;
						ShowItem(slot02Icon, item);
					}
					else if (currentSlot == 3 && slot02 == null)
					{
						slot02 = item;
						ShowItem(slot03Icon, item);
					}
					else if (currentSlot == 1 && slot01 != null)
					{
						RemoveItem(currentSlot);
						slot01 = item;
						ShowItem(slot01Icon, item);
					}
					else if(currentSlot == 2 && slot02 != null)
					{
						RemoveItem(currentSlot);
						slot02 = item;
						ShowItem(slot02Icon, item);
					}
					else
					{
						RemoveItem(currentSlot);
						slot03 = item;
						ShowItem(slot03Icon, item);
					}

					if (currentSlot == 1 || currentSlot == 2) currentSlot++;
					else if(currentSlot == 3)currentSlot-=2;
					else currentSlot--;

					break;
				}
		}
		UpdateResult();
	}

	public void RemoveItem(int slot)
	{
		if (slot == 1 && slot01 != null)
		{
			slot01 = null;
			slot01Icon.color = new Color32(255, 255, 255, 0);
			slot01Icon.sprite = null;
			currentSlot = 1;
			Debug.Log("Remove from Slot " + slot);
		}
		else if (slot == 2 && slot02 != null)
		{
			slot02 = null;
			slot02Icon.color = new Color32(255, 255, 255, 0);
			slot02Icon.sprite = null;
			currentSlot = 2;
			Debug.Log("Remove from Slot " + slot);
		}
		else if (slot == 3 && slot03 != null)
		{
			slot03 = null;
			slot03Icon.color = new Color32(255, 255, 255, 0);
			slot03Icon.sprite = null;
			Debug.Log("Remove from Slot " + slot);
		}

		UpdateResult();
	}

	void ShowItem(Image icon, Item item)
    {
		icon.sprite = item.icon;
		icon.color = Color.white;
    }

	void UpdateResult()
	{
		Item[] results = GetResults();
		if (results != null && results.Length != 0)
		{
			ResetSlots();
			foreach (Item result in results)
			{
				ShowCreateItem(result);
			}	
		}
		else if(numberOfSlots == 2 && results != null)
        {
			Smoke();
			ResetSlots();
		}

		if(numberOfSlots == 3)
        {
			Item[] advancedResults = GetResultsAdvanced();
			if (advancedResults != null && advancedResults.Length != 0)
			{
				ResetSlots();
				foreach (Item result in advancedResults)
				{
					ShowCreateItem(result);
				}
			}
			else if (advancedResults != null)
			{
				Smoke();
				ResetSlots();
			}
		}
	}

	void ShowCreateItem(Item item)
	{
		if (!Discoveries.Instance.HasDiscovered(item))
		{
			Discoveries.Instance.Discover(item);
		}
	}

	Item[] GetResults()
	{
		if (slot01 == null || slot02 == null)
			return null;

		List<Item> items = new List<Item>();

		foreach (Recipe recipe in recipes)
		{
			if(recipe.advanced == false)
            {
				if ((recipe.input01 == slot01 && recipe.input02 == slot02) ||
					(recipe.input01 == slot02 && recipe.input02 == slot01))
				{
					if (!Inventory.Instance.HasItem(recipe.result))
					{
						items.Add(recipe.result);
					}
				}
			}
		}

		return items.ToArray();
	}

	Item[] GetResultsAdvanced()
	{
		if (slot01 == null || slot02 == null || slot03 == null)
			return null;

		List<Item> items = new List<Item>();

		foreach (Recipe recipe in recipes)
		{
			if (recipe.advanced == true)
			{

				if ((recipe.input01 == slot01 && recipe.input02 == slot02 && recipe.input03 == slot03) ||
				(recipe.input01 == slot01 && recipe.input02 == slot03 && recipe.input03 == slot02) ||
				(recipe.input01 == slot02 && recipe.input02 == slot01 && recipe.input03 == slot03) ||
				(recipe.input01 == slot02 && recipe.input02 == slot03 && recipe.input03 == slot01) ||
				(recipe.input01 == slot03 && recipe.input02 == slot01 && recipe.input03 == slot02) ||
				(recipe.input01 == slot03 && recipe.input02 == slot02 && recipe.input03 == slot01))
				{
					if (!Inventory.Instance.HasItem(recipe.result))
					{
						items.Add(recipe.result);
					}
				}
			}
		}

		return items.ToArray();
	}

	void ResetSlots()
    {
		RemoveItem(1);
		RemoveItem(2);
		RemoveItem(3);
		currentSlot = 1;
	}

	void Smoke()
    {
		if(numberOfSlots == 2)
        {
			slot01Icon.transform.parent.Find("Smoke").GetComponent<Animator>().Play("Smoke");
			slot02Icon.transform.parent.Find("Smoke").GetComponent<Animator>().Play("Smoke");
		}
		else
        {
			slot01Icon.transform.parent.Find("Smoke").GetComponent<Animator>().Play("Smoke");
			slot02Icon.transform.parent.Find("Smoke").GetComponent<Animator>().Play("Smoke");
			slot03Icon.transform.parent.Find("Smoke").GetComponent<Animator>().Play("Smoke");
		}
    }

	public void UnlockSlot()
    {
		numberOfSlots++;
		slot03Icon.transform.parent.gameObject.SetActive(true);
	}

	Recipe GetRandomRecipe()
    {
		for(int i=0;i<recipes.Length;i++)
        {
			if (!Inventory.Instance.HasItem(recipes[i].result))
			{
				if(recipes[i].advanced == false && Inventory.Instance.items.Contains(recipes[i].input01) && Inventory.Instance.items.Contains(recipes[i].input02))
                {
					return recipes[i];				
				}
				else if (PlayerPrefs.GetInt("Slot") == 1 && Inventory.Instance.items.Contains(recipes[i].input01) && Inventory.Instance.items.Contains(recipes[i].input02) && Inventory.Instance.items.Contains(recipes[i].input03))
                {
					return recipes[i];
				}
			}
		}
		return null;
    }

	public void Hint()
    {
		if(tempRecipe == null || Inventory.Instance.items.Contains(tempRecipe.result)) tempRecipe = GetRandomRecipe();

		if (tempRecipe == null) return;

		if(Wallet.keys != 0) Wallet.SetAmount(Wallet.keys - 1);

		ResetSlots();

		if (tempRecipe.GetInput() == 1)
        {
			AddItem(tempRecipe.input01);
			tempRecipe.SetInput(2);
		}
		else if(tempRecipe.GetInput() == 2)
        {
			AddItem(tempRecipe.input01);
			AddItem(tempRecipe.input02);
			tempRecipe.SetInput(3);
		}
		else
        {
			AddItem(tempRecipe.input01);
			AddItem(tempRecipe.input02);
			AddItem(tempRecipe.input03);
		}
    }
}
