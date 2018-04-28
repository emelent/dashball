using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BasicBit
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class PlatformerMovement2D: MonoBehaviour{

		[Header("Platformer Movement")]
		public bool  allowMovement = true;
		public int maxJumps = 1;
		public float jumpForce = 10f;
		public float yThreshold = 0.49f;
		public LayerMask whatIsGround;


		[SerializeField]
		protected bool grounded = false;
		[SerializeField]
		protected int jumpCount = 0;

		[SerializeField]
		protected float motion = 0f;

		protected Rigidbody2D rb;

		void Awake() {
			onAwake();
		}

		void Update() {
			onUpdate();
		}

		void OnCollisionEnter2D(Collision2D collision){
			groundCheck(collision);
		}

		void FixedUpdate(){
			handleMovement();
		}

		

		protected void onAwake(){
			rb =  GetComponent<Rigidbody2D>();
		}
		protected void onUpdate(){
			if(grounded){
				jumpCount = 0;
			}
		}
		protected virtual void handleMovement(){}
		protected virtual void onJump(){}

		protected virtual void groundCheck(Collision2D collision){
			if(rb.IsTouchingLayers(whatIsGround)){
				for(int i=0; i < collision.contacts.Length; i++){
					if(collision.contacts[i].normal.y > yThreshold){
						grounded = true;
					}
				}
			}
		}

		public virtual void Jump(){
			if(jumpCount >= maxJumps) return;

			Vector2 vel = rb.velocity;
			vel.y = jumpForce;
			rb.velocity  = vel;
			++jumpCount;
			grounded = false;
			onJump();
		}

		public void SetJumpCount(int j){
			jumpCount = Mathf.Clamp(j, 0, maxJumps);
		}

		public void Move(float move){
			motion = Mathf.Clamp(move, -1f, 1f);
		}

		public bool isMoving(){
			return allowMovement && motion != 0;
		}
		public bool isGrounded(){
			return grounded;
		}
	}
}