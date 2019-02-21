using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Replay
{
    public List<double> states;
    public double reward;

    public Replay(double xr, double ballz, double ballvx, double r)
    {
        states = new List<double>();
        states.Add(xr);
        states.Add(ballz);
        states.Add(ballvx);
        reward = r;
    }
}


public class Brain : MonoBehaviour 
{
    public GameObject ball;

    ANN aNN;
    float reward = 0.0f;                                        //reward to associate with action
    List<Replay> replaysMemory = new List<Replay>();            //memory - list of past action and reward
    int mCapacity = 10000;                                      //memory capacity

    float discount = 0.99f;                                     //how much future states affect rewards
    float exploreRate = 100.0f;                                 //chance of picking random action
    float maxExploreRate = 100.0f;                              //max chance value
    float minExploreRate = 0.01f;                               //min chance value
    float exploreDecay = 0.0001f;                               //chance decay amount for each update

    Vector3 ballStartPos;                                       //record start position of the object
    int failCount = 0;                                          //count when the ball is droped
    float tiltSpeed = 0.5f;                                     //make sure this is large enought so that the q value multiplied by it is enough to recover balance when the ball gets a good speed up
    float timer = 0;                                            //timer to keep track of balancing
    float maxBalanceTime = 0;                                   //record time ball is kept balanced


	// Use this for initialization
	void Start () 
    {
        aNN = new ANN(3, 2, 1, 6, 0.2f);
        ballStartPos = ball.transform.position;
        Time.timeScale = 5.0f;
	}

    GUIStyle gUIStyle = new GUIStyle();
	private void OnGUI()
	{
        gUIStyle.fontSize = 25;
        gUIStyle.normal.textColor = Color.white;
        GUI.BeginGroup(new Rect(10, 10, 600, 150));
        GUI.Box(new Rect(0, 0, 140, 140), "Stats", gUIStyle);
        GUI.Label(new Rect(10, 25, 500, 30), "Fails : " + failCount, gUIStyle);
        GUI.Label(new Rect(10, 50, 500, 30), "Decay Rate : " + exploreRate, gUIStyle);
        GUI.Label(new Rect(10, 75, 500, 30), "Last Best Balance : " + maxBalanceTime, gUIStyle);
        GUI.Label(new Rect(10, 100, 500, 30), "This Balance: " + timer, gUIStyle);
        GUI.EndGroup();
	}

	// Update is called once per frame
	void Update () 
    {
		
	}

	private void FixedUpdate()
	{
        timer += Time.deltaTime;
        List<double> states = new List<double>();
        List<double> qs = new List<double>();

        //this three is inputs
        states.Add(this.transform.rotation.x); 
        states.Add(ball.transform.position.z);
        states.Add(ball.GetComponent<Rigidbody>().angularVelocity.x);

        // all about qs
        //qs = SoftMax(aNN.CalcOutput(states));


	}
}
