using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDecalsWhereBulletHits : MonoBehaviour {

    // Use this for initialization
    public GameObject decalPref;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision other)
    {
        var decal = Instantiate(decalPref);
        decal.transform.position = other.transform.position;
        decal.transform.rotation = Quaternion.identity;
    }
}
