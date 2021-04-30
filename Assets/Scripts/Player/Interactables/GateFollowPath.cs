using UnityEngine;
using PathCreation;

public class GateFollowPath : MonoBehaviour
{

    public PathCreator pathCreator;
    public float speed = 5;
    float distanceTraveled;

    // Bool to help determine when the gate should be moving
    bool isMoving = false;

    // This adds the option in the inspecter to select from Reverse, Loop, and Stop
    public EndOfPathInstruction end;
    

    
    void Update()
    {

        if (isMoving == true)
        {
            distanceTraveled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTraveled, end);
        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cursor"))
        {
            isMoving = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cursor"))
        {
            isMoving = false;
        }
    }

}
