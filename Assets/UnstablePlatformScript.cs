using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstablePlatformScript : MonoBehaviour {

	public string platformSound = "Platform";
	public float activeGravityScale  = 0f;
	public Color touchColor = Color.white;
	SpriteRenderer spriteRenderer;
	Rigidbody2D rb;
	bool touched = false;

	void Awake(){
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.collider.tag == "Player"){
			rb.gravityScale = activeGravityScale;
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
