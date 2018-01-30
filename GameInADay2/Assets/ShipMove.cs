using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMove : MonoBehaviour {

    private Rigidbody2D rigid;
    public float speed;
    public AudioSource source;
    public AudioClip clip;
    public float volLowRange;
    public float volHighRange;

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
        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(clip, vol);
        if (source.isPlaying)
            Debug.Log("Explosion Sound");

        int score = PlayerPrefs.GetInt("Score");
        score++;
        PlayerPrefs.SetInt("Score", score);

        collision.gameObject.SetActive(false);
        gameObject.SetActive(false);

       
    }
}
