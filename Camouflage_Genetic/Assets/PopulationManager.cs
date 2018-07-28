using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Librairies contenant des fonctions de tri des listes

public class PopulationManager : MonoBehaviour {

	public GameObject personPrefab;
	public int populationSize = 10;
	List<GameObject> population = new List<GameObject>();
	public static float elapsed = 0;
	int trialTime = 10;
	int generation = 1;

	// Affichage d'informations à l'écran
	GUIStyle guiStyle = new GUIStyle();
	void OnGUI()
	{
		guiStyle.fontSize = 50;
		guiStyle.normal.textColor = Color.white;
		GUI.Label(new Rect(10, 10, 100, 20), "Generation : " + generation, guiStyle);
		GUI.Label(new Rect(10, 65, 100, 20), "Trial Time : " + (int)elapsed, guiStyle);
	}

	// Use this for initialization
	void Start () {
		for(int i = 0; i < populationSize; i++)
		{
			Vector3 pos = new Vector3(Random.Range(-9,9),Random.Range(-4.5f,4.5f),0);
			GameObject go = Instantiate(personPrefab, pos, Quaternion.identity);
			go.GetComponent<DNA>().r = Random.Range(0.0f,1.0f);
			go.GetComponent<DNA>().g = Random.Range(0.0f,1.0f);
			go.GetComponent<DNA>().b = Random.Range(0.0f,1.0f);
			go.GetComponent<DNA>().s = Random.Range(0.05f,0.5f);

			// Correction : remplacé par une ligne dans l'initilisation de l'objet enfant
			//go.transform.localScale = new Vector3(go.GetComponent<DNA>().s,go.GetComponent<DNA>().s,0);

			population.Add(go);
		}
	}
	
	// Un GameObject est un objet représentant 1 personnage
	GameObject Breed(GameObject parent1, GameObject parent2)
	{
		// Attribution d'une position aléatoire au nouvel enfant
		Vector3 pos = new Vector3(Random.Range(-9,9),Random.Range(-4.5f,4.5f),0);
		// Création de l'objet enfant
		GameObject offspring = Instantiate(personPrefab, pos, Quaternion.identity);
		DNA dna1 = parent1.GetComponent<DNA>();
		DNA dna2 = parent2.GetComponent<DNA>();
		// Mixage des ADN des parents
		// Ou Mutation de l'ADN (attribution d'une couleur aléatoire)
		if(Random.Range(0,1000) > 5)
		{
			offspring.GetComponent<DNA>().r = Random.Range(0,10) < 5 ? dna1.r : dna2.r;
			offspring.GetComponent<DNA>().g = Random.Range(0,10) < 5 ? dna1.g : dna2.g;
			offspring.GetComponent<DNA>().b = Random.Range(0,10) < 5 ? dna1.b : dna2.b;
			offspring.GetComponent<DNA>().s = Random.Range(0,10) < 5 ? dna1.s : dna2.s;
		}
		else
		{
			offspring.GetComponent<DNA>().r = Random.Range(0.0f,1.0f);
			offspring.GetComponent<DNA>().g = Random.Range(0.0f,1.0f);
			offspring.GetComponent<DNA>().b = Random.Range(0.0f,1.0f);
			offspring.GetComponent<DNA>().s = Random.Range(0.05f,0.5f);
		}
		
		// Mise à jour du scale de l'enfant
		// Correction : remplacé par une ligne dans l'initilisation de l'objet enfant
		//offspring.transform.localScale = new Vector3(offspring.GetComponent<DNA>().s,offspring.GetComponent<DNA>().s,0);

		return offspring;
	}


	void BreedNewPopulation()
	{
		// Creation d'une nouvelle population
		List<GameObject> newPopulation = new List<GameObject>();
		// Tri de la population précédente par le temps avant click
		// Modification de OrderBy par OrderByDescending pour conserver les
		// premiers selectionnés au lieu des derniers
		List<GameObject> sortedList = population.OrderByDescending(o => o.GetComponent<DNA>().timeToDie).ToList();

		population.Clear(); // Vidage de la liste population actuelle
		// Remplissage de la nouvelle population à partir de la moitié la plus performante
		// de l'ancienne génération (arbitrairement la moitié, pourrait être plus ou moins)
		for(int i = (int)(sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++)
		{
			population.Add(Breed(sortedList[i], sortedList[i+1]));
			population.Add(Breed(sortedList[i+1], sortedList[i]));
		}

		// Destruction de la liste triée
		for(int i = 0; i < sortedList.Count; i++)
		{
			Destroy(sortedList[i]);
		}
		generation++;
	}

	// Update is called once per frame
	void Update () {
		elapsed += Time.deltaTime;
		if(elapsed > trialTime)
		{
			BreedNewPopulation();
			elapsed = 0;
		}
		
	}
}
