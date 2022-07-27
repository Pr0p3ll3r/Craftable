using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Sprite soundOn, soundOff, vibrationOn, vibrationOff, musicOn, musicOff;
    public Image soundImage, vibrationImage, musicImage;
    public AudioMixer audioMixer;
    public TextMeshProUGUI sortText;

    void Start()
    {
        // When game starts set the settings to the game.
        SetSounds();
        SetMusic();
        SetVibration();
        SetSort();
    }

    // When player selected to change sound state.
    public void ChangeSounds()
    {
        // Get sounds state.
        bool active = GetSetting("Sounds");
        
        // If sounds is active
        if(active)
        {
            // Disable sounds in the game.
            soundImage.sprite = soundOff;
            audioMixer.SetFloat("SFXVolume", -80);
            ChangeSetting("Sounds", 0);
        }
        else
        {
            // Enable sounds in the game.
            soundImage.sprite = soundOn;
            audioMixer.SetFloat("SFXVolume", 0);
            ChangeSetting("Sounds", 1);
        }

        AudioManager.Instance.Play("Clock");
    }

    // When player selected to change sound state.
    public void ChangeMusic()
    {
        // Get sounds state.
        bool active = GetSetting("Music");

        // If sounds is active
        if (active)
        {
            // Disable sounds in the game.
            musicImage.sprite = musicOff;
            audioMixer.SetFloat("MusicVolume", -80);
            ChangeSetting("Music", 0);
        }
        else
        {
            // Enable sounds in the game.
            musicImage.sprite = musicOn;
            audioMixer.SetFloat("MusicVolume", 0);
            ChangeSetting("Music", 1);
        }

        AudioManager.Instance.Play("Clock");
    }

    // When player selected to change vibration state.
    public void ChangeVibration()
    {
        // Get vibration state.
        bool active = GetSetting("Vibration");
        
        // If vibration is active
        if(active)
        {
            // Disable vibrations in the game.
            vibrationImage.sprite = vibrationOff;
            ChangeSetting("Vibration", 0);
        }
        else
        {
            // Enable vibrations in the game.
            vibrationImage.sprite = vibrationOn;
            ChangeSetting("Vibration", 1);
        }

        AudioManager.Instance.Play("Clock");
    }

    public void ChangeSort()
    {
        // Get sort state.
        bool active = GetSetting("Sorted");

        // If sort is active.
        if (active)
        {
            // Disable sort.
            sortText.text = "SORT";
            ChangeSetting("Sorted", 0);
            Inventory.Instance.SortAlphabetically(false, null);
        }
        else
        {
            // Enable sort.
            sortText.text = "UNDO";
            ChangeSetting("Sorted", 1);
            Inventory.Instance.SortAlphabetically(true, null);
        }

        AudioManager.Instance.Play("Clock");
    }

    // Used to get setting value.
    public static bool GetSetting(string name)
    {
        return PlayerPrefs.GetInt(name, 1) == 1 ? true : false;
    }

    // Used to change setting value.
    private void ChangeSetting(string name, int state)
    {
        PlayerPrefs.SetInt(name, state);
    }

    // Setting sounds at the start of the game.
    private void SetSounds()
    {
        // Get sound state.
        bool active = GetSetting("Sounds");
        
        // If sounds is active.
        if(active)
        {
            // Enable sounds.
            soundImage.sprite = soundOn;
            audioMixer.SetFloat("SFXVolume", 0);
        }
        else
        {
            // Disable sounds.
            soundImage.sprite = soundOff;
            audioMixer.SetFloat("SFXVolume", -80);
        }
    }

    // Setting sounds at the start of the game.
    private void SetMusic()
    {
        // Get sound state.
        bool active = GetSetting("Music");

        // If sounds is active.
        if (active)
        {
            // Enable sounds.
            musicImage.sprite = musicOn;
            audioMixer.SetFloat("MusicVolume", 0);
        }
        else
        {
            // Disable sounds.
            musicImage.sprite = musicOff;
            audioMixer.SetFloat("MusicVolume", -80);
        }
    }

    private void SetVibration()
    {
        // Get vibration state.
        bool active = GetSetting("Vibration");
        
        // If vibration is active.
        if(active)
        {
            // Enable vibration.
            vibrationImage.sprite = vibrationOn;
        }
        else
        {
            // Disable vibration.
            vibrationImage.sprite = vibrationOff;
        }
    }

    private void SetSort()
    {
        // Get sort state.
        bool active = GetSetting("Sorted");

        // If sort is active.
        if (active)
        {
            // Enable sort.
            sortText.text = "UNDO";
        }
        else
        {
            // Disable sort.
            sortText.text = "SORT";
        }
    }
}
