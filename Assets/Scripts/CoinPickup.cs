using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {

	[SerializeField] AudioClip coinPickupSFX;
	[SerializeField] int pointsForCoinPickup = 100;

	void OnTriggerEnter2D (Collider2D collider) {
		AudioSource.PlayClipAtPoint (coinPickupSFX, Camera.main.transform.position);
		FindObjectOfType<GameSession> ().AddToScore (pointsForCoinPickup);
		Destroy (gameObject);
	}
}
