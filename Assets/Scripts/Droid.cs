﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droid : Selectable
{
    protected override void BaseStart()
    {
        entity = EntityType.Droid;
    }

}
