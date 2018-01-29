using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class PlanetManagerScript : MonoBehaviour {

    public GameObject[] Planets;
    public TextAsset PlanetNamelist;
    List<string> nameList = new List<string>();
    GameObject selectedPlanet;
    // Use this for initialization
    void Start () {
        // get random planet name
        string text = PlanetNamelist.text;
        nameList.AddRange(Regex.Split(text, ","));

        for (int i = 0; i < Planets.Length; i++)
        {
            int randomIndex = Random.Range(0, nameList.Count);
            Planets[i].GetComponent<PlanetFleetScript>().PlanetName = nameList[randomIndex];
            nameList.RemoveAt(randomIndex);
        }
    }

    public void PlanetSelected(string planetName)
    {
        
    }


    // Update is called once per frame
    void Update () {

    }
}
