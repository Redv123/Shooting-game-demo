using UnityEngine;
using System.Collections;

public class GenerateBallon : MonoBehaviour
{
    [SerializeField] private Transform[] startPoint;
    [SerializeField] private GameObject enemy;
    [SerializeField] private AudioClip music;
    private readonly WaitForSeconds shortWait = new(5f);
    private readonly WaitForSeconds nextOne = new(0.2f);
    private bool half = false;
    private float startPointX, startPointY;

    void Start()
    {
        GameData.end = false; // reset level end state when level starts
        StartCoroutine(End());
        StartCoroutine(GenerateEnemies());
        StartCoroutine(HalfTime());
        if (GameData.Level == 1)
        {
            MusicManager.Instance.PlayMusic(music);
        }
    }
    IEnumerator GenerateEnemies()
    {
        while (!GameData.end)
        {
            if (GameData.Level == 1 && half)
            {
                for (int i = 0; i < 4; i++)
                {
                    Generate();
                    yield return nextOne;
                }
            }
            else if (GameData.Level == 2 && half){
                for (int i = 0; i < 2; i++)
                {
                    Generate();
                    yield return nextOne;
                }
            }
            else
            {
                Generate();
            }
            yield return shortWait;
        }
    }

    IEnumerator HalfTime()
    {
        yield return new WaitForSeconds(20f);
        half = true;
    }

    private void Generate()
    {
        int random = Random.Range(0, startPoint.Length);
        startPointX = startPoint[random].position.x;
        startPointY = startPoint[random].position.y;
        Instantiate(enemy, new Vector2(startPointX, startPointY), Quaternion.identity);
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(40f);
        GameData.end = true;
        Unit.CheckWin?.Invoke();
    }
}
