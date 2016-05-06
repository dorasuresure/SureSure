using UnityEngine;
using System.Collections;

public class BossGate : MonoBehaviour {

	RectTransform rt;

	public GameObject BOSSHP;

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			BOSSHP.SetActiveRecursively (true);
		}
	}
}