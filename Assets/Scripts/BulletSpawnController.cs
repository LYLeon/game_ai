using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnController : MonoBehaviour {

    public GameObject BulletPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Fire() {
        GameObject bullet = Instantiate(BulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * 6;
        Destroy(bullet, 3);
    }
}
