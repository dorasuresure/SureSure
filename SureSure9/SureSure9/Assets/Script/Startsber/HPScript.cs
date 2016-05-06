using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HPScript : MonoBehaviour
{

    RectTransform rt;

    //?J?n
    public GameObject player;   //?v???C???[
	public Text Text;  //?Q?[???I?[?o?[??????
    private bool gameOver = false;  //?Q?[???I?[?o?[????
    //?I??

    // Use this for initialization
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    //?J?n
    void Update()
    {
        if (rt.sizeDelta.x <= 0)
        {
            GameOver();
        }
        if (gameOver)
        {
            Text.enabled = true;
        }
        /*if (Input.GetMouseButtonDown(0) && rt.sizeDelta.x <= 0)
        {
            //?^?C?g????????
            Application.LoadLevel("title");
        }
        */
		//A????????
		if (Input.GetKeyDown("joystick button 0") && rt.sizeDelta.x <= 0)
		{
			Application.LoadLevel("title");
		}
		//B????????
		if (Input.GetKeyDown("joystick button 1") && rt.sizeDelta.x <= 0)
		{
			Application.LoadLevel("title");
		}
		
		//X????????
		if (Input.GetKeyDown("joystick button 2") && rt.sizeDelta.x <= 0)
		{
			Application.LoadLevel("title");
		}
		
		//Y????????
		if (Input.GetKeyDown("joystick button 3") && rt.sizeDelta.x <= 0)
		{
			Application.LoadLevel("title");
		}

		//Startボタンをクリック
		if (Input.GetKeyDown("joystick button 7")&& rt.sizeDelta.x <= 0)
		{
			Application.LoadLevel("mainScene7");
		}
    }

    public void HPDown(int ap)
    {
        rt.sizeDelta -= new Vector2(ap, 0);
    }
	public void HPUp (int hp)
	{
		rt.sizeDelta += new Vector2 (hp,0);
		if (rt.sizeDelta.x > 330f) {
			rt.sizeDelta = new Vector2 (330f, 62f);
		}
	}


    public void GameOver()
    {
        Destroy(player);
        gameOver = true;
        Debug.Log("gameover");
    }
}
