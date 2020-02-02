using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class InteractableFire : Objects.InteractableObject
    {
		private float m_startTime;

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

		public void Update()
		{
			//if ((Time.time - m_startTime) > 5f)
			//{
			//	m_startTime = Time.time;
			//	GameManager.m_instance.m_resourcesManager.DecreasePlayerLifePoints(10);
			//}
		}

		private void CrewInteract()
        {
            ModHp(-10);
            if (m_hp <= 0)
                Destroy(gameObject);
        }
	}
}
