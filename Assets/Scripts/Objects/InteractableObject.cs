using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class InteractableObject : MonoBehaviour
    {
        [Header("Circle")]
        public Vector3  m_circleCenterOffset;
        public float    m_circleRadius;
        
        [Header("Placement")]
        public int                  m_placementAngleMinDeg;
        public int                  m_placementAngleMaxDeg;
        public float                m_characterRadius;

        [Header("HP")]
        public int                  m_hpMax;

        [Tooltip("Begin HP")]
        public int                  m_hp;

        private float               m_placementAngleMin;
        private float               m_placementAngleMax;
        private float               m_placementStep;
        private int                 m_pointCount;

        private GameObject[]        m_reservedPoints;

        void Start()
        {
			//
			//
			m_placementAngleMinDeg -= (int)transform.rotation.eulerAngles.y;
			m_placementAngleMinDeg = m_placementAngleMinDeg % 360;
			m_placementAngleMaxDeg -= (int)transform.rotation.eulerAngles.y;
			m_placementAngleMaxDeg = m_placementAngleMaxDeg % 360;

			if (m_placementAngleMaxDeg < m_placementAngleMinDeg)
			{
				int tmp = m_placementAngleMaxDeg;
				m_placementAngleMaxDeg = m_placementAngleMinDeg;
				m_placementAngleMinDeg = tmp;
			}

			//
			//
			m_placementAngleMin = m_placementAngleMinDeg * Mathf.Deg2Rad;
            m_placementAngleMax = m_placementAngleMaxDeg * Mathf.Deg2Rad;

			//
			//
            FindStep();

			//
			//
            m_pointCount = Mathf.CeilToInt((m_placementAngleMax - m_placementAngleMin) / m_placementStep);
            m_reservedPoints = new GameObject[m_pointCount];
        }

        void Update()
        {
			
        }

        public bool HasFreePlacementPoint()
        {
            foreach(GameObject unit in m_reservedPoints)
            {
                if (unit == null)
                {
                    return true;
                }
            }

            return false;
        }

        public bool GetPlacementPoint(GameObject unit, out Vector3 placementPoint)
        {
            float currentAngle = m_placementAngleMin;

            for (int pointNum = 0; pointNum < m_pointCount; ++pointNum)
            {
                if (m_reservedPoints[pointNum] == null)
                {
                    placementPoint = ComputePoint(currentAngle);
                    m_reservedPoints[pointNum] = unit;

                    return true;
                }

                currentAngle += m_placementStep;
            }

            placementPoint = new Vector3();
            return false;
        }

        public void FreePlacement(GameObject unit)
        {
            for (int i = 0; i < m_reservedPoints.Length; ++i)
            {
                if (m_reservedPoints[i] == unit)
                {
                    m_reservedPoints[i] = null;
                    return;
                }
            }
        }

        public virtual void Interact(GameObject unit)
        {

        }

        private void FindStep()
        {
            float currentAngle = m_placementAngleMin;

            while (Mathf.Abs(m_placementAngleMax - currentAngle) > 0.01f)
            {
                currentAngle += 0.01f;

                float distance = Vector3.Distance(ComputePoint(currentAngle), ComputePoint(m_placementAngleMin));

                if (distance >= m_characterRadius / 2.0f)
                {
                    m_placementStep = currentAngle - m_placementAngleMin;
                    return;
                }
            }
        }

        private Vector3 ComputePoint(float angle)
        {
            Vector3 res = new Vector3();
            res.x = m_circleRadius * Mathf.Cos(angle);
            res.z = m_circleRadius * Mathf.Sin(angle);

			res += (transform.position + m_circleCenterOffset);

            return res;
        }

		private void OnDrawGizmos()
		{
			Gizmos.DrawWireSphere(transform.position + m_circleCenterOffset, m_circleRadius);
		}
	}
}