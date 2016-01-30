using UnityEngine;
using System.Collections;

public class mbModelController : MonoBehaviour {

//	[ Label ( "干支プレハブ" ) ]
	public GameObject[] m_Prefab;

	//	中央に表示されるモデル.
	GameObject[] m_centerObjs;

	GameObject m_ObjParent;

	//	各プレイヤーが直前に取得した干支のモデル.
	GameObject[] m_uiBeforeGetObjs;

	int m_centerPos = 0;

//	[ Label ( "干支が動くのにかかる時間" ) ]
	public float m_AnimationTime = 0.5f;

//	[ Label ( "yearによらず干支が切り替わるのにかかる時間を一定にする" ) ]
	public bool m_IsAnimationTimeSolid = false;

//	[ Label ( "干支が動く距離" ) ]
	public float m_AnimationDist = 5.0f;

	public float m_GetObjAnimationTime = 0.5f;

	public GameObject[] m_PlayerGetObjPos;

	bool m_isDoneAnimation = true;
	public bool IsDone {
		get { return m_isDoneAnimation; }
	}
		
	public void Start () {

		if ( m_centerObjs != null ) return;

		//	プレハブから実際に表示するオブジェクトを生成.
		//	ただし最初は非表示にしておく.
		m_ObjParent = new GameObject ( "objParent" );
		m_ObjParent.transform.parent = transform;
		m_ObjParent.SetActive ( false );
		m_centerObjs = new GameObject [ m_Prefab.Length ];
		for (int i = 0; i < m_centerObjs.Length; ++i) {
			m_centerObjs [ i ] = GameObject.Instantiate ( m_Prefab [ i ] ) as GameObject;
			m_centerObjs [ i ].transform.parent = m_ObjParent.transform;
		}
	}

	public void SetStartParameter ( int playerMax, int firstIndex = -1 ) {

		Start ();

		if ( firstIndex == -1 ) {
			firstIndex = Random.Range ( 0, m_centerObjs.Length );
		}

		Debug.Log ( "firstIndex" + firstIndex.ToString () );

		m_centerPos = firstIndex;

		if ( m_uiBeforeGetObjs != null ) {
			for ( int i = 0;i < m_uiBeforeGetObjs.Length; ++i ) {
				GameObject.Destroy ( m_uiBeforeGetObjs [ i ] );
			}
		}

		m_uiBeforeGetObjs = new GameObject[ playerMax ];
		m_ObjParent.SetActive ( true );
		for ( int i = 0;i < m_centerObjs.Length;++i ) {
			setObjPos ( i, 0.0f );
		}
	}

	public void SelectYear ( int playerIndex, int year ) {

		if ( !IsDone )
			return;

		m_isDoneAnimation = false;

		StartCoroutine ( changeCenterObj ( playerIndex, year ) );
	}

	IEnumerator changeCenterObj ( int playerIndex, int year ) {

		Debug.Log ( "pindex : " + playerIndex.ToString () + ", year : " + year.ToString () );

		//	干支を動かす.
		for ( int i = 0;i < year;++i ) {
			float animationTime = ( m_IsAnimationTimeSolid ) ? m_AnimationTime / year :  m_AnimationTime;
			while ( animationTime > 0.0f ) {

				setObjPos ( 1.0f - ( animationTime / m_AnimationTime ) );

				animationTime -= Time.deltaTime;

				yield return null;
			}

			m_centerPos = ( m_centerPos + 1 ) % m_centerObjs.Length;
		}

		//	プレイヤーの取得した干支を更新する.
		Debug.Log ( "player" + playerIndex.ToString () + " get " + m_centerPos.ToString () );
		yield return StartCoroutine ( updateGetObj ( playerIndex, m_centerPos ) );
	}

	void setObjPos ( float animationRate ) {
		
		for ( int i = 0;i < m_centerObjs.Length;++i ) {
			setObjPos ( i, animationRate );
		}

	}

	void setObjPos ( int objIndex, float animationRate ) {

		Vector3 pos = m_centerObjs [ objIndex ].transform.localPosition;
		pos.x = ( objIndex - m_centerPos - animationRate ) * m_AnimationDist;
		if ( pos.x < -m_AnimationDist * 2 ) {
			pos.x += m_AnimationDist * m_centerObjs.Length;
		}
		m_centerObjs [ objIndex ].transform.localPosition = pos;
	}

	IEnumerator updateGetObj ( int playerIndex, int objIndex ) {

		if ( m_uiBeforeGetObjs == null )
			yield break;

		//	前回取得した干支のオブジェクトを削除.
		if ( m_uiBeforeGetObjs [ playerIndex ] != null ) {
			GameObject.Destroy ( m_uiBeforeGetObjs [ playerIndex ] );
		}

		//	移動元,移動先.
		Transform baseTrans = m_centerObjs [ m_centerPos ].transform;
		Transform targetTrans = m_PlayerGetObjPos [ playerIndex ].transform;

		//	今回取得した干支のオブジェクトを生成.
		m_uiBeforeGetObjs [ playerIndex ] = GameObject.Instantiate ( m_Prefab [ objIndex ] ) as GameObject;
		m_uiBeforeGetObjs [ playerIndex ].transform.parent = m_ObjParent.transform;
		m_uiBeforeGetObjs [ playerIndex ].transform.position = baseTrans.position;

		float timer = m_GetObjAnimationTime;
		while ( timer > 0.0f ) {
			timer -= Time.deltaTime;
			m_uiBeforeGetObjs [ playerIndex ].transform.position = Vector3.Lerp ( baseTrans.position, targetTrans.position, 1.0f - ( timer / m_GetObjAnimationTime ) );
			yield return null;
		}

		m_isDoneAnimation = true;
	}
}
