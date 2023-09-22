using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultEffect : MonoBehaviour
{
	[SerializeField] float speed = 10f;
	
 
	void Update ()
	{
		transform.Rotate(speed*Vector3.forward*Time.unscaledDeltaTime);
	}
}
