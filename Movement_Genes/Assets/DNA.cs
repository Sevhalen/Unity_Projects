using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA {

	List<int> genes = new List<int>();
	int dnaLength = 0;
	int maxValues = 0;

	// Initialisation d'un gene
	public DNA(int l, int v)
	{
		dnaLength = l;
		maxValues = v;
		SetRandom();
	}

	public void SetRandom()
	{
		genes.Clear(); // Vide la liste de genes
		for(int i = 0; i < dnaLength; i++)
		{
			genes.Add(Random.Range(0, maxValues));
		}
	}

	public void SetInt(int pos, int value)
	{
		genes[pos] = value;
	}

	// Méthode de combinaison des ADN des parents :
	// la moitié provenant d'un parent, l'autre de l'autre parent
	public void Combine(DNA d1, DNA d2)
	{
		for(int i = 0; i < dnaLength; i++)
		{
			if(i < dnaLength/2.0)
			{
				int c = d1.genes[i];
				genes[i] = c;
			}
			else
			{
				int c = d2.genes[i];
				genes[i] = c;
			}
		}
	}

	// Méthode de mutation : attribution d'une valeur aléatoire
	// sur un gene aléatoire
	public void Mutate()
	{
		genes[Random.Range(0,dnaLength)] = Random.Range(0, maxValues);
	}

	public int GetGene(int pos)
	{
		return genes[pos];
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
