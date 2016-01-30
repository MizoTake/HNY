using UnityEngine;
using System.Collections;

public class ReadImageStatus : SingletonMonoBehaviour <ReadImageStatus> {
    
    public bool NumberCheck{ get; set; }
    
    public bool SceneToGame{ get; set; }
    
    public int Number{ get; set; }
    
    public int PlayPeople{ get; set; }
    
    //state
    private const int STATE_MANU = 0;
    private const int STATE_MAIN = 1;
    
    private bool _initMain;
    
    //参加人数
    private const int MIN_NUMBER = 3;
    private const int MAX_NUMBER = 6;
    private int _peopleNumber;

	// Use this for initialization
	void Start () {
	   _peopleNumber = MIN_NUMBER;
       TestARNumber.Instance.ImageNumber = _peopleNumber;
       SceneToGame = false;
       _initMain = true;
       DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        /*
       //Debug.Log(Application.loadedLevel);
	   switch(Application.loadedLevel){
           case STATE_MANU:
                //JoinPeople();
                //MainSceneへ
                if(SceneToGame){
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
	}
    
    public void UpdateManage(){
        //Debug.Log(Application.loadedLevel);
	   switch(Application.loadedLevel){
           case STATE_MANU:
                JoinPeople();
                //MainSceneへ
                if(SceneToGame){
                    Application.LoadLevel(STATE_MAIN);
                }
                break;
           case STATE_MAIN:
                //年を進める数字の更新処理
                NumberUpdate();
                break;
       }
    }
    
    public void JoinPeople(){
        switch(Number){
            case 1:
                if(!NumberCheck || _peopleNumber >= MAX_NUMBER) break;
                _peopleNumber += 1;
                
                break;
            case 2:
                if(!NumberCheck || _peopleNumber <= MIN_NUMBER) break;
                _peopleNumber -= 1;
                break;
            case 3:
                _initMain = true;
                SceneToGame = true;
                break;
        }
        //更新
        TestARNumber.Instance.ImageNumber = _peopleNumber;
        PlayPeople = _peopleNumber;
    }
    
    private void NumberUpdate(){
        if(_initMain) {
            Number = -1;
            //mbGameManager.Instance.SetPlayerNumber(PlayPeople);
            //Debug.Log("参加人数："+PlayPeople);
            _initMain = false;
        }
        //TestARNumber.Instance.ImageNumber = Number;
        mbGameManager.Instance.GetYearValueByCamera(Number);
    }
}
