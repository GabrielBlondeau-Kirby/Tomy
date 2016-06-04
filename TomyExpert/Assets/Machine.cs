using UnityEngine;
using System.Collections;

public class Machine {

	private string nomMachine;
	private string nomMachineObjectTarget;
	private ArrayList listeElements;
	private ArrayList listeElementsObject;

	public Machine(string nomMachine, string nomMachineObjectTarget, ArrayList listeElements, ArrayList listeElementsObject) {
		this.nomMachine = nomMachine;
		this.nomMachineObjectTarget = nomMachineObjectTarget;
		this.listeElements = listeElements;
		this.listeElementsObject = listeElementsObject;
	}

	public string getNomMachine() {
		return this.nomMachine;
	}

	public ArrayList getListeElements() {
		return this.listeElements;
	}

	public string getNomMachineObjectTarget() {
		return this.nomMachineObjectTarget;
	}

	public ArrayList getListeElementsObject() {
		return this.listeElementsObject;
	}
}
