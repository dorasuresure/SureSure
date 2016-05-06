using UnityEngine;
using System.Collections;

public class fairy2Script : MonoBehaviour {

	private new Rigidbody2D rigidbody2D;
	private GameObject Fairy;
	public int FairyHp = 3;	//敵のHP
	public int attackPoint = 66;	//攻撃力
	public int MpUp = 66;	//倒した時増えるMP
	public HPScript HPScript;//ドラゴンンのHP
	public SubMp SubMp;		//人間のSUBMP



	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D>();

	}

	void Update () {
		rigidbody2D.velocity = new Vector2 (0f, rigidbody2D.velocity.y);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Doragon") {
			HPScript.HPDown(attackPoint);
		}
	}
	//*注意2ヒットするため改善の余地あり
	//理由キャラにコライダーが二つあるから
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Slash") {
			//SubMpメッソドを呼び出す引数はgainMp
			SubMp.Mpgain(MpUp);
			StartCoroutine("Damage");
			FairyHp--;
			if(FairyHp <= 0){
				Destroy(gameObject);
			}
		}
		if (col.tag == "mahou"){
			Destroy(gameObject);
		}
	}
	IEnumerator Damage()
	{
		gameObject.layer = LayerMask.NameToLayer("FairyDamage");
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
		gameObject.layer = LayerMask.NameToLayer("Fairy");
	}
}