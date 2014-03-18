using UnityEngine;
using System.Collections;

public class Reactor : MonoBehaviour {

	public bool fireOn = false;
	private ParticleEmitter[] emitters;

	// Use this for initialization
	void Start () {
		fireOn = false;
		emitters = GetComponentsInChildren<ParticleEmitter>() as ParticleEmitter[];
	}

	public void Activate () { 
		if (fireOn == false) {
			fireOn = true;
			foreach (ParticleEmitter emitter in emitters) {
				emitter.emit = true;
			}
		}
	}
	public void Deactivate () {
		if (fireOn == true) {
			fireOn = false;
			foreach (ParticleEmitter emitter in emitters) {
				emitter.emit = false;
			}
		}
	}
}
