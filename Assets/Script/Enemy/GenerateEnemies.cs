using UnityEngine;
using System.Collections;

public class GenerateEnemies : MonoBehaviour
{

    private readonly WaitForSeconds shortWait = new(5f);
    private float startPointX;
    private float startPointY;
    [SerializeField] private GameObject enemy;
    void Start()
    {
        startPointX = transform.position.x;
        startPointY = transform.position.y;
        StartCoroutine(Generate());
    }
    
    private IEnumerator Generate()
    {
        startPointY += Random.Range(-5f, 20f);
        Instantiate(enemy, new Vector2(startPointX,startPointY), Quaternion.identity);
        yield return shortWait;
        StartCoroutine(Generate());
    }
}
