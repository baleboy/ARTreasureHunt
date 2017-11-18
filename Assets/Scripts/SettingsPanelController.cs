using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelController : MonoBehaviour {

	public DetectorController m_Detector;
	public Dropdown m_AreaDropdown;

	const string GAME_AREA_MAX_PREF = "GameAreaSizeMax";
	float[] m_maxScale = {5f, 15f, 50f};

	void Start () {
		int scaleOption = PlayerPrefs.GetInt(GAME_AREA_MAX_PREF);
		m_AreaDropdown.value = scaleOption;
		m_Detector.SetScale(0.2f, m_maxScale[scaleOption]);
	}

	public void SetGameAreaSize(int s) {
		Debug.Log("Max scale option value = " + s.ToString());
		m_Detector.SetMaxScale(m_maxScale[s]);
		PlayerPrefs.SetInt(GAME_AREA_MAX_PREF, s);
		Debug.Log("Max scale = " + m_maxScale[s].ToString());
	}

	// Update is called once per frame
	void Update () {

	}
}
