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

		// Update is called once per frame
		void Update () {
			float distance = Vector3.Distance(m_marker.transform.position, m_camera.transform.position);
			m_detector.SetValue(distance);
			m_distanceText.text = distance.ToString("N");
		}
	}
}
