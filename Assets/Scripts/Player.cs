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
	Rigidbody2D myRigibody;
	Animator myAnimator;
	Collider2D myCollider;

	// Messages then methods
	void Start () {
		myRigibody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
		myCollider = GetComponent<Collider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		Run ();
		Jump ();
		ClimbLadder ();
		FlipSprite ();
	}

    private void Run ()
	{
		float h = CrossPlatformInputManager.GetAxis ("Horizontal");
		Vector2 playerVelocity = new Vector2 (h * runSpeed, myRigibody.velocity.y);
		myRigibody.velocity = playerVelocity;

		bool playerHasHorizontalSpeed = Mathf.Abs (myRigibody.velocity.x) > Mathf.Epsilon;
		myAnimator.SetBool ("isRunning", playerHasHorizontalSpeed);
	}

	private void Jump () {
		bool canJump = myCollider.IsTouchingLayers (LayerMask.GetMask ("Ground"));

		if (CrossPlatformInputManager.GetButtonDown ("Jump") && canJump) {
			Vector2 jumpVelocityToAdd = new Vector2 (0f, jumpSpeed);
			myRigibody.velocity += jumpVelocityToAdd;
		}
	}

	private void ClimbLadder() {
		bool isTouchingLadder = myCollider.IsTouchingLayers (LayerMask.GetMask ("Climbing"));

		if (isTouchingLadder) {
			float v = CrossPlatformInputManager.GetAxis ("Vertical");
			Vector2 climbVelocity = new Vector2 (myRigibody.velocity.x, v * climbSpeed);
			myRigibody.velocity = climbVelocity;
			myRigibody.gravityScale = 0;

			bool playerHasVerticalSpeed = Mathf.Abs (myRigibody.velocity.y) > Mathf.Epsilon;
			myAnimator.SetBool ("isClimbing", playerHasVerticalSpeed);
		} else {
			myRigibody.gravityScale = 1;
			myAnimator.SetBool ("isClimbing", false);
		}
	}

	private void FlipSprite () {
		bool playerHasHorizontalSpeed = Mathf.Abs (myRigibody.velocity.x) > Mathf.Epsilon;
		if (playerHasHorizontalSpeed) {
			transform.localScale = new Vector2 (Mathf.Sign(myRigibody.velocity.x) * 3, 3);
		}
	}
}
