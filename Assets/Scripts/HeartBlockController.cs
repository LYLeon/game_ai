using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBlockController : MonoBehaviour, EffectArea.EffectAreaListener
{
    private List<AttackBlockController> blockNodes = new List<AttackBlockController>();

    // Use this for initialization
    void Start () {
        GetComponentInChildren<EffectArea>().listener = this;
        InvokeRepeating("GenerateEnergy", 0, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void GenerateEnergy()
    {
        foreach(AttackBlockController atkBLock in blockNodes) {
            atkBLock.AddEnergy(1);
        }
    }

    void EffectArea.EffectAreaListener.OnEnter(GameObject obj)
    {
        AttackBlockController atkController = obj.GetComponent<AttackBlockController>();
        if (atkController != null) {
            blockNodes.Add(atkController);
        }
    }

    void EffectArea.EffectAreaListener.OnExit(GameObject obj)
    {
        AttackBlockController atkController = obj.GetComponent<AttackBlockController>();
        if (atkController != null)
        {
            blockNodes.Remove(atkController);
        }
    }
}
