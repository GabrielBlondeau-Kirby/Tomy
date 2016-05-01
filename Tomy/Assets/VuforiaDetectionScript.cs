using UnityEngine;
using System.Collections;
using Vuforia;
using Image = UnityEngine.UI.Image;
using Text = UnityEngine.UI.Text;


public class VuforiaDetectionScript : MonoBehaviour, ITrackableEventHandler {

	private TrackableBehaviour mTrackableBehaviour;

	//private bool mShowGUIButton = false;
	//private Rect mButtonRect = new Rect(50,50,920,60);
	private string s = "";

	void Start () {
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour) {
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
	}

	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			
			//mShowGUIButton = true;


			if (gameObject.name == "ObjectTargetPorteTelephone") {
				s = "Détection du porte téléphone rose";
				//Lancer avec un petit retardement la scene suivante du type procedure_portetelephone_step01
			} else if (gameObject.name == "ImageTargetStones") {
				s = "Détection de l'image avec les pierres";

			}


			//Trouver s'il n'y a pas une façon plus simple de trouver un objet
			Image[] imgs = Canvas.FindObjectsOfType<Image>();
			foreach (Image img in imgs){
				if (img.name == "ImagePatienter") {
					img.sprite = Resources.Load<Sprite>("Images/picto_valider");
				}
			}

			Text[] texts = Canvas.FindObjectsOfType<Text> ();
			foreach (Text text in texts){
				if (text.name == "TextElementDetecte") {
					text.text = s;
				}
			}

		}
		else
		{
			//Trouver s'il n'y a pas une façon plus simple de trouver un objet
			Image[] imgs = Canvas.FindObjectsOfType<Image>();
			foreach (Image img in imgs){
				if (img.name == "ImagePatienter") {
					img.sprite = Resources.Load<Sprite>("Images/picto_patienter");
				}
			}

			Text[] texts = Canvas.FindObjectsOfType<Text> ();
			foreach (Text text in texts){
				if (text.name == "TextElementDetecte") {
					text.text = "";
				}
			}

			//mShowGUIButton = false;
		}
	}

	void OnGUI() {
		/*if (mShowGUIButton) {
			// draw the GUI button
			if (GUI.Button(mButtonRect, s)) {
				// do something on button click 
			}
		}*/
	}
}
