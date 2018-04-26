using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public int movementSpeed = 300;
	public int jumpSpeed = 6;
	public LayerMask whatIsGround;

	const float GROUND_CHECK_RADIUS = 0.2f;

	private int mInputH;
	private bool mDoJump = false;
	private bool mMoving = false;
	private bool mJumping = false;
	private bool mGrounded = false;

	private Vector2 mVel = new Vector2();

	// refs
	private Rigidbody2D mRb;
	private Transform mGroundCheck;


	void Awake () {
		mRb = GetComponent<Rigidbody2D> ();
		mGroundCheck = transform.Find("GroundCheck");
	}

	void Update () {
		handleInput ();
	}

	void  FixedUpdate(){
		handleMovement ();
		groundCheck();
	}
		
	void OnCollisionEnter2D(Collision2D collision){
		if(mRb.velocity.y == 0){
			mGrounded = true;
		}
	}

	void handleMovement(){
		mVel.y = mRb.velocity.y;
		mVel.x =  movementSpeed * mInputH * Time.deltaTime;
		// jump
		if (mDoJump) {
			mVel.y = jumpSpeed;
			mDoJump = false;
			mJumping = true;
		}

		// update velocity
		mRb.velocity = mVel;
	}
		
	void groundCheck(){
		Collider2D[] colliders = Physics2D.OverlapCircleAll(mGroundCheck.position, GROUND_CHECK_RADIUS, whatIsGround);
		mGrounded = false;
		for(int i=0; i < colliders.Length; i++){
			if(colliders[i].gameObject != gameObject){
				mGrounded = true;
				if(Mathf.Abs(mRb.velocity.y)  <= 1f &&  mJumping){
					print( Time.time + ": Not jumping");
					mJumping = false;
				}
				break;
			}
		}
	}

	void handleInput(){
		mInputH = (int) Input.GetAxisRaw ("Horizontal");
		mMoving = mInputH != 0;
		mDoJump = (Input.GetAxisRaw ("Vertical") == 1f && mGrounded && Mathf.Abs(mVel.y) <= 0.1f);

		// flip x scale if moving left
		float xScale = (mInputH != 0)? Mathf.Abs(transform.localScale.x) * mInputH: transform.localScale.x;
		transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
	}
}
