using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCtrl : MonoBehaviour
{

	public GameObject ink;			// 잉크 사진
	public GameObject text;			// 점수 텍스트
	public GameObject canvas;		// 캔버스
	public GameObject textpos;		// 점수 위지
	public GameObject target;		// 바닥 블럭
	public GameObject CameraCtrl;	// 카메라 컨트롤
	public Rigidbody rb;			// Rigidbody
	public int t_index;				// 타겟 인덱스
	public static int score;		// 점수
	public static int fscore;		// 점수 증가
	

	bool isGround = true;			// 바닥 충돌체크
	float power = 900f;				// 점프 힘
	int level = 1;					// 레벨
	int scorex = 1;					// 점수 증가폭

	GameObject Aink;				// 잉크 생성
	GameObject Atext;				// 점수 생성

	private IEnumerator coroutine;

	void Start()
	{
		StartCoroutine(Randomrotation());
		t_index = 0;
		target = GameManager.instance.FM.GetComponent<FloorManager>().floor[t_index];
		coroutine = Cameramove();
	}

	
	
	void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Floor" && isGround == true)  // 바닥에 닿으면
        {
			rb.velocity = new Vector3(0, 0, 0);
			StartCoroutine(SphereCrack());
			scorex = 1;
			StopCoroutine(coroutine);
			int ran = Random.Range(0, 360);

			Vector3 pos = col.contacts[0].point; 

			rb.velocity = Vector3.up * 17;  // 위 벡터로 17만큼 값을 줌
			// rb.AddForce(Vector3.up * power);  // 위 방향으로 power(900) 만큼의 힘을 줌
			Aink = Instantiate(ink, pos, Quaternion.Euler(0, ran, 0)) as GameObject; 
			Aink.transform.SetParent(col.transform);
			isGround = false;
			
        }
    }

	void OnTriggerEnter(Collider col)
	{
		if(col.transform.tag == "Level")
		{
			BreakFloor();  // 바닥 부수기
			StartCoroutine(coroutine);
			fscore = scorex * level;
			Atext = Instantiate(text);
			Atext.transform.SetParent(canvas.transform);
			Atext.transform.position = textpos.transform.position;
			score += fscore;
			scorex++;
		}
	}
	void FixedUpdate()
	{
		if(rb.velocity.magnitude > 20)  // if(최고속도를 넘어가면)
		{
			rb.velocity = new Vector3(0, -20, 0);  // 최대속도 17 고정
		}
		if(rb.velocity.magnitude > 5)  // 중첩충돌 방지
		{
			isGround = true;
		}
	}

	IEnumerator Randomrotation()
	{
		for(int i = 1; i<GameManager.instance.FM.GetComponent<FloorManager>().floor.Length ; i++)
		{
			float ran = Random.Range(0, 360);
			GameManager.instance.FM.GetComponent<FloorManager>().floor[i].transform.rotation = Quaternion.Euler(-90, ran, 0);
			yield return new WaitForSeconds(0);
		}
	}

	IEnumerator Cameramove()
	{
		while(true)
		{
			CameraCtrl.transform.position = new Vector3
			(
			transform.position.x,
			transform.position.y+4.9f,
			transform.position.z+3
			);
			yield return null;
		}
	}

	IEnumerator SphereCrack()
	{
		while(transform.localScale.y > 0.5)
		{
			transform.localScale -= new Vector3(0, 0.1f, 0);
			yield return null;
		}
		while(transform.localScale.y < 1.5)
		{
			transform.localScale += new Vector3(0, 0.1f, 0);
			yield return null;
		}
		while(transform.localScale.y > 1)
		{
			transform.localScale -= new Vector3(0, 0.1f, 0);
			yield return null;
		}
	}

	void BreakFloor()
	{
		Destroy(target);
		t_index++;
		target = GameManager.instance.FM.GetComponent<FloorManager>().floor[t_index];
	}

	
}
