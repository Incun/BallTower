using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePlusManager : MonoBehaviour
{
	Text scorePtext;
	public Color color;
	RectTransform scorepos;
	int a = 255;
	
	
	void Start ()
	{
		Debug.Log("start");
		scorePtext = GetComponent<Text>();
		scorepos = GetComponent<RectTransform>();
		color = GetComponent<Text>().color;

		scorePtext.text = "+" + SphereCtrl.fscore.ToString();
		StartCoroutine(ScorePlus());
	}

	void Update()
	{
		Debug.Log(color.r +" "+ color.g +" "+ color.b +" "+ color.a +" "+ a);
	}

	IEnumerator ScorePlus()
	{
		Debug.Log("start");
		while(color.a > 0)
		{
			
			scorepos.localPosition += Vector3.up*4;
			color = new Color(color.r, color.g, color.b, a);
			a -= 10;
			yield return null;
			
		}
		Destroy(gameObject);
	}

}
