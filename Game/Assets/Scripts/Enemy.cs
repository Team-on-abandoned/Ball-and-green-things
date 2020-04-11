using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[Header("Random dance anim")]
	[SerializeField] byte danceAnimationsCount = 7;
	Animator anim;

	[Header("Random mesh")]
	[SerializeField] GameObject capsule;
	[SerializeField] GameObject[] meshPrefab;

	private void Awake() {
		Destroy(capsule.gameObject);

		byte id = (byte)Random.Range(0, meshPrefab.Length);
		GameObject spawnedMesh = Instantiate(meshPrefab[id], new Vector3(transform.position.x, 0, transform.position.z), transform.rotation, transform);

		anim = spawnedMesh.GetComponent<Animator>();
	}

	void Start() {
		anim.SetInteger("RandomDanceAnim", Random.Range(0, danceAnimationsCount));
	}

	void Update() {

	}
}
