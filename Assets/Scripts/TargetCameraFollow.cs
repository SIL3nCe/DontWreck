using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;


public class TargetCameraFollow : MonoBehaviour
{

    public InputAction m_displacementActions;
    public GameObject Target;
    public GameObject RestrictedArea;
    private CinemachineVirtualCamera m_vCam;

    public float MaxZoom;
    public float MinZoom;

    private void Start()
    {
        m_vCam = gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    private void Awake()
    {

    }

    void OnEnable()
    {
        m_displacementActions.Enable();
    }

    void OnDisable()
    {
        m_displacementActions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 c_v2 = m_displacementActions.ReadValue<Vector2>();
        if (CheckTargetIsContains(new Vector3(Target.transform.position.x, Target.transform.position.y, Target.transform.position.z + c_v2.x)))
        {
            Target.transform.Translate(new Vector3(0, 0, c_v2.x));
        }


        if (((m_vCam.m_Lens.FieldOfView - c_v2.y) < MaxZoom) && ((m_vCam.m_Lens.FieldOfView - c_v2.y) > MinZoom))
        {
            m_vCam.m_Lens.FieldOfView -= c_v2.y;
        }
    }

    bool CheckTargetIsContains(Vector3 toCheck)
    {
        return RestrictedArea.GetComponent<BoxCollider>().bounds.Contains(toCheck);
    }



}
