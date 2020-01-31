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
using UnityEngine.InputSystem;

public class UnitSelector : MonoBehaviour
{
	[Header("Configuration")]
	[Tooltip("The tag of the units (only the game objects with this tag will be selectable)")]
	string m_unitTag;   //< The tag that will be checked for gameobjects that are selectable

	[Header("Other")]
	[Tooltip("The image that is used to display the selection rect")]
	public Image m_selectionRectImage;

	public GameObject[] units;

	//
	// Private
	//
	private bool m_isSelecting;		//< Indicates if the user is currently selecting 
	private Vector2 m_mouseStartPosition;	//< The current selection start position for the current selection

    void Start()
    {
		//
		// We hide the selection rect image
		m_selectionRectImage.enabled = false;
	}

    // Update is called once per frame
    void Update()
    {
		//
		// Simple select
		if (Input.GetMouseButtonUp(0) && !m_isSelecting)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo))
			{
				Debug.LogError("Raycasted : " + hitInfo.collider.transform.name);
			}
		}

		//
		// Check if the mouse is pressed
		if (Input.GetMouseButton(0))
		{
			//
			// We check if the mouse is moving
			if (Mouse.current.delta.x.ReadValue() != 0f && Mouse.current.delta.y.ReadValue() != 0f)
			{
				//
				// We store the mouse position if it is the first frame we are selecting
				if (!m_isSelecting)
				{
					//
					// Store the mouse position
					m_mouseStartPosition = Input.mousePosition;
				}

				//
				// We are selecting
				m_isSelecting = true;
			}
		}
		else
		{
			//
			// We are selecting 
			m_isSelecting = false;

			//
			// Hide the image
			m_selectionRectImage.enabled = false;
		}

		//
		// We are selecting
		if (m_isSelecting)
		{
			//
			// We display the selection rect
			m_selectionRectImage.enabled = true;

			//
			// We retrieve the position of the mouse
			Vector2 currentMousePosition = Input.mousePosition;

			//
			// We set the position of the selection rect to the current position of the mouse
			m_selectionRectImage.transform.position = m_mouseStartPosition;

			//
			// Compute the height of the selection rect 
			float width = m_mouseStartPosition.x - currentMousePosition.x;
			float height = m_mouseStartPosition.y - currentMousePosition.y;

			//
			// We set the scale of the image rect
			m_selectionRectImage.transform.localScale = new Vector3(width > 0f ? -1f : 1f, height > 0f ? 1f : -1f, 1f);

			//
			// Set the size of the rect
			m_selectionRectImage.rectTransform.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));

			//
			//
			//
			Vector3 startViewportPosition = Camera.main.ScreenToViewportPoint(m_mouseStartPosition);
			Vector3 endViewportPosition = Camera.main.ScreenToViewportPoint(currentMousePosition);

			Vector3 min = Vector3.Min(startViewportPosition, endViewportPosition);
			Vector3 max = Vector3.Max(startViewportPosition, endViewportPosition);
			min.z = Camera.main.nearClipPlane;
			max.z = Camera.main.farClipPlane;

			Bounds bounds = new Bounds();
			bounds.SetMinMax(min, max);

			foreach (var item in units)
			{
				if (bounds.Contains(Camera.main.WorldToViewportPoint(item.transform.position)))
				{
					Debug.LogWarning("GO " + item.transform.name + " is selected");
				}
			}

		}
	}

	private void OnGUI()
	{
	}
}
