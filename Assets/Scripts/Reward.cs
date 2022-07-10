using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Reward", menuName = "Reward")]
public class Reward : ScriptableObject
{
    public int itemsRequired;
    public List<Item> items;

    public bool unlocked = false;
}
