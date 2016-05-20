using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using Text = UnityEngine.UI.Text;

public class ListProceduresController : MonoBehaviour {

	public GameObject ContentPanel;
	public GameObject ListItemPrefab;

	ArrayList Procedures;

	void Start () {

		Text[] texts = Canvas.FindObjectsOfType<Text> ();
		foreach (Text text in texts){
			if (text.name == "TextTitre") {
				text.text = ApplicationModel.getMachine ().getNomMachine () + " - Choix de la procédure";
			}
		}
			
		// 1. Get the data to be displayed
		Procedures = ApplicationModel.getProceduresList();

		// 2. Iterate through the data,
		//	  instantiate prefab,
		//	  set the data,
		//	  add it to panel
		foreach(string Procedure in Procedures){
			GameObject newProcedure = Instantiate(ListItemPrefab) as GameObject;
			ListMachineitemController controller = newProcedure.GetComponent<ListMachineitemController>();
			controller.MachineName.text = Procedure;
			string _name = Procedure; // Ne pas retirer, si on passe directement Machine à clickMachine on envoie tjrs la dernière valeur de Machines, vive Unity --'
			controller.Choose.onClick.AddListener(delegate{clickMachine(_name);});
			newProcedure.transform.parent = ContentPanel.transform;
			newProcedure.transform.localScale = Vector3.one;
		}
	}


	public void clickMachine(string nomProcedure) {
		ApplicationModel.setProcedure (nomProcedure);
		string scene = "procedure_" + ApplicationModel.getMachine ().getNomScene () + "_" + ApplicationModel.getProcedure ().getNomScene () + "_step_01";
		SceneManager.LoadScene (scene);
	}
		
}
