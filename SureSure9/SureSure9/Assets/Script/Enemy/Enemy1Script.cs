using UnityEngine;
using System.Collections;

public class Enemy1Script : MonoBehaviour {

	private new Rigidbody2D rigidbody2D;
	public int speed = 0;	//移動速度
	public int attackPoint = 67;	//人間に与える攻撃力攻撃力
	//public int DattackPoint = 25;	//ドラゴンに与える攻撃力
	public int EnemyHp = 3;			//敵のHP
	public int exp = 50;		//経験値
	private new Renderer renderer;
	public HPScript HPScript;		//playerのHPscriptを入れる
	//public HPScript DHPScript;	//doragonのHPscriptを入れる
	public doragon doragon;			//ドラゴンの中にある経験値を渡す用
	
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D>();
		renderer = GetComponent<Renderer> ();
	}
	
	void Update () {
		rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);
	}
	void OnTriggerEnter2D (Collider2D col)
	{
		//火に当たったら消える
		if (col.tag == "fire") {
			EnemyHp --;
			StartCoroutine("EDamage");
			if(EnemyHp <= 0){
				Destroy(gameObject);
			}
		}
		//噛みつかれたらdoragonの経験値を増やし消える
		if (col.tag == "bite") {
			EnemyHp --;
			StartCoroutine("EDamage");
			if(EnemyHp <= 0){
				Destroy(gameObject);
				doragon.doragonexp(exp);
			}
		}
	}
	//playerに当たったらHPを減らす
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player") {
			HPScript.HPDown(attackPoint);
		}
		//エネミーの攻撃がドラゴンに当たったよう
		/*if (col.gameObject.tag == "Doragon") {
			DHPScript.HPDown(DattackPoint);
		}*/
	}
	IEnumerator EDamage()
	{
		gameObject.layer = LayerMask.NameToLayer("EnemyDamage");
		//10roope

			int count = 5;
			while (count > 0) {
				//toumei
				renderer.material.color = new Color (1,1,1,0);
				//0.05matu
				yield return new WaitForSeconds(0.05f);
				renderer.material.color = new Color(1,1,1,1);
				//0.05matu
				yield return new WaitForSeconds(0.05f);
				count--;
			}
			//LAYER->Player
			gameObject.layer = LayerMask.NameToLayer("Enemy");

	}
}
