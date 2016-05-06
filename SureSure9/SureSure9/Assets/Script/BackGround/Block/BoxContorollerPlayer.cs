using UnityEngine;
using System.Collections;

public class BoxContorollerPlayer : MonoBehaviour {
	
	//下キー(K)を押す
	bool isDownKey = false;
	
	void Start ()
	{
		StartCoroutine ("EndofTime");
	}
	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == "Player"){
			collider2D.enabled = false;
		}
	}
	void OnTriggerExit2D(Collider2D col){
		if(col.tag == "Player"){
			collider2D.enabled = true;
		}
	}
	void FixedUpdate (){
		if (Input.GetKey (KeyCode.K)) {
			isDownKey = true;
			collider2D.enabled = false;
			StartCoroutine ("EndofTime");
		}
	}
	IEnumerator EndofTime()
	{
		yield return new WaitForSeconds(0.3f);
		isDownKey = false;
		collider2D.enabled = true;
	}
}