using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhicLine : MonoBehaviour
{
    public List<GameObject> circlePool;
    public int poolSize=2;

    public List<GameObject> circleList;

    public GameObject cir;
    //the pointer object always follow the mouse position
    private GameObject pointer;
    private GameObject prev;

    //top two object distance
    [SerializeField]
    float distance;

    //last mouse position
    Vector3 lmp;

    float offsetY;
    float offsetX;

    public SpriteRenderer head;

    //when enconter a new obstcal sign a new startPoint
    public GameObject startPoint;

    /*
    public GameObject[] circles;
    //public Vector2[] points;
    */
    public float force = 100;
    private int len = 2;
    //public Color c1 = Color.yellow;
    //public Color c2 = Color.red;

    void Start()
    {
        //populate the pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject circle = Instantiate(cir, transform.position, Quaternion.identity);
            circle.SetActive(false);
            circlePool.Add(circle);
        }

        //add the first circle in to queue and set rigidbody static
        GameObject firstCircle = Instantiate(cir, transform.position, Quaternion.identity);
        firstCircle.transform.position = new Vector3(0, -3, 0);
        //before the circle pool is empty the last circle is static
        //firstCircle.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        circleList.Add(firstCircle);

        //add the point circle
        GameObject PointerCircle = Instantiate(cir, transform.position, Quaternion.identity);
        PointerCircle.transform.position = new Vector3(0, -2.5f, 0);
        //PointerCircle.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        circleList.Add(PointerCircle);
        len = circleList.Count;
        //len = circles.Length;

        //points = new Vector2[len];

        //LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        //lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        //lineRenderer.widthMultiplier = 0.2f;
        //lineRenderer.positionCount = len;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        //float alpha = 1.0f;
        //Gradient gradient = new Gradient();
        //gradient.SetKeys(
        //    new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
        //    new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        //);
        //lineRenderer.colorGradient = gradient;
        startPoint = circleList[0];

    }

    //check distance from start to end
    void checkDisSE(GameObject start)
    {
        //GameObject start = circleList[0];
        GameObject end = circleList[len - 1];
        float currentDis = Vector3.Distance(end.transform.position, start.transform.position);
        //resonable count
        int RCount = (int)(currentDis / 0.1);
        if (circleList.Count>4&&circleList.Count > RCount)
        {
            GameObject gonaRemove = circleList[len-2];
            gonaRemove.SetActive(false);
            circleList.Remove(circleList[len-2]);
        }
    }

    void checkTopTwoDistance()
    {
        GameObject top = circleList[len-1];

        GameObject second = circleList[len - 2];

        distance = Vector3.Distance(top.transform.position, second.transform.position);


        if (distance > 0.2)
        {
            //add one to list
            for (int i = 0; i < circlePool.Count; i++)
            {
                GameObject c = circlePool[i];
                if (!c.activeSelf)
                {
                    c.transform.position = top.transform.position;
                    c.SetActive(true);
                    circleList.Add(c);
                    circleList[len - 1].GetComponent<SpriteRenderer>().color = Color.white;
                    len++;
                    circleList[len - 1].GetComponent<SpriteRenderer>().color = Color.red;
                    break;
                }
            }
        }

    }

    void Update()
    {
        //last circle collider to the ob;

        len = circleList.Count;
        //LineRenderer line = GetComponent<LineRenderer>();
        //float t = Time.time;
        // for (int j = 0; j < len; j++) {
        //    Vector3 po = new Vector3(j*0.5f,Mathf.Sin(j+t),0);
        //    line.SetPosition(j,po);
        // }

        //for (int i = 0; i < len; i++)
        //{
        //  Vector3 po = new Vector3(points[i].x, points[i].y, 0);
        //line.SetPosition(i, po);
        //}

            var mousePos = Input.mousePosition;
            mousePos.z = 0.5f;
        

        if (Input.GetMouseButtonDown(0))
        {
            //record current mousePosition
            lmp = Camera.main.ScreenToWorldPoint(mousePos);

        }

        if (Input.GetMouseButton(0))
        {
            //Debug.Log("moving");
            offsetX = Camera.main.ScreenToWorldPoint(mousePos).x - lmp.x;
            offsetY = Camera.main.ScreenToWorldPoint(mousePos).y - lmp.y;

            GameObject top = circleList[len - 1];
            top.transform.position = new Vector3(top.transform.position.x + offsetX/100, top.transform.position.y+offsetY/100, top.transform.position.z );

        }


        linePhysics();
        checkTopTwoDistance();
        checkDisSE(startPoint);

    }


    //line objects
    void linePhysics()
    {
        //make points attracted to one another

        //apply gravity
        //for (int j = 1; j < len - 1; j++)
        //{
        //    points[j] += Vector2.down * 9.8f * Time.deltaTime / len;

        //}

        for (int i = 1; i < len-1; i++)
        {
            Vector3 offsetToPrev = circleList[i - 1].transform.position - circleList[i].transform.position;
            Vector3 offsetToNext = circleList[i + 1].transform.position - circleList[i].transform.position;
            Vector3 velocity = offsetToPrev * force + offsetToNext * force;

            circleList[i].transform.position+=velocity * Time.deltaTime / len*20;
        }

    }


}