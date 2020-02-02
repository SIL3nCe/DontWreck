using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public abstract class InteractableResource : InteractableObject
    {

        new protected void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        void Update()
        {

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
            ModHp(-1);
            if (m_hp <= 0)
            {
                GetResource();
                Destroy(gameObject);
            }
        }

        protected abstract void GetResource();
    }
}
