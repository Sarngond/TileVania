using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {
	[SerializeField] float runSpeed = 5f;

	private Rigidbody2D myRigibody;

	// Use this for initialization
	void Start () {
		myRigibody = GetComponent<Rigidbody2D> ();
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
	}

	private void FlipSprite () {
		bool playerHasHorizontalSpeed = Mathf.Abs (myRigibody.velocity.x) > Mathf.Epsilon;
		if (playerHasHorizontalSpeed) {
			transform.localScale = new Vector2 (Mathf.Sign(myRigibody.velocity.x) * 3, 3);
		}
	}
}
