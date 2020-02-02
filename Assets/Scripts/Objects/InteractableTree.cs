using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class InteractableTree : InteractableResource
    {
        protected override void GetResource()
        {
            GameManager.m_instance.m_resourcesManager.AddWood(10);
        }
    }
}
