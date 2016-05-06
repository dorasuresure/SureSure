using UnityEngine;
using System.Collections;

public class MpScript : MonoBehaviour {
	
	RectTransform rt;
	public int num = 2;
	
	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform>();
	}
	//subMpが溜まると１増える
	public void MpStock(int mps){
		rt.sizeDelta += new Vector2(mps,0);
		num++;
		//最大値を越えたら留める
		if(rt.sizeDelta.x > 260f){
			rt.sizeDelta = new Vector2(260f,58f);
			num--;
		}
	}
	//playerが魔法を使うと減る
	public void MpCost(int mpc){
		rt.sizeDelta -= new Vector2(mpc,0);
		num--;
	}
}
