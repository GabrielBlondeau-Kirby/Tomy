using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Init : MonoBehaviour {


	private static ArrayList listeMachines = new ArrayList() {
		new Machine("Imprimante 3D", "ImprimanteAD", new ArrayList() { "Plateau", "Bouton" },  new ArrayList() { "Plateau", "Bouton" }),
		new Machine("Imprimante 3D - Plateau", "PlaqueImprimante", new ArrayList() { "Plaque de verre" }, new ArrayList() { "PlaqueVerre" }),
		new Machine("Porte téléphone rose", "PorteTelephoneRose", new ArrayList() {"Test 1", "Test 2" }, new ArrayList() {"Test1", "Test2" }),
		new Machine("Porte téléphone rose", "PorteTelephoneRose", new ArrayList() {"Test 1", "Test 2" }, new ArrayList() {"Test1", "Test2" }),
		new Machine("Porte téléphone rose", "PorteTelephoneRose", new ArrayList() {"Test 1", "Test 2" }, new ArrayList() {"Test1", "Test2" }),

	};

	// Use this for initialization
	void Start () {
		GameObject dropDownMachineObj = GameObject.Find ("DropdownMachine");
		Dropdown dropDownMachine = dropDownMachineObj.GetComponent<Dropdown> ();

		foreach (Machine machine in listeMachines) {
			Dropdown.OptionData elem = new Dropdown.OptionData(machine.getNomMachine());
			dropDownMachine.options.Add(elem);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static ArrayList getListeMachines() {
		return listeMachines;
	}


}
