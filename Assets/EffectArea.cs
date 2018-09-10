using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectArea : MonoBehaviour {

    public interface EffectAreaListener {
        void OnEnter(GameObject obj);
        void OnExit(GameObject obj);
    }

    public EffectAreaListener listener;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("On trigger enter :" + collision.name);
        listener.OnEnter(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        listener.OnExit(collision.gameObject);
    }
}
