using UnityEngine;
using System.Collections;

public class ARCameraManage : SingletonMonoBehaviour<ARCameraManage> {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ActiveFalse(){
		gameObject.SetActive (false);
	}
}
