using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Animator animator;
    public GameObject game;
    public GameObject enemyGenerator;

	
	void Start () {

		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
        bool gameJugando = game.GetComponent<GameController>().gameState == GameState.Jugando;
		if(gameJugando && (Input.GetKeyDown ("up") || Input.GetMouseButtonDown (0))){
			UpdateState ("PlayerSalta");
		}
	}

	public void UpdateState(string state = null){

		if(state != null){
		
			animator.Play(state);		
		}
			
	}


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            UpdateState("PlayerDie");
            game.GetComponent<GameController>().gameState = GameState.Ended;
            enemyGenerator.SendMessage("CancelGenerator", true);

        }

    }

}
