using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {
	[SerializeField] GameObject explodePrefab;
	bool isExplode = false;

	private void OnDestroy() {
		if (isExplode)
			return;
		isExplode = true;
		GameObject explode = Instantiate(explodePrefab, transform.position, Quaternion.identity, null);
		explode.transform.localScale = transform.localScale;
		LeanTween.delayedCall(explode, 0.75f, () => {
			explode.GetComponent<ParticleSystem>().Stop();
			LeanTween.delayedCall(explode, 5.0f, () => {
				Destroy(explode);
			});
		});
	}
}
