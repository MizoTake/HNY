using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageApper : MonoBehaviour {
	private int animalState;
	private int currentPlayer;
	private bool stateJudge;
	public Texture[] animalImages;
	private Texture[] onPlayerImages = new Texture[6];
	private GameObject root;

	// Use this for initialization
	void Start () {
		root = GameObject.Find ("Canvas/Panel");
		animalState = 0;
		currentPlayer = 0;
		stateJudge = false;
		for (int i = 0; i < 6; i++) {
			string names = "AnimalApper" + (i + 1).ToString();
			onPlayerImages[i] = animalImages[12];
			root.transform.FindChild (names).GetComponent<RawImage> ().texture = onPlayerImages [i];
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (stateJudge == true) {
			onPlayerImages [currentPlayer] = animalImages [animalState];
			string names = "AnimalApper" + currentPlayer.ToString();
			root.transform.FindChild (names).GetComponent<RawImage> ().texture = onPlayerImages [currentPlayer];
			stateJudge = false;
		}
	}

	public void ReadIndex(int playerNum, int currentAnimal){
		if (stateJudge == false) {
			animalState = currentAnimal;
			currentPlayer = playerNum;
			stateJudge = true;
		}
	}
}
