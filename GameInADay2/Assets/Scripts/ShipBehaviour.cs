using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBehaviour : MonoBehaviour {

    public int ShipHealth = 10;
    public int ShipDamage = 1;
    public float ShipSpeed = 10.0f;
    public float AttackTimer;

    public float OrbitRadius;
    public float OrbitRadiusSpeed;
    public float OrbitSpeed;
    public GameObject ShipParent;
    public int OwnerNumber;

    bool enemyDetected;
    List<GameObject> enemiesInRange = new List<GameObject>();
    float attackWait;
    public GameObject LaserSprite;
    GameObject laserTarget;
	// Use this for initialization
	void Start () {
        attackWait = AttackTimer;
	}
	
	// Update is called once per frame
	void Update () {
        if(ShipParent)
        {

            var orbitPosition = (transform.position - ShipParent.transform.position).normalized * OrbitRadius + ShipParent.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, orbitPosition, OrbitRadiusSpeed * Time.deltaTime);

            if(transform.position == orbitPosition)
            {
                // orbit around planet
                transform.RotateAround(ShipParent.transform.position, new Vector3(0, 0, 1), OrbitSpeed * Time.deltaTime);

                // look for enemies in orbit
                if (!enemyDetected)
                {
                    LaserSprite.SetActive(false);
                    // check owner numbers of all ships in orbit
                    foreach (GameObject ship in ShipParent.GetComponent<PlanetFleetScript>().planetFleet)
                    {
                        if (ship.GetComponent<ShipBehaviour>().OwnerNumber != OwnerNumber)
                        {
                            enemiesInRange.Add(ship);
                            enemyDetected = true;
                        }
                    }
                }

                // attack enemy ships
                if (enemyDetected)
                {
                    attackWait -= Time.deltaTime;
                    if (attackWait <= 0.0f)
                    {
                        attackWait = AttackTimer;
                        int enemy = Random.Range(0, enemiesInRange.Count);
                        enemiesInRange[enemy].GetComponent<ShipBehaviour>().ShipHealth -= ShipDamage;

                        transform.LookAt(enemiesInRange[enemy].transform);
                        LaserSprite.SetActive(true);
                        LaserSprite.transform.position = transform.position;
                        laserTarget = enemiesInRange[enemy];
                        LaserSprite.transform.LookAt(laserTarget.transform);
                    }
                    if(LaserSprite.activeInHierarchy)
                    {
                        LaserSprite.transform.position = Vector3.MoveTowards(transform.position, orbitPosition, ShipSpeed * Time.deltaTime);
                    }
                }

                // destroy and return to pool
                if (ShipHealth <= 0)
                {
                    gameObject.SetActive(false);
                    ShipParent.GetComponent<PlanetFleetScript>().planetFleet.Remove(gameObject);
                    ShipParent.GetComponent<PlanetFleetScript>().activeShips--;
                }

                // orbiting planet belongs to enemy and no enemies detected in orbit
                if (ShipParent.GetComponent<PlanetFleetScript>().OwnerNumber != OwnerNumber && !enemyDetected)
                {
                    // transfer control of planet to player
                    ShipParent.GetComponent<PlanetFleetScript>().OwnerNumber = OwnerNumber;
                }
            }
        }
	}

    
}
