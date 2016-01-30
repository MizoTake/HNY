using UnityEngine;
using System.Collections;

public class mbparticletest : MonoBehaviour {

	public Transform m_BaseTrans;
	public Transform m_TargetTrans;
	public mbParticleController m_Particle;

	void OnGUI () {

		for ( int i = 0;i < 6;++i ) {
			if ( GUILayout.Button ( "Player" + ( i + 1 ).ToString () ) ) {
				m_Particle.RunParticleEfffect ( i, m_BaseTrans.position, m_TargetTrans.position );
			}
		}

	}
}
