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
	///==================================================================================
	///PRIVATE STRUCTURES AND ENUMS
	///==================================================================================

	enum ETargetType
	{
		OBJECT,
		CHARACTER,
		NONE
	}

	struct STarget
	{
		public STarget(  ETargetType type, Objects.InteractableObject interactable
			           , EnemyController enemy)
		{
			this.type = type;
			interactableTarget = interactable;
			enemyTarget = enemy;
		}

		public ETargetType					type;

		public Objects.InteractableObject	interactableTarget;
		public EnemyController				enemyTarget;
	}

	///==================================================================================
	///PUBLIC VARIABLES
	///==================================================================================

	public Crew.CrewController			m_crewController;

	[Header("Sounds")]
	public AudioClip[]					m_setDestinationSounds;
	public AudioClip[]					m_selectedSounds;
    public AudioClip					m_deathSound;
    public AudioClip					m_repairSound;
    public AudioClip					m_urinationSound;

	///==================================================================================
	///PRIVATE VARIABLES
	///==================================================================================

	private UI.UnitUI					m_ui;

	private STarget						m_target;

	private int							m_maxHp;
	private int							m_hp;
	private bool						m_isSelected;
	private bool						m_animStopped;

	///==================================================================================
	///ENGINE METHODS
	///==================================================================================

	private void Awake()
	{
		m_ui = transform.Find("UnitUI").GetComponent<UI.UnitUI>();

		m_target = new STarget(ETargetType.NONE, null, null);

		m_maxHp = 100;

		SetHP(m_maxHp);
		SetSelected(false);

		m_animStopped = true;
	}

	private void Start()
	{
	}

	private void LateUpdate()
	{
		if (m_target.type == ETargetType.CHARACTER && m_target.enemyTarget && !IsAtRange(m_target.enemyTarget.transform.position))
		{
			m_crewController.SetDestination(m_target.enemyTarget.transform.position);
		}

		if (IsInteracting())
		{
			RotateTowardTarget(m_target.interactableTarget.transform.position);

			PlayRightAnimation();
		}
		else if(IsAttacking())
		{
			//We stop in order to attack
			m_crewController.ClearPath();

			RotateTowardTarget(m_target.enemyTarget.transform.position);

			PlayAnimation(Crew.CrewController.AnimationType.E_ATTACKING);
		}
		else
		{
			PlayAnimation(Crew.CrewController.AnimationType.E_NONE);
		}
	}


	///==================================================================================
	///PUBLIC METHODS
	///==================================================================================

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
		if (!m_animStopped && m_target.type == ETargetType.CHARACTER)
		{
			if(m_target.enemyTarget.TakeDamage(10) <= 0 || !m_target.enemyTarget)
			{
				ClearTarget();
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
		if (!m_animStopped && m_target.type == ETargetType.OBJECT)
		{
			m_target.interactableTarget.Interact(gameObject);
		}
    }

    public void SetObjective(Vector3 destination, GameObject clickedObject)
	{
		//We clear the current target before set a new one
		ClearTarget();

		if (clickedObject != null)
		{
			//If the user clicked on an interactable object
			//We attempt to reserve a placement point
			if (clickedObject.GetComponent<Objects.InteractableObject>() != null)
			{
				// Set the new target to the interactable object
				SetTarget(clickedObject.GetComponent<Objects.InteractableObject>());

				//If we fail to reserve we stay at our current position
				if (!m_target.interactableTarget.GetPlacementPoint(gameObject, out destination))
				{
					ClearTarget();
					destination = m_crewController.transform.position;
				}
			}
			else
			{
				if (clickedObject.GetComponent<EnemyController>())
				{
					SetTarget(clickedObject.GetComponent<EnemyController>());
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
		if (m_target.type != ETargetType.OBJECT)
		{
			return;
		}

		Objects.InteractableObject target = m_target.interactableTarget;

		if (target.GetComponent<Objects.InteractableBoatObject>())
		{
			if (newHP == target.m_hpMax)
			{
				ClearTarget();
			}
		}

		if (newHP == 0)
		{
			if (target.GetComponent<Objects.InteractableFire>())
			{
				ClearTarget();
			}
		}
	}

	public Vector3 GetDestination()
	{
		return m_crewController.GetDestination();
	}

	public bool IsInteracting()
	{
		if (m_target.type == ETargetType.OBJECT)
		{
			return m_crewController.GetDistanceToDestination() <= m_crewController.GetStoppingDistance();
		}

		return false;
    }

	public bool IsAttacking()
	{
		if (m_target.type == ETargetType.CHARACTER && m_target.enemyTarget)
		{
			return IsAtRange(m_target.enemyTarget.transform.position);
		}

		return false;
	}

	public bool IsAtRange(Vector3 targetPosition)
	{
		return Mathf.Abs((transform.position - targetPosition).magnitude) <= 3.0f;
	}

	public void OnDeath()
	{
		if (m_target.type == ETargetType.OBJECT)
		{
			m_target.interactableTarget.FreePlacement(gameObject);
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

	///==================================================================================
	///PRIVATE METHODS
	///==================================================================================

	private void RotateTowardTarget(Vector3 target)
	{
		Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);
		float fVal = Mathf.Min(2.0f * Time.deltaTime, 1);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, fVal);
	}

	private void ClearTarget()
	{
		if (m_target.type == ETargetType.OBJECT)
		{
			m_target.interactableTarget.FreePlacement(gameObject);
		}

		m_target.type = ETargetType.NONE;
		m_target.interactableTarget = null;
		m_target.enemyTarget = null;

		PlayAnimation(Crew.CrewController.AnimationType.E_NONE);
	}

	private void SetTarget(EnemyController enemy)
	{
		ClearTarget();

		m_target.type = ETargetType.CHARACTER;
		m_target.enemyTarget = enemy;
	}

	private void SetTarget(Objects.InteractableObject interactable)
	{
		ClearTarget();

		m_target.type = ETargetType.OBJECT;
		m_target.interactableTarget = interactable;
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
		if (m_target.type == ETargetType.OBJECT)
		{
			Objects.InteractableObject target = m_target.interactableTarget;

			if (target.GetComponent<Objects.InteractableCannon>())
			{
				if (target.m_hp != target.m_hpMax)
				{
					PlayAnimation(Crew.CrewController.AnimationType.E_REPAIRING);
				}
				else
				{
					PlayAnimation(Crew.CrewController.AnimationType.E_INTERACTING);
				}
			}
			else if (target.GetComponent<Objects.InteractableBoatObject>())
			{
				if (target.m_hp != target.m_hpMax)
				{
					PlayAnimation(Crew.CrewController.AnimationType.E_REPAIRING);
				}
				else
				{
					PlayAnimation(Crew.CrewController.AnimationType.E_NONE);
				}
			}
			else if (target.GetComponent<Objects.InteractableFire>())
			{
				PlayAnimation(Crew.CrewController.AnimationType.E_PIPIYING);
			}
			else if (target.GetComponent<Objects.InteractableResource>())
			{
				PlayAnimation(Crew.CrewController.AnimationType.E_INTERACTING);
			}
		}
	}
}
