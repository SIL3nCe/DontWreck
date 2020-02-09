using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class InteractableCannon : InteractableObject
    {
        [Header("Cannon Loading")]
        public int  m_maxCharge;

        [Header("Cannon Fire")]
        public int  m_coalFireCost;

        [Header("Cannon Repair")]
        public int  m_woodRepairCost;

        [Header("Cannon objects")]
        public GameObject m_cannonFullHealth;
        public GameObject m_cannonDamaged;

		[Header("Post Effect")]
		public ParticleSystem m_shotPostFX;

		[Header("Sound")]
		public AudioClip m_audioClipDestroy;
		public AudioClip[] m_audioClipsFire;
		private bool dirtyFirstPlaySound = true;

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

            if (m_hp == m_hpMax && hp > 0)
            {
                ResetCharge();
            }

            base.SetHp(hp);

            if (hp < m_hpMax)
            {
                m_cannonFullHealth.SetActive(false);
                m_cannonDamaged.SetActive(true);
            }
            else
            {
                m_cannonFullHealth.SetActive(true);
                m_cannonDamaged.SetActive(false);
				
				if (!dirtyFirstPlaySound)
				{
					GetComponent<AudioSource>().PlayOneShot(m_audioClipDestroy);
				}
			}

			dirtyFirstPlaySound = false;

		}

        private void CrewInteract()
        {
            if (m_hp != m_hpMax)
            {
                Repair();
            }
            else if (Charge(10))
            {
                Fire();
            }
        }

        private void EnnemyInteract()
        {
            ModHp(-10);
        }

        private void Repair()
        {
            ResourcesManager resourcesManager = GameManager.m_instance.m_resourcesManager;
            
            if (resourcesManager.GetWoodCount() >= m_woodRepairCost)
            {
                ModHp(10);
                ResetCharge();

                resourcesManager.AddWood(-m_woodRepairCost);
            }
        }

        private bool Charge(int chargeAmount)
        {
            m_charge = Mathf.Clamp(m_charge + chargeAmount, 0, m_maxCharge);
            m_objectUI.SetProgressBar(m_charge / (float)m_maxCharge);

            if (m_charge == m_maxCharge)
            {
                return true;
            }

            m_objectUI.SetProgressBarDisplayed(true);

            return false;
        }

        private void ResetCharge()
        {
            m_objectUI.SetProgressBarDisplayed(false);
            m_charge = 0;
        }

        private void Fire()
        {
            ResourcesManager resourcesManager = GameManager.m_instance.m_resourcesManager;

            if (resourcesManager.GetCoalCount() >= m_coalFireCost)
            {
                if (!GetComponent<AudioSource>().isPlaying)
                {
                    GetComponent<AudioSource>().PlayOneShot(m_audioClipsFire[Random.Range(0, m_audioClipsFire.Length)]);
                }

                m_shotPostFX.Play();

                GameManager.m_instance.m_resourcesManager.DecreaseEnemiesLifePoints(2);

                ResetCharge();

                resourcesManager.AddCoal(-m_coalFireCost);
            }
		}
    }
}
