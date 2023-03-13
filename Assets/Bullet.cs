using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 15;


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

    }


    // if colliding with bullet or player, destroy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Bullet") && !collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        if(collision.gameObject.CompareTag("Destroyable"))
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }


}
