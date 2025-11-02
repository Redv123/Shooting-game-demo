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
        StartCoroutine(Generate());
    }
    
    private IEnumerator Generate()
    {
        startPointY = Random.Range(-50f, 50f);
        Instantiate(enemy, new Vector2(startPointX,startPointY), Quaternion.identity);
        yield return shortWait;
    }
}
