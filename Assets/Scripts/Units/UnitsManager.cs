/*
   Project      : GGJ20-JambonBoursin
   Author		: Yannis Beaux (Kranck)
   Date		    : 01 / 02 / 2020
   Description  : Manager for all the units
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class UnitsManager : MonoBehaviour
{
	[Header("Spawn")]
	public Transform m_spawnPoint;
	public Transform m_spawnStartDestination;

	[Header("Unit")]
	public GameObject m_unitPrefab;
	public AudioClip m_unitSpawnAudioClip;

	//
	//
	private List<Unit> m_units = new List<Unit>();

	// Start is called before the first frame update
	void Start()
    {
		//
		// Ensure spwan point and spawn start destination are not null
		Assert.AreNotEqual(m_spawnPoint, null);
		Assert.AreNotEqual(m_spawnStartDestination, null);

		//
		// 
		GameManager.m_instance.m_worldClickDestinationSetter.AddOnClickedCallback(OnClicked);
	}

	/// <summary>
	/// Called when the user clicked in the world and that it's a valid position for the units. It triggers the movement of the unit
	/// </summary>
	/// <param name="clickPosition"></param>
	public void OnClicked(Vector3 clickPosition, GameObject clickedObject)
	{
		//
		//
		MoveSelectedUnitsToPosition(clickPosition, clickedObject);
	}
	
	public List<Unit> GetUnits()
	{
		return m_units;
	}

	public void UnpopUnit(Unit unit)
	{
		GameManager.m_instance.m_resourcesManager.AddCrew(-1);
		m_units.Remove(unit);
		GameManager.m_instance.m_unitSelector.GetSelectedUnits().Remove(unit);
		Destroy(unit);
	}

	public void SpawnUnit()
	{
		//
		// Spawn the unit and place it
		GameObject newUnit = Instantiate(m_unitPrefab, m_spawnPoint.position, new Quaternion()) as GameObject;
		bool warped = newUnit.GetComponent<NavMeshAgent>().Warp(m_spawnPoint.position);

		//
		//
		if (!newUnit.GetComponent<AudioSource>().isPlaying)
		{
			newUnit.GetComponent<AudioSource>().PlayOneShot(m_unitSpawnAudioClip);
		}

		//
		// Set the destination of the unit
		newUnit.GetComponent<Unit>().SetObjective(m_spawnStartDestination.position + new Vector3(Random.Range(-8f, 8f), 0f, Random.Range(-8f, 8f)), null);

		//
		// Add the unit to the array of units
		m_units.Add(newUnit.GetComponent<Unit>());
	}

	public void MoveSelectedUnitsToPosition(Vector3 position, GameObject clickedObject)
	{
		//
		// Retrieve the selected units
		List<Unit> selectedUnits = GameManager.m_instance.m_unitSelector.GetSelectedUnits();

		//
		// Set the destination of the units
		float multiplier = 2f;
		float fAngle = 0f;
		foreach (Unit unit in selectedUnits)
		{
			//
			// Generate a point on the circle with the given angle
			float fX = Mathf.Sin(Mathf.Deg2Rad * fAngle);
			float fY = Mathf.Cos(Mathf.Deg2Rad * fAngle);

			//
			// Add the multiplier to offset the circle
			fX *= multiplier;
			fY *= multiplier;

			//
			// Compute the destination
			Vector3 realDestination = new Vector3(position.x + fX, position.y + 0f, position.z + fY);

			//
			// Set the objective of the unit
			unit.SetObjective(realDestination, clickedObject);

			//
			// We increase the angle
			fAngle += Random.Range(30f, 100f);

			//
			// If the angle > 360, we reset the angle and increase the multiplier
			if (fAngle >= 360)
			{
				fAngle -= 360f;
				multiplier += 1f;
			}
		}
	}
}
