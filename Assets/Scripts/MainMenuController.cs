using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.XR.iOS {
	public class MainMenuController : MonoBehaviour {

		public GameObject m_PlaceMarkerPanel;
		public GameObject m_ScanningPanel;
		public GameObject m_MainMenuPanel;

		private ARTrackingState m_tracking_state = ARTrackingState.ARTrackingStateNotAvailable;

		void Start () {
			UnityARSessionNativeInterface.ARSessionTrackingChangedEvent += TrackingChanged;
		}

		public void StartGame() {
			if (m_tracking_state == ARTrackingState.ARTrackingStateNormal) {
				m_PlaceMarkerPanel.SetActive(true);
			} else {
				m_ScanningPanel.SetActive(true);
			}
			m_MainMenuPanel.SetActive(false);
		}

		void TrackingChanged(UnityARCamera camera) {
			Debug.Log("Tracking changed: " + camera.trackingState.ToString());
			m_tracking_state = camera.trackingState;
		}
	}
}
