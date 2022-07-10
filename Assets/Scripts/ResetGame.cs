using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    private AnimationController animationController;

    void Start()
    {
        animationController = this.GetComponent<AnimationController>();
    }

    public void OpenWindow()
    {
        animationController.OpenWindow();
    }

    // If player selects close window button.
    public void CloseWindow()
    {
        // Close window animation.
        animationController.CloseWindow();
    }

    // If player selected to reset all game progress.
    public void Reset()
    {
        // Delete all saved player prefs.
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Main");
    }
}
