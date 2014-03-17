using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	
	public Player player;
	public Transform playerStart;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R)) {
			player.ResetAt(playerStart.position);
		}
	}
}
