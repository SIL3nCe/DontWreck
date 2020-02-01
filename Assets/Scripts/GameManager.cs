using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
	public WorldClickDestinationSetter m_worldClickDestinationSetter;
	public UnitSelector m_unitSelector;
	public UnitsManager m_unitManager;

	//
	// Singleton
	//
	public static GameManager m_instance;

	public void Awake()
	{
		if (m_instance != this && m_instance != null)
		{
			Destroy(gameObject);
		}
		m_instance = this;
	}

	// Start is called before the first frame update
	void Start()
    {
		//
		// Ensure the World Click Destination Setter is not null
		Assert.AreNotEqual(null, m_worldClickDestinationSetter);

		//
		// Ensure the Unit Selector is not null
		Assert.AreNotEqual(null, m_unitSelector);

		//
		// Ensure the unit manager is not null
		Assert.AreNotEqual(null, m_unitManager);
    }
}
