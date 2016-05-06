using UnityEngine;
using System.Collections;

public class mahou : MonoBehaviour {

	private GameObject player;
	private float sokudo = -7f;	//下に落ちる
	private float destoroytime = 12f;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		//rigidbody2Dコンポーネントを取得
		Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
		rigidbody2D.velocity = new Vector2 (0*player.transform.localScale.x,sokudo * player.transform.localScale.y);
		Vector2 temp = transform.localScale;
		temp.x = player.transform.localScale.x;
		transform.localScale = temp;
		//2秒後に消滅
		Destroy(gameObject, destoroytime);
	}
	//敵に当たったら消す
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Ground") {
			Destroy(gameObject);
		}
	}
}
