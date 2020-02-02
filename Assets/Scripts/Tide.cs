using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tide : MonoBehaviour
{
    public delegate void OnTideDown();
    public delegate void OnTideUp();


    public bool m_IsActive = true; // if false the tide won't move
    public float m_Speed = 100; // time in second to go down/up
    public float m_CurrentValue = 7; // current 
    public float m_DownValue = 3; // value of the down tide, going up when reaching it 
    public float m_UpValue = 24; // value of the down tide, going up when reaching it
    public bool m_GoingDown = true; // true when tide is going down, false when going up
    public bool m_StopDown = false; // true when tide is going down, false when going up
    public bool m_StopUp = true; // true when tide is going down, false when going up

    public OnTideDown m_callbackOnTideDown = null;
    public OnTideUp m_callbackOnTideUp = null;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsActive)
        {
            float fValue = ((m_UpValue - m_DownValue)/m_Speed)*Time.deltaTime;
            if (m_GoingDown)
            {
                fValue *= -1.0f;
            }

            m_CurrentValue += fValue;
            if (m_GoingDown)
            {
                if (m_CurrentValue < m_DownValue)
                {
                    m_GoingDown = false;
                    m_CurrentValue = m_DownValue;
                    if (m_StopDown)
                    {
                        m_IsActive = false;
                    }
                    m_callbackOnTideDown?.Invoke();
                }
            }
            else
            {
                if (m_CurrentValue > m_UpValue)
                {
                    m_GoingDown = true;
                    m_CurrentValue = m_UpValue;
                    if (m_StopUp)
                    {
                        m_IsActive = false;
                    }
                    m_callbackOnTideUp?.Invoke();
                }
            }
        }

        Vector3 vPos =transform.position;
        transform.position = new Vector3(vPos.x, m_CurrentValue, vPos.z);
    }

    bool IsActive()
    {
        return m_IsActive;
    }

    void SetActive(bool bIsActive)
    {
        m_IsActive = bIsActive;
    }

    public void AddOnTideDownCallback(OnTideDown OnTideDown)
    {
        m_callbackOnTideDown += OnTideDown;
    }

    public void RemoveOnTideDownCallback(OnTideDown OnTideDown)
    {
        m_callbackOnTideDown -= OnTideDown;
    }

    public void ClearOnTideDownCallbacks()
    {
        m_callbackOnTideDown = null;
    }

    public void AddOnTideUpCallback(OnTideUp OnTideUp)
    {
        m_callbackOnTideUp += OnTideUp;
    }

    public void RemoveOnTideUpCallback(OnTideUp OnTideUp)
    {
        m_callbackOnTideUp -= OnTideUp;
    }

    public void ClearOnTideUpCallbacks()
    {
        m_callbackOnTideUp = null;
    }
}
