﻿/*
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

	[Header("Sounds")]
	public AudioClip[] m_setDestinationSounds;
	public AudioClip[] m_selectedSounds;
    public AudioClip m_deathSound;
    public AudioClip m_repairSound;
    public AudioClip m_urinationSound;

	//
	// Private
	//
	private UI.UnitUI					m_ui;

	private Objects.InteractableObject	m_interactableTarget;
	private EnemyController				m_enemyTarget;

	private int							m_maxHp;
	private int							m_hp;
	private bool						m_isSelected;
	private bool						m_animStopped;

	private void Start()
	{
		m_ui = transform.Find("UnitUI").GetComponent<UI.UnitUI>();

		m_interactableTarget = null;

		m_maxHp = 100;

		SetHP(m_maxHp);
		SetSelected(false);

		m_animStopped = true;
	}

	private void LateUpdate()
	{
		if (m_enemyTarget && !IsAtRange(m_enemyTarget.transform.position))
		{
			m_crewController.SetDestination(m_enemyTarget.transform.position);
		}

		if (IsInteracting())
		{
			RotateTowardTarget(m_interactableTarget.transform.position);

			PlayRightAnimation();
		}
		else if(IsAttacking())
		{
			//We stop in order to attack
			m_crewController.ClearPath();

			RotateTowardTarget(m_enemyTarget.transform.position);

			PlayAnimation(Crew.CrewController.AnimationType.E_ATTACKING);
		}
		else
		{
			PlayAnimation(Crew.CrewController.AnimationType.E_NONE);
		}
	}

	/// <summary>
	/// Set the selected status of this unit
	/// </summary>
	/// <param name="isSelected"></param>
	public void SetSelected(bool isSelected)
	{
		if (!m_ui)
			return;

		//
		// Play a random sound
		if (!m_isSelected && isSelected)
		{
			if (!GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().PlayOneShot(m_selectedSounds[Random.Range(0, m_selectedSounds.Length)]);
			}
		}

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

	/// <summary>
	/// Take damages from EnemyController
	/// </summary>
	/// <returns>Remaining hp</returns>
	public int TakeDamages(int damages)
	{
		SetHP(m_hp - damages);

		if (m_hp <= 0)
		{
			PlayDeathSound();
			GameManager.m_instance.m_unitManager.UnpopUnit(this);
			Destroy(gameObject);
		}

		return m_hp;
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

    public void Attack()
    {
		if (!m_animStopped)
		{
			if(m_enemyTarget.TakeDamage(10) <= 0)
			{
				m_enemyTarget = null;
				PlayAnimation(Crew.CrewController.AnimationType.E_NONE);
				m_crewController.SetDestination(m_crewController.transform.position);
			}
		}
    }

    public void Repair()
    {
		Interact();
    }

    public void Extenguish()
    {
		Interact();
	}

    public void Interact()
    {
		if (!m_animStopped)
		{
			m_interactableTarget?.Interact(gameObject);
		}
    }

    public void SetObjective(Vector3 destination, GameObject clickedObject)
	{
		//If an interactable target is already set
		//we inform it that we let our placement position
		if (m_interactableTarget != null)
		{
			m_interactableTarget.FreePlacement(gameObject);
		}

		if (clickedObject != null)
		{
			//If the user clicked on an interactable object
			//We attempt to reserve a placement point
			if (clickedObject.GetComponent<Objects.InteractableObject>() != null)
			{
				if (m_enemyTarget != null)
				{
					PlayAnimation(Crew.CrewController.AnimationType.E_NONE);
					m_enemyTarget = null;
				}

				//
				// Store the interactable
				Objects.InteractableObject oldTarget = m_interactableTarget;
				m_interactableTarget = clickedObject.GetComponent<Objects.InteractableObject>();

				if (oldTarget != m_interactableTarget)
				{
					PlayAnimation(Crew.CrewController.AnimationType.E_NONE);
				}

				//If we fail to reserve we stay at our current position
				if (!m_interactableTarget.GetPlacementPoint(gameObject, out destination))
				{
					m_interactableTarget = null;
					destination = m_crewController.transform.position;
				}
			}
			else
			{
				m_interactableTarget = null;
				m_enemyTarget = null;
				PlayAnimation(Crew.CrewController.AnimationType.E_NONE);

				if (clickedObject.GetComponent<EnemyController>())
				{
					m_enemyTarget = clickedObject.GetComponent<EnemyController>();
				}
			}
		}

		m_crewController.SetDestination(destination);

		//
		// Play a sound
		if (!GetComponent<AudioSource>().isPlaying)
		{
			GetComponent<AudioSource>().PlayOneShot(m_setDestinationSounds[Random.Range(0, m_setDestinationSounds.Length)]);
		}		
	}

	public void OnTargetHpChanged(int newHP)
	{
		if (m_interactableTarget.GetComponent<Objects.InteractableBoatObject>())
		{
			if (newHP == m_interactableTarget.m_hpMax)
			{
				PlayAnimation(Crew.CrewController.AnimationType.E_NONE);
			}
		}

		if (newHP == 0)
		{
			if (m_interactableTarget.GetComponent<Objects.InteractableFire>())
			{
				m_crewController.SetAnimation(Crew.CrewController.AnimationType.E_NONE);
				m_interactableTarget = null;
			}
		}
	}

	public Vector3 GetDestination()
	{
		return m_crewController.GetDestination();
	}

	public bool IsInteracting()
	{
		if (m_interactableTarget != null)
		{
			return m_crewController.GetDistanceToDestination() <= m_crewController.GetStoppingDistance();
		}

		return false;
    }

	public bool IsAttacking()
	{
		if (m_enemyTarget != null)
		{
			return IsAtRange(m_enemyTarget.transform.position);
		}

		return false;
	}

	public bool IsAtRange(Vector3 targetPosition)
	{
		return Mathf.Abs((transform.position - targetPosition).magnitude) <= 3.0f;
	}

	public void OnDeath()
	{
		if (m_interactableTarget != null)
		{
			m_interactableTarget.FreePlacement(gameObject);
		}
	}

    public void PlayDeathSound()
    {
		AudioSource.PlayClipAtPoint(m_deathSound, Camera.main.transform.position, 0.4f);
    }

    public void PlayRepairSound()
    {
        GetComponent<AudioSource>().clip = m_repairSound;
        GetComponent<AudioSource>().Play();
    }

    public void PlayUrinationSound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio.clip != m_urinationSound)
        {
            audio.clip = m_urinationSound;
            audio.Play();
        }
        else if (!audio.isPlaying)
        {
            audio.Play();
        }
    }

    public void StopSounds()
    {
        GetComponent<AudioSource>().Stop();
    }

	private void RotateTowardTarget(Vector3 target)
	{
		Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);
		float fVal = Mathf.Min(2.0f * Time.deltaTime, 1);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, fVal);
	}

	private void PlayAnimation(Crew.CrewController.AnimationType animationType)
	{
		if (animationType == Crew.CrewController.AnimationType.E_NONE)
		{
			m_animStopped = true;
		}
		else
		{
			m_animStopped = false;
		}

		m_crewController.SetAnimation(animationType);
	}

	private void PlayRightAnimation()
	{
		if (m_interactableTarget.GetComponent<Objects.InteractableCannon>())
		{
			if (m_interactableTarget.m_hp != m_interactableTarget.m_hpMax)
			{
				PlayAnimation(Crew.CrewController.AnimationType.E_REPAIRING);
			}
			else
			{
				PlayAnimation(Crew.CrewController.AnimationType.E_INTERACTING);
			}
		}
		else if (m_interactableTarget.GetComponent<Objects.InteractableBoatObject>())
		{
			if (m_interactableTarget.m_hp != m_interactableTarget.m_hpMax)
			{
				PlayAnimation(Crew.CrewController.AnimationType.E_REPAIRING);
			}
			else
			{
				PlayAnimation(Crew.CrewController.AnimationType.E_NONE);
			}
		}
		else if (m_interactableTarget.GetComponent<Objects.InteractableFire>())
		{
			PlayAnimation(Crew.CrewController.AnimationType.E_PIPIYING);
		}
		else if (m_interactableTarget.GetComponent<Objects.InteractableResource>())
		{
			PlayAnimation(Crew.CrewController.AnimationType.E_INTERACTING);
		}
	}
}
