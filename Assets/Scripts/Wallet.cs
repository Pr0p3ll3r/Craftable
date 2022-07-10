using UnityEngine;
using TMPro;

public class Wallet : MonoBehaviour
{
    public static int keys;
    private static TextMeshProUGUI walletText;

    void Start()
    {
        keys = PlayerPrefs.GetInt("KeysAmount", 0);
        walletText = this.GetComponent<TextMeshProUGUI>();
        DisplayAmount();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.B)) keys++;
    }

    public static void SetAmount(int amountToSet)
    {
        keys = amountToSet;
        DisplayAmount();
        PlayerPrefs.SetInt("KeysAmount", keys);
    }

    // Display player keys to the screen.
    private static void DisplayAmount()
    {
        walletText.text = keys.ToString("00");
    }
}
