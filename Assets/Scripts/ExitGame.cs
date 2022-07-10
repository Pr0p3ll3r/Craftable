using UnityEngine;

public class ExitGame : MonoBehaviour
{
    private AnimationController animationController;

    void Start()
    {
        animationController = this.GetComponent<AnimationController>();
    }

    void Update()
    {
        #if UNITY_ANDROID
        // Check if back/esc button was pressed.
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            animationController.OpenWindow();
        }    
        #endif    
    }

    public void No()
    {
        animationController.CloseWindow();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
