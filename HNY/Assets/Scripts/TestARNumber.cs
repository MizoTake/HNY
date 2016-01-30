using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestARNumber : SingletonMonoBehaviour<TestARNumber> {
    
    private int _imageNumber;
    private Text _textObj;
    
    public int ImageNumber{ get{ return _imageNumber; } set{ _imageNumber = value; } }

	// Use this for initialization
	void Start () {
        _textObj = GetComponent<Text>();
	   _imageNumber = -1;
       _textObj.text = "";
	}
	
	// Update is called once per frame
	void Update () {
	   if(_imageNumber != -1){
           _textObj.text = "" + _imageNumber;
       }
	}
}
