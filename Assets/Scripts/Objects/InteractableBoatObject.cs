using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class InteractableBoatObject : Objects.InteractableObject
    {
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
        }

        private void EnnemyInteract()
        {
            ModHp(-10);
        }
    }
}
