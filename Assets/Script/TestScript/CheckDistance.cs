using UnityEngine;

public class CheckDistance : MonoBehaviour
{
    [SerializeField] private GameObject PlayerPosition;

    void FixedUpdate()
    {
        Debug.Log("The current distance is:" + (PlayerPosition.transform.position - transform.position).magnitude);
    }
}
