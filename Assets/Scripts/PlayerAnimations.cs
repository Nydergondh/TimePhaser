using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : HumanoidAnimations {

    public void SetPlayerWeapon(int value) {
        objAnim.SetInteger("WeaponId", value);
    }

}
