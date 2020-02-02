using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class InteractableObject : MonoBehaviour
    {
        private struct SLocation
        {
            public GameObject unit;
            public Vector3 vLocation;
            public bool bReserved;
        }

        [Header("Circle")]
        public Vector3  m_circleCenterOffset;
        public float    m_circleRadius;
        
        [Header("Placement")]
        public int                  m_placementAngleMinDeg;
        public int                  m_placementAngleMaxDeg;
        public float                m_characterRadius;

        [Header("HP")]
        public int                  m_hpMax;

        public int                  m_hp;

        [Header("UI")]
        public UI.ObjectUI          m_objectUI;

        private float               m_placementAngleMin;
        private float               m_placementAngleMax;
        private float               m_placementStep;
        private int                 m_pointCount;

        private SLocation[]        aLocations;

        protected void Start()
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

            if (m_pointCount != 0)
            {
                aLocations = new SLocation[m_pointCount];
                float fCurrentAngle = m_placementAngleMin;
                for (int pointNum = 0; pointNum < m_pointCount; ++pointNum)
                {
                    aLocations[pointNum] = new SLocation
                    {
                        vLocation = ComputePoint(fCurrentAngle),
                        unit = null,
                        bReserved = false,
                    };

                    GameObject prim2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    prim2.transform.position = aLocations[pointNum].vLocation;

                    fCurrentAngle += m_placementStep;

                    if ((Mathf.Deg2Rad * 360.0f) - m_placementStep < fCurrentAngle)
                    {
                        m_pointCount = pointNum;
                        break;
                    }
                }
            }

            SetHp(m_hp);
            m_objectUI.SetProgressBarDisplayed(false);
        }

        void Update()
        {
			
        }

        public void SetHp(int hp)
        {
            if (hp > m_hpMax)
            {
                hp = m_hpMax;
            }
            else if(hp < 0)
            {
                hp = 0;
            }

            m_hp = hp;

            m_objectUI.SetLifeBarFill(m_hp / (float)m_hpMax);

            m_objectUI.SetLifeBarDisplayed(m_hp != m_hpMax);
        }

        public bool HasFreePlacementPoint(out Vector3 vLocation)
        {
            foreach(SLocation location in aLocations)
            {
                if (!location.bReserved)
                {
                    vLocation = location.vLocation;
                    return true;
                }
            }

            vLocation = new Vector3();
            return false;
        }

        public bool GetPlacementPoint(GameObject unit, out Vector3 placementPoint)
        {
            for (int pointNum = 0; pointNum < m_pointCount; ++pointNum)
            {
                if (!aLocations[pointNum].bReserved)
                {
                    placementPoint = aLocations[pointNum].vLocation;
                    aLocations[pointNum].unit = unit;
                    aLocations[pointNum].bReserved = true;

                    return true;
                }
            }

            placementPoint = new Vector3();
            return false;
        }

        public void FreePlacement(GameObject unit)
        {
            for (int pointNum = 0; pointNum < m_pointCount; ++pointNum)
            {
                if (aLocations[pointNum].unit == unit)
                {
                    aLocations[pointNum].unit = null;
                    aLocations[pointNum].bReserved = false;
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
            
            Vector3 vMinAngle = ComputePoint(m_placementAngleMin);

            while (Mathf.Abs(m_placementAngleMax - currentAngle) > 0.01f)
            {
                currentAngle += 0.01f;

                float distance = Vector3.Distance(ComputePoint(currentAngle), vMinAngle);

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