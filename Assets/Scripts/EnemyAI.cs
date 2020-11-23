using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    Transform target;
    public float speed = 200f;
    public float nextWayPointDistance = 3f;
    public Transform enemyGFX;
    private Animator animator;

    public Vector3 startPos;
    public Vector3 endPos;
    private float wanderSpeed = 1f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    Scene activeScene;
    string activeSceneName;
    bool chasePlayerOn;
    private bool chase;

    private EnemyStats stats;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
        animator = GetComponent<Animator>();
        stats = gameObject.GetComponentInChildren<EnemyStats>();
        chase = false;

        findScene();
        // InvokeRepeating("UpdatePath", 0f, 0.5f);

    
    }

    void findScene(){

        activeScene = SceneManager.GetActiveScene();
        activeSceneName = activeScene.name;
        if (activeSceneName == "TutorialScene"){
            chasePlayerOn = false;
        }
        else{
            chasePlayerOn = true;
            InvokeRepeating("UpdatePath", 0f, 0.5f);
        }

        
    }

    void UpdatePath(){
        if (seeker.IsDone()){
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p){
        if(!p.error){
            path = p;
            currentWaypoint = 0;
        }
    }

    void Awake(){
         startPos = this.transform.position;
         endPos = new Vector3(this.transform.position.x - 2, this.transform.position.y, this.transform.position.z);
         
     }

    // Update is called once per frame
    void FixedUpdate()
    {

        setChase();

        float minAttackDistance = 12.5f;

        float distance = Vector3.Distance(target.position, enemyGFX.position);

        if (chasePlayerOn){
            if (distance < minAttackDistance)
            {
                animator.SetBool("isMoving", true);
                Chase();
            }
            else{
                animator.SetBool("isMoving", false);       
            }
        }
  
    }

    public void setChase(){
        if (!chase){
            chase = checkStats();
        if (chase){
            turnOnUpdatePath();
            chasePlayerOn = true;
            }
        }
    }

    public bool checkStats()
    {
        if(stats.getHit)
        {  
            // chasePlayerOn = true;
            return true;
            // It is object tagged with TagB
        }
        return false;
    }

    public void turnOnUpdatePath(){
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }


    void Chase(){

        if (path == null){
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count){
            reachedEndOfPath = true;
            return;
        }
        else{
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWayPointDistance){
            currentWaypoint++;
        }

        changeDirection(force);

    }

     void changeDirection(Vector2 force){

        if (rb.velocity.x <= -0.01f){
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
            // animator.SetFloat("moveX", force.x);
            // animator.SetBool("isMoving", true);
        
        }
        else if(rb.velocity.x >= 0.01f){
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
            //  animator.SetFloat("moveX", force.x);
            // animator.SetBool("isMoving", true);
        }
         

     }
 
}
