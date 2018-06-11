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
	}

    private void Run ()
	{
		float h = CrossPlatformInputManager.GetAxis ("Horizontal");
		Vector2 playerVelocity = new Vector2 (h * runSpeed, myRigibody.velocity.y);
		myRigibody.velocity = playerVelocity;
	}
}
