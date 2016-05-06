using UnityEngine;
using System.Collections;

public class wolf : MonoBehaviour
{
		public Transform areaTopLeft;
		public Transform areaBottomRight;
		public GameObject firePrefab;
		public float minTime = 1f;
		public float maxTime = 1f;
		public Transform player;
		public GameObject playerObject;
		private float distanceX = 1f;
		private float distanceY = 1f;
		
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
			while (true)
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
		
		void Start()
		{
			StartCoroutine(Attack());
		}
		void Update()
		{
			if (playerObject == null)
				return;
			Vector3 playerPos = playerObject.transform.position;			//プレイヤーの位置
			//float distanceX = Mathf.Abs(playerPos.x-transform.position.x);	//距離x
			//float distanceY = Mathf.Abs(playerPos.y-transform.position.y);	//距離y
			float direction = playerPos.x - transform.position.x;			//方向
			int nowdir = direction > 0 ? 1 : -1;
			if (dir != nowdir)
			{
				
				transform.localScale = new Vector2(-transform.localScale.x,transform.localScale.y);
				dir = nowdir;
			}
		}
}