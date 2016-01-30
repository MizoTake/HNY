﻿using UnityEngine;
using System.Collections;

public class mbGameManager : MonoBehaviour {

	public mbModelController m_ModelController;

	int m_playerIndex = -1;

	int m_playerMax = 0;

	//	プレイヤー数を切り替える.
	public void SetPlayerNumber ( int playerNumber ) {

		m_playerMax = playerNumber;

		m_playerIndex = 0;

		m_ModelController.SetStartParameter ( playerNumber );
	}

	//	カメラから取得した
	public void GetYearValueByCamera ( int year ) {

		m_ModelController.SelectYear ( m_playerIndex, year );

		m_playerIndex = ( m_playerIndex + 1 ) % m_playerMax;
	}


#if UNITY_EDITOR
	void OnGUI () {

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
