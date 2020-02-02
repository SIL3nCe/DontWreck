using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class InteractableCoal : InteractableResource
    {
        protected override void GetResource()
        {
            GameManager.m_instance.m_resourcesManager.AddCoal(15);
        }
    }
}
