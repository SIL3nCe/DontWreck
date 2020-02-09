using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class InteractableBoatObject : Objects.InteractableObject
    {
        [Header("Repair Cost")]
        public int m_woodRepairCost;

        public new void Start()
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

        private void CrewInteract()
        {
            ResourcesManager resourcesManager = GameManager.m_instance.m_resourcesManager;

            if (resourcesManager.GetWoodCount() >= m_woodRepairCost)
            {
                ModHp(10);

                resourcesManager.AddWood(-m_woodRepairCost);
            }
        }

        private void EnnemyInteract()
        {
            ModHp(-10);
        }
    }
}
