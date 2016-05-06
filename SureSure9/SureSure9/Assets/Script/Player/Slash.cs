using UnityEngine;
using System.Collections;

public class Slash : MonoBehaviour {

	private GameObject player;
	private float destoroytime = 0.2f;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		//rigidbody2Dコンポーネントを取得
		Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
		rigidbody2D.velocity = new Vector2 (0*player.transform.localScale.x,player.transform.localScale.y);
		Vector2 temp = transform.localScale;
		temp.x = player.transform.localScale.x;
		transform.localScale = temp;
		//0.2秒後に消滅
		Destroy(gameObject, destoroytime);
	}
}

