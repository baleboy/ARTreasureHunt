using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectorController : MonoBehaviour {

	public DialController m_dial;
	float m_min = 0.4f;
	float m_max = 5;
	float m_intensity = 0.5f;
	float m_maxBeepInterval = 3f;
	float m_minBeepInterval = 0.5f;
	float m_elapsedTime = 0;
	float m_beepInterval;

	AudioSource m_player;

	public void SetScale(float min, float max) {
		m_min = min;
		m_max = max;
	}

	public void SetMaxScale(float max) {
		m_max = max;
	}

	public void SetValue(float v) {
		v = Mathf.Abs(v);
		if (v > m_max) {
			m_intensity = 1;
		} else {
			m_intensity = (v - m_min) / (m_max - m_min);
		}
		m_dial.SetIntensity(m_intensity);
		m_beepInterval = m_minBeepInterval + (m_maxBeepInterval - m_minBeepInterval) * m_intensity;
	}

	// Use this for initialization
	void Start () {
		m_player = GetComponentsInChildren<AudioSource>()[0];
		m_minBeepInterval = m_player.clip.length;
	}

	void OnEnable() {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	void OnDisable() {
		Screen.sleepTimeout = SleepTimeout.SystemSetting;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.activeInHierarchy) {
			m_elapsedTime += Time.deltaTime;
			if (m_elapsedTime >= m_beepInterval) {
					m_player.Play();
					m_elapsedTime = 0;
			}
		}
	}
}
