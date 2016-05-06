using UnityEngine;
using System.Collections;

public class MPItemScript : MonoBehaviour {
	public MpScript MpScript;

	void OnCollisionEnter2D (Collision2D col){
		if (col.gameObject.tag == "Player") {
			MpScript.MpStock (65);
			Destroy (gameObject);
		}
	}
}
