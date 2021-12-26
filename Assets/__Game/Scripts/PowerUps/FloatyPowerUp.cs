using UnityEngine;

public class FloatyPowerUp : MonoBehaviour
{
    [Range(10, 180)]public float rotateSpeed;

    void Update(){
        transform.RotateAround(transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
