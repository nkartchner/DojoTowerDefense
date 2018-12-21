using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComeandgoBehaviour : MonoBehaviour {

    public float speedX = 0.5f;
    public float distx = 150;
    public float speedY = 0f;
	// Use this for initialization
	void Start () {
    
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3 (speedX, speedY, 0));
        if (transform.position.x > distx || transform.position.x < -distx)
        {
            speedX = -speedX;
            speedY = -speedY;
        }

    }
}
