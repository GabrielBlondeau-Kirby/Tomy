using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Machine {

	private string nomMachine;
	private string nomScene;

	private ArrayList listeProcedures;

	public Machine(string nomMachine, string nomScene, ArrayList listeProcedures) {
		this.nomMachine = nomMachine;
		this.nomScene = nomScene;
		this.listeProcedures = listeProcedures;
	}

	public string getNomMachine(){
		return this.nomMachine;
	}

	public string getNomScene(){
		return this.nomScene;
	}

	public ArrayList getProcedures(){
		return this.listeProcedures;
	}
}
