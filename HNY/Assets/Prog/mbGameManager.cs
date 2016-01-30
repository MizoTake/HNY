using UnityEngine;
using System.Collections;

public class mbGameManager : SingletonMonoBehaviour<mbGameManager> {

	public mbModelController m_ModelController;

	int m_playerIndex = -1;

	int m_playerMax = 0;
    
    void Awake(){
        SetPlayerNumber(ReadImageStatus.Instance.PlayPeople);
        //Debug.Log("参加人数："+ReadImageStatus.Instance.PlayPeople);
    }

	//	プレイヤー数を切り替える.
	public void SetPlayerNumber ( int playerNumber ) {

		m_playerMax = playerNumber;

		m_playerIndex = 0;

		m_ModelController.SetStartParameter ( playerNumber );
	}

	//	カメラから取得した
	public void GetYearValueByCamera ( int year ) {

		if ( m_ModelController.IsDone ) return;

		if ( m_playerMax == 0 ) {
			Debug.LogError ( "playerMax is 0." );
			return;
		}

		m_ModelController.SelectYear ( m_playerIndex, year );

		m_playerIndex = ( m_playerIndex + 1 ) % m_playerMax;
	}


#if UNITY_EDITOR || true
	void OnGUI () {

		if ( !m_ModelController.IsDone ) {
			return;
		}

		GUILayout.BeginVertical ();

		GUILayout.BeginHorizontal ();
		for ( int i = 3;i <= 6;++i ) {
			if ( GUILayout.Button ( i.ToString () + "人で始める" ) ) {
				SetPlayerNumber ( i );
			}
		}
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		for ( int i = 1;i <= 3;++i ) {
			if ( GUILayout.Button ( i.ToString () + "year" ) ) {
				GetYearValueByCamera ( i );
			}
		}
		GUILayout.EndHorizontal ();

		if ( GUILayout.Button ( "ランダムyear" ) ) {
			GetYearValueByCamera ( Random.Range ( 1, 4 ) );
		}

		GUILayout.EndVertical ();
	}
#endif


}
