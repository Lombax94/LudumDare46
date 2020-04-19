﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {

    public Node[,] nodes = new Node[10, 10];

    private void Start() {

        for (int i = 0; i < 10; i++) {
            for (int j = 0; j < 10; j++) {
                nodes[i, j] = new Node();
                nodes[i, j].type = 0;
            }
        }

        for (int i = 0; i < 10; i++) {
            for (int j = 0; j < 1; j++) {
                nodes[i, j].type = 1;
            }
        }

        for (int i = 9; i < 10; i++) {
            for (int j = 1; j < 9; j++) {
                nodes[i, j].type = 2;
            }
        }

        for (int i = 0; i < 10; i++) {
            for (int j = 7; j < 8; j++) {
                nodes[i, j].type = 4;
            }
        }

       

        nodes[9, 0].type = 3;


    }

    public bool Draw = false;
    private void OnDrawGizmos() {
        if (Draw == true) {

            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    if (nodes[i, j].type == 0) {
                        Gizmos.color = Color.blue;

                    }
                    if (nodes[i, j].type == 1) {
                        Gizmos.color = Color.red;

                    }
                    if (nodes[i, j].type == 2) {
                        Gizmos.color = Color.green;

                    }
                    if (nodes[i, j].type == 4) {
                        Gizmos.color = Color.yellow;

                    }
                    if (nodes[i, j].type == 2) {
                        Gizmos.color = Color.grey;

                    }
                    Gizmos.DrawSphere((Vector3.right * i) + (Vector3.up * j) + (Vector3.one * 0.5f), 0.25f);

                }
            }
        }
    }

}

public class Node {




    public int type = 0;
    public void OnExit(Movement m) {
        if (type == 1)
            m.OnExitGround();
        if (type == 2)
            m.OnExitClimb();
        if (type == 3) {
            m.OnExitClimb();
            m.OnExitGround();
        }
        if (type == 4) {
            m.OnExitRoofClimb();
        }
    }

    public void OnEnter(Movement m) {
        if (type == 1)
            m.OnEnterGround();
        if (type == 2)
            m.OnEnterClimb();
        if (type == 3) {
            m.OnEnterGround();
            m.OnEnterClimb();
        }
        if (type == 4) {
            m.OnEnterRoofClumb();
        }
    }

}




/*
public class NodeBehaviourClimbable : NodeBehaviours {
public override void EnterNode(Movement m, int x, int y) {
   throw new System.NotImplementedException();
}

public override void ExitNode(Movement m, int x, int y) {

}
public override bool NodeBehaviourExecuted(Movement m, int x, int y) {
   Debug.Log("Climbable");
   return false;
}

}


public class Collision : MonoBehaviour {

public NodeMap NodeMapTest = new NodeMap();

// Start is called before the first frame update
void Start() {

}

// Update is called once per frame
void Update() {

}

public bool Draw = false;
void OnDrawGizmos() {
   // Draw a yellow sphere at the transform's position

   if (Draw == true) {

       /*    for (int i = 0; i < 40; i++) {
               for (int j = 0; j < 40; j++) {
                   if (NodeMapTest._Nodes[i, j].nodeType == 0) {
                       Gizmos.color = Color.blue;

                   }
                   if (NodeMapTest._Nodes[i, j].nodeType == 1) {
                       Gizmos.color = Color.red;

                   }
                   if (NodeMapTest._Nodes[i, j].nodeType == 2) {
                       Gizmos.color = Color.green;

                   }
                   Gizmos.DrawSphere((Vector3.right * 0.25f * i) + (Vector3.up * 0.25f * j), 1);

               }
           }*/

