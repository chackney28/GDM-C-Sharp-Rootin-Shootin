
using UnityEngine;
using UnityEngine.InputSystem;

public class InputInterperter : MonoBehaviour
{
    public Vector3 movementThing;

    // Update is called once per frame
    void OnMove(InputValue inputValue)
    {
        movementThing = inputValue.Get<Vector3>();
        Debug.Log("moved");
    }
}
