using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class InteractableFire : Objects.InteractableObject
    {
        public new void Start()
        {
            base.Start();
        }

        public override void Interact(GameObject unit)
        {
            if (unit.GetComponent<Crew.CrewController>() != null)
            {
                CrewInteract();
            }
        }

        private void CrewInteract()
        {
            ModHp(-10);
        }
    }
}
