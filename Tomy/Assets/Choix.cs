using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

/* Méthodes appliquées à des boutons de l'UI*/
public class Choix : MonoBehaviour {

	//bouton accueil procédure
	public void lancerProcedure() {
		ApplicationModel.isProcedure = true;
		ApplicationModel.isDiagnostic = false;
		SceneManager.LoadScene ("reconnaissance_machine");
	}

	//bouton accueil diagnostic
	public void lancerDiagnostic() {
		ApplicationModel.isProcedure = false;
		ApplicationModel.isDiagnostic = true;
		SceneManager.LoadScene ("reconnaissance_machine");
	}

	//bouton choisir manuellement sur la page de reconnaissance de la machine
	public void choisirManuellement() {
		SceneManager.LoadScene ("choix_machine");
	}

	//boutons ">" sur les pages de procédure
	//boutons "non" sur les pages de diagnostic
	public void nextStep() {
		ApplicationModel.nextStep ();

		if (ApplicationModel.isProcedure) {
			string scene = "procedure_" + ApplicationModel.getMachine ().getNomScene () + "_" + ApplicationModel.getProcedure ().getNomScene () + "_step_" +  ApplicationModel.getStep();
			SceneManager.LoadScene (scene);
		} else {
			string scene = "diagnostic" + ApplicationModel.getMachine ().getNomScene () + "_step_" +  ApplicationModel.getStep();
			SceneManager.LoadScene (scene);
		}

	}

	//Dernier bouton non, qui marque la fin du diagnostic
	public void diagnosticFail() {
		SceneManager.LoadScene ("diagnostic_fail");
	}

	//bouton "<" sur les pages de procédure et de diagnostic
	public void previousStep() {
		ApplicationModel.previousStep ();
		if (ApplicationModel.getStep().Equals("00")) {
			if (ApplicationModel.isProcedure) {
				SceneManager.LoadScene ("procedure_choice");
			} else {
				SceneManager.LoadScene ("reconnaissance_machine");
			}

		}
		else {
			if (ApplicationModel.isProcedure) {
				string scene = "procedure_" + ApplicationModel.getMachine ().getNomScene () + "_" + ApplicationModel.getProcedure ().getNomScene () + "_step_" +  ApplicationModel.getStep();
				SceneManager.LoadScene (scene);
			} else {
				string scene = "diagnostic" + ApplicationModel.getMachine ().getNomScene () + "_step_" +  ApplicationModel.getStep();
				SceneManager.LoadScene (scene);
			}

		}	
	}

	//bouton oui sur la page de diagnostic (on donne en paramètre le nom de la procédure à appeler
	public void goToProcedure(string nomProcedure) {
		ApplicationModel.setProcedure (nomProcedure);
		string scene = "procedure_" + ApplicationModel.getMachine ().getNomScene () + "_" + ApplicationModel.getProcedure ().getNomScene () + "_step_01";
		SceneManager.LoadScene (scene);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
}
