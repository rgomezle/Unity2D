using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {

    public GameObject enemyPrefab;
    public float generatorTimer = 2f;

	void Start () {   
	}
	void Update () {
	}

    void CreateEnemy(){
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    public void StartGenerator(){
        InvokeRepeating("CreateEnemy", 0F, generatorTimer);
    }

    public void CancelGenerator(bool clean = false){
        CancelInvoke("CreateEnemy");

        if (clean){

            Object[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in allEnemies){
                Destroy(enemy);
            }
        }
    }


}
