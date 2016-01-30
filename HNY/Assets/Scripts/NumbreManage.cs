using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NumbreManage : MonoBehaviour {
	//定数宣言
	private const float MIN_LIGHT = 0f;
	private const float MAX_LIGHT = 1;
	private const float ADD_SPEED = 0.05f;

	public RawImage[] images;  //干支のイメージ配列
	private bool[] judges = new bool[3];
	private bool finish;
	public int currentlyNum;

	private float[] alphaManage = new float[3];


	// Use this for initialization
	void Start () {
		finish = true;
		for (int i = 0; i < 3; i++) {
			alphaManage [i] = (int)images [i].color.a;
			judges [i] = false;
		}
	}

	// Update is called once per frame
	void Update () {
		currentlyNum = ReadImageStatus.Instance.PlayPeople;
		Debug.Log (currentlyNum);
		for (int i = 0; i < 3; i++) {
			if (judges [i] == true) {
				StartCoroutine ("turnOn", i);
			}

			if (judges [i] == false) {
				StartCoroutine ("turnOff", i);
			}
		}


		/*if (Input.GetKeyDown (KeyCode.A)) {
			judges [currentNum] = false;
			currentNum++;
			Debug.Log ("passed");
			if (currentNum >= 12) {
				currentNum = 0;
			}
			judges [currentNum] = true;
		}*/

	}

	void FixedUpdate() {
		for(int i = 0; i < 3; i++){
			judges[i] = false;
		}
		for (int i = 0; i < currentlyNum - 3; i++) {
			judges [i] = true;
		}
	}

	private IEnumerator turnOn(int num){
		if(alphaManage[num] <= MAX_LIGHT){
			alphaManage [num] += ADD_SPEED;
			Color c = images [num].color;
			c.a = alphaManage [num];
			images [num].color = c;
		}
		yield return null;
	}

	private IEnumerator turnOff(int deleterNum){
		if (alphaManage [deleterNum] >= MIN_LIGHT) {
			alphaManage [deleterNum] -= ADD_SPEED;
		}
		Color c = images [deleterNum].color;
		c.a = alphaManage [deleterNum];
		images [deleterNum].color = c;
		yield return null;
	}
}