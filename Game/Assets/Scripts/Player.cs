using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	[Header("Shooting")]
	[System.NonSerialized] public bool isCanShoot = true;
	[SerializeField] float bulletSpeed = 5.0f;
	[SerializeField] Transform bulletSpawnPos;
	[SerializeField] GameObject bulletPrefab;
	[SerializeField] Vector3 minScale = new Vector3(0.2f, 0.2f, 0.2f);

	GameObject bullet;

	public float GetPathWidth() {
		return transform.localScale.x + 0.3f;
	}

	public float GetBulletWidth() {
		return bullet?.transform?.localScale.x ?? 0;
	}

	public void InitShoot() {
		bullet = Instantiate(bulletPrefab, bulletSpawnPos.position, Quaternion.identity, null);
		bullet.transform.localScale = minScale;
		transform.localScale -= minScale;
	}

	public void OnHold(Vector3 pos, float holdTime) {
		Vector3 scaleChange = Vector3.one * holdTime / 100f;
		bullet.transform.localScale += scaleChange;
		transform.localScale -= scaleChange;

		if (transform.localScale.x < minScale.x) {
			GameManager.instance.OnLose();
		}
	}

	public void ShootTo(Vector3 pos, float holdTime) {
		Rigidbody rb = bullet?.GetComponent<Rigidbody>();
		if (rb == null)
			return;
		Vector3 velocity = (pos - rb.transform.position).normalized;
		velocity.y = 0.0f;
		rb.velocity = velocity.normalized * bulletSpeed;
		bullet = null;
	}
}
