using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseCanvasScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        //transform.position = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        transform.position = Input.mousePosition;

    }
}
