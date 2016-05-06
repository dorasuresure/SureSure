using UnityEngine;
using System.Collections;

public class DBblock : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == "fire"){
			Destroy(gameObject);
		}
	}
}
