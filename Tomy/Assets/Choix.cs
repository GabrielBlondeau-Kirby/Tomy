using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
