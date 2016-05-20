using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Classe du modèle qui garde dans des attributs statiques les valeurs à conserver tout au long
du cycle de vie de l'application */
public class ApplicationModel {

	private static Machine machine; //machine scannée ou sélectionnée
	public static bool isProcedure = false; //mode procédure choisi (sur l'écran d'accueil)
	public static bool isDiagnostic = false; //mode diagnostic choisi (sur l'écran d'accueil)
	private static Procedure procedure; //procédure sélectionnée
	private static int step = 0; //numéro de l'étape de la procédure ou du diagnostic


	//liste des machines et des procédures disponibles
	//TODO Pour l'instant écrit en dur mais il faudrait les mettre dans un fichier texte ou une bd
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
				step = 1;
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

	public static string getStep() {
		if (step < 10) {
			return "0" + step.ToString();
		} else {
			return step.ToString();
		}
	}

	public static void nextStep(){
		step++;
	}

	public static void previousStep() {
		step--;
	}

	public static void setStep(int newStep) {
		step = newStep;
	}

}
