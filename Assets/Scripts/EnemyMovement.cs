using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	[SerializeField] float moveSpeed = 1f;
	Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (IsFacingRight ()) {
			myRigidbody.velocity = new Vector2 (moveSpeed, 0);
		} else {
			myRigidbody.velocity = new Vector2 (-moveSpeed, 0);
		}
	}

	private bool IsFacingRight () {
		return transform.localScale.x > 0;
	}
		
	private void OnTriggerExit2D (Collider2D collider) {
		transform.localScale = new Vector2 (-(Mathf.Sign (myRigidbody.velocity.x)), 1f);
	}
}
