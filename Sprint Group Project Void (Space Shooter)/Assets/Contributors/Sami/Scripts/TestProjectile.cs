using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AD1815
{
    public class TestProjectile : MonoBehaviour
    //This script will give speed to the projectile and decide how much damage it does.
    //Should be attached to the projectile. It should already be attached.
    {
        float iDamage;
        float speed;
        float selfDestroyDelay;
        // Start is called before the first frame update
        void Start()
        {
            iDamage = 50; //Insert amount of damage here. If you want to test stuff, leave damage at zero.
            speed = 10; //How fast the arrow travels.
            selfDestroyDelay = 5; //Times it takes for the projectile to destroy itself, Default 5 seconds;
            Invoke("DestroySelf", selfDestroyDelay);
        }

        // Update is called once per frame
        void Update()
        {
            //This moves the projectile forwards.
            transform.Translate(new Vector2(0f, 1f) * speed * Time.deltaTime);
        }
        //Collision Detector
        void OnTriggerEnter2D(Collider2D collision) //Change identifier to whatever you want. We want to hit the collider, that is around the sprite/object to simulate a "hit".
        {
            if (collision.gameObject.name == "Asteroid") // Very simple identifier. Will not work on anything, except object named "Asteroid". Maybe later on, I highly recommend, that we add tag onto enemies like "Enemy" Tag, so identifier can be more universal.
            {
                // This has to be fixed on later. Basically there should be method in the target object, that this should call, which then does damage to the target object.
                // Not very data-secure to directly modify public values, like health in objects. Good practice to learn early on is to use public methods to affect private fields/variables.

                /*collision.gameObject.GetComponent<HealthSystem>().RecieveHit(iDamage); 
                DestroySelf();
                */
            }
        }
        void DestroySelf()
        {
            Destroy(gameObject);
        }
    } 
}
