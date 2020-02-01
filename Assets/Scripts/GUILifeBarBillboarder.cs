/*
   Project      : GGJ20-JambonBoursin
   Author		: Yannis Beaux (Kranck)
   Date		    : 01 / 02 / 2020
   Description  : Manager for all the units
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUILifeBarBillboarder : MonoBehaviour
{
    void LateUpdate()
    {
		transform.forward = Camera.main.transform.forward;
    }
}
