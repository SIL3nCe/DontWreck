/*
   Project      : GGJ20-JambonBoursin
   Author		: Yannis Beaux (Kranck)
   Date		    : 31 / 01 / 2020
   Description  : Unit Selector
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelector : MonoBehaviour
{
	[Header("Configuration")]
	[Tooltip("The tag of the units (only the game objects with this tag will be selectable)")]
	string m_unitTag;   //< The tag that will be checked for gameobjects that are selectable

	public Image m_selectionRectImage;

	//
	// Private
	//
	private bool m_isSelecting;
	private Vector2 m_mouseStartPosition;
	private float m_currentWidth;
	private float m_currentHeight;
	private Vector2 m_currentMousePosition;


    void Start()
    {
        // ...
    }

    // Update is called once per frame
    void Update()
    {
		//
		//
		if (Input.GetMouseButton(0))
		{
			//
			//
			if (!m_isSelecting)
			{
				m_mouseStartPosition = Input.mousePosition;
			}

			m_isSelecting = true;
		}
		else
		{
			m_isSelecting = false;
		}

		if (m_isSelecting)
		{
			m_currentMousePosition = Input.mousePosition;

			m_selectionRectImage.transform.position = m_mouseStartPosition;

			m_currentWidth = m_mouseStartPosition.x - m_currentMousePosition.x;
			m_currentHeight = m_mouseStartPosition.y - m_currentMousePosition.y;

			m_selectionRectImage.transform.localScale = new Vector3(m_currentWidth > 0f ? -1f : 1f, m_currentHeight > 0f ? 1f : -1f, 1f);

			m_selectionRectImage.rectTransform.sizeDelta = new Vector2(Mathf.Abs(m_currentWidth), Mathf.Abs(m_currentHeight));
		}
	}

	private void OnGUI()
	{
	}
}
