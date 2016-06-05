using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Choice : MonoBehaviour {

	string arrowProperty = "position";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	ArrayList listeMachine = Init.getListeMachines();

	public void dropDownMachineOnValueChanged(int value) {

		GameObject dropDownMachineObj = GameObject.Find ("DropdownMachine");
		Dropdown dropDownMachine = dropDownMachineObj.GetComponent<Dropdown> ();
		int value2 = dropDownMachine.value;


		GameObject dropDownMontrerObj = GameObject.Find ("DropdownMontrer");
		Dropdown dropDownMontrer = dropDownMontrerObj.GetComponent<Dropdown> ();

		if (value2 != 0) {
			Machine m = (Machine) listeMachine[value2 - 1];
			dropDownMontrer.options.Clear ();
			Dropdown.OptionData elemInit = new Dropdown.OptionData ("- Choix d'un élément -");
			dropDownMontrer.options.Add (elemInit);
			foreach (string element in m.getListeElements()) {
				Dropdown.OptionData elem = new Dropdown.OptionData (element);
				dropDownMontrer.options.Add (elem);
			}

			GameObject test = GameObject.Find ("Panel").transform.Find ("ButtonAfficherFleche").gameObject;
			test.SetActive (true);

		} else {
			dropDownMontrer.options.Clear ();
			Dropdown.OptionData elemInit = new Dropdown.OptionData ("- Choix d'un élément -");
			dropDownMontrer.options.Add (elemInit);

			GameObject.Find ("ButtonAfficherFleche").gameObject.SetActive (false);

		}

	}

	public void dropDownElementOnValueChanged(int value) {




	}

	public void AfficherCacher() {

		GameObject btnAfficherObj = GameObject.Find ("TextButtonAfficher");
		Text textBtnAfficher = btnAfficherObj.GetComponent<Text> ();
		if (textBtnAfficher.text.Equals ("Afficher")) {
			textBtnAfficher.text = "Cacher";
		} else {
			textBtnAfficher.text = "Afficher";
		}

		GameObject dropDownMachineObj = GameObject.Find ("DropdownMachine");
		Dropdown dropDownMachine = dropDownMachineObj.GetComponent<Dropdown> ();
		int valueMachine = dropDownMachine.value;


		GameObject dropDownMontrerObj = GameObject.Find ("DropdownMontrer");
		Dropdown dropDownMontrer = dropDownMontrerObj.GetComponent<Dropdown> ();
		int valueElement = dropDownMontrer.value;

		Machine machine = (Machine)listeMachine [valueMachine - 1];
		string element = (string)machine.getListeElementsObject() [valueElement - 1];

		GameObject networkManagerObject = GameObject.Find ("NetworkManager");
		NetworkManager networkManager = networkManagerObject.GetComponent<NetworkManager> ();
		networkManager.sendMessageSurimpression (machine.getNomMachineObjectTarget(), element);
	}


	public void setArrowProperty(string property) {
		this.arrowProperty = property;
	}

	public void changeArrowProperty(string axisOperation) {

		GameObject dropDownMachineObj = GameObject.Find ("DropdownMachine");
		Dropdown dropDownMachine = dropDownMachineObj.GetComponent<Dropdown> ();
		int valueMachine = dropDownMachine.value;
		Machine machine = (Machine)listeMachine [valueMachine - 1];

		GameObject networkManagerObject = GameObject.Find ("NetworkManager");
		NetworkManager networkManager = networkManagerObject.GetComponent<NetworkManager> ();
		networkManager.sendMessageChangeArrowProperty(machine.getNomMachineObjectTarget(), this.arrowProperty, axisOperation);
	}


}
