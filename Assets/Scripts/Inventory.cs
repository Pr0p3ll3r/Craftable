using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<Item> items;
    [SerializeField] private List<Item> startItems;

    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform itemPool;
    [SerializeField] private GameObject tutorial;

    private Item[] allItems;

    private void Awake()
    {
        Instance = this;
        allItems = Resources.LoadAll<Item>("Items");
        if (PlayerPrefs.GetInt("New") == 0) PlayerPrefs.SetInt("Sorted", 0);
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        //check if it's first time playing
        if (PlayerPrefs.GetInt("New") == 0)
        {
            PlayerPrefs.SetInt("New", 1);
            PlayerPrefs.SetInt("NextID", 0);
            foreach (Item item in startItems)
            {
                AddItem(item);
            }
            tutorial.SetActive(true);
        }
        else
        {
            LoadItems();
        }
    }

    public void AddItem(Item item)
    {
        Debug.Log("Add item to Inventory: " + item.name);

        //Save
        PlayerPrefs.SetInt($"{item.name}", 1);
        PlayerPrefs.SetInt($"{item.name}ID", PlayerPrefs.GetInt("NextID"));
        PlayerPrefs.SetInt("NextID", PlayerPrefs.GetInt("NextID") + 1);
        item.ID = PlayerPrefs.GetInt($"{item.name}ID");

        items.Add(item);
        if (PlayerPrefs.GetInt("Sorted") == 1)
        {
            SortAlphabetically(true, item);
            return;
        }
        GameObject itemObj = Instantiate(itemPrefab, itemPool);
        ItemDisplay display = itemObj.GetComponent<ItemDisplay>();
        if (display != null)
            display.Setup(item);
        itemObj.GetComponent<Animator>().Play("Show");
    }

    public int GetAmount() { return items.Count; }

    public bool HasItem(Item item)
    {
        if (items.Contains(item))
            return true;
        else return false;
    }

    void LoadItems()
    {
        items.Clear();

        for (int i = 0; i < allItems.Length; i++)
        {
            Item item = allItems[i];
            if(PlayerPrefs.GetInt($"{item.name}") == 1)
            {
                items.Add(item);
                Debug.Log("Add item to Inventory: " + item.name);
                PlayerPrefs.SetInt($"{item.name}", 1);
                item.ID = PlayerPrefs.GetInt($"{item.name}ID");
            }
        }

        if (PlayerPrefs.GetInt("Sorted") == 1) SortAlphabetically(true, null);
        else SortAlphabetically(false, null);
    }

    public void SortAlphabetically(bool alphabetically, Item i)
    {
        foreach (Transform child in itemPool.transform)
        {
            Destroy(child.gameObject);
        }

        if (alphabetically) items.Sort(new ItemAlphabetically());
        else items.Sort(new ItemID());

        foreach (Item item in items)
        {
            GameObject itemObj = Instantiate(itemPrefab, itemPool);
            ItemDisplay display = itemObj.GetComponent<ItemDisplay>();
            if (display != null)
                display.Setup(item);
        }
    }
}
