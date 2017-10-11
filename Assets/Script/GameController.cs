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
    public GameObject uiScore;
    public Text pointsText;
    public Text recordText;
	public GameObject player;
    public GameObject enemyGenerator;
	public GameState gameState = GameState.Parado;
    private AudioSource musicPLayer;
    public float scaleTime = 6f;
    public float scaleInc = .25f;
    private int points = 0;



    //Juego Inicia
    void Start() {
        musicPLayer = GetComponent<AudioSource>();
        recordText.text = "BEST: " + GetMaxScore().ToString();
	}

    //Juego se Actualiza
	void Update () {

        bool userAction = Input.GetKeyDown("up") || Input.GetMouseButtonDown(0);

    
        if (gameState == GameState.Parado && userAction) {
			gameState = GameState.Jugando;
			uiParado.SetActive (false);
            uiScore.SetActive(true);

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

    public void IncreasePoints(){

        //points++;
        pointsText.text = (++points).ToString();

        if(points >= GetMaxScore())
        {
            recordText.text = "BEST: " + points.ToString();
            SaveScore(points);
        }

    }

    public int GetMaxScore(){

        return PlayerPrefs.GetInt("Max Points", 0);

    }

    public void SaveScore(int currentPoints){

        PlayerPrefs.SetInt("Max Points", currentPoints);

    }



}
