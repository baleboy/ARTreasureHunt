using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityEngine.XR.iOS {
	public class GameLogic : MonoBehaviour {

		public DetectorController m_detector;
		public GameObject m_marker;
		public GameObject m_camera;
		public GameObject m_MainMenuPanel;
		public GameObject m_ScanningPanel;
		public GameObject m_PlaceMarkerPanel;
		public GameObject m_SettingsPanel;
		public Dropdown m_AreaDropdown;

		public Text m_distanceText;
		public Text m_debugText;

		enum State {Start, Scanning, Placing, Searching};

		private State m_state = State.Start;
		private ARTrackingState m_tracking_state = ARTrackingState.ARTrackingStateNotAvailable;

		const string GAME_AREA_MAX_PREF = "GameAreaSizeMax";

		float[] m_maxScale = {5f, 15f, 50f};


		void Start () {
			int scaleOption = PlayerPrefs.GetInt(GAME_AREA_MAX_PREF);
			m_AreaDropdown.value = scaleOption;
			m_detector.SetScale(0.2f, m_maxScale[scaleOption]);
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
		}

		public void OnSettingsButton() {
			m_MainMenuPanel.SetActive(false);
			m_SettingsPanel.SetActive(true);
		}

		public void SetGameAreaSize(int s) {
			Debug.Log("Max scale option value = " + s.ToString());
			m_detector.SetMaxScale(m_maxScale[s]);
			PlayerPrefs.SetInt(GAME_AREA_MAX_PREF, s);
			Debug.Log("Max scale = " + m_maxScale[s].ToString());
		}

		// Update is called once per frame
		void Update () {
			float distance = Vector3.Distance(m_marker.transform.position, m_camera.transform.position);
			m_detector.SetValue(distance);
			m_distanceText.text = distance.ToString("N");
		}
	}
}
