using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnums : MonoBehaviour
{
    public enum BossStates
    {
        idle = 0,
        chase = 1,
        hurt = 2,
        memory = 3,
        attack = 4,
        travel = 5,
    }
}
