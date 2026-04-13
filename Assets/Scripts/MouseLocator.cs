
using UnityEngine;

public class MouseLocator : MonoBehaviour
{
    public float mouseX = 0f;
    public float mouseY = 0f;

    public Vector3 mouseLocation;
    // Update is called once per frame
    void Update()
    {
        mouseLocation = Input.mousePosition;
        float mouseX = mouseLocation.x;
        float mouseY = mouseLocation.y;

        //print("Locate: " + x + ", " + y);
    }
}
