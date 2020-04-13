using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLine : MonoBehaviour {
	public int EnemiesCount => enemiesInRange.Count;
	public List<Enemy> enemiesInRange = new List<Enemy>();

	[SerializeField] LineRenderer lr;
	[SerializeField] BoxCollider bx;

	public void SetWidth(float w) {
		bx.size = new Vector3(bx.size.x, w, bx.size.z);
		lr.startWidth = lr.endWidth = w;
	}

	private void OnTriggerEnter(Collider other) {
		Enemy enemy = other.GetComponent<Enemy>();
		if (enemy != null && !enemiesInRange.Contains(enemy)) {
			enemiesInRange.Add(enemy);
		}
	}

	private void OnTriggerExit(Collider other) {
		Enemy enemy = other.GetComponent<Enemy>();
		if(enemy != null && enemiesInRange.Contains(enemy)) {
			enemiesInRange.Remove(enemy);
		}
	}
}
