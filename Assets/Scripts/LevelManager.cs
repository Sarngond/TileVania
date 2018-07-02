using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadLevel (string levelName) {
		SceneManager.LoadScene (levelName);
	}

	public void Quit () {
		Application.Quit ();
		print ("Quit Requested");
	}
}
