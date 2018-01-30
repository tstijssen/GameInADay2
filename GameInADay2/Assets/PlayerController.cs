using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;

}


public class PlayerController : MonoBehaviour {

    private Rigidbody2D rigid;
    public GameObject controller;
    public float PlayerSpeed;

    public Boundary boundary;
    public AudioSource source;
    public AudioClip clip;
    public float volLowRange;
    public float volHighRange;
    public GameObject shotSpawn;
    public GameObject shot;
    public float timer;
    ObjectPooler pool;

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody2D>();
        pool = GetComponent<ObjectPooler>();
	}

    void Update()
    {
        timer -= Time.deltaTime;
        if(Input.GetMouseButton(0) && timer <= 0.0f)
        {
            timer = 0.5f;
            GameObject shotClone = pool.GetPooledObject();
            shotClone.SetActive(true);
            shotClone.transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);
            shotClone.transform.rotation = transform.rotation;
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(clip, vol);
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rigid.velocity = movement * PlayerSpeed;

        rigid.position = new Vector2
            (
                Mathf.Clamp(rigid.position.x, boundary.xMin, boundary.xMax),
                Mathf.Clamp(rigid.position.y, boundary.yMin, boundary.yMax)
            );
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
            controller.GetComponent<Controller>().GameOver();
    }
}
