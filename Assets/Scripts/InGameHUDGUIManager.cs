/*
   Project      : GGJ20-JambonBoursin
   Author		: Yannis Beaux (Kranck)
   Date		    : 01 / 02 / 2020
   Description  : Manages the in game HUD of the player
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class InGameHUDGUIManager : MonoBehaviour
{
	public Text m_woodTextCount;				//< The text that displays the number of wood resources
	public Text m_coalTextCount;				//< The text that displays the number of coal resources
	public Text m_jambonBoursinTextCount;		//< The text that displays the number of jambon boursin resources
	public Text m_crewTextCount;				//< The text that displays the number of crew resources

    void Start()
    {
		//
		// Ensure all the text are not null
		Assert.AreNotEqual(null, m_woodTextCount);
		Assert.AreNotEqual(null, m_coalTextCount);
		Assert.AreNotEqual(null, m_jambonBoursinTextCount);
		Assert.AreNotEqual(null, m_crewTextCount);
    }

	/// <summary>
	/// Set the number of wood resources
	/// </summary>
	/// <param name="count">The current number of wood resources available</param>
	public void SetWoodCount(int count)
	{
		m_woodTextCount.text = count.ToString();
	}

	/// <summary>
	/// Set the number of coal resources available
	/// </summary>
	/// <param name="count">The current number of coal resources available</param>
	public void SetCoalCount(int count)
	{
		m_coalTextCount.text = count.ToString();
	}

	/// <summary>
	/// Set the number of available jambon boursin resources
	/// </summary>
	/// <param name="count">The current number of available jambon boursin resources</param>
	public void SetJambonBoursinCount(int count)
	{
		m_jambonBoursinTextCount.text = count.ToString();
	}

	/// <summary>
	/// Set the number of crew resources available
	/// </summary>
	/// <param name="count">The current number of available crew resources</param>
	public void SetCrewCount(int count)
	{
		m_crewTextCount.text = count.ToString();
	}
}
