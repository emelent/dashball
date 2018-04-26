using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BasicBit
{
	//Velocity-based Platformer Movement
	public class VPlatformerMovement2D : PlatformerMovement2D {

		public float movementSpeed = 200f;

		protected override void handleMovement(){
			if(!allowMovement) return;

			Vector2 vel =  rb.velocity;
			vel.x = movementSpeed *  motion * Time.deltaTime;
			rb.velocity = vel;
		}
	}
}

