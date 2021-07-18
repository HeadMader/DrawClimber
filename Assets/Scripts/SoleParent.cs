using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoleParent : MonoBehaviour
{
	internal DrawLines drawLines;
	internal Transform parent;
	void Start()
	{
		transform.localEulerAngles = drawLines.transform.localEulerAngles;
		transform.parent = parent;
		transform.position = parent.position;
		transform.rotation = parent.rotation;
	}

	
}
