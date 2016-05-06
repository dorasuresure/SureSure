using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour {
    //生み出す敵プレファブ
    public GameObject enemyPrefab;
	GameObject[] existEnemys;
    //ジェネレーターのHP
	public int BlockHp = 3;
    //生み出せる敵の最大数
    public int maxMonster = 4;
	// Use this for initialization

	void Start () {
        //今いる敵の数と敵の最大数を比較
        existEnemys = new GameObject[maxMonster];
        //周期的に実行
        StartCoroutine(Exec());
	}

	void Update (){
        //もしもHPが0になったら
		if (BlockHp <= 0) {
            //消滅
			Destroy (gameObject);
		}
	}
    //トリガーに当たったら
	void OnTriggerEnter2D(Collider2D col){
        //もしもbiteが当たったらHPを減らす
        if (col.tag == "bite"){
			BlockHp--;
        }
        //もしもSlashが当たったらHPを減らす
        if (col.tag == "Slash"){
			BlockHp--;
        }
        //もしもmahouが当たったらHPを減らす
        if (col.tag == "mahou"){
			BlockHp--;
		}
	}
    //敵を作成
    IEnumerator Exec()
    {
        while (true)
        {
            Generate();
            yield return new WaitForSeconds(10.0f);
        }
    }
	void Generate ()
    {
        for(int enemyCount = 0; enemyCount < existEnemys.Length; ++enemyCount)
        {
           if( existEnemys[enemyCount] == null)
            {//敵を作成
                existEnemys[enemyCount] = Instantiate(enemyPrefab, transform.position, transform.rotation) as GameObject;
                return;
            }
        }
	}
}
