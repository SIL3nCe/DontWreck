using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorIconsManager : MonoBehaviour
{
	[Header("Icons")]
	public Texture2D m_defaultIcon;
	public Texture2D m_interactWoodIcon;
	public Texture2D m_interactCoalIcon;
	public Texture2D m_interactFightIcon;
	public Texture2D m_interactRepairIcon;
	public Texture2D m_interactCannonIcon;
	public Texture2D m_interactFireIcon;

    // Start is called before the first frame update
    void Start()
    {
		Cursor.SetCursor(m_defaultIcon, Vector2.zero, CursorMode.Auto);
	}

    // Update is called once per frame
    void Update()
    {
		Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo))
		{
			if (hitInfo.collider.GetComponent<Objects.InteractableObject>() != null)
			{
				if (hitInfo.collider.GetComponent<Objects.InteractableCannon>() != null)
				{
					//
					// Cannon
					Objects.InteractableCannon cannon = hitInfo.collider.GetComponent<Objects.InteractableCannon>();
					if (cannon.m_hp == cannon.m_hpMax)
					{
						// TODO: Canon icon
						Cursor.SetCursor(m_interactCannonIcon, Vector2.zero, CursorMode.Auto);
					}
					else
					{
						Cursor.SetCursor(m_interactRepairIcon, Vector2.zero, CursorMode.Auto);
					}
				}
				else if (hitInfo.collider.GetComponent<Objects.InteractableBoatObject>() != null)
				{
					Objects.InteractableBoatObject boatObject = hitInfo.collider.GetComponent<Objects.InteractableBoatObject>();
					
					if (boatObject.m_hp != boatObject.m_hpMax)
					{
						Cursor.SetCursor(m_interactRepairIcon, Vector2.zero, CursorMode.Auto);
					}
				}
				else if (hitInfo.collider.GetComponent<Objects.InteractableFire>() != null)
				{
					Cursor.SetCursor(m_interactFireIcon, new Vector2(0, 32.0f), CursorMode.Auto);
				}
				else
				{
					Cursor.SetCursor(m_defaultIcon, Vector2.zero, CursorMode.Auto);
				}
			}
			else if (hitInfo.collider.GetComponent<EnemyController>() != null)
			{
				Cursor.SetCursor(m_interactFightIcon, Vector2.zero, CursorMode.Auto);
			}
			else
			{
				Cursor.SetCursor(m_defaultIcon, Vector2.zero, CursorMode.Auto);
			}
		}
		else
		{
			Cursor.SetCursor(m_defaultIcon, Vector2.zero, CursorMode.Auto);
		}
    }
}
