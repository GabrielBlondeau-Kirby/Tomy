using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReturnButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Escape)){

			string name = SceneManager.GetActiveScene ().name;

			if (name.Contains ("reconnaissance_machine")) {
				SceneManager.LoadScene ("accueil");
			} else if (name.Contains ("choix_machine")) {
				SceneManager.LoadScene ("reconnaissance_machine");
			} else if (name.Contains ("procedure_choice")) {
				SceneManager.LoadScene ("reconnaissance_machine");
			} else if (name.StartsWith("procedure_")){

				ApplicationModel.previousStep ();
				if (ApplicationModel.getStep().Equals("00")) {
					SceneManager.LoadScene ("procedure_choice");
				}
				else {
					string scene = "procedure_" + ApplicationModel.getMachine ().getNomScene () + "_" + ApplicationModel.getProcedure ().getNomScene () + "_step_" +  ApplicationModel.getStep();
					SceneManager.LoadScene (scene);
				}	
			} else if (name.StartsWith("diagnostic_")){
				ApplicationModel.previousStep ();
				if (ApplicationModel.getStep().Equals("00")) {
					SceneManager.LoadScene ("reconnaissance_machine");
				}
				else {
					string scene = "diagnostic_" + ApplicationModel.getMachine ().getNomScene () + "_step_" +  ApplicationModel.getStep();
					SceneManager.LoadScene (scene);
				}	
			}

			
		}
	}
}
