using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CapsuleCollider collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = this.transform.GetComponent<CapsuleCollider>();
    }

    bool CollisionCheck()
    {
        RaycastHit hit;
        Vector3 p1 = transform.position + collider.center + Vector3.up * -collider.height * 0.5F;
        Vector3 p2 = p1 + Vector3.up * collider.height;
        bool check = Physics.CapsuleCast(p1, p2, collider.radius, transform.forward,out hit, 1);
        return check;
    }

    // Update is called once per frame
    void Update()
    {
        //Replace with Input system later
        if (Input.GetKeyDown(KeyCode.W) && !CollisionCheck())
        {
            this.transform.position += transform.forward.normalized;
        }
        else if (Input.GetKeyDown(KeyCode.S) && !CollisionCheck())
        {
            this.transform.position -= transform.forward.normalized;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            this.transform.Rotate(new Vector3(0, -90, 0), Space.Self);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            this.transform.Rotate(new Vector3(0, 90, 0), Space.Self);
        }
    }
}
