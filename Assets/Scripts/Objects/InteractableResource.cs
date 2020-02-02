using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public abstract class InteractableResource : InteractableObject
    {

		public AudioClip[] m_gatherClip;

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
			GetResource();

			//
			// Play audio clips 
			if (GetComponent<AudioSource>() != null && m_gatherClip.Length > 0)
			{
				GetComponent<AudioSource>().PlayOneShot(m_gatherClip[Random.Range(0, m_gatherClip.Length)]);
			}

			if (m_hp <= 0)
            {
                Destroy(gameObject);
            }
        }

        protected abstract void GetResource();
    }
}
