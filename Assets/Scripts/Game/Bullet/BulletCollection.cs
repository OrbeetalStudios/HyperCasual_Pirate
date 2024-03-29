using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBulletCollection", menuName = "Create New Bullet Collection")]
public class BulletCollection : ScriptableObject
{
    public List<BulletPrototype> bullets = new List<BulletPrototype>();

    public Bullet TakeBullet(BulletPrototype.eBulletID type)// Cerca direttamente il proiettile con l'ID specificato
    {
      
        foreach (var bullet in bullets)
        {
            if (bullet.bulletID == type)
            {
                // Se trova il proiettile, lo istanzia e lo restituisce
                GameObject newObjBullet = GameObject.Instantiate(bullet.bulletPrefab);
                Bullet bulletComponent = newObjBullet.GetComponent<Bullet>();
                return bulletComponent;
            }
        }

        // Se non viene trovato un proiettile con l'ID specificato, genera un errore
        Debug.LogError("No bullet found with type: " + type);
        return null;
    }
}
