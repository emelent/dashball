using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GM : MonoBehaviour {

	public static GM instance;
	public int numLevels = 2;
	public float nextLevelDelay = 1f;
	CameraShake cameraShake;
	
	[SerializeField]
	int numBlocks;
	// Use this for initialization
	void Awake () {
		if(instance == null){
			instance = this;
		}
		cameraShake = Camera.main.GetComponent<CameraShake>();
		instance.numBlocks = GameObject.FindGameObjectsWithTag("Platform").Length;
	}

	IEnumerator goToNextLevel(){
		yield return new WaitForSeconds(nextLevelDelay);
		int  nextLevel =  SceneManager.GetActiveScene().buildIndex + 1;
		nextLevel = (nextLevel == numLevels)? 0: nextLevel;
		SceneManager.LoadScene(nextLevel);
	}
	
	public static void DecrementBlocks(){
		instance.numBlocks--;
		if(instance.numBlocks  ==  0){
			instance.StartCoroutine(instance.goToNextLevel());
		}
	}

	public static void ShakeCamera(float amount, float duration){
		instance.cameraShake.Shake(amount, duration);
	}
}
