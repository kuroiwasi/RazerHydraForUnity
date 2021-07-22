using UnityEngine;
using Sixense;

public class AxisGun : MonoBehaviour
{
    public RazerHydraInput Controller;
    public RazerHydraAxis.TYPE AxisType;
    public GameObject Bullet;
    public float Speed = 1;
    public float CoolingTime = 0.1f;
    private float timeElapsed = 100f;

    void Update()
    {
        if (timeElapsed > CoolingTime)
        {
            if (Controller.GetButton(AxisType))
            {
                GameObject bullet = Instantiate(Bullet, transform.position, transform.rotation);
                bullet.GetComponent<Rigidbody>().AddForce(transform.forward * Speed);
            }
            timeElapsed = 0;
        }
        else
        {
            timeElapsed += Time.deltaTime;
        }
    }
}