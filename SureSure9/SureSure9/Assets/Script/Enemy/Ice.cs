﻿using UnityEngine;
using System.Collections;

public class Ice : MonoBehaviour {

	//private GameObject Fairy;
	private float speed = -10f;
	private float destoroytime = 1f;
	// Use this for initialization
	void Start () {
	}

	public void Shoot(MonoBehaviour Fairy)
	{
		//speed = dir * speed;
		//Fairy = GameObject.FindWithTag("Fairy");
		//rigidbody2Dコンポーネントを取得
		Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
		rigidbody2D.velocity = new Vector2 (speed * Fairy.transform.localScale.x, rigidbody2D.velocity.y);
		Vector2 temp = transform.localScale;
		temp.x = Fairy.transform.localScale.x * 2f;
		transform.localScale = temp;
		//1秒後に消滅
		Destroy(gameObject, destoroytime);
	}



	void OnTriggerEnter2D (Collider2D col)
	{
		/*if (col.gameObject.tag == "Doragon") {
			Destroy(gameObject);
		}*/
	}
}
