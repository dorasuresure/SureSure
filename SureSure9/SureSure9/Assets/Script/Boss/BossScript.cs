using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour {

	public Transform areaTopLeft;
	public Transform areaBottomRight;
	public GameObject firePrefab;
	public float minTime = 1f;
	public float maxTime = 1f;
	public Transform player;
	public GameObject playerObject;
	private float distanceX = 1f;
	private float distanceY = 1f;
	private bool Hit = true;

	private new Rigidbody2D rigidbody2D;
	public bossHP BossHPber;	//ボスHPを入れるよう
	public int attackPoint = 67;	//人間に与える攻撃力攻撃力
	public int DattackPoint = 67;	//ドラゴンに与える攻撃力
	public int BossDamage = 67;		//一回の攻撃でボスが受けるダメージ
	public HPScript HPScript;		//playerのHPscriptを入れる
	public HPScript DHPScript;		//doragonのHPscriptを入れる
	
	void Start(){
		rigidbody2D = GetComponent<Rigidbody2D>();
		StartCoroutine(Attack());
	}
	void OnTriggerEnter2D (Collider2D col)
	{
		/*火に当たるとダメージ
		if (col.tag == "fire") {
			HitBoss ();
		}
		*/
		//噛みつかれたらダメージ
		if (col.tag == "bite") {
			HitBoss ();
		}
		//魔法に当たるとダメージ
		if (col.tag == "mahou") {
			HitBoss ();
		}
	}
	void HitBoss(){
		BossHPber.HPDown(BossDamage);
		StartCoroutine("BDamage");
	}

	int dir = -1; // 左
	IEnumerator Attack()
	{
		// 移動の計算
		Vector3 from_position = transform.position;
		Vector3 to_position = new Vector3(
			Random.Range(areaTopLeft.position.x, areaBottomRight.position.x),
			Random.Range(areaBottomRight.position.y, areaTopLeft.position.y),
			transform.position.z);
		float start_time = Time.time;
		float move_time = Random.Range(minTime, maxTime);
		// 移動処理
		while (Hit = true)
		{
			float t = (Time.time - start_time) / move_time;
			transform.position = Vector3.Lerp(from_position, to_position, t);
			if (t >= 1f)
				break;
			yield return null;
		}
		
		if (distanceY < 20 && distanceX < 10)
		{
			GameObject obj =
				Instantiate(firePrefab, new Vector3(transform.position.x ,transform.position.y), Quaternion.identity) as GameObject;
			//obj.SendMessage ("Shoot", dir);
			kamituki a = obj.GetComponent<kamituki>();
			a.Shoot((MonoBehaviour)this);
		}
		// 次の移動＆攻撃を開始
		StartCoroutine(Attack());
	}

	IEnumerator BDamage()
	{
		gameObject.layer = LayerMask.NameToLayer("BossDamage");
		Hit = false;
		//10roope
		int count = 10;
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
		gameObject.layer = LayerMask.NameToLayer("Boss");
		Hit = true;
	}
	void Update()
	{
		if (playerObject == null)
			return;
		Vector3 playerPos = playerObject.transform.position;			//プレイヤーの位置
		float direction = playerPos.x - transform.position.x;			//方向
		int nowdir = direction > 0 ? 1 : -1;
		if (dir != nowdir)
		{
			transform.localScale = new Vector2(-transform.localScale.x,transform.localScale.y);
			dir = nowdir;
		}
	}
}

