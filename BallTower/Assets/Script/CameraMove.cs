using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
	public float rotate_spd = 100.0f;

	public GameObject pillar;
 
	void OnMouseDrag() 
	{
   		float temp_y_axis = Input.GetAxis("Mouse X") * rotate_spd * Time.deltaTime;
   		pillar.transform.Rotate(pillar.transform.rotation.x, -temp_y_axis, 0);
	}

}
