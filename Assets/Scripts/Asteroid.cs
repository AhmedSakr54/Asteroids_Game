using UnityEngine;

/// <summary>
/// Deals with spawning and moving the asteroid objects, that are targeting the space ship
/// </summary>
public class Asteroid : MonoBehaviour
{
    [SerializeField]
    Sprite[] asteroids = new Sprite[3];


    const float MinImpulse = 2f;
    const float MaxImpulse = 4f;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer asteroidRenderer = GetComponent<SpriteRenderer>();
        asteroidRenderer.sprite = asteroids[Random.Range(0, 3)];
    }

    public void Initialize(Direction asteroidDirection)
    {

        float angle = getAngleBasedOnDirection(asteroidDirection);
        startMoving(angle);
    }

    void startMoving(float angle)
    {
        float randomMagnitude = Random.Range(MinImpulse, MaxImpulse);
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        Rigidbody2D rigid2D = GetComponent<Rigidbody2D>();
        rigid2D.AddForce(randomMagnitude * direction, ForceMode2D.Impulse);
    }
    float getAngleBasedOnDirection(Direction asteroidDirection)
    {
        switch(asteroidDirection)
        {
            case Direction.UP:
                return Random.Range(75 * Mathf.Deg2Rad, 106 * Mathf.Deg2Rad);
            case Direction.DOWN:
                return Random.Range(255 * Mathf.Deg2Rad, 286 * Mathf.Deg2Rad);
            case Direction.RIGHT:
                return Random.Range(-15 * Mathf.Deg2Rad, 16 * Mathf.Deg2Rad);
            case Direction.LEFT:
                return Random.Range(165 * Mathf.Deg2Rad, 196 * Mathf.Deg2Rad);
            default:
                return 0f;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        GameObject collisionObject = collision.gameObject;
        AudioManager.Play(AudioClipName.AsteroidHit);

        // decrease the scale of the asteroids upon getting hit by a bullet
        if (transform.localScale.x <= 0.5)
        {
            Destroy(gameObject);
        }
        else if (collisionObject.GetComponent<SpaceShip>() == null)
        {
            Vector3 newScale = GetComponent<Transform>().localScale / 2;
            GetComponent<Transform>().localScale = newScale;
            float newRadius = GetComponent<CircleCollider2D>().radius / 2;
            GetComponent<CircleCollider2D>().radius = newRadius;
            Destroy(collisionObject);

            GameObject smallAsteroid1 = Instantiate<GameObject>(gameObject,
                                                                transform.position,
                                                                Quaternion.identity);
            smallAsteroid1.GetComponent<Asteroid>().startMoving(Random.Range(0, 2 * Mathf.PI));

            GameObject smallAsteroid2 = Instantiate<GameObject>(gameObject,
                                                                transform.position,
                                                                Quaternion.identity);
            smallAsteroid2.GetComponent<Asteroid>().startMoving(Random.Range(0, 2 * Mathf.PI));
            
        }

        Destroy(gameObject);
    }

}
