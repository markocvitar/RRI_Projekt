using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassTwoMovement : PlayerMovement
{
    [SerializeField] private ClassTwoShooting class2Shooting;

    private void FixedUpdate() {
        if(class2Shooting.isUsingAbilityOne) return;
        movementHandler();
        checkFallDamage();
        if(class2Shooting.isUsingAbilityTwo) return;
        checkAnimation();
    }
}
