using UnityEngine;
using System.Collections;

public class rakka : MonoBehaviour {

    public int attackPoint = 50;	//攻撃力
    public HPScript HPScript;
	public HPScript DHPScript;

    //playerに当たったらHPを減らす
	void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            HPScript.HPDown(attackPoint);
        }
		if (col.gameObject.tag == "Doragon")
		{
			DHPScript.HPDown(attackPoint);
		}
    }
}
