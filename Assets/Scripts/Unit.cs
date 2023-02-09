using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Unit : MonoBehaviour //this contains code for all bears
{
    public Laser laserPrefab;//we can now specify a prefab laser
    public int fullHealth = 100;
    public int damage = 10; //when hitting something, you dole out 10 damage

    public float respawnTime = 5.0f;//we will respawn in 5s after death
    private int health; //our currentHealth

    // Start is called before the first frame update
    public int team;
    public float viewAngle = 80; //this willl allow us to modify the vision for our Units
    protected Rigidbody rb;
    private Vector3 startPos;
    private const float RAYCAST_LENGTH = 0.3f;

    protected Animator animator; //this is the animator for the bear. We will need to initialize this in our Start
    private Color myColor; //this will be the color of our bear
    private Eye[] eyes = new Eye[2];//this is for our 2 eyes
    internal bool isAlive = true;



    protected virtual void Start()
    {
        eyes = GetComponentsInChildren<Eye>(); //this gives us back our array of eyes
        animator = GetComponent<Animator>(); //this gives us access to our animator inside of the PlayerController class
        rb = GetComponent<Rigidbody>();//this will get a component that is a rigid body
        myColor = GameManager.Instance.teams[team];
        transform.Find("Teddy_Body").GetComponent<SkinnedMeshRenderer>().material.color = myColor; //this changes the material color from one thing to another
        startPos = this.transform.position;
        Respawn();

    }
    protected void ShootAt(Transform target)
    {
        Unit unit = target.GetComponent<Unit>();
        if(unit != null)
        {
            unit.OnHit(this);
        }
    }
    protected void ShowLasers(Vector3 targetPosition)
    {
        foreach(Eye eye in eyes)
        {
            Laser laser = Instantiate(laserPrefab) as Laser;//we are creating a laser prefab as a laser
            laser.Init(myColor, eye.transform.position, targetPosition);//we initialize our laser to go from our eye to the target position and the color is the team color
        }
    }
    protected bool CanSee(Transform target, Vector3 targetPosition)
    {
        Vector3 startPos = (eyes[0].transform.position + eyes[1].transform.position) / 2;
        Vector3 direction = targetPosition - startPos;
        if(Vector3.Angle(transform.forward, direction) > viewAngle)
        {
            return false;//if we are trying to look at something and the angle is too large, we cannot see it
        }
        LayerMask mask = ~LayerMask.GetMask("Outpost");
        Ray ray = new Ray(startPos, direction);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            //now we have to check if what we hit is not the target
            if(hit.transform != target)
            {
                return false;//in other words, if we are looking at another object in front of our object, we can't see it.
            }
        }
        return true;//we only get here to return true if we manage to not fail in one of the previous conditions.

    }
    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
    protected bool IsGrounded()
    {
        Vector3 origin = transform.position;
        origin.y += RAYCAST_LENGTH * 0.5f;
        LayerMask mask = LayerMask.GetMask("Terrain");
        return Physics.Raycast(origin, Vector3.down, RAYCAST_LENGTH, mask);
    }
    protected virtual void OnHit(Unit attacker)
    {
        Debug.Log("you hit me");
        health -= attacker.damage; //when you are hit you take some damage
        if(health <= 0) //once your health is below or equal to 0, you die
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        if (!isAlive)
            return;
        isAlive = false;
        gameObject.layer = LayerMask.NameToLayer("DeadTeddy");
        animator.SetBool("Die", true);
        Invoke("Respawn", respawnTime);
    }
    protected virtual void Respawn()
    {
        //when we respawn, we need to 1) reset the position, 2) set them to alive again, 3) end the death animation, change the layer, and give them back some health
        isAlive = true;
        health = fullHealth;
        animator.SetBool("Die", false);
        gameObject.layer = LayerMask.NameToLayer("LiveTeddy");
        this.transform.position = startPos;
    }
}
