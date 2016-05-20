using UnityEngine;
using System.Collections;
using Vuforia;
using Image = UnityEngine.UI.Image;
using Text = UnityEngine.UI.Text;
using UnityEngine.SceneManagement;


public class VuforiaDetectionScript : MonoBehaviour, ITrackableEventHandler {

	private TrackableBehaviour mTrackableBehaviour;

	private string s = "";

	void Start () {
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour) {
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
	}

	//quand une image ou un modèle 3D est détecté
	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
		
			//on teste quel est l'élément détecté grâce au non du gameObject
			string nomMachine = "";
			if (gameObject.name == "ObjectTargetPorteTelephone") {
				s = "Détection du porte téléphone rose";
				nomMachine = "Porte téléphone rose";
				//Lancer avec un petit retardement la scene suivante du type procedure_portetelephone_step01
			} else if (gameObject.name == "ImageTargetStones") {
				s = "Détection de l'image avec les pierres";
				nomMachine = "Stones";
			} else if (gameObject.name == "ObjectTargetImprimante3D") {
				s = "Détection de l'imprimante 3D";
				nomMachine = "Imprimante 3D";
			}

			//On indique sur l'UI qu'on a trouvé un élément 
			//TODO Trouver s'il n'y a pas une façon plus simple de trouver un objet
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

			//On met à jour la machine dans le Modèle et on lance l'étape suivante
			ApplicationModel.setMachine(nomMachine);
			if (ApplicationModel.isProcedure) {
				SceneManager.LoadScene ("procedure_choice");
			} else if (ApplicationModel.isDiagnostic) {
				ApplicationModel.setStep (1);
				SceneManager.LoadScene ("diagnostic_" + ApplicationModel.getMachine().getNomScene() + "_step_01");
			}

		}
		else //perte de la détection d'un élément
		{
			//on met à jour l'UI 
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
				
		}
	}

	void OnGUI() {
	}



}
