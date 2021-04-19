using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserScript : MonoBehaviour
{
    public Transform pos1, pos2;
    public float speed;
    public Transform startPos;

    Vector3 nextPos;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = startPos.position;
    }

    // Update is called once per frame
    void OnTriggerStay2D(Collider2D collision)
    {



        if (transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }


        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);

    }

    void OnCollisionExit2D(Collision2D collision)

    {

        if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }


}
