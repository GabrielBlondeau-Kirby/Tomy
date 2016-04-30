using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Choix : MonoBehaviour {


	public void lancerProcedure() {
		SceneManager.LoadScene ("procedure_step_01");
	}

	public void lancerDiagnostic() {
		SceneManager.LoadScene ("diagnostic_step_01");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
