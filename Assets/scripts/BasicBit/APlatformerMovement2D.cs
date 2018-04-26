using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BasicBit
{
	//Acceleration-based Platformer Movement
	public class APlatformerMovement2D: PlatformerMovement2D {

		public float acceleration = 800f;
		public float speedLimit = 20f;

		protected override void handleMovement(){
			if(!isMoving()) return;

			// never go over the speed limit
			float x = rb.velocity.x;
			bool opposingMomentum =!(x < 0 && motion < 0 || x > 0 && motion > 0);
			if(Mathf.Abs(x) >= speedLimit && !opposingMomentum) return;

			// accelerate
			rb.AddForce(Vector2.right * motion * acceleration * Time.deltaTime);
		}
	}
	
}
