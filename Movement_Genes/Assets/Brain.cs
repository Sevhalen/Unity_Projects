// Brain remplace le contrôle des personnages dans le jeu
// Réalise l'interface entre l'ADN et le personnage
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson; // Donne accès aux contrôles du personnage

[RequireComponent(typeof (ThirdPersonCharacter))] // Assurance de la présence d'un personnage
public class Brain : MonoBehaviour {

	public int DNALength = 1; // Fixation sur 1 gene pour montrer un exemple
	public float timeAlive;
	public DNA dna;

	// Récupération du personnage
	private ThirdPersonCharacter m_Character;
	private Vector3 m_Move;
	private bool m_Jump;
	bool alive = true;

	// Méthode pour "tuer" le personnage s'il touche un objet
	// indiqué comme "dead"
	// Typiquement, le sol blanc dans l'exemple dispose d'un tag "dead"
	void OnCollisionEnter(Collision obj)
	{
		if(obj.gameObject.tag == "dead")
		{
			alive = false;
		}
	}

	// Méthode d'initiamlisation s'executant lors de l'instanciation
	// d'un prefab
	public void Init()
	{
		// Initialisation de l'ADN
		// 0 forward
		// 1 back
		// 2 left
		// 3 right
		// 4 jump
		// 5 crouch
		dna = new DNA(DNALength,6);
		m_Character = GetComponent<ThirdPersonCharacter>();
		timeAlive = 0;
		alive = true;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Remplacement des mouvements du personnage par des mouvements commandés
	// par le code
	private void FixedUpdate()
	{
		// Lecture de l'ADN
		float h = 0;
		float v = 0;
		bool crouch = false;
		if(dna.GetGene(0) == 0) v = 1;
		else if(dna.GetGene(0) == 1) v = -1;
		else if(dna.GetGene(0) == 2) h = -1;
		else if(dna.GetGene(0) == 3) h = 1;
		else if(dna.GetGene(0) == 4) m_Jump = true;
		else if(dna.GetGene(0) == 5) crouch = true;

		m_Move = v*Vector3.forward + h*Vector3.right;
		m_Character.Move(m_Move, crouch, m_Jump);
		m_Jump = false;
		if(alive)
			timeAlive += Time.deltaTime;
	}
}
