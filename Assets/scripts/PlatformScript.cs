using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {

	public Color touchColor;
	SpriteRenderer spriteRenderer;
	AudioSource audioSrc;
	bool touched = false;

	void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer>();
		audioSrc = GetComponent<AudioSource>();
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.collider.tag == "Player"){
			Color color = collision.collider.GetComponent<SpriteRenderer>().color;
			if(spriteRenderer.color != color){
				audioSrc.Play();
				spriteRenderer.color = color;
			}
			if(!touched){
				GM.DecrementBlocks();
				touched = true;
			}
		}
	}

	void OnCollisionExit2D(Collision2D collision){
		if(collision.collider.tag == "Player"){
			if(spriteRenderer.color == touchColor){
				Destroy(gameObject);
			}
		}
	}
}
