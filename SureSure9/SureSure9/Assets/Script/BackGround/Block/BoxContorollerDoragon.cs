using UnityEngine;
using System.Collections;

public class BoxContorollerDoragon : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == "Doragon"){
			collider2D.enabled = false;
		}
	}
	void OnTriggerExit2D(Collider2D col){
		if(col.tag == "Doragon"){
			collider2D.enabled = true;
		}
	}
}
