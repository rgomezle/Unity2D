using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Animator animator;
    public GameObject game;
    public GameObject enemyGenerator;
    private AudioSource audioPlayer;
    public AudioClip jumpClip;
    public AudioClip dieClip;
    public AudioClip pointClip;
    private float startY;
    public ParticleSystem dust;


	//Player inicia con animación de pie
	void Start (){

		animator = GetComponent<Animator> ();
        audioPlayer = GetComponent<AudioSource>();
        startY = transform.position.y;
    }
	
    //Se actuaiza animación a caminar
	void Update () {

        bool isGrounded = transform.position.y == startY;
        bool gameJugando = game.GetComponent<GameController>().gameState == GameState.Jugando;
        bool userAction = Input.GetKeyDown("up") || Input.GetMouseButtonDown(0);


        if (isGrounded && gameJugando && userAction){
            UpdateState ("PlayerSalta");
            //Comprueba que el jugador no esté saltando
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerSalta")){
                audioPlayer.clip = jumpClip;
                audioPlayer.Play();
            }
		}
	}

    //Se actualiza estado del jugador
	public void UpdateState(string state = null){

		if(state != null){
		
			animator.Play(state);		
		}
	}

    //Triger de colisión con el enemigo
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy"){

            UpdateState("PlayerDie");
            game.GetComponent<GameController>().gameState = GameState.Ended;
            enemyGenerator.SendMessage("CancelGenerator", true);
            game.SendMessage("ResetTimeScale");

            //Audio
            game.GetComponent<AudioSource>().Stop();
            audioPlayer.clip = dieClip;
            audioPlayer.Play();

            DustStop();

        } else if (other.gameObject.tag == "Points"){
            game.SendMessage("IncreasePoints");
            
            audioPlayer.clip = pointClip;
            audioPlayer.Play();

        }
    }

    void GameReady(){

        game.GetComponent<GameController>().gameState = GameState.Ready;
    }

    void DustPlay(){

        dust.Play();
    }

    void DustStop(){
        dust.Stop();
    }



}
