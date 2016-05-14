using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class ListMachinesController : MonoBehaviour {


	public GameObject ContentPanel;
	public GameObject ListItemPrefab;

	ArrayList Machines;

	void Start () {

		// 1. Get the data to be displayed
		Machines = new ArrayList (){
			"Imprimante 3D",
			"Porte téléphone rose",
			"Stones",
			"Imprimante 3D",
			"Porte téléphone rose",
			"Stones",
			"Imprimante 3D",
			"Porte téléphone rose",
			"Stones",
			"Imprimante 3D",
			"Porte téléphone rose"
		};

		// 2. Iterate through the data,
		//	  instantiate prefab,
		//	  set the data,
		//	  add it to panel
		foreach(string Machine in Machines){
			GameObject newMachine = Instantiate(ListItemPrefab) as GameObject;
			ListMachineitemController controller = newMachine.GetComponent<ListMachineitemController>();
			controller.MachineName.text = Machine;
			string _name = Machine; // Ne pas retirer, si on passe directement Machine à clickMachine on envoie tjrs la dernière valeur de Machines, vive Unity --'
			controller.Choose.onClick.AddListener(delegate{clickMachine(_name);});
			newMachine.transform.parent = ContentPanel.transform;
			newMachine.transform.localScale = Vector3.one;
		}
	}


	public void clickMachine(string nomMachine) {
		Debug.Log(nomMachine); //TODO remove
		ApplicationModel.machine = nomMachine;
		if (ApplicationModel.isProcedure) {
			SceneManager.LoadScene ("procedure_choice");
		} else if (ApplicationModel.isDiagnostic) {
			SceneManager.LoadScene ("diagnostic_step_01");
		}

	}
}
