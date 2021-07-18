using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fuvk : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{

	internal bool isOnPlane = false; 
	public void OnPointerEnter(PointerEventData eventData)
	{
		isOnPlane = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isOnPlane = false;
	}
}

