using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GamepadInput;

public class Player : MonoBehaviour {
	
	public float Pspeed = 4f; //歩くスピード
	private float spede2 = 8f; //ドラゴンのレベルが2になった時用
	public float JumpPower = 300;//ジャンプ力
	public float fireRate = 0.5f;//攻撃間隔
	private float nextfire = 0.0f;
	public LayerMask groundLayer;//地面に接しているかチェック
	public LayerMask AgroundLayer;//空中の"
	public LayerMask DoragonLayer;//ドラゴンに乗るよう
	public GameObject mainCamera;
	public GameObject Slash;//剣攻撃
	public GameObject mahou;//魔法攻撃
	private new Rigidbody2D rigidbody2D;
	private Animator anim;
	private bool isGrounded;	//地面
	private bool isAGrounded;	//すり抜ける地面
	private bool isDoragon;		//ドラゴンに乗るよう
	private new Renderer renderer;
	public MpScript MpScript;	//playerMP
	private int healPoint = 66; //回復力
	public HPScript PHP;		//playerHP
	public HPScript DHP;		//DoragonHP
	private bool Jump;
	private bool damage = false;
	public GameObject Doragonn; //ドラゴンの経験値を持ってくるよう

	void Start () {
		//各コンポーネントをキャッシュしておく
		anim = GetComponent<Animator>();
		rigidbody2D = GetComponent<Rigidbody2D>();
		renderer = GetComponent<Renderer> ();
	}

