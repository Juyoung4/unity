using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour 
{	
	public float speed;
	public GUIText countText;
	public GUIText winText;
	public float LimitTime;
	public GameObject restartbtn;
	public GameObject Missile_set;

	private int count;	
	falling_Missile fm;
	string gameState;

    void Start()
	{
		fm = Missile_set.GetComponent<falling_Missile>();
        count = 3;
		SetCountText();
		winText.text = "";
		gameState = "run";
	}
	
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
       
        // Vector3 screenPos = camera.WorldToScreenPoint(transform.position);
        // Debug.Log("target is " + screenPos.x + " pixels from the left");
        // Debug.Log("target is " + screenPos.y + " pixels from the bottom");

        GetComponent<Rigidbody>().AddForce (movement * speed * Time.deltaTime);

		countDonw();
		if (LimitTime < 0) {
			gameState = "lose";
		}
	}

	void countDonw(){
		if (gameState == "run"){
			LimitTime -= Time.deltaTime;
			winText.text = "time : " + string.Format("{0:f3}", LimitTime);
		}
		else if (gameState == "win"){
			GameWin();
		}
		else{
			GameOver();
		}
		
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.tag == "Missile")
		{
			count = count - 1;
			SetCountText();
		}

		if(other.gameObject.tag == "FinishLine")
		{
			gameState = "win";
		}
	}
	
	void SetCountText ()
	{
		countText.text = "Life: " + count.ToString();
		if(count <= 0)
		{
			GameOver();
		}
	}

	void GameOver() 
	{
		restartbtn.SetActive(true);
		gameState = "lose";
		fm.stop_spawn_missile();
		winText.text = "Game Over";
	}

	void GameWin()
	{
		restartbtn.SetActive(true);
		gameState = "win";
		fm.stop_spawn_missile();
		winText.text = "Game win";
	}

	public void Restart()
	{
		SceneManager.LoadScene("MiniGame");
	}
}