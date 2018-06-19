using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

	// Config
	[SerializeField] float runSpeed = 5f;
	[SerializeField] float jumpSpeed = 5f;
	[SerializeField] float climbSpeed = 5f;

	// State
	bool isAlive = true;

	// Cached component references
	Rigidbody2D myRigidbody;
	Animator myAnimator;
	CapsuleCollider2D myBodyCollider;
	BoxCollider2D myFeetCollider;
	float gravityScaleAtStart;

	// Messages then methods
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
		myBodyCollider = GetComponent<CapsuleCollider2D> ();
		myFeetCollider = GetComponent<BoxCollider2D> ();
		gravityScaleAtStart = myRigidbody.gravityScale;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isAlive) { return; }

		Run ();
		ClimbLadder ();
		Jump ();
		FlipSprite ();
		Die ();
	}

    private void Run ()
	{
		float h = CrossPlatformInputManager.GetAxis ("Horizontal");
		Vector2 playerVelocity = new Vector2 (h * runSpeed, myRigidbody.velocity.y);
		myRigidbody.velocity = playerVelocity;

		bool playerHasHorizontalSpeed = Mathf.Abs (myRigidbody.velocity.x) > Mathf.Epsilon;
		myAnimator.SetBool ("isRunning", playerHasHorizontalSpeed);
	}

	private void ClimbLadder() {
		bool isTouchingLadder = myFeetCollider.IsTouchingLayers (LayerMask.GetMask ("Climbing"));

		if (isTouchingLadder) {
			float v = CrossPlatformInputManager.GetAxis ("Vertical");
			Vector2 climbVelocity = new Vector2 (myRigidbody.velocity.x, v * climbSpeed);
			myRigidbody.velocity = climbVelocity;
			myRigidbody.gravityScale = 0;

			bool playerHasVerticalSpeed = Mathf.Abs (myRigidbody.velocity.y) > Mathf.Epsilon;
			myAnimator.SetBool ("isClimbing", playerHasVerticalSpeed);
		} else {
			myRigidbody.gravityScale = gravityScaleAtStart;
			myAnimator.SetBool ("isClimbing", false);
		}
	}

	private void Jump () {
		bool canJump = myFeetCollider.IsTouchingLayers (LayerMask.GetMask ("Ground"));

		if (CrossPlatformInputManager.GetButtonDown ("Jump") && canJump) {
			Vector2 jumpVelocityToAdd = new Vector2 (0f, jumpSpeed);
			myRigidbody.velocity += jumpVelocityToAdd;
		}
	}

	private void Die () {
		if (myBodyCollider.IsTouchingLayers (LayerMask.GetMask ("Enemy", "Hazards"))) {
			isAlive = false;
			myAnimator.SetTrigger ("isDead");
			myRigidbody.velocity = new Vector2 (2f * -transform.localScale.x, 20f);
		}
	}

	private void FlipSprite () {
		bool playerHasHorizontalSpeed = Mathf.Abs (myRigidbody.velocity.x) > Mathf.Epsilon;
		if (playerHasHorizontalSpeed) {
			transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x) * 1, 1);
		}
	}
}
