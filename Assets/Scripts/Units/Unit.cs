/*
   Project      : GGJ20-JambonBoursin
   Author		: Yannis Beaux (Kranck), Loic Mathiot (Asterius)
   Date		    : 01 / 02 / 2020
   Description  : Represent a unit
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	public Crew.CrewController			m_crewController;

	//
	// Private
	//
	private UI.UnitUI					m_ui;

	private Objects.InteractableObject	m_interactableTarget;

	private int							m_maxHp;
	private int							m_hp;
	private bool						m_isSelected;

	private void Start()
	{
		m_ui = transform.Find("UnitUI").GetComponent<UI.UnitUI>();

		m_interactableTarget = null;

		m_maxHp = 100;

		SetHP(m_maxHp);
		SetSelected(false);
	}

	/// <summary>
	/// Set the selected status of this unit
	/// </summary>
	/// <param name="isSelected"></param>
	public void SetSelected(bool isSelected)
	{
		m_isSelected = isSelected;

		m_ui.SetDisplayed(isSelected);

		//
		// Set the highlight property of the material
		GetComponentInChildren<Renderer>().material.SetInt("_HighLight", isSelected ? 1 : 0);
	}

	/// <summary>
	/// Return true if this unit is selected, false otherwise
	/// </summary>
	/// <returns></returns>
	public bool IsSelected()
	{
		return m_isSelected;
	}

	public void SetHP(int hp)
	{
		if (hp < 0)
		{
			hp = 0;
		}

		if (hp > m_maxHp)
		{
			hp = m_maxHp;
		}

		m_hp = hp;

		m_ui.SetLifeBarFill(m_hp / (float)m_maxHp);
	}

	public int GetHP()
	{
		return m_hp;
	}

	public void Hit(int damage)
	{
		SetHP(m_hp - damage);
	}

	public void SetObjective(Vector3 destination, GameObject clickedObject)
	{
		if (m_interactableTarget != null)
		{
			m_interactableTarget.FreePlacement(this);
		}

		m_interactableTarget = clickedObject.GetComponent<Objects.InteractableObject>();

		if (m_interactableTarget != null)
		{
			if (!m_interactableTarget.GetPlacementPoint(this, out destination))
			{
				destination = m_crewController.transform.position;
			}
		}

		m_crewController.SetDestination(destination);
	}

	public Vector3 GetDestination()
	{
		return m_crewController.GetDestination();
	}
}