/*   }
}

}

public class NodeMap {

const int DimentionX = 10;
const int DimentionY = 10;
const int Size = 4;

public Nodes[,] _Nodes = new Nodes[DimentionX * Size, DimentionY * Size];
public NodeBehaviours[] testing = new NodeBehaviours[3];

int nodePointX = 0;
int nodePointY = 0;


public NodeMap() {
   testing[0] = new NodeBehaviourAir();
   testing[1] = new NodeBehaviourRock();
   testing[2] = new NodeBehaviourClimbable();

   for (int i = 0; i < DimentionX * Size; i++) {
       for (int j = 0; j < DimentionY * Size; j++) {
           _Nodes[i, j] = new Nodes();
           _Nodes[i, j].nodeType = 0;
           _Nodes[i, j].nodeIdX = i;
           _Nodes[i, j].nodeIdY = j;
       }
   }


   for (int i = 0; i < 10; i++) {
       for (int j = 0; j < 4; j++) {

           _Nodes[i, j].nodeType = 1;

       }
   }

   for (int i = 36; i < 40; i++) {
       for (int j = 4; j < 40; j++) {

           _Nodes[i, j].nodeType = 1;


       }
   }


   //_Nodes[0 + i, 0].nodeType = 1;
   //_Nodes[0 + i, 9].nodeType = 1;
   //_Nodes[0, 0 + i].nodeType = 1;
   //_Nodes[9, 0 + i].nodeType = 1;
}




public void CollisionCheck(Movement m) {
   //   nodePointX = (int)(m.transform.position.x + m.MovementVectorGravity.x + m.MovementVector.x);

   if (m.MovementVectorGravity.y + m.MovementVector.y > 0) {//Jumping (or being levetated)
       nodePointY = (int)(m.HeadPoint + m.MovementVectorGravity.y + m.MovementVector.y);
       if (nodePointY > m.PlayerPositionY) {
           testing[_Nodes[m.PlayerPositionX, m.PlayerPositionY].nodeType].ExitNode(m, m.PlayerPositionX, m.PlayerPositionY);
           testing[_Nodes[m.PlayerPositionX, nodePointY].nodeType].EnterNode(m, m.PlayerPositionX, nodePointY);
       }
   }

   if (m.MovementVectorGravity.y + m.MovementVector.y < 0) {//Falling
       nodePointY = (int)(m.FeetPoint + m.MovementVectorGravity.y + m.MovementVector.y);
       if (nodePointY < m.PlayerPositionY) {
           testing[_Nodes[m.PlayerPositionX, m.PlayerPositionY].nodeType].ExitNode(m, m.PlayerPositionX, m.PlayerPositionY);
           testing[_Nodes[m.PlayerPositionX, nodePointY].nodeType].EnterNode(m, m.PlayerPositionX, nodePointY);
       }
   }









   /*
   //check upper and lower collision vectors. then if all that is fine, go to the middel vector which is movement vector.
   //m.MovementVector find the boxes i collide with.
   nodePointX = (int)(m.transform.position.x + m.MovementVectorGravity.x + m.MovementVector.x);
   Debug.Log("HERE");
   if (m.MovementVectorGravity.y + m.MovementVector.y > 0) {//Jumping (or being levetated)
       nodePointY = (int)(m.HeadPoint + m.MovementVectorGravity.y + m.MovementVector.y);

       if (nodePointX != m.PlayerPositionX && nodePointY != m.PlayerPositionY) {//If Jumping Into 2 Different Nodes At The Same Time.
       //       Debug.LogWarning("Warning, Exception Detected");
           if (testing[_Nodes[m.PlayerPositionX, nodePointY].nodeType].NodeBehaviourExecuted(m, m.PlayerPositionX, nodePointY) == false) {
               testing[_Nodes[nodePointX, m.PlayerPositionY].nodeType].NodeBehaviourExecuted(m, nodePointX, m.PlayerPositionY);
           }
       } else {
           if (nodePointX != m.PlayerPositionX) {//Jumping Into A Node to The Side
               testing[_Nodes[nodePointX, m.PlayerPositionY].nodeType].NodeBehaviourExecuted(m, nodePointX, m.PlayerPositionY);
           } else if (nodePointY != m.PlayerPositionY) {//Jumping Into A Node Above
               testing[_Nodes[m.PlayerPositionX, nodePointY].nodeType].NodeBehaviourExecuted(m, m.PlayerPositionX, nodePointY);
           } else {
               //Nothing Was Collided With;
           }
       }
   } else if (m.MovementVectorGravity.y + m.MovementVector.y < 0) {
   Debug.Log("Falling");
       nodePointY = (int)(m.FeetPoint + m.MovementVectorGravity.y + m.MovementVector.y);

       if (nodePointX != m.PlayerPositionX && nodePointY != m.PlayerPositionY) {//If Jumping Into 2 Different Nodes At The Same Time.
       //       Debug.LogWarning("Warning, Exception Detected");
           if (testing[_Nodes[m.PlayerPositionX, nodePointY].nodeType].NodeBehaviourExecuted(m, m.PlayerPositionX, nodePointY) == false) {
               testing[_Nodes[nodePointX, m.PlayerPositionY].nodeType].NodeBehaviourExecuted(m, nodePointX, m.PlayerPositionY);
           }
       } else {
           if (nodePointX != m.PlayerPositionX) {//Jumping Into A Node to The Side
               testing[_Nodes[nodePointX, m.PlayerPositionY].nodeType].NodeBehaviourExecuted(m, nodePointX, m.PlayerPositionY);
           } else if (nodePointY != m.PlayerPositionY) {//Jumping Into A Node Above
               testing[_Nodes[m.PlayerPositionX, nodePointY].nodeType].NodeBehaviourExecuted(m, m.PlayerPositionX, nodePointY);
           } else {
               //Nothing Was Collided With;
           }
       }

   } else {

       if (nodePointX != m.PlayerPositionX) {//Jumping Into A Node to The Side
           testing[_Nodes[nodePointX, m.PlayerPositionY].nodeType].NodeBehaviourExecuted(m, nodePointX, m.PlayerPositionY);
       } else {
           //This Happends If 1. Player Stands Completely Still. 2. Player Jumps Straight Up And Gravity Lowers The Y Value To 0, But Is Still Falling. So Do NOT Do Anything Here Other Then Wait On Nodes To Stop The Player.
       }

   }

   m.UpdatePositions();*/
