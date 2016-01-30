using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerJudge : MonoBehaviour {
	private const int MAX_PLAYER = 6;
	public int member;

	private GameObject root;
	private RawImage[] appers = new RawImage[MAX_PLAYER];
	public Texture[] images;

	// Use this for initialization
	void Start () {
		member = 4;
		root = GameObject.Find ("Canvas/Panel");
		for (int i = 0; i < 6; i++) {
			string names = "Player" + (i + 1).ToString ();
			appers [i] = root.transform.FindChild (names).GetComponent<RawImage> ();
			if (i + 1 <= member) {
				appers [i].texture = images [i];
			} else {
				appers [i].texture = images [images.Length - 1];
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
