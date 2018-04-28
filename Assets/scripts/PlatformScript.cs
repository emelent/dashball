using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {

	public string platformSound = "Platform";
	public Color touchColor;
	SpriteRenderer spriteRenderer;
	bool touched = false;

	void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.collider.tag == "Player"){
			Color color = collision.collider.GetComponent<SpriteRenderer>().color;
			if(spriteRenderer.color != color){
				
				GM.PlaySound(platformSound);
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
