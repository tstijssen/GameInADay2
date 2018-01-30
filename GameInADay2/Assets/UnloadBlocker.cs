using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class UnloadBlocker : MonoBehaviour {

    public AudioSource source;
	// Use this for initialization
	void Awake() {
        DontDestroyOnLoad(transform.gameObject);
        source.Stop();
        source.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
