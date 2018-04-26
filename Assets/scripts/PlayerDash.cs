using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDash : BasicBit.VPlatformerMovement2D {


	[Header("Dash")]
	public float dashSpeed =  100f;
	public float dashDuration = 0.4f;
	public float dashShakeAmount = 0.5f;
	public float dashShakeDuration = 0.1f;
	public int maxDashes = 1;
	public Color dashColor;
	public Gradient dashGradient;
	public float waterFriction = 1f;
	public bool inWater =false;
	[SerializeField]
	Vector2 vel;
	bool dashing = false;
	float dashTime = 0f;
	float verticalMotion = 0f;
	
	[SerializeField]
	int dashCount = 0;

	Vector2  dashDir;
	ParticleSystem dashParticles;
	Color defaultColor;
	Gradient trailGradient;
	SpriteRenderer spriteRenderer;
	TrailRenderer trail;
	AudioSource dashSound;
	AudioSource jumpSound;

	void Awake() {
		onAwake();
		dashParticles = transform.Find("DashParticles").GetComponent<ParticleSystem>();	
		trail =transform.Find("Trail").GetComponent<TrailRenderer>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		defaultColor = spriteRenderer.color;
		trailGradient = trail.colorGradient;
		trail.colorGradient  = dashGradient;
		spriteRenderer.color = dashColor;
		dashSound = transform.Find("DashSound").GetComponent<AudioSource>();
		jumpSound = transform.Find("JumpSound").GetComponent<AudioSource>();
	}
	
	void Update() {
		Move(Input.GetAxisRaw("Horizontal"));
		MoveInWater(Input.GetAxisRaw("Vertical"));
		if(Input.GetKeyDown(KeyCode.X)){
			dash();
		}else if(Input.GetKeyDown(KeyCode.Z)){
			Jump();
		}
	
		if(grounded){
			jumpCount = 0;
			dashCount = 0;

			spriteRenderer.color = dashColor;
			trail.colorGradient = dashGradient;
		}
		if(Input.GetKeyDown(KeyCode.Backspace)){
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	void FixedUpdate(){
		handleDash();
		handleMovement();
		handleInWaterMovement();
		vel = rb.velocity;
	}

	void dash(){
		if(dashCount >= maxDashes) return;
		float  inputH = Input.GetAxisRaw("Horizontal");
		float  inputV = Input.GetAxisRaw("Vertical");
		if(inputV == 0 && inputH == 0) return;

		spriteRenderer.color = defaultColor;
		trail.colorGradient = trailGradient;
		dashParticles.Play();
		dashSound.Play();
		dashing = true;
		dashDir = new Vector2(inputH, inputV);
		dashTime = Time.time + dashDuration;
		++dashCount;
		allowMovement = false;
	}

	protected override void onJump(){
		jumpSound.Play();
	}
	void handleDash(){
		if(!dashing) return;
		if(Time.time > dashTime &&  dashing){
			rb.velocity = Vector2.zero;
			dashing = false;
			allowMovement = true;
			spriteRenderer.color = defaultColor;
			trail.colorGradient = trailGradient;
			GM.ShakeCamera(dashShakeAmount, dashShakeDuration);
		}else if(dashing){
			dashParticles.Stop();
			rb.velocity = dashSpeed * dashDir * Time.deltaTime;
		}
	}

	public void MoveInWater(float move){
		verticalMotion = Mathf.Clamp(move, -1f, 1f);
	}

	public void handleInWaterMovement(){
		if(!allowMovement || verticalMotion == 0f || !inWater) return;

		Vector2 vel= rb.velocity;
		vel.y = movementSpeed  * verticalMotion * Time.deltaTime * waterFriction;
		rb.velocity = vel;
	}

	public void Reset(){
		rb.Sleep();
		dashCount = 0;
		jumpCount = 0;
		allowMovement = true;
	}
}
