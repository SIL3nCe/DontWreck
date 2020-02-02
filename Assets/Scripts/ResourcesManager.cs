/*
   Project      : GGJ20-JambonBoursin
   Author		: Yannis Beaux (Kranck)
   Date		    : 01 / 02 / 2020
   Description  : Manages the in resources of the player
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ResourcesManager : MonoBehaviour
{
	[Header("Default values")]
	public int m_startWoodCount;			//< The start number of wood
	public int m_startCoalCount;			//< The start number of coal
	public int m_startJambonBoursinCount;	//< The start number of jambon boursin
	public int m_startCrewCount;            //< The start number of crew
	public int m_startPlayerLifePoints;
	public int m_startEnemiesLifePoints;
	public int m_enemiesLifePointsIncrements;

	[Header("GUI")]
	public InGameHUDGUIManager m_inGameHUDGUIManager;
	public EnnemiesAndPlayerLifeGUIManager m_lifePointsGUIManager;

	//
	// Private
	//
	private int m_currentWoodCount;
	private int m_currentCoalCount;
	private int m_currentJambonBoursinCount;
	private int m_currentCrewCount;
	private int m_currentPlayerLifePoints;
	private int m_currentEnemiesLifePoints;

	public void Start()
	{
		//if (ES3.KeyExists("playerWoodCount"))
		//{
		//	m_startWoodCount = ES3.Load<int>("playerWoodCount");
		//}

		//
		// We ensurr the in game HUD GUI manager is not null
		Assert.IsNotNull(m_inGameHUDGUIManager);

		//
		// Set the current number of resources
		m_currentWoodCount = m_startWoodCount;
		m_currentCoalCount = m_startCoalCount;
		m_currentJambonBoursinCount = m_startJambonBoursinCount;
		m_currentCrewCount = m_startCrewCount;
		m_currentPlayerLifePoints = m_startPlayerLifePoints;
		m_currentEnemiesLifePoints = m_startEnemiesLifePoints;

		//
		// Update the GUI according to the current values
		m_inGameHUDGUIManager.SetWoodCount(m_currentWoodCount);
		m_inGameHUDGUIManager.SetCoalCount(m_currentCoalCount);
		m_inGameHUDGUIManager.SetJambonBoursinCount(m_currentJambonBoursinCount);
		m_inGameHUDGUIManager.SetCrewCount(m_currentCrewCount);

		//
		// Spawn the units with a 5s delay
		Invoke("SpawnStartUnits", 5f);

		//
		// Initialize the lifepoints
		m_lifePointsGUIManager.SetPlayerHP(m_currentPlayerLifePoints, m_startPlayerLifePoints);
		m_lifePointsGUIManager.SetEnemiesHP(m_currentEnemiesLifePoints, m_startEnemiesLifePoints);
	}

	public void OnDisable()
	{
		//
		//
		//ES3.Save<int>("playerWoodCount", m_currentWoodCount);
	}

	public void SpawnStartUnits()
	{
		//
		// Spawn all the units
		for (int unit = 0; unit < m_currentCrewCount; ++unit)
		{
			GameManager.m_instance.m_unitManager.Invoke("SpawnUnit", unit * 0.4f);
		}
	}

	/// <summary>
	/// Add wood the the current wood count
	/// </summary>
	/// <param name="count">The count of wood to add</param>
	public void AddWood(int count)
	{
		// update count
		m_currentWoodCount += count;

		// Update GUI
		m_inGameHUDGUIManager.SetWoodCount(m_currentWoodCount);
	}
	
	/// <summary>
	/// Add coal to the current count
	/// </summary>
	/// <param name="count">The number of coal to add</param>
	public void AddCoal(int count)
	{
		// update count
		m_currentCoalCount += count;

		// Update GUI
		m_inGameHUDGUIManager.SetCoalCount(m_currentCoalCount);
	}

	/// <summary>
	/// Add jambon boursin 
	/// </summary>
	/// <param name="count">The number of jambon boursin to add</param>
	public void AddJambonBoursin(int count)
	{
		// update count
		m_currentJambonBoursinCount += count;

		// Update GUI
		m_inGameHUDGUIManager.SetJambonBoursinCount(m_currentJambonBoursinCount);
	}

	/// <summary>
	/// Add count crew members 
	/// </summary>
	/// <param name="count">Number of crew to add</param>
	public void AddCrew(int count)
	{
		// update count
		m_currentCrewCount += count;

		// Update GUI
		m_inGameHUDGUIManager.SetCrewCount(m_currentCrewCount);

		//
		// Spawn all the units
		for (int unit = 0; unit < count; ++unit)
		{
			GameManager.m_instance.m_unitManager.Invoke("SpawnUnit", unit);
		}

		if (m_currentCoalCount <= 0)
		{
			Application.Quit();
		}
	}

	/// <summary>
	/// Return the current number of wood
	/// </summary>
	/// <returns>The number of wood</returns>
	public int GetWoodCount()
	{
		return m_currentWoodCount;
	}

	/// <summary>
	/// Return the current number of coal
	/// </summary>
	/// <returns>The number of coal</returns>
	public int GetCoalCount()
	{
		return m_currentCoalCount;
	}

	/// <summary>
	/// Return the current number of jambon boursin
	/// </summary>
	/// <returns>The number of jambon boursin</returns>
	public int GetjambonBoursinCount()
	{
		return m_currentJambonBoursinCount;
	}

	/// <summary>
	/// Return the current number of crew
	/// </summary>
	/// <returns>The number of crew</returns>
	public int GetCrewCount()
	{
		return m_currentCrewCount;
	}

	public void DecreasePlayerLifePoints(int count = 10)
	{
		m_currentPlayerLifePoints -= count;
		m_lifePointsGUIManager.SetPlayerHP(m_currentPlayerLifePoints, m_startPlayerLifePoints);
	}

	public void DecreaseEnemiesLifePoints(int count = 10)
	{
		m_currentEnemiesLifePoints -= count;
		m_lifePointsGUIManager.SetEnemiesHP(m_currentEnemiesLifePoints, m_startEnemiesLifePoints);

		if (m_currentEnemiesLifePoints <= 0)
		{
			GameObject.Find("OutputTransitionScene").GetComponent<BattleBoatOutput>().LoadScene(5000);
		}
	}
}
