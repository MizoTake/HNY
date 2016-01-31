using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimalLineManage : MonoBehaviour {
	//定数宣言
	private const float MIN_LIGHT = 0.5f;
	private const float MAX_LIGHT = 1;
	private const float WAIT_FRAME = 0.3f;
	private const float ADD_SPEED = 0.1f;

	private Image[] lines = new Image[12];  //干支のイメージ配列
	private bool[] judges = new bool[12];
	public int currentNum;
	private bool finish;
	public Image one;
	public Image two;
	public Image three;
	public Image four;
	public Image five;
	public Image six;
	public Image seven;
	public Image eight;
	public Image nine;
	public Image ten;
	public Image eleven;
	public Image twelve;

	private float[] alphaManage = new float[12];
	private bool[] firstJudge = new bool[12];


	// Use this for initialization
	void Start () {
		finish = true;
		lines [0] = one;
		lines [1] = two;
		lines [2] = three;
		lines [3] = four;
		lines [4] = five;
		lines [5] = six;
		lines [6] = seven;
		lines [7] = eight;
		lines [8] = nine;
		lines [9] = ten;
		lines [10] = eleven;
		lines [11] = twelve;
		for (int i = 0; i < 12; i++) {
			alphaManage [i] = (int)lines [i].color.a;
			firstJudge [i] = false;
			judges [i] = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 12; i++) {
			if (judges [i] == true) {
				StartCoroutine ("turnOn", i);
			}

			if (judges [i] == false) {
				StartCoroutine ("turnOff", i);
			}
		}
	}

	void FixedUpdate(){
		for(int i = 0; i < 12; i++){
			judges[i] = false;
		}
		judges [currentNum] = true;
	}

	private IEnumerator turnOn(int num){
		if(firstJudge[num] == false){
			yield return new WaitForSeconds (WAIT_FRAME);
			firstJudge[num] = true;
		}
		if(alphaManage[num] <= MAX_LIGHT && firstJudge[num] == true){
			alphaManage [num] += ADD_SPEED;
			Color c = lines [num].color;
			c.a = alphaManage [num];
			lines [num].color = c;
		}
	}

	private IEnumerator turnOff(int deleterNum){
		if (alphaManage [deleterNum] >= MIN_LIGHT) {
			alphaManage [deleterNum] -= ADD_SPEED;
		}
		firstJudge [deleterNum] = false;
		Color c = lines [deleterNum].color;
		c.a = alphaManage [deleterNum];
		lines [deleterNum].color = c;
		yield return null;
	}
}
