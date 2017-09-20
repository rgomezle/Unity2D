using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	[Range (0f, 0.20f)]
	public float parallaxSpeed = 0.02f;
	public RawImage background;
	public RawImage platform;

	public GameObject uiParado;
	public GameObject player;

	//Enumerador, lista de opciones
	public enum GameState {Parado, Jugando};
	//Por defecto el juego está parado
	public GameState gameState = GameState.Parado;

	void Start () {
		
	}
	void Update () {

		//Detecta el inicio del Juego
		if (gameState == GameState.Parado && (Input.GetKeyDown ("up") || Input.GetMouseButtonDown (0))) {
			gameState = GameState.Jugando;
			uiParado.SetActive (false);
			player.SendMessage("UpdateState", "PlayerRun");


		} else if (gameState == GameState.Jugando) {
			Parallax ();
		}
		}

	void Parallax(){
	
		float finalSpeed = parallaxSpeed * Time.deltaTime;
		background.uvRect = new Rect (background.uvRect.x + finalSpeed * 2, 0f, 1f, 1f);
		platform.uvRect = new Rect (platform.uvRect.x + finalSpeed * 8, 0f, 1f, 1f);
	
	}

}
