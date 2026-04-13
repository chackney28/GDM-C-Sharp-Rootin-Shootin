using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Item Data")]
public class BoonBase : ScriptableObject
{
    public string id;

    [Header("Movement Stats")]
    public float baseSpeed = 0f;
    public float speedMult = 1f;
    public float dashTime = 0f;
    public float dashSpeedMult = 1f;

    [Header("Gun Stats")]
    public float pistolMod = 1f;
    public float shotgunMod = 1f;
    public float automaticMod = 1f;
    public float throwableMod = 1f;
    public float meleeeMod = 1f;
}