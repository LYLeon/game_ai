using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformNode : MonoBehaviour {

    private List<PlatformNode> neibors;


    public PlatformNode() {
        neibors = new List<PlatformNode>();
    }

    public void AddNode(PlatformNode node) {
        neibors.Add(node);
    }

    public List<PlatformNode> GetNearByNodes() {
        return neibors;
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
