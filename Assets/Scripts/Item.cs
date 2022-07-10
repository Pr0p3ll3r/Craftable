using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{ 
    public Sprite icon;
    [TextArea]
    public string discoveryText;
    public string discoveryTitle;

    public int ID;
}

public class ItemAlphabetically : IComparer<Item>
{
    public int Compare(Item i1, Item i2)
    {
        return i1.name.CompareTo(i2.name);
    }
}

public class ItemID : IComparer<Item>
{
    public int Compare(Item i1, Item i2)
    {
        return i1.ID.CompareTo(i2.ID);
    }
}
