﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReadImageStatus : SingletonMonoBehaviour <ReadImageStatus> {
    public AudioClip _selectSE;
    public AudioClip _decisionSE;
    private AudioSource _se;
    
    private GameObject _ARforPC;
    private GameObject _ARforSmartPhone;
    
    public bool NumberCheck{ get; set; }
    
    public bool SceneToNext{ get; set; }
    
    public int Number{ get; set; }
    
    public int PlayPeople{ get; set; }
    
    //state
    private const int STATE_MENU = 0;
    private const int STATE_MAIN = 1;
    
    private bool _initMain;
    
    //参加人数
    private const int MIN_NUMBER = 3;
    private const int MAX_NUMBER = 6;
    private int _peopleNumber;

	// Use this for initialization
	void Start () {
	   _peopleNumber = MIN_NUMBER + 1;
       PlayPeople = _peopleNumber;
       //TestARNumber.Instance.ImageNumber = _peopleNumber;
       SceneToNext = false;
       _initMain = true;
       DontDestroyOnLoad(this.gameObject);
       _se = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        /*
       //Debug.Log(Application.loadedLevel);
	   switch(Application.loadedLevel){
           case STATE_MANU:
                //JoinPeople();
                //MainSceneへ
                if(SceneToNext){
                    PlayPeople = Number;
                    Application.LoadLevel(STATE_MAIN);
                }
                break;
           case STATE_MAIN:
                //年を進める数字の更新処理
                NumberUpdate();
                break;
       }
       */
        Debug.Log("しーん"+SceneManager.sceneCountInBuildSettings + "  " + SceneManager.sceneCount);
	}
    
    public void UpdateManage(){
	   switch(SceneManager.GetActiveScene().buildIndex){
           case STATE_MENU:
                JoinPeople();
                //MainSceneへ
                if(SceneToNext){
					ARCameraManage.Instance.ActiveFalse ();
                    //Application.LoadLevel(STATE_MAIN);
                    FadeManager.Instance.LoadLevel("Main", 0.5f);
                    SceneToNext = false;
                }
                break;
           case STATE_MAIN:
                //年を進める数字の更新処理
                NumberUpdate();
                if(SceneToNext){
					ARCameraManage.Instance.ActiveFalse ();
                    FadeManager.Instance.LoadLevel("Menu", 0.5f);
                    SceneToNext = false;
                }
                break;
       }
    }
    
    public void JoinPeople(){
        switch(Number){
            case 1:
                if(_peopleNumber >= MAX_NUMBER) break;
                _peopleNumber += 1;
                _se.PlayOneShot(_selectSE);
                break;
            case 2:
                if(_peopleNumber <= MIN_NUMBER) break;
                _peopleNumber -= 1;
                _se.PlayOneShot(_selectSE);
                break;
            case 3:
                _initMain = true;
                SceneToNext = true;
                _se.PlayOneShot(_decisionSE);
                break;
        }
        //更新
        //TestARNumber.Instance.ImageNumber = _peopleNumber;
        PlayPeople = _peopleNumber;
    }
    
    private void NumberUpdate(){
        /*
        if(_initMain) {
            //Number = -1;
            //mbGameManager.Instance.SetPlayerNumber(PlayPeople);
            //Debug.Log("参加人数："+PlayPeople);
            _initMain = false;
        }
        */
        if(Number == 4){
            SceneToNext = true;
        }else{
            //TestARNumber.Instance.ImageNumber = Number;
            mbGameManager.Instance.GetYearValueByCamera(Number);
        }
    }
}
