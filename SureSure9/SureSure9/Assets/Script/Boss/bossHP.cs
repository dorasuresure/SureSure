using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bossHP : MonoBehaviour {
	
	RectTransform Brt;

	public GameObject Boss;	//ボスを入れる場所
	public Text Clear;		//クリア文字を入れる場所
	private bool gameClear  = false;
	private bool Cleareturn = false;

	private IEnumerable BossClea(){
		Debug.Log ("Clea");
		//５秒待つ
		yield return new WaitForSeconds (5f);
		Application.LoadLevel("title");
	}

	void Start()
	{	//rtを取得
		Brt = GetComponent<RectTransform>();
		StartCoroutine("BossClea");
	}

	void Update()
	{
		//BossHpが0になったら
		if (Brt.sizeDelta.x <= 0)
		{	//GameClearを呼び出す
			GameClear();
		}

		//もしGameClearになったら
		if (gameClear)
		{	//クリア文字を出す
			Clear.enabled = true;
		}

		//A????????
		if (Input.GetKeyDown("joystick button 0") && Cleareturn)
		{
			Application.LoadLevel("title");
		}
		//B????????
		if (Input.GetKeyDown("joystick button 1") && Cleareturn)
		{
			Application.LoadLevel("title");
		}
		
		//X????????
		if (Input.GetKeyDown("joystick button 2") && Cleareturn)
		{
			Application.LoadLevel("title");
		}
		
		//Y????????
		if (Input.GetKeyDown("joystick button 3") && Cleareturn)
		{
			Application.LoadLevel("title");
		}
		
		//Startボタンをクリック
		if (Input.GetKeyDown("joystick button 7")&& Cleareturn)
		{
			Application.LoadLevel("mainScene7");
		}
	}



	//ボスのHPを減らす処理
	public void HPDown(int BDame)
	{	//Bap分Hpを減らす
		Brt.sizeDelta -= new Vector2(BDame, 0);
	}

	//呼び出されるGameClear
	public void GameClear()
	{
		Destroy(Boss);
		gameClear = true;
	}

}