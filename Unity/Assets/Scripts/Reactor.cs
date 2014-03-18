using UnityEngine;
using System.Collections;

public class Reactor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void Activate () {
		ParticleEmitter[] emitters = GetComponentsInChildren<ParticleEmitter>() as ParticleEmitter[];
		foreach (ParticleEmitter emitter in emitters) {
			emitter.emit = true;
		}
	}
}
