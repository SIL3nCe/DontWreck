using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class InteractableCannon : InteractableObject
    {
        [Header("Cannon Loading")]
        public int  m_maxCharge;

        private int m_charge = 0;
        
        new protected void Start()
        {
            base.Start();
        }

        void Update()
        {

        }

        public override void Interact(GameObject unit)
        {
            if (unit.GetComponent<Crew.CrewController>() != null)
            {
                CrewInteract();
            }
            else if (unit.GetComponent<EnemyController>() != null)
            {
                EnnemyInteract();
            }
        }

        public override void SetHp(int hp)
        {
            base.SetHp(hp);
        }

        private void CrewInteract()
        {
            if (Charge(10))
            {
                Fire();
            }
        }

        private void EnnemyInteract()
        {
            ModHp(-10);
        }

        private bool Charge(int chargeAmount)
        {
            m_charge += chargeAmount;

            if (m_charge > m_maxCharge)
            {
                m_objectUI.SetProgressBarDisplayed(false);
                m_charge = 0;
                return true;
            }

            m_objectUI.SetProgressBarDisplayed(true);
            m_objectUI.SetProgressBar(m_charge / (float)m_maxCharge);

            return false;
        }

        private void Fire()
        {

        }
    }
}
