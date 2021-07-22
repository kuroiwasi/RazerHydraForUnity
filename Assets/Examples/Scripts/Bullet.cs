using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < -1) Destroy(gameObject);
    }
}
