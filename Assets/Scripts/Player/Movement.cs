using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public Collision collisions;
    public bool _enableA = true;
    public bool _enableD = true;
    public bool _enableW = true;
    public bool _enableS = true;
    public bool _enableJump = true;
    public bool gravity = true;
    public Vector3 MovementVector = Vector3.zero;
    public Vector3 MovementVectors = Vector3.zero;
    public int positionX = 1;
    public int positionY = 1;
    public float speed = 1f;
    public float gracityval = 0.01f;
    public float jumpforce = 1f;

    public float Jumptimer = 0;
    private void Update() {


        if (_enableA == true) {
            if (Input.GetKey(KeyCode.A)) {
                MovementVector = Vector3.left * Time.deltaTime * speed;
                transform.position += MovementVector;
            }
        }

        if (_enableD == true) {
            if (Input.GetKey(KeyCode.D)) {
                MovementVector = Vector3.right * Time.deltaTime * speed;
                transform.position += MovementVector;
            }
        }
        if (_enableJump == true) {
            if (Input.GetKey(KeyCode.W)) {
                Jumptimer += Time.deltaTime;
                if (Jumptimer >= 0.3f) {
                    Jumptimer = 0;

                    _enableJump = false;
                    MovementVectors.y = 0;
                    MovementVectors += Vector3.up * jumpforce;
                    gravity = true;
                }
            }
        }


        if (_enableW == true) {
            if (Input.GetKey(KeyCode.W)) {
                MovementVector = Vector3.up * Time.deltaTime * speed;
                transform.position += MovementVector;
            }
        }

        if (_enableS == true) {
            if (Input.GetKey(KeyCode.S)) {
                MovementVector = Vector3.down * Time.deltaTime * speed;
                transform.position += MovementVector;
            }
        }



        if (gravity == true) {
            MovementVectors += Vector3.down * Time.deltaTime * 9.81f * gracityval;
            transform.position += MovementVectors;

        }

        if ((int)transform.position.y != positionY) {
            Debug.Log("JHE");
            collisions.nodes[positionX, positionY].OnExit(this);
            collisions.nodes[positionX, (int)transform.position.y].OnEnter(this);


            if ((int)transform.position.y < positionY) {
                if (collisions.nodes[positionX, (int)transform.position.y].type != 0) {
                    transform.position = (Vector3.right * transform.position.x) + (Vector3.up * ((int)transform.position.y + (1 * 0.99f)));
                }
            } else {
             
                if (collisions.nodes[positionX, (int)transform.position.y].type != 0) {
                    transform.position = (Vector3.right * transform.position.x) + (Vector3.up * ((int)transform.position.y + (0.01f)));

                }
            }
        }

        if ((int)transform.position.x != positionX) {
            collisions.nodes[positionX, positionY].OnExit(this);
            collisions.nodes[(int)transform.position.x, positionY].OnEnter(this);

            if (collisions.nodes[(int)transform.position.x, positionY].type != 0) {
                if ((int)transform.position.x < positionX)
                    transform.position = (Vector3.right * ((int)transform.position.x + (0.99f))) + (Vector3.up * transform.position.y);
                else
                    transform.position = (Vector3.right * ((int)transform.position.x + (0.01f))) + (Vector3.up * transform.position.y);
            }
        }


        positionY = (int)transform.position.y;
        positionX = (int)transform.position.x;

    }



    public void OnExitGround() {
        Debug.Log("EXITONGROUND");
        _enableS = true;
    }
    public void OnEnterGround() {
        Debug.Log("ENTER Ground");
        _enableS = false;
        gravity = false;
        _enableJump = true;
        MovementVectors = Vector3.zero;

    }
    public void OnExitClimb() {
        Debug.Log("exit CLIMB");
        _enableD = true;
        _enableW = false;
        gravity = true;
        MovementVectors = Vector3.zero;
        _enableJump = true;

    }
    public void OnEnterClimb() {
        Debug.Log("ENTER CLUMB");
        _enableD = false;
        _enableW = true;
        _enableJump = false;
        gravity = false;
        MovementVectors = Vector3.zero;

    }
    public void OnExitRoofClimb() {
        Debug.Log("Roof cliumb Exit");
        gravity = true;
        _enableJump = true;
        _enableW = false;
        _enableS = false;
        MovementVectors = Vector3.zero;

    }
    public void OnEnterRoofClumb() {
        Debug.Log("ENTER RoofCllumb");
        _enableJump = false;
        _enableW = false;
        _enableS = true;
        gravity = false;
        MovementVectors = Vector3.zero;
        
    }








}
/*
public class Movement : MonoBehaviour {

    public Collision test;
    public Vector3 PreviousPosition = Vector3.zero;
    public Vector3 CurrentPosition = Vector3.zero;
    public Vector3 MovementVector = Vector3.zero;
    public Vector3 MovementVectorGravity = Vector3.zero;
    public Vector3 MovementVectorTest = Vector3.zero;

    public Vector2 ColliderPoints = new Vector2(-0.25f, 0.25f);
    public float HeadPoint = 0;
    public float FeetPoint = 0;


    public float OffsetVectorDist = 0.1f;


    public bool testingConnections = false;

    public int PlayerPositionX = 1;
    public int PlayerPositionY = 1;

    private bool _enableA = true;
    public bool EnableA { get { return _enableA; } set { _enableA = value; } }

    private bool _enableD = true;
    public bool EnableD { get { return _enableD; } set { _enableD = value; } }

    public bool EnableW = false;
    public bool EnableS = false;

    public bool CanJump = false;
    public bool Grounded = false;
    private int ButtonsDown = 0;
    public bool Walking = false;

    public float MovementSpeed = 1f;
    public float JumpForce = 10f;
    public float GravityModifyer = 0.01f;

    // Start is called before the first frame update
    void Start() {
        transform.position = Vector3.one * 3.5f;
    }

    // Update is called once per frame
    void Update() {

        if (testingConnections == true) {
            testingConnections = false;
        }

        if (_enableA == true) {
            if (Input.GetKey(KeyCode.A)) {
                MovementVector += Vector3.left * Time.deltaTime;
                Walking = true;
            }
        }

        if (_enableD == true) {
            if (Input.GetKey(KeyCode.D)) {
                MovementVector += Vector3.right * Time.deltaTime;
                Walking = true;
            }
        }

        if (EnableW == true) {
            if (Input.GetKey(KeyCode.W)) {
                if (CanJump == true) {
                    MovementVectorGravity.y = 0;
                    MovementVectorGravity += Vector3.up * Time.deltaTime * JumpForce;
                } else {
                    MovementVector += Vector3.up * Time.deltaTime;
                }
                Walking = true;
            }
        }

        if (EnableS == true) {
            if (Input.GetKey(KeyCode.S)) {
                MovementVector += Vector3.down * Time.deltaTime;
                Walking = true;
            }
        }

        if (Grounded == false) {
            MovementVectorGravity += Vector3.down * Time.deltaTime * 9.81f * GravityModifyer;
            Walking = true;
        }


        if (Grounded == false || Walking == true) {
            HeadPoint = transform.position.y + ColliderPoints.y;
            FeetPoint = transform.position.y + ColliderPoints.x;

            test.NodeMapTest.CollisionCheck(this);
        }


    }

    public void UpdatePositions() {
        transform.position += MovementVector * MovementSpeed;
        transform.position += MovementVectorGravity;
        MovementVector = Vector3.zero;
        Walking = false;

        CurrentPosition = transform.position;

        PlayerPositionX = (int)CurrentPosition.x;
        PlayerPositionY = (int)CurrentPosition.y;

    }

    public void IgnoreGravity() {
        Grounded = true;
        MovementVectorGravity = Vector3.zero;
    }





























}
*/