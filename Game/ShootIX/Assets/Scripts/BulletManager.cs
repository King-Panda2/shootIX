using UnityEngine;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int maxBullets;
    [SerializeField] private float spawnOffset;

    private List<Bullet> currentBullets;
    private Bullet activeBullet;

    void Start()
    {
        currentBullets = new List<Bullet>();

        // Create initial bullets
        for (int i = 0; i < maxBullets; i++)
        {
            CreateBullet();
        }
        SetActiveBullet();
    }
    
    private void CreateBullet()
    {
        // Create new bullet
        float xPosOffset = spawnOffset * currentBullets.Count;
        Vector3 gunPos = gameObject.transform.position;
        Vector3 spawnPosition = new Vector3(gunPos.x + xPosOffset, gunPos.y, gunPos.z);
        GameObject newBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        
        // Set the color/type of the bullet
        Bullet newBulletComponent = newBullet.GetComponent<Bullet>();
        List<Color> bulletWeights = MainFace.Instance.GetBulletWeights();
        Color bulletColor = bulletWeights[Random.Range(0, bulletWeights.Count)];
        newBulletComponent.BulletColor = bulletColor;
        newBullet.GetComponent<SpriteRenderer>().color = bulletColor;

        currentBullets.Add(newBulletComponent);
    }

    private void SetActiveBullet()
    {
        activeBullet = currentBullets[0];
        activeBullet.transform.position += new Vector3(0.0f, 0.5f, 0.0f);
    }

    private void ShootBullet()
    {
        activeBullet.Shoot();
        
        OnBulletFired();
    }

    private void OnBulletFired()
    {
        // Shift all bullets up the queue
        for (int i = 0; i < currentBullets.Count; i++)
        {
            currentBullets[i].gameObject.transform.position -= new Vector3(spawnOffset, 0.0f, 0.0f);
        }

        // Remove current active bullet
        currentBullets.Remove(activeBullet);
        
        // Set next active bullet
        if (currentBullets.Count >= 1)
        {
            SetActiveBullet();
        }

        // Create new bullet
        CreateBullet();
    }
}
