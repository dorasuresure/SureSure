using UnityEngine;
using System.Collections;

public class PBblock : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == "mahou"){
			Destroy(gameObject);
		}
	}
}
