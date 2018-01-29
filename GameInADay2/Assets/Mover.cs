using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    private Rigidbody2D rigid;
    public float speed;
    public float maxY;
    public float minY;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        rigid.velocity = transform.up * speed;

        // hit boundary
        if (transform.position.y > maxY || transform.position.y < minY)
        {
            transform.position = new Vector3(0,0,0);
            gameObject.SetActive(false);
        }
	}
}
