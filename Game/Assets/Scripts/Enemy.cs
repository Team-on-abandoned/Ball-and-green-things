using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[Header("Random dance anim")]
	[SerializeField] byte danceAnimationsCount = 7;
	Animator anim = null;

	[Header("Random mesh")]
	[SerializeField] GameObject capsule = null;
	[SerializeField] GameObject[] meshPrefab = null;

	private void Awake() {
		Destroy(capsule.gameObject);

		byte id = (byte)Random.Range(0, meshPrefab.Length);
		GameObject spawnedMesh = Instantiate(meshPrefab[id], new Vector3(transform.position.x, 0.89f, transform.position.z), transform.rotation, transform);
		anim = spawnedMesh.GetComponent<Animator>();
	}

	void Start() {
		if(Random.Range(0, 8) == 0) {
			Vector3 pos = transform.position;
			pos.y = -0.89f;
			transform.position = pos;
			anim.SetInteger("RandomDanceAnim", Random.Range(0, danceAnimationsCount));
		}
	}

	private void OnDestroy() {
		LeanTween.cancel(gameObject, false);
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Bullet")) {
			LeanTween.delayedCall(gameObject, 0.1f, () => {
				if (other != null)
					Destroy(other.gameObject);
				Destroy(gameObject);
			});
		}
	}
}
