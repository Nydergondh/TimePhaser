﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : HumanoidAnimations {

    public void SetPlayerTimeBubble(bool value) {
        objAnim.SetBool("TimeBubble", value);
    }

}
