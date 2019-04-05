using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	Text scoreT;


	void Awake()
	{
		scoreT = GetComponent<Text>();
	}

	void Update()
	{
		scoreT.text = SphereCtrl.score.ToString();
	}
}
