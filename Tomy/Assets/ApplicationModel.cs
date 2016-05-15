using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ApplicationModel {

	//private static string machine = "";
	private static Machine machine;
	public static bool isProcedure = false;
	public static bool isDiagnostic = false;
	//private static string procedure = "";
	private static Procedure procedure;

	private static ArrayList listeMachines = new ArrayList (){
		new Machine("Imprimante 3D", "imprimante3d", new ArrayList() {
			new Procedure("Changement du scotch", "changer_scotch"),
			new Procedure("Procédure 2", "changer_scotch"),
			new Procedure("Procédure 3", "changer_scotch"),
			new Procedure("Procédure 4", "changer_scotch"),
			new Procedure("Procédure 5", "changer_scotch"),
			new Procedure("Procédure 6", "changer_scotch"),
		}),

		new Machine("Porte téléphone rose", "porte_tel", new ArrayList() {
			new Procedure("Test", "test"),
			new Procedure("Procédure 2", "test"),
			new Procedure("Procédure 3", "test"),
			new Procedure("Procédure 4", "test"),
			new Procedure("Procédure 5", "test"),
			new Procedure("Procédure 6", "test"),
		}),

		new Machine("Stones", "stones", new ArrayList() {
			new Procedure("Test", "test"),
			new Procedure("Procédure 2", "test"),
			new Procedure("Procédure 3", "test"),
			new Procedure("Procédure 4", "test"),
			new Procedure("Procédure 5", "test"),
			new Procedure("Procédure 6", "test"),
		}),

		new Machine("Imprimante 3D", "imprimante3d", new ArrayList() {
			new Procedure("Changement du scotch", "changer_scotch"),
			new Procedure("Procédure 2", "changer_scotch"),
			new Procedure("Procédure 3", "changer_scotch"),
			new Procedure("Procédure 4", "changer_scotch"),
			new Procedure("Procédure 5", "changer_scotch"),
			new Procedure("Procédure 6", "changer_scotch"),
		}),

		new Machine("Porte téléphone rose", "porte_tel", new ArrayList() {
			new Procedure("Test", "test"),
			new Procedure("Procédure 2", "test"),
			new Procedure("Procédure 3", "test"),
			new Procedure("Procédure 4", "test"),
			new Procedure("Procédure 5", "test"),
			new Procedure("Procédure 6", "test"),
		}),

		new Machine("Stones", "stones", new ArrayList() {
			new Procedure("Test", "test"),
			new Procedure("Procédure 2", "test"),
			new Procedure("Procédure 3", "test"),
			new Procedure("Procédure 4", "test"),
			new Procedure("Procédure 5", "test"),
			new Procedure("Procédure 6", "test"),
		}),

		new Machine("Imprimante 3D", "imprimante3d", new ArrayList() {
			new Procedure("Changement du scotch", "changer_scotch"),
			new Procedure("Procédure 2", "changer_scotch"),
			new Procedure("Procédure 3", "changer_scotch"),
			new Procedure("Procédure 4", "changer_scotch"),
			new Procedure("Procédure 5", "changer_scotch"),
			new Procedure("Procédure 6", "changer_scotch"),
		}),

		new Machine("Porte téléphone rose", "porte_tel", new ArrayList() {
			new Procedure("Test", "test"),
			new Procedure("Procédure 2", "test"),
			new Procedure("Procédure 3", "test"),
			new Procedure("Procédure 4", "test"),
			new Procedure("Procédure 5", "test"),
			new Procedure("Procédure 6", "test"),
		}),

		new Machine("Stones", "stones", new ArrayList() {
			new Procedure("Test", "test"),
			new Procedure("Procédure 2", "test"),
			new Procedure("Procédure 3", "test"),
			new Procedure("Procédure 4", "test"),
			new Procedure("Procédure 5", "test"),
			new Procedure("Procédure 6", "test"),
		})
	};

	public static Machine getMachine() {
		return machine;
	}

	public static void setMachine(string newMachine) {
		foreach (Machine m in listeMachines) {
			if (m.getNomMachine().Equals(newMachine)) {
				machine = m;
			}
		}
	}

	public static Procedure getProcedure() {
		return procedure;
	}

	public static void setProcedure(string newProcedure) {
		foreach (Procedure p in machine.getProcedures()){
			if (p.getNomProcedure().Equals(newProcedure)) {
				procedure = p;
			}
		}
	}

	public static ArrayList getMachinesList() {
		ArrayList list = new ArrayList ();
		foreach (Machine machine in listeMachines){
			list.Add (machine.getNomMachine ());
		}
		return list;
	}

	public static ArrayList getProceduresList() {
		ArrayList list = new ArrayList ();
		if (machine != null) {
			foreach (Procedure procedure in machine.getProcedures()){
				list.Add (procedure.getNomProcedure ());
			}
		}
		return list;
	}


}
