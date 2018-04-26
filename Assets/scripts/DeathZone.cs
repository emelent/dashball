using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour {

	public bool visible = true;

	void Awake(){
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		if(sr){
			sr.enabled = visible;
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		if(collider.tag == "Player"){
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.collider.tag == "Player"){
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

}
