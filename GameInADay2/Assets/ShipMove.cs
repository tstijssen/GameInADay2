using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMove : MonoBehaviour {

    private Rigidbody2D rigid;
    public float speed;

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = -transform.up * speed;

        // hit boundary
        if (transform.position.y < -5)
        {
            transform.position = new Vector3(0, 0, 0);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
