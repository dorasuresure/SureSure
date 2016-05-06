using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    //マウスの左ボタンをクリック

		/*コントローラーのボタン
			BボタンがX判定
			AボタンがB判定
			XボタンがA判定
			YボタンがY判定
		 */
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Application.LoadLevel("mainScene6");
        }
	}
}
