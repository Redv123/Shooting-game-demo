using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    [SerializeField] private ArrowMovement arrowPrefab;
    [SerializeField] private int poolSize = 4;
    [SerializeField] private AudioClip soundEffect;

    private readonly Queue<ArrowMovement> arrowPool = new Queue<ArrowMovement>();

    void Awake()
    {
        FillPool();
    }

    public bool TryShoot(Vector2 position, bool flipX)
    {
        // Empty pool means all arrows are already active on screen.
        if (arrowPool.Count == 0 || arrowPrefab == null)
        {
            return false;
        }

        ArrowMovement arrow = arrowPool.Dequeue();
        arrow.Init(flipX, ReleaseArrow);
        arrow.transform.position = position;
        arrow.gameObject.SetActive(true);
        Sound.OnSound.Invoke(soundEffect);
        return true;
    }

    private void FillPool()
    {
        if (arrowPrefab == null)
        {
            Debug.LogError("ArrowShooter requires an ArrowMovement prefab reference.", this);
            return;
        }

        for (int i = 0; i < poolSize; i++)
        {
            ArrowMovement arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            // Player survives scene changes, so pooled arrows must survive with it.
            DontDestroyOnLoad(arrow.gameObject);
            arrow.gameObject.SetActive(false);
            arrowPool.Enqueue(arrow);
        }
    }

    private void ReleaseArrow(ArrowMovement arrow)
    {
        // Disable first so the arrow stops moving and can be reused later.
        arrow.gameObject.SetActive(false);
        arrowPool.Enqueue(arrow);
    }
}
