using UnityEngine;
using System.Collections;
public class BallonAction : Unit
{
    public float speed = 7f;
    [SerializeField] private bool move = true;
    private readonly WaitForSeconds shortWait = new(0.1f);


    void Start()
    {
        if (move == true)
        {
            int random = Random.Range(0, 3);

            if (random == 1)
            {
                StartCoroutine(StartBiger());
            }
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
        //Check if the player win
        CheckWin?.Invoke();
    }

    void FixedUpdate()
    {
        if (move)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    IEnumerator StartBiger()
    {
        StartCoroutine(Biger());
        yield return new WaitForSeconds(5f);
        OnScored?.Invoke(0);
    }

    IEnumerator Biger()
    {
        StartCoroutine(KillBellon());
        while (true)
        {
            yield return shortWait;
            transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
        }
    }

    IEnumerator KillBellon()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
        CheckWin?.Invoke();
    }
}
