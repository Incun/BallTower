using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    

    public GameObject FM;

	void Awake () {
        if(instance)
        {
            Destroy(instance);
        }
        instance = this;
    }
}
