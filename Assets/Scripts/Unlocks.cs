using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlocks : MonoBehaviour
{
    private Inventory inventory;
    private List<Reward> rewards;

    void Start()
    {
        inventory = Inventory.Instance;
        rewards = new List<Reward>(Resources.LoadAll<Reward>("Rewards"));
        LoadUnlocks();
    }

    void Update()
    {
        Reward[] rewardsArray = rewards.ToArray();
        foreach (Reward reward in rewardsArray)
        {
            if (reward.itemsRequired == inventory.GetAmount() && reward.itemsRequired != 0)
            {
                Unlock(reward);
                rewards.Remove(reward);
            }
        }
    }

    void Unlock(Reward reward)
    {
        foreach (Item item in reward.items)
        {
            Debug.Log(item.name + " UNLOCKED!!!");
            Discoveries.Instance.Discover(item);
        }
    }

    void LoadUnlocks()
    {
        if (PlayerPrefs.GetInt("Slot") == 1)
            Crafter.Instance.UnlockSlot();
    }
}
