using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

	public GameObject Doragon;
	private float speed = 10f;
	private float destoroytime = 1f;
	// Use this for initialization
	void Start () {
		Doragon = GameObject.FindWithTag("Doragon");
		//rigidbody2Dコンポーネントを取得
		Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
		rigidbody2D.velocity = new Vector2 (speed * Doragon.transform.localScale.x, rigidbody2D.velocity.y);
		Vector2 temp = transform.localScale;
		temp.x = Doragon.transform.localScale.x * -1;
		transform.localScale = temp;
		//1秒後に消滅
		Destroy(gameObject, destoroytime);
	}
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Enemy") {
			Destroy(gameObject);
		}
		if (col.gameObject.tag == "Block") {
			Destroy(gameObject);
		}
	}
}
