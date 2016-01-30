using UnityEngine;
using System.Collections;

public class TextNumber : MonoBehaviour {
    
    public int _stepYear;

	// Use this for initialization
	void Start () {
	   TestARNumber.Instance.ImageNumber = _stepYear;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
