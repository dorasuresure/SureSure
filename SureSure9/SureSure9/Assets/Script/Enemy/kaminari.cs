using UnityEngine;
using System.Collections;

public class kaminari : MonoBehaviour {
	private GameObject Fairy2;
	private float speed = -3f;
	private float speed2 = 0f;
	private float destoroytime = 3f;
	// Use this for initialization
	void Start () {
	}
	
	public void Shoot(int dir)
	{
		//speed = dir * speed;
		Fairy2 = GameObject.FindWithTag("Fairy2");
		//rigidbody2Dコンポーネントを取得
		Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
		rigidbody2D.velocity = new Vector2 (speed2 * Fairy2.transform.position.x, speed * Fairy2.transform.localScale.y);
		Vector2 temp = transform.localScale;
		temp.x = Fairy2.transform.localScale.x;
		transform.localScale = temp;
		//1秒後に消滅
		Destroy(gameObject, destoroytime);
	}
	
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Doragon") {
			Destroy(gameObject);
		}
	}
}
