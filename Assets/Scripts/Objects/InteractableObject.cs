using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class InteractableObject : MonoBehaviour
    {
        public CircleCollider2D     m_placementCircle;
        public int                  m_placementAngleMinDeg;
        public int                  m_placementAngleMaxDeg;
        public float                m_characterRadius;

        private float               m_placementAngleMin;
        private float               m_placementAngleMax;
        private float               m_placementStep;
        private int                 m_pointCount;

        private Unit[]              m_reservedPoints;

        void Start()
        {
            m_placementAngleMin = m_placementAngleMinDeg * Mathf.Deg2Rad;
            m_placementAngleMax = m_placementAngleMaxDeg * Mathf.Deg2Rad;

            FindStep();

            m_pointCount = Mathf.CeilToInt((m_placementAngleMax - m_placementAngleMin) / m_placementStep);
        }

        void Update()
        {

        }

        public bool GetPlacementPoint(Unit unit, out Vector3 placementPoint)
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

        public void FreePlacement(Unit unit)
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
            res.x = m_placementCircle.radius * Mathf.Cos(angle);
            res.z = m_placementCircle.radius * Mathf.Sin(angle);

            return res;
        }
    }
}