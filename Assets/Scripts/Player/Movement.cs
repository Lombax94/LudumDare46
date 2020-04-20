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
    public float ColliderX = 0.1f; 
    public float Collidery = 0.2f;

    public Vector2 PreviousPos = Vector2.zero;
    public Vector2 CurrentPos = Vector2.zero;


    public int CurrentX = 0;
    public int CurrentY = 0;

    private void Start() {
        positionX = (int)(transform.position.x * 4);
        positionY = (int)(transform.position.y * 4);
        Debug.Log(positionX + " | " + transform.position.x + " | " + (transform.position.x * 4) + " | " + ((int)(transform.position.x * 4)));
        Debug.Log(positionY + " | " + transform.position.y + " | " + (transform.position.y * 4) + " | " + ((int)(transform.position.y * 4)));

    }

    public void SetNodeType(Vector3 position, int type) {
        collisions.SetNode((int)(position.x * 4), (int)(position.y * 4), type);
    }


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
            if (Input.GetKey(KeyCode.Space)) {
                Jumptimer += Time.deltaTime;
                if (Jumptimer >= 0.3f) {
                    Jumptimer = 0;

                    _enableJump = false;
                    gravity = true;
                    MovementVectors.y = 0;
                    MovementVectors += Vector3.up * jumpforce;
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




        CurrentX = (int)(transform.position.x * 4);
        CurrentY = (int)(transform.position.y * 4);


        if (CurrentY != positionY) {
            Debug.Log("JHE");

            if(collisions.nodes[positionX, positionY].type != collisions.nodes[positionX, CurrentY].type) {


            collisions.nodes[positionX, positionY].OnExit(this);
            collisions.nodes[positionX, CurrentY].OnEnter(this);

            }

     /*       if (CurrentY < positionY) {
                if (collisions.nodes[positionX, CurrentY].type != 0) {
                    transform.position = (Vector3.right * transform.position.x) + (Vector3.up * (CurrentY + (1 * 0.99f)));
                }
            } else {
             
                if (collisions.nodes[positionX, CurrentY].type != 0) {
                    transform.position = (Vector3.right * transform.position.x) + (Vector3.up * (CurrentY + (0.01f)));

                }
            }*/
        }

        if (CurrentX != positionX) {
            if (collisions.nodes[positionX, positionY].type != collisions.nodes[CurrentX, positionY].type) {

                collisions.nodes[positionX, positionY].OnExit(this);
                collisions.nodes[CurrentX, positionY].OnEnter(this);

            }
         /*   if (collisions.nodes[CurrentX, positionY].type != 0) {
                if (CurrentX < positionX)
                    transform.position = (Vector3.right * (CurrentX + (0.99f))) + (Vector3.up * transform.position.y);
                else
                    transform.position = (Vector3.right * (CurrentX + (0.01f))) + (Vector3.up * transform.position.y);
            }*/
        }


        positionX = CurrentX;
        positionY = CurrentY;

    }

  








    public float inX = 0;
    public float inY = 0;
    public Vector2 movementvector = Vector2.zero;

    public void PushOutOfNode(Node n) {


        //but ok, lets just try to push out.

        if((CurrentPos - PreviousPos).y > 0) {
            if(PreviousPos.y + Collidery > n.y) {




                CurrentPos.y = PreviousPos.y + Collidery;
            } else {

                if ((CurrentPos - PreviousPos).x < 0) {
                    CurrentPos.x = PreviousPos.x + ColliderX;
                } else {

                }
            }

        }








        if (PreviousPos.x < n.x + 0.5f) {
        }






    }



    public void OnExitZero() {
        Debug.Log("EXit Air");
        gravity = false;
        MovementVectors = Vector3.zero;

        _enableW = true;
        _enableS = true;

    }
    public void OnExitOne() {
        Debug.Log("EXit Wall");
        //hmmmm, TODO

    }
    public void OnExitTwo() {
        Debug.Log("EXit Ground");

        _enableS = true;
        _enableW = true;

    }
    public void OnExitThree() {
        Debug.Log("EXit ClimbWallRight");
        _enableA = true;
        _enableJump = true;


    }
    public void OnExitFour() {
        Debug.Log("EXit ClimbWallLeft");
        _enableD = true;
        _enableJump = true;


    }
    public void OnExitFive() {
        Debug.Log("EXit ClimbCeiling");
        _enableW = true;
        _enableJump = true;
    }
    public void OnExitSix() {
        Debug.Log("EXit GroundToClimb");
        _enableW = true;

    }
    public void OnExitSeven() {
        Debug.Log("EXit ClimbSideToClimbCeiling");
        _enableJump = true;

    }



    public void OnEnterZero() {
        Debug.Log("Enter Air");
        gravity = true;
        _enableW = false;
        _enableS = false;

    }
    public void OnEnterOne() {
        Debug.Log("Enter Wall");
        //hmmmm, TODO
    }
    public void OnEnterTwo() {
        Debug.Log("Enter Ground");

        _enableS = false;
        _enableW = false;
        _enableJump = true;

    }
    public void OnEnterThree() {
        Debug.Log("Enter ClimbWallRight");
        _enableA = false;
        _enableJump = false;

    }
    public void OnEnterFour() {
        Debug.Log("Enter ClimbWallLeft");
        _enableD = false;
        _enableJump = false;

    }
    public void OnEnterFive() {
        Debug.Log("Enter ClimbCeiling");

        _enableW = false;
        _enableJump = false;

    }
    public void OnEnterSix() {
        Debug.Log("Enter GroundToClimb");
        _enableW = false;
        _enableJump = true;
    
    }
    
    public void OnEnterSeven() {
        Debug.Log("Enter ClimbSideToClimbCeiling");
        _enableJump = false;

    }






    /*
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
        */







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