using UnityEngine;
using Sixense;

public class ControllerMove : MonoBehaviour
{
    public RazerHydraInput controller;

    void Update()
    {
        transform.position = controller.GetPosition();
        transform.rotation = controller.GetRotation();
    }
}
