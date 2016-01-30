using UnityEngine;
using System.Collections;

public class mbParticleController : MonoBehaviour {

	public float m_ShineEffectTime = 0.2f;
	public float m_RunEffectTime = 0.5f;
	public float m_GetEffectTime = 0.2f;

	public Color[] m_ColorList;

	public ParticleSystem m_ShineEffect;
	public ParticleSystem m_TrailEffect;
	public ParticleSystem m_GetEffect;

	public void RunParticleEfffect ( int colorIndex, Vector3 basePos, Vector3 targetPos ) {
		StartCoroutine ( RunEffects ( colorIndex, basePos, targetPos ) );
	}

	public IEnumerator RunEffects ( int colorIndex, Vector3 basePos, Vector3 targetPos ) {
		yield return StartCoroutine ( runEffect ( colorIndex, m_ShineEffect, basePos, basePos, m_ShineEffectTime ) );
		yield return StartCoroutine ( runEffect ( colorIndex, m_TrailEffect, basePos, targetPos, m_RunEffectTime ) );
		yield return StartCoroutine ( runEffect ( colorIndex, m_GetEffect, targetPos, targetPos, m_GetEffectTime ) );
	}

	IEnumerator runEffect ( int colorIndex, ParticleSystem particle,  Vector3 basePos, Vector3 targetPos, float runEffectTime ) {

		transform.position = basePos;
		particle.startColor = ( colorIndex == -1 ) ? Color.white : m_ColorList [ colorIndex ];
		particle.gameObject.SetActive ( true );

		float timer = runEffectTime;
		while ( timer > 0.0f ) {
			timer -= Time.deltaTime;
			transform.position = Vector3.Lerp ( basePos, targetPos, 1.0f - ( timer / m_RunEffectTime ) );
			yield return null;
		}

		particle.gameObject.SetActive ( false );
	}

}
