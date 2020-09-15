using UnityEngine;

public class Bullet : MonoBehaviour
{

    const float BulletLifeTime = 1.0f;

    float bulletSpawnTimer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bulletSpawnTimer += Time.deltaTime;

        if (bulletSpawnTimer >= BulletLifeTime)
        {
            Destroy(gameObject);
        }
    }


}
