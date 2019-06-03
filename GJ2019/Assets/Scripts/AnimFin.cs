using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFin : MonoBehaviour
{
    [SerializeField]
    PlayerHitController hitController;

    public void AttackAnimFin()
    {
        hitController.SetCollider(false);
    }
}
