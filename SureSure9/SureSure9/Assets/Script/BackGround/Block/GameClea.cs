using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameClea : MonoBehaviour {
	private bool Clea = false;
	private new Rigidbody2D rigidbody2D;
	public Text CleaText;

	void Start(){
		rigidbody2D = GetComponent<Rigidbody2D>();
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		//人間が触れたらクリア
		if (col.gameObject.tag == "Player") {
			Clea = true;
		}

		//ドラゴンが触れたらクリア
		if (col.gameObject.tag == "Doragon") {
			Clea = true;
		}
	}

	void Update(){
		if (Clea) {
			//クリア文字を出す
			CleaText.enabled = true;
			//コルーチンを呼び出す
			StartCoroutine(gameClea());
		}
	}
	private IEnumerator gameClea() {
		//2秒待つ
		yield return new WaitForSeconds(3.5f);
		Debug.Log("Clea");
		Application.LoadLevel("title");
	}
}
