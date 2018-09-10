using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBlockController : MonoBehaviour {

    private const int ACTIVATE_ENERGY_THRESHOLD = 3;
    private int energyLevel = 0;

	// Use this for initialization
	void Start () {
        InvokeRepeating("TryActivate", 0, 1);
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void TryActivate() {
        if (energyLevel >= ACTIVATE_ENERGY_THRESHOLD) {
            Attack();        
            energyLevel -= ACTIVATE_ENERGY_THRESHOLD;
        }
    }

    private void Attack() {
        Debug.Log("activate attack block:" + gameObject);
    }

    public void AddEnergy(int amount) {
        energyLevel += amount;
        TryActivate();
    }
}
