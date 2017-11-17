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

		public Text m_distanceText;
		public Text m_debugText;

		enum State {Start, Scanning, Placing, Searching};

		private State m_state = State.Start;
		private ARTrackingState m_tracking_state = ARTrackingState.ARTrackingStateNotAvailable;

		// Use this for initialization
		void Start () {
			m_detector.SetScale(0.2f,5);
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
		}

		void OnEnable()
    {
        UnityARSessionNativeInterface.ARSessionTrackingChangedEvent += TrackingChanged;
    }

		void TrackingChanged(UnityARCamera camera) {
			Debug.Log("Tracking changed");
			m_tracking_state = camera.trackingState;

			if (camera.trackingState == ARTrackingState.ARTrackingStateNormal) {
				m_debugText.text = "State: Normal (" + camera.trackingState.ToString() + ")";
				if (m_state == State.Scanning) {
					m_ScanningPanel.SetActive(false);
					m_PlaceMarkerPanel.SetActive(true);
				}
			}
			if (camera.trackingState == ARTrackingState.ARTrackingStateLimited) {
				m_debugText.text = "State: Limited (" + camera.trackingState.ToString() + ")";
			}
			// m_debugText.text = camera.trackingState.ToString() + ", " + camera.trackingReason.ToString();
		}

		public void OnStartButton() {
			if (m_tracking_state == ARTrackingState.ARTrackingStateNormal) {
				m_PlaceMarkerPanel.SetActive(true);
				m_state = State.Placing;
			} else {
				m_ScanningPanel.SetActive(true);
				m_state = State.Scanning;
			}
			m_MainMenuPanel.SetActive(false);
		}

		public void OnSettingsButton() {
			m_MainMenuPanel.SetActive(false);
			m_SettingsPanel.SetActive(true);
		}

		public void SetGameAreaSize(int s) {
			Debug.Log("Max scale option value = " + s.ToString());
			float maxScale = 5.0f;
			switch(s){
				case 0:
					maxScale = 5.0f;
					break;
				case 1:
					maxScale = 15.0f;
					break;
				case 2:
					maxScale = 50.0f;
					break;
			}
			m_detector.SetMaxScale(maxScale);
			Debug.Log("Max scale = " + maxScale.ToString());
		}

		// Update is called once per frame
		void Update () {
			float distance = Vector3.Distance(m_marker.transform.position, m_camera.transform.position);
			m_detector.SetValue(distance);
			m_distanceText.text = distance.ToString("N");
		}
	}
}
