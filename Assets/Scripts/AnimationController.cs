using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animation anim;

    void Start()
    {
        anim = GetComponent<Animation>();
    }
    
    // Playing idle animation for menu components.
    public void PlayIdle()
    {
        anim.Play(anim.name + "-Idle");
    }

    // Playing window open animation.
    public void OpenWindow()
    {
        anim.Play("Window-In");
        AudioManager.Instance.Play("Whoosh");
    }

    // Playing window close animation.
    public void CloseWindow()
    {
        anim.Play("Window-Out");
        AudioManager.Instance.Play("Whoosh");
    }
}
