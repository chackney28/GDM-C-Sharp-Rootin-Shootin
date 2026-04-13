using UnityEngine;
using System.Collections.Generic;

public class StatsManager : MonoBehaviour
{
    [Header("Movement Stats")]
    public float baseSpeed = 2.5f;
    public float speedMult = 1f;
    public float dashTime = 1.5f;
    public float dashSpeedMult = 1.5f;

    [Header("Gun Stats")]
    public float pistolMod = 1f;
    public float shotgunMod = 1f;
    public float automaticMod = 1f;
    public float throwableMod = 1f;
    public float meleeeMod = 1f;

    //public List<GameObject> boonList;
}
