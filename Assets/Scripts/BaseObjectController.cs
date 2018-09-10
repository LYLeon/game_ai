using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObjectController : MonoBehaviour {

    public int DEFAULT_HP = 100;
    private int hp;
	// Use this for initialization
	void Start () {
        hp = DEFAULT_HP;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) {
            Destroy(collision.gameObject);
            TakeDamage(5);
        }
    }

    public void TakeDamage(int damage) {
        hp -= damage;
        if (hp < 0) {
            Destroy(gameObject);
        }
    }
}
