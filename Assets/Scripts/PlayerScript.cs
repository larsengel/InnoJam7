﻿using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public int playerNumber;
	public string playerName;
	private GlewGunScript gun;
    private GameObject planet;
	public Transform catchObject = null;
    public bool catchFollowing = false;
	[SerializeField]
	private float speed;

	public enum DIRECTION
	{LEFT = -1,	NONE = 0, RIGHT = 1	}
	[SerializeField]
	private DIRECTION movingDirection;

	public enum AIM
	{ DOWN = -1, NONE = 0, UP = 1 }
	[SerializeField]
	internal AIM gunDirection;

	void Start () 
	{
		planet = GameMaster.Earth;
		gun = this.gameObject.transform.GetChild(0).gameObject.GetComponent<GlewGunScript>();
	}
	
	// Update is called once per frame
	void Update() 
	{
		GetInputs();
		Move();
		gun.Reloade();
	}


	private void GetInputs()
	{
        switch (playerNumber)
        {
            case 1:
			    movingDirection=(DIRECTION)Input.GetAxis("PlayerAControll");
				gunDirection = (AIM)Input.GetAxis("AimA");
				if(Input.GetKeyDown(KeyCode.Joystick1Button2)) // X
					gun.Fire();
				if(Input.GetKeyDown(KeyCode.Joystick1Button0)) // A
					Catch();
                if(Input.GetKeyDown(KeyCode.Joystick1Button1)) // B
                    Jump();
                break;
            case 2:
			    movingDirection=(DIRECTION)Input.GetAxis("PlayerBControll");
				gunDirection = (AIM)Input.GetAxis("AimB");
				if(Input.GetKeyDown(KeyCode.Joystick2Button2))
					gun.Fire();
				if(Input.GetKeyDown(KeyCode.Joystick2Button0))
                    Catch();
                if(Input.GetKeyDown(KeyCode.Joystick2Button1))
                    Jump();
                break;
        }
	}

	private void Move()
	{

        //move left or right...
        this.transform.position += (movingDirection == DIRECTION.LEFT) ? -this.transform.right * speed * 0.01f : (movingDirection == DIRECTION.RIGHT) ? this.transform.right * speed * 0.01f : Vector3.zero;
        //correct orientation angle...
        this.transform.up = -(planet.GetComponent<CircleCollider2D>().center - new Vector2(this.transform.position.x, this.transform.position.y)).normalized;
        //correct position (distance to planet-center)...
        this.transform.position += -transform.up * ((Vector2.Distance(this.planet.transform.position, new Vector2(this.transform.position.x, this.transform.position.y)) + this.transform.localScale.y) - (planet.GetComponent<CircleCollider2D>().radius * 2 - this.transform.localScale.y * 2));


        if (this.catchObject != null && catchFollowing == true)
        {
            this.catchObject.transform.position += (movingDirection == DIRECTION.LEFT) ? -this.catchObject.transform.right * speed * 0.01f : (movingDirection == DIRECTION.RIGHT) ? this.catchObject.transform.right * speed * 0.01f : Vector3.zero;
            this.catchObject.transform.up = -(planet.GetComponent<CircleCollider2D>().center - new Vector2(this.catchObject.transform.position.x, this.catchObject.transform.position.y)).normalized;
            this.catchObject.transform.position += -transform.up * ((Vector2.Distance(this.planet.transform.position, new Vector2(this.catchObject.transform.position.x, this.catchObject.transform.position.y)) + this.catchObject.transform.localScale.y) - (planet.GetComponent<CircleCollider2D>().radius * 2 - this.catchObject.transform.localScale.y * 2));

        }
    }

    private void Catch()
    {
        if (this.catchObject != null && catchFollowing == false)	//Aufnehmen
        {
            catchFollowing = true;
            this.catchObject.transform.Translate(Vector3.up * 0.4f);

        }
        else if (this.catchObject != null && catchFollowing == true) //Ablegen
        {
            catchFollowing = false;
            this.catchObject.transform.Translate(Vector3.up * -0.4f);
            this.catchObject = null;
        }
    }

    private void Jump()
    {

    }

}
