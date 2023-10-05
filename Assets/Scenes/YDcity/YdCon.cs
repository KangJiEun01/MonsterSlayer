
using UnityEngine;

public class YdCon : MonoBehaviour
{
    public float speed = 1.0f;
    public Transform [] bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 5;
    public float particleDalay = 0.5f;

    private int i = 0;
    [SerializeField]
    private bool closed = false;

   
    Animator anim;
    CharacterController controller;


    public float mouseSensitivity = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        anim.SetFloat("speedMultiplier", speed);

    }

    // Update is called once per frame
    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        Vector3 newRotation = transform.rotation.eulerAngles + new Vector3(0, mouseX, 0);
        transform.rotation = Quaternion.Euler(newRotation);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetAxis("Horizontal") > 0.1f)
            {
                anim.SetBool("turnLeft", true);
                anim.SetBool("turnRight", false);
            }
            if (Input.GetAxis("Horizontal") < -0.1f)
            {
                anim.SetBool("turnRight", true);
                anim.SetBool("turnLeft", false);
            }
        }
        else
        {
            anim.SetBool("turnLeft", false);
            anim.SetBool("turnRight", false);
        }

            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
            {
                anim.SetFloat("Turns", Input.GetAxis("Horizontal"));
            }
            else
            {
                anim.SetFloat("Turns", 0);
            }

            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
            {
                anim.SetFloat("Speed", Input.GetAxis("Vertical"));
            }
            else
            {
                anim.SetFloat("Speed", 0);
            }

           if (Input.GetKeyUp("r"))
           {
                closed = !closed;
                anim.SetBool("close", closed);
           }

           if (Input.GetKeyDown("e"))
           {
               anim.SetBool("shoot_bool", true);
           }
           if (Input.GetKeyUp("e"))
           {
               anim.SetBool("shoot_bool", false);
           }

           if ((Input.GetKeyDown("p"))) //death
           {
               anim.SetBool("die", true);
               this.enabled = false;
           } 

           if ((Input.GetKeyDown("u"))) //look around
           {
               anim.SetBool("look1", true);
           } else { anim.SetBool("look1", false); }

           if ((Input.GetKeyDown("o"))) //take damage 1
           {
               anim.SetBool("hitLeft", true);
           } else { anim.SetBool("hitLeft", false); }

           if ((Input.GetKeyDown("i"))) //take damage 2
           {
               anim.SetBool("hitRight", true);
           } else { anim.SetBool("hitRight", false); }        
    }

    void Shoot ()
    {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint[i].position, bulletSpawnPoint[i].rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint[i].forward * bulletSpeed;

            i++;
            if (i >= bulletSpawnPoint.Length) i = 0;
    }
}

