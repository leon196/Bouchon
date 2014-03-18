using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private bool modeEdit = false;
	private bool canControl = true;
	private bool fireOn = false;
	private float speedReactor = 4.0f;
	private RaycastHit ray;
	private GameObject prefabReactor;
	private Reactor[] reactors;

	// Use this for initialization
	void Start () {
		prefabReactor = Instantiate(Resources.Load("Reactor")) as GameObject;	
		reactors = gameObject.GetComponentsInChildren<Reactor>() as Reactor[];
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P)) {
			modeEdit = !modeEdit;
			if (!modeEdit) {
				reactors = gameObject.GetComponentsInChildren<Reactor>() as Reactor[];
			}
			rigidbody.isKinematic = modeEdit;
			rigidbody.WakeUp();
		}
		if (modeEdit) {
			Ray cameraMouse = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast(cameraMouse.origin, cameraMouse.direction, out ray, 1024.0f)) {
				prefabReactor.transform.position = ray.point;
				if (Input.GetButtonDown ("Fire1")) {
					prefabReactor.transform.parent = transform;
					prefabReactor = Instantiate(Resources.Load("Reactor")) as GameObject;	
				}
			}
		} else {
			if (canControl) {


				if (Input.GetKey(KeyCode.T)) {
					Reactor reactor = reactors[0];
					rigidbody.AddForceAtPosition(transform.up * speedReactor, reactor.transform.position);
				}


				if (Input.GetKey(KeyCode.F)) {
					
					Reactor reactor = reactors[1];
					rigidbody.AddForceAtPosition(transform.up * speedReactor, reactor.transform.position);
				}


				if (Input.GetKey(KeyCode.H)) {
					
					Reactor reactor = reactors[2];
					rigidbody.AddForceAtPosition(transform.up * speedReactor, reactor.transform.position);
				}


				if (Input.GetKey(KeyCode.B)) {
					
					Reactor reactor = reactors[3];
					rigidbody.AddForceAtPosition(transform.up * speedReactor, reactor.transform.position);
				}

				if (Input.GetButton ("Jump")) {
					//transform.rotation = Quaternion.Slerp(transform.rotation, Camera.main.transform.rotation, Time.deltaTime);
					rigidbody.AddForce(transform.up * 12);
					/*
					foreach (Reactor reactor in reactors) {
						rigidbody.AddForceAtPosition(transform.up * 12, reactor.transform.position);
					}
					*/
					if (fireOn == false) {
						fireOn = true;
						foreach (Reactor reactor in reactors) {
							ParticleEmitter[] emitters = reactor.GetComponentsInChildren<ParticleEmitter>() as ParticleEmitter[];
							foreach (ParticleEmitter emitter in emitters) {
								emitter.emit = true;
							}
						}
					}
				} else {
					if (fireOn == true) {
						fireOn = false;
						foreach (Reactor reactor in reactors) {
							ParticleEmitter[] emitters = reactor.GetComponentsInChildren<ParticleEmitter>() as ParticleEmitter[];
							foreach (ParticleEmitter emitter in emitters) {
								emitter.emit = false;
							}
						}
					}
				}
			}
		}
	}
	
	public void ResetAt(Vector3 position) {
		if 	(GetComponent<Rigidbody>() == null) {
			gameObject.AddComponent<Rigidbody>();
		}
		rigidbody.AddForce(Vector3.zero, ForceMode.VelocityChange);
		rigidbody.isKinematic = true;
		transform.rotation = Quaternion.identity;
		transform.position = position;
		rigidbody.isKinematic = false;
		canControl = true;
	}
	
	void OnCollisionEnter(Collision other) {
		GameObject collided = other.gameObject;
		if (collided.GetComponent<Goulot>()) {
			CollisionWithGoulot(collided);
		}
	}

	void CollisionWithGoulot(GameObject goulot) {

		goulot.GetComponent<ParticleEmitter>().emit = true;
		rigidbody.isKinematic = true;
		transform.position = goulot.transform.position;
		transform.rotation = Quaternion.identity;
		canControl = false;
		
		this.Destroy(rigidbody);
	}
}