/*
}


}

public class Nodes {

public int nodeType = 0;
public int nodeIdX = 0;
public int nodeIdY = 0;

}

public abstract class NodeBehaviours {

public abstract bool NodeBehaviourExecuted(Movement m, int x, int y);

public abstract void ExitNode(Movement m, int x, int y);
public abstract void EnterNode(Movement m, int x, int y);


}

public class NodeBehaviourRock : NodeBehaviours {
public override void EnterNode(Movement m, int x, int y) {

}

public override void ExitNode(Movement m, int x, int y) {

}

public override bool NodeBehaviourExecuted(Movement m, int x, int y) {
Debug.Log("Rock Executed");

if (m.PlayerPositionX != x && m.PlayerPositionY != y) {//if im going towards a node that is not to any side but diagonal
    //       Debug.LogWarning("Warning, Exception Detected");
    m.MovementVector.x = 0;//TODO needs testing, not sure if this will work.

} else if (m.PlayerPositionX != x && m.PlayerPositionY == y) {//going towards left or right

    if (m.PlayerPositionX < x) {
        m.EnableD = false;
    } else {
        m.EnableA = false;
    }


    m.MovementVectorGravity.x = 0;
    m.MovementVector.x = 0;

} else { //(m.PlayerPositionX == x && m.PlayerPositionY != y) { //going towards up or down

    if (m.PlayerPositionY < y) {
        m.MovementVectorGravity.y /= 2;
    } else {
        m.MovementVector.y = 0;
        m.MovementVectorGravity.y = 0;
        m.Grounded = true;

    }

}

return true;

}


}



































public class NodeBehaviourAir : NodeBehaviours {
public override void EnterNode(Movement m, int x, int y) {
throw new System.NotImplementedException();
}

public override void ExitNode(Movement m, int x, int y) {

}
public override bool NodeBehaviourExecuted(Movement m, int x, int y) {
Debug.Log("AIR");
return false;
}

}



public class NodeBehaviourClimbable : NodeBehaviours {
public override void EnterNode(Movement m, int x, int y) {
throw new System.NotImplementedException();
}

public override void ExitNode(Movement m, int x, int y) {

}
public override bool NodeBehaviourExecuted(Movement m, int x, int y) {
Debug.Log("Climbable");
return false;
}

}*/
   