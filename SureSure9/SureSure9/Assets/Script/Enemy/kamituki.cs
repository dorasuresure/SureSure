using UnityEngine;
using System.Collections;

public class kamituki : MonoBehaviour
{
	
	//private GameObject Enemy;
	private float speed = 5f;
	private float destoroytime = 0.2f;
	// Use this for initialization
	void Start () {
	}
	
	public void Shoot(MonoBehaviour Enemy)
	{
		//speed = dir * speed;
		//Enemy = GameObject.FindWithTag("Enemy");
		//rigidbody2Dコンポーネントを取得
		Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
		rigidbody2D.velocity = new Vector2 (speed * Enemy.transform.localScale.x, rigidbody2D.velocity.y);
		Vector2 temp = transform.localScale;
		temp.x = Enemy.transform.localScale.x * 2f;
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