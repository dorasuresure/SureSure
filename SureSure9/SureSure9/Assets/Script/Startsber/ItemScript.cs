using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour {

	public int healPoint = 67;
	public HPScript HPScript;
	public HPScript DHPScript;

	void OnCollisionEnter2D (Collision2D col)
	{
		//playerと衝突した時
		if (col.gameObject.tag == "Player") {
			//LifeUpメソッドを呼び出す　引数はhealPoint
			HPScript.HPUp(healPoint);
			//アイテムを削除する
			Destroy(gameObject);
		}
		//doragonと衝突した時
		if (col.gameObject.tag == "Doragon") {
			//LifeUpメソッドを呼び出す　引数はhealPoint
			DHPScript.HPUp(healPoint);
			//アイテムを削除する
			Destroy(gameObject);
		}
	}
}