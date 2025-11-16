using UnityEngine;
using System.Collections;

public class GenerateBallon : MonoBehaviour
{
    [SerializeField] private Transform[] startPoint;
    [SerializeField] private GameObject enemy;
    private readonly WaitForSeconds shortWait = new(5f);
    private float startPointX, startPointY;


    void Start()
    {
        GameData.end = false; // reset level end state when level starts
        StartCoroutine(End());
        StartCoroutine(Generate());
    }
    private IEnumerator Generate()
    {
        int random = Random.Range(0, startPoint.Length);
        startPointX = startPoint[random].position.x;
        startPointY = startPoint[random].position.y;

        Instantiate(enemy, new Vector2(startPointX, startPointY), Quaternion.identity);
        yield return shortWait;
        if (!GameData.end)
        {
            StartCoroutine(Generate());
        }
    }

    private IEnumerator End()
    {
        yield return new WaitForSeconds(60f);
        GameData.end = true;

        //Check if the player win
        Unit.OnScored?.Invoke(0);
    }
}
