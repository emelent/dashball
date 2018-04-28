using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour {

	public float waterFriction = 0.8f;
	void OnTriggerEnter2D(Collider2D collider){
		if(collider.tag == "Player"){

			collider.GetComponent<PlayerDash>()
				.waterFriction = waterFriction;
				
			collider.GetComponent<PlayerDash>()
				.inWater = true;
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		if(collider.tag == "Player"){
			collider.GetComponent<PlayerDash>()
				.inWater = false;
			
		}
	}
}
