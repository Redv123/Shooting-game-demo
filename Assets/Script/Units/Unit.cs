using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] protected int point = 0;
    protected SpriteRenderer characterSprite;
    private bool die = false;
    [SerializeField] protected int hp = 1;
    [SerializeField] protected AudioClip destroySound;

    public static Action<int> OnScored;

    public void Hit(int damage)
    {
        if (die == true)
        {
            return;
        }
        hp -= damage;
        if (hp <= 0)
        {
            die = true;
            Die();
        }
    }

    private void Die()
    {
        Sound.OnSound.Invoke(destroySound);
        gameObject.tag = "Untagged";
        Destroy(gameObject);
        OnScored.Invoke(point);
    }
}