	void Update ()
	{
	GamepadState state = GamepadInput.GamePad.GetState (GamePad.Index.One);
	
	isGrounded = Physics2D.Linecast (
		transform.position + transform.up * 0.1f,
		transform.position - transform.up * 0.9f,
		groundLayer);
	isAGrounded = Physics2D.Linecast (
		transform.position + transform.up * 0.1f,
		transform.position - transform.up * 0.9f,
		AgroundLayer);
	isDoragon = Physics2D.Linecast (
		transform.position + transform.up * 0.1f,
		transform.position - transform.up * 0.9f,
		DoragonLayer);
	//isGrounded=true且つJumpボタンを押した時Jumpメソッド実行
		//Jump
		if (GamePad.GetButtonDown (GamePad.Button.B, GamePad.Index.One)) {
			if(isGrounded || isAGrounded || isDoragon){
				anim.SetBool ("Jump", true);
				Jump = true;
				isGrounded = false;
				isAGrounded = false;
				anim.SetBool ("Dash", false);
				rigidbody2D.AddForce (Vector2.up * JumpPower);
			}
		}
		if(isGrounded){
			anim.SetBool ("Jump", false);
			Jump = false;
		}
		if(isAGrounded){
			anim.SetBool ("Jump", false);
			Jump = false;
		}
			Vector2 Position = transform.position;
		//左キー: -1、右キー: 1、上キー: 1、下キー: -1
		float x = Input.GetAxisRaw ("Horizontal1");
		//左か右を入力したら
		if (x != 0) {
			//入力方向へ移動
			anim.SetBool ("Dash", true);
			if(x > -0.01){
				x = 1;
			}else if(x < 0.01){
				x = -1;
			}
			rigidbody2D.velocity = new Vector2 (x * Pspeed, rigidbody2D.velocity.y);
			//localScale.xを-1にすると画像が反転する
			Vector2 temp = transform.localScale;
			temp.x = x;
			transform.localScale = temp;
		} else {
			//横移動の速度を0にしてピタッと止まるようにする
			rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
			anim.SetBool ("Dash", false);
		}


		//画面中央から左に6移動した位置をplayerが超えたら
		if (transform.position.x > mainCamera.transform.position.x - 5) {
			//カメラの位置を取得
			Vector3 cameraPos = mainCamera.transform.position;
			//playerの位置から右に4移動した位置を画面中央にする
			cameraPos.x = transform.position.x + 3;
			mainCamera.transform.position = cameraPos;
			Vector2 pos = transform.position;
		}
		//ドラゴンに乗っている時用
		float dx = Input.GetAxisRaw ("Horizontal2");
		if (isDoragon) {
			if (dx != 0) {
				//入力方向へ移動
				//薄くならないよう
				if (dx > -0.01) {
					dx = 1;
				} else if (dx < 0.01) {
					dx = -1;
				}
				rigidbody2D.velocity = new Vector2 (dx * Pspeed, rigidbody2D.velocity.y);
			}
		}
		//Bで通常攻撃
		if (GamePad.GetButtonDown (GamePad.Button.X, GamePad.Index.One)&&Jump == false&&Time.time > nextfire) {
			nextfire = Time.time + fireRate;
			anim.SetTrigger("attack");
			Instantiate(Slash, transform.position + new Vector3(1f * transform.localScale.x,0f,0f), transform.rotation);
			}
		//Bで通常攻撃(空中)
		if (GamePad.GetButtonDown (GamePad.Button.X, GamePad.Index.One)&&Jump&&Time.time > nextfire) {
			nextfire = Time.time + fireRate;
			rigidbody2D.velocity = Vector2.zero;
			Physics.gravity = new Vector3(0f,-30f,0f);
			anim.SetTrigger("Airattack");
			Instantiate(Slash, transform.position + new Vector3(1f * transform.localScale.x,0f,0f), transform.rotation);
			}
		//ZでMPを１減らして魔法
		if (GamePad.GetButtonDown (GamePad.Button.A, GamePad.Index.One)&& Jump == false&&Time.time > nextfire) { 
			MpScript PlayerMp = MpScript.GetComponent<MpScript>();
			if(PlayerMp.num >= 1){
			nextfire = Time.time + fireRate;
			anim.SetTrigger("mahou");
			Instantiate(mahou, transform.position + new Vector3(2f * transform.localScale.x,3f,0f), transform.rotation);
			MpScript.MpCost(66);
				}
			}
		//空中で魔法
		if (GamePad.GetButtonDown (GamePad.Button.A, GamePad.Index.One)&&Jump&&Time.time > nextfire) {
			MpScript PlayerMp = MpScript.GetComponent<MpScript>();
			if(PlayerMp.num >= 1){
				nextfire = Time.time + fireRate;
				anim.SetTrigger("Airmahou");
				Instantiate(mahou, transform.position + new Vector3(2f * transform.localScale.x,3f,0f), transform.rotation);
				MpScript.MpCost(66);
			}
		}
		//ドラゴンの上に乗った状態で回復魔法
		if (GamePad.GetButtonDown (GamePad.Button.Y, GamePad.Index.One)&& isDoragon && Time.time > nextfire){
			MpScript PlayerMp = MpScript.GetComponent<MpScript>();
			if(PlayerMp.num >= 1){
			nextfire = Time.time + fireRate;
			anim.SetTrigger("Heal");
			PHP.HPUp(healPoint);
			DHP.HPUp(healPoint);
			MpScript.MpCost(66);
			}
		}
	}
	//敵の攻撃に当たったらコルーチン"Damage"に行く
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Enemy") {
			StartCoroutine("Damage");
		}
		//妖精に当たった時よう
		/*if (col.gameObject.tag == "Fairy") {
			StartCoroutine("Damage");
		}*/
	}
	void OnTriggerEnter2D (Collider2D col){
		if (col.tag == "EBite"){
			PHP.HPDown(66);
			StartCoroutine("Damage");
		}
	}
	//点滅するコルーチン
	IEnumerator Damage()
	{
		gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
		//15roope
		int count = 15;
		anim.SetBool ("Pdamege",true);
		damage = true;
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
		gameObject.layer = LayerMask.NameToLayer("Player");
		anim.SetBool ("Pdamege",false);
		damage = false;
	}
}

