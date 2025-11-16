public class BallonAction : Unit
{
    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
