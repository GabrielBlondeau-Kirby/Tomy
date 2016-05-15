using UnityEngine;
using System.Collections;

public class Procedure {

	private string nomProcedure;
	private string nomScene;

	public Procedure(string nomProcedure, string nomScene) {
		this.nomProcedure = nomProcedure;
		this.nomScene = nomScene;
	}

	public string getNomProcedure(){
		return this.nomProcedure;
	}

	public string getNomScene(){
		return this.nomScene;
	}

	
}
