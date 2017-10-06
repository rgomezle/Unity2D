using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState {Parado, Jugando, Ended, Ready};

public class GameController : MonoBehaviour {

	[Range (0f, 0.20f)]
	public float parallaxSpeed = 0.02f;
	public RawImage background;
	public RawImage platform;
	public GameObject uiParado;
	public GameObject player;
    public GameObject enemyGenerator;
	public GameState gameState = GameState.Parado;
    private AudioSource musicPLayer;
    public float scaleTime = 6f;
    public float scaleInc = .25f;




    //Juego Inicia
    void Start() {
        musicPLayer = GetComponent<AudioSource>();
	}

    //Juego se Actualiza
	void Update () {

        bool userAction = Input.GetKeyDown("up") || Input.GetMouseButtonDown(0);

    
        if (gameState == GameState.Parado && userAction) {
			gameState = GameState.Jugando;
			uiParado.SetActive (false);
			player.SendMessage("UpdateState", "PlayerRun");
            player.SendMessage("DustPlay");
            enemyGenerator.SendMessage("StartGenerator");
            musicPLayer.Play();
            InvokeRepeating("GameTimeScale", scaleTime, scaleTime);


		} else if (gameState == GameState.Jugando) {
			Parallax ();
		}

    //Juego preparado para reiniciarse
        else if (gameState == GameState.Ready) {

            if(userAction){
                RestartGame();
            }
        }
    }

    //Animación de Fondo y Profundidad
	void Parallax(){
	
		float finalSpeed = parallaxSpeed * Time.deltaTime;
		background.uvRect = new Rect (background.uvRect.x + finalSpeed * 2, 0f, 1f, 1f);
		platform.uvRect = new Rect (platform.uvRect.x + finalSpeed * 8, 0f, 1f, 1f);
	}

    //Post muerte se reinicia
    public void RestartGame(){

        SceneManager.LoadScene("game");
    }

    void GameTimeScale(){

        Time.timeScale += scaleInc;
        Debug.Log("Ritmo aumentado "+Time.timeScale.ToString());
    }

    public void ResetTimeScale(){

        CancelInvoke("GameTimeScale");
        Time.timeScale = 1f;
        Debug.Log("Ritmo restablecido " + Time.timeScale.ToString());

    }

}
