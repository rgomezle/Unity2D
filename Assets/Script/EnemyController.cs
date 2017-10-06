using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float velocity = 2f;
    private Rigidbody2D rbd2;

	void Start () {
        rbd2 = GetComponent<Rigidbody2D>();
        rbd2.velocity = Vector2.left * velocity;
	}
	
	void Update () {
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Destroyer"){
            Destroy(gameObject);
        }
    }

}
