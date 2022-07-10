using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
	public int slot;

	public void OnPointerClick(PointerEventData eventData)
	{
		Crafter.Instance.RemoveItem(slot);
	}
}
