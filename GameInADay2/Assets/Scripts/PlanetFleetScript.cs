using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class PlanetFleetScript : MonoBehaviour {

    public float SpawnTime;
    public GameObject PlanetTextCanvas;
    public Text PlanetInfo;

    int maxShips;
    float timer;
    bool planetSelected;
    int builtShips;
    public int activeShips;
    public List<GameObject> planetFleet;
    public string PlanetName = "NoName";
    public int OwnerNumber;

	// Use this for initialization
	void Start () {
        maxShips = GetComponent<ObjectPooler>().amountToPool;
        planetFleet = new List<GameObject>();
        timer = SpawnTime;
        activeShips = 0;
        builtShips = 0;
        planetSelected = false;
        PlanetInfo = PlanetTextCanvas.GetComponentInChildren<Text>();
        string info = PlanetName + "\n Ships: " + activeShips + "\n Built: " + builtShips;
        PlanetInfo.text = info;
    }

    // Update is called once per frame
    void Update () {

        timer -= Time.deltaTime;

        // spawn ship to fleet
        if(timer <= 0.0f && builtShips < maxShips)
        {
            Debug.Log("Spawning Ship");
            timer = SpawnTime;

            GameObject newShip = GetComponent<ObjectPooler>().GetPooledObject();
            newShip.SetActive(true);
            newShip.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            newShip.GetComponent<ShipBehaviour>().ShipParent = gameObject;
            newShip.GetComponent<ShipBehaviour>().OwnerNumber = OwnerNumber;
            planetFleet.Add(newShip);

            builtShips++;
            activeShips++;

            string info = PlanetName + "\n Ships: " + activeShips + "\n Built: " + builtShips;
            PlanetInfo.text = info;
            if (planetSelected)
                PlayerPrefs.SetInt("SelectedFleet", activeShips);
        }
    }

    private void OnMouseOver()
    {
        // move fleet to new target
        if(Input.GetMouseButtonDown(1))
        {
            if(PlayerPrefs.GetString("SelectedPlanet") != PlanetName)
            {
                Debug.Log("Moving Fleet from " + PlayerPrefs.GetString("SelectedPlanet") + " to " + PlanetName);

                GameObject[] allPlanets = GetComponentInParent<PlanetManagerScript>().Planets;
                // search for selected planet
                for(int i = 0; i < allPlanets.Length; ++i)
                {
                    if (allPlanets[i].GetComponent<PlanetFleetScript>().PlanetName == PlayerPrefs.GetString("SelectedPlanet")) 
                    {
                        allPlanets[i].GetComponent<PlanetFleetScript>().AddShips(gameObject);
                        //AddShips(allPlanets[i]);
                        break;
                    }
                }
            }
        }
    }

    public void AddShips(GameObject sender)
    {
        foreach (GameObject ship in planetFleet)
        {
            ship.GetComponent<ShipBehaviour>().ShipParent = sender;
        }
        sender.GetComponent<PlanetFleetScript>().planetFleet.AddRange(planetFleet);
        sender.GetComponent<PlanetFleetScript>().activeShips += activeShips;
        activeShips = 0;
        string info = PlanetName + "\n Ships: " + activeShips + "\n Built: " + builtShips;
        PlanetInfo.text = info;
    }

    // fleet selected
    private void OnMouseDown()
    {
        Debug.Log("Fleet Selected");
        planetSelected = !planetSelected;
        if (planetSelected)
        {
            PlanetInfo.color = Color.red;
            PlayerPrefs.SetInt("SelectedFleet", activeShips);
            PlayerPrefs.SetString("SelectedPlanet", PlanetName);
        }
        else
            PlanetInfo.color = Color.white;
    }

}
