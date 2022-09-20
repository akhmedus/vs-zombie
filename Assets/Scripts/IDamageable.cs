using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void Hit(float dmg, Vector3 hitPnt, Vector3 hitDir);

    void Damage(float dmg);
}
