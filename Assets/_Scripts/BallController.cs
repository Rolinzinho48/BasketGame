using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallController : MonoBehaviour
{
    public float force;
    private Rigidbody2D rb;
    private Vector2 dir;
    private int cond;

    public static bool canPlay;
    bool havePoint;

    public GameObject[] Points;
    public GameObject prefab;
    public GameObject prefab2;
    
   
    public int numberPoints;

    public Pontos point;

    void Start()
    {
        Points = new GameObject[numberPoints];
        
        rb = GetComponent<Rigidbody2D>();
        canPlay = false;
        havePoint = false;
        cond = 1;
       
    }
    void Update()
    {
        if(canPlay){
            LookAtMousePos();
            ApplyForce();
            OrganizaPosiçãoPontos();
        }
        
        if(Input.GetMouseButtonDown(0)){
            canPlay = true;
        }
    }
    private void LookAtMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        dir = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = dir;
        cond = 0;
    }
    private void ApplyForce()
    {
        if (Input.GetMouseButtonUp(0))
        {
            rb.AddForce(new Vector2(-dir.x*force, -dir.y*force));
            cond = 1;
            canPlay = false;
        }
    }

    private Vector2 PontosCalculados(float t)
    {
        //p = p1 + v * t + (a * t ^ 2) / 2
        Vector2 pontos = (Vector2)transform.position + (-dir * (force / 50)) * t + 0.5f * Physics2D.gravity * t * t;
        return pontos;
    }
    private void OrganizaPosiçãoPontos()
    {
        if(cond == 0){
            if(Points[0]!=null){
                for (int i = 0; i < numberPoints; i++)
                    {
                        Points[i].transform.position = PontosCalculados(i*0.1f);
                    }
            }
        }
        else if(cond == 1){
            for (int i = 0; i < numberPoints; i++)
            {
                
                Destroy(Points[i]);
                havePoint = false;
            }
        }
    }
   

    void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.tag == "ground"){
            cond = 0;
            
            if((!havePoint) && (rb.velocity.y==0f)){
                for (int i = 0; i < numberPoints; i++)
                {
                    Points[i] = Instantiate(prefab, transform.position, Quaternion.identity);
                    Points[i].transform.parent = this.gameObject.transform; 
                    
                }
                havePoint = true;
            }

        }
        if(col.gameObject.tag == "cesta"){
            Pontos.AddCont();
          //  Destroy(GetComponent<BallController>());
            Instantiate(prefab2, new Vector2(Random.Range(-6.0f, 3.0f),0), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}