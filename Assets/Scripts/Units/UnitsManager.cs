/*
   Project      : GGJ20-JambonBoursin
   Author		: Yannis Beaux (Kranck)
   Date		    : 01 / 02 / 2020
   Description  : Manager for all the units
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
	public List<Unit> m_units = new List<Unit>();

	// Start is called before the first frame update
	void Start()
    {
		//
		// 
		GameManager.m_instance.m_worldClickDestinationSetter.AddOnClickedCallback(OnClicked);
	}

    // Update is called once per frame
    void Update()
    {
        
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

	public void MoveSelectedUnitsToPosition(Vector3 position, GameObject clickedObject)
	{
		//
		// Retrieve the selected units
		List<Unit> selectedUnits = GameManager.m_instance.m_unitSelector.GetSelectedUnits();

		//
		// Set the destination of the units
		foreach (Unit unit in selectedUnits)
		{
			Vector3 realDestination = new Vector3(position.x + Random.Range(0f, 3f), position.y + Random.Range(0f, 3f), position.z + Random.Range(0f, 3f));

			unit.SetObjective(realDestination, clickedObject);
		}
	}
}
