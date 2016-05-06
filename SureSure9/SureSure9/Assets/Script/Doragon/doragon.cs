using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GamepadInput;

public class doragon : MonoBehaviour {
	public float Dspeed = 4f; //左右移動速度
	public float fry = 2f;//上下移動速度
	public float Dspeed2 = 8f; //レベル2左右移動速度
	public float fry2 = 4f;//レベル2上下移動速度
	public float fireRate = 0.5f;//火吹き攻撃間隔
	public float biteRate = 0.5f;//噛みつき攻撃間隔
	private float nextfire = 0.0f;//fireRateを入れるよう
	public LayerMask PlayerLayer;//プレイヤーが乗ってるレイヤー
	private bool isPlayer;	//人間が乗っている判定
	public GameObject Fire;//攻撃の火を入れる
	public GameObject Bite;//噛みつきエフェクトを入れる
	public GameObject Level1;//Level1を入れる
	public GameObject Level2;//Level2を入れる
	public GameObject Level3;//Level3を入れる
	private int sumexp; //合計経験値
	private bool level2 = false;
	private bool level3 = false;
	private new Rigidbody2D rigidbody2D;//rigidbody2Dを定義
	private Animator anim;//Animatorを定義
	private new Renderer renderer;
	public HPScript HPScript;//ドラゴンンのHP

	void Start () {
		//各コンポーネントをキャッシュしておく
		anim = GetComponent<Animator>();
		rigidbody2D = GetComponent<Rigidbody2D>();
		renderer = GetComponent<Renderer> ();
	}
		public void doragonexp(int dexp){
		sumexp += dexp;
		Debug.Log(sumexp);
	}

	void Update ()
	{
		isPlayer = Physics2D.Linecast (
			transform.position + transform.up * 0.9f,
			transform.position - transform.up * 0.1f,
			PlayerLayer);
		//左キー: -1、右キー: 1、上キー: 1、下キー: -1
		float x = Input.GetAxisRaw ("Horizontal2");
		float y = Input.GetAxisRaw ("Vertical2");
		//左か右を入力したら
		if (x != 0) {
			//入力方向へ移動
			//レベル2なら
			if(x > -0.01){
				x = 1;
			}else if(x < 0.01){
				x = -1;
			}
			//speedで移動
			if(sumexp >= 200){
				rigidbody2D.velocity = new Vector2 (x * Dspeed2, rigidbody2D.velocity.y);
			}else{
			rigidbody2D.velocity = new Vector2 (x * Dspeed, rigidbody2D.velocity.y);
			}
			//localScale.xを-1にすると画像が反転する
			Vector2 Dtemp = transform.localScale;
			Dtemp.x = x;
			transform.localScale = Dtemp;
		} else {
			//横移動の速度を0にしてピタッと止まるようにする
			rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
		}

		if (y != 0) {
		//入力方向へ移動
		//fryの速度で移動
		if(sumexp >= 200)
		{
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, y * fry2);
		}else{
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, y * fry);}

		} else {
			//横移動の速度を0にしてピタッと止まるようにする
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, 0);
		}

		//カメラの座標取得
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
		Vector2 pos = transform.position;
		//カメラの左右に出ないようにする
		pos.x = Mathf.Clamp (pos.x, min.x + 0.5f, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y - 5f, max.y);
		transform.position = pos;
		//Aを押して火を噴く
		if (GamePad.GetButtonDown (GamePad.Button.X, GamePad.Index.Two) && Time.time > nextfire) {
			anim.SetTrigger ("DFire");
			nextfire = Time.time + fireRate;
			Instantiate (Fire, transform.position + new Vector3 (1f * x, 0f, 0f), transform.rotation);
		}
		//Xを押して噛みつき攻撃
		if (GamePad.GetButtonDown (GamePad.Button.A, GamePad.Index.Two) && Time.time > nextfire) {
			anim.SetTrigger ("DBite");
			nextfire = Time.time + biteRate;
			Instantiate (Bite, transform.position + new Vector3 (1.5f * transform.localScale.x, 0f, 0f), transform.rotation);
		}
		//経験値が100を越えたら
		if (sumexp >= 200) {
			level2 = true;
		}
		if (sumexp >= 400) {
			level3 = true;
			level2 = false;
		}
		if (level2) {
			Level1.SetActiveRecursively (false);
			Level2.SetActiveRecursively (true);
		}else if(level3){
			Level2.SetActiveRecursively (false);
			Level3.SetActiveRecursively (true);
		}


	}
		//妖精に当たったらダメージコルーチンへ
		void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Fairy") {
            StartCoroutine("DoragonDamage");
		}
		if (col.gameObject.tag == "Fairy2") {
			StartCoroutine("DoragonDamage");
		}
	}
	void OnTriggerEnter2D (Collider2D col){
		if (col.tag == "ice"){
			HPScript.HPDown(66);
			StartCoroutine("DoragonDamage");
		}
	}
		//点滅するコルーチン
		IEnumerator DoragonDamage()
		{
		gameObject.layer = LayerMask.NameToLayer("DoragonDamage");
		//15回ループ
		anim.SetBool("Ddamege",true);
		int count = 15;
		while (count > 0) {
			//透明になる
			renderer.material.color = new Color (1,1,1,0);
			//0.05待つ
			yield return new WaitForSeconds(0.05f);
			renderer.material.color = new Color(1,1,1,1);
			//0.05待つ
			yield return new WaitForSeconds(0.05f);
			count--;
		}
		//LAYER->Player
		anim.SetBool("Ddamege",false);
		gameObject.layer = LayerMask.NameToLayer("Doragon");
	}
}