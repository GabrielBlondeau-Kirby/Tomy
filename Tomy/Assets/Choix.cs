using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class Choix : MonoBehaviour {


	public void lancerProcedure() {
		ApplicationModel.isProcedure = true;
		ApplicationModel.isDiagnostic = false;
		SceneManager.LoadScene ("reconnaissance_machine");
	}

	public void lancerDiagnostic() {
		ApplicationModel.isProcedure = false;
		ApplicationModel.isDiagnostic = true;
		SceneManager.LoadScene ("reconnaissance_machine");
	}

	public void choisirManuellement() {
		SceneManager.LoadScene ("choix_machine");
	}


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

	public void diagnosticFail() {
		SceneManager.LoadScene ("diagnostic_fail");
	}

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
