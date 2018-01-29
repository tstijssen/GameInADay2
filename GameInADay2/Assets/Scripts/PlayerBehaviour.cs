using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour {

    GameObject[] playerFleet;
    GameObject SelectedPlanet;

    public Text PlayerSelectedPlanetName;
    public Text PlayerSelectedFleetSize;

    // Use this for initialization
    void Start () {
		
	}
	

	// Update is called once per frame
	void Update () {
        PlayerSelectedPlanetName.text = PlayerPrefs.GetString("SelectedPlanet");
        PlayerSelectedFleetSize.text = "Ships: " + PlayerPrefs.GetInt("SelectedFleet");

    }
}
