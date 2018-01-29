using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetaryBehaviour : MonoBehaviour {

    public float SpinSpeed;
    public string PlanetName;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // planetary spin

        transform.RotateAround(transform.position, new Vector3(0, 0, 1), Time.deltaTime * SpinSpeed);
	}
}
