using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
			population.Add(go);
		}
		
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
