﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : HumanoidAnimations {

    public void SetPlayerTimeBubble(bool value) {
        objAnim.SetBool("TimeBubble", value);
    }

    public void SetPlayerDash(bool value) {
        objAnim.SetBool("Dash", value);
    }

    public void SetDuck(bool value) {
        //objAnim.SetBool("Duck", value);
        print("Duck Seted");
    }

}
