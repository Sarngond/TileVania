using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

	// Config
	[SerializeField] float runSpeed = 5f;

	// State
	bool isAlive = true;

	// Cached component references
	Rigidbody2D myRigibody;
	Animator myAnimator;

	// Messages then methods
	void Start () {
		myRigibody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		Run ();
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

	private void FlipSprite () {
		bool playerHasHorizontalSpeed = Mathf.Abs (myRigibody.velocity.x) > Mathf.Epsilon;
		if (playerHasHorizontalSpeed) {
			transform.localScale = new Vector2 (Mathf.Sign(myRigibody.velocity.x) * 3, 3);
		}
	}
}
