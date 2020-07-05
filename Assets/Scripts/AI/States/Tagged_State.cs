﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tagged_State : State
{
    private float localtaggedTime;

    public override void OnEnter(Base_Ghost ghost)
    {
        localtaggedTime = ghost.TaggedTime;
    }

    public override void OnExit(Base_Ghost ghost)
    {
        localtaggedTime = ghost.TaggedTime;
        ghost.myHealth = 1;
        ghost.GotRescuedGhost = false;
        ghost.InFleeZone = false;
    }

    public override void OnUpdate(Base_Ghost ghost)
    {
        PathRequestManager.RequestPath(ghost.transform.position, ghost.spawn.transform.position, ghost.OnPathFound);

        ghost.myMat.SetColor("_Color", Color.green);

        if (ghost.GotRescuedGhost == true) {
            ghost.ChangeState("Patrol");
        }

        if (ghost.InFleeZone == true) {
            localtaggedTime -= Time.deltaTime;
        }

        if (localtaggedTime <= 0) {
            ghost.ChangeState("Patrol");
        }
    }
}
