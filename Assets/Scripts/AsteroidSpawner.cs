using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefabAsteroid;

    List<GameObject> asteroids = new List<GameObject>();

    public List<GameObject> Asteroids { get { return asteroids; } }
    // Start is called before the first frame update
    void Start()
    {
        GameObject asteroid = Instantiate<GameObject>(prefabAsteroid);
        float radius = asteroid.GetComponent<CircleCollider2D>().radius;
        Destroy(asteroid);

        float screenZ = -Camera.main.transform.position.z;
        // spawning an asteroid in the left of the screen to move to the right
        spawnAsteroid(Direction.RIGHT, ScreenUtils.ScreenLeft + radius, 0, screenZ);

        // spawining an asteroid in the right of the screen to move to the left
        spawnAsteroid(Direction.LEFT, ScreenUtils.ScreenRight - radius, 0, screenZ);

        // spawining an asteroid in the bottom of the screen to move up
        spawnAsteroid(Direction.UP, 0, ScreenUtils.ScreenBottom + radius, screenZ);

        // spawining an asteroid in the top of the screen to move down
        spawnAsteroid(Direction.DOWN, 0, ScreenUtils.ScreenTop - radius, screenZ);
    }

    void spawnAsteroid(Direction asteroidDirection, float xLoc, float yLoc, float zLoc)
    {
        Vector3 AsteroidLocation = new Vector3(xLoc, yLoc, zLoc);

        GameObject asteroid = Instantiate<GameObject>(prefabAsteroid,
                                                      AsteroidLocation, 
                                                      Quaternion.identity);
        asteroids.Add(asteroid);
        asteroid.GetComponent<Asteroid>().Initialize(asteroidDirection);

    }

    void Update()
    {
        if (asteroids.Count == 0)
        {
            Debug.Log("You destroyed all the asteroids");
        }    
    }

    public void DestoryAsteroid(GameObject destroyedAsteroid)
    {
        asteroids.Remove(destroyedAsteroid);
        Destroy(destroyedAsteroid);
    }
}
