using UnityEngine;

public class GuideArrow : MonoBehaviour
{
    public Transform Target { get; set; }

    public void Update()
    {
        //you may not like it, but this is what peek performance looks like :D
        transform.LookAt(Target);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
