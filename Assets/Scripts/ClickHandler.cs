using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    public int clickCount = 2;
    float lastClick = 0f;
    public float interval = 0.5f;
    public void OnPointerClick(PointerEventData eventData)
    {
        //#if UNITY_EDITOR
        //if (eventData.clickCount >= clickCount)
        //{
        //    if (eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemDisplay>().item == null)
        //        return;

        //    Crafter.Instance.AddItem(eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemDisplay>().item);
        //    eventData.pointerCurrentRaycast.gameObject.GetComponent<Animator>().Play("Click");
        //    AudioManager.Instance.Play("Click2");
        //}
        //#endif


        if ((lastClick+interval)>Time.time)
        {
            if (eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemDisplay>().item == null)
                return;

            Crafter.Instance.AddItem(eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemDisplay>().item);
            eventData.pointerCurrentRaycast.gameObject.GetComponent<Animator>().Play("Click");
            AudioManager.Instance.Play("Click2");
        }

        lastClick = Time.time;
    }
}
