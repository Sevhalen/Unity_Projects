using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour {

	// Gene for color
	public float r;
	public float g;
	public float b;

	// Ajout d'un gene pour la taille du personnage
	public float s;

	bool dead = false;

	public float timeToDie = 0;

	SpriteRenderer sRenderer;
	Collider2D sCollider;

	// Action lors d'un click sur un personnage
	void OnMouseDown()
	{
		dead = true;
		timeToDie = PopulationManager.elapsed;
		//Debug.Log("Dead at : " + timeToDie);
		sRenderer.enabled = false; // Fait disparaitre le personnage
		sCollider.enabled = false; // Rend le pesonnage non-clickable
	}

	// Use this for initialization
	void Start () {
		// Initialise les informations correspondant
		// au personnage concerné
		sRenderer = GetComponent<SpriteRenderer>();
		sCollider = GetComponent<Collider2D>();
		sRenderer.color = new Color(r,g,b);
		this.transform.localScale = new Vector3(s,s,s);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
