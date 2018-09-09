using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBlockController : Block {

	// Use this for initialization
	void Start () {
        InvokeRepeating("HeartBeat", 0, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void HeartBeat()
    {
        // Debug.Log("Heart beats.");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggering 2D." + collision.ToString());

    }
}
