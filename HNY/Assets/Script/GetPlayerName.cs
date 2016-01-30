using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetPlayerName : MonoBehaviour {
	public  string player;
	private Text playerName;

	// Use this for initialization
	void Start () {
		playerName = transform.FindChild ("PlayerName").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		playerName.text = player;
	}
}
