using UnityEngine;
using System.Collections;
using Pathfinding.RVO;

public class SimpleRVOObstacle : MonoBehaviour
{

    void Start()
    {
        //Get the simulator for this scene
        Pathfinding.RVO.Simulator sim = (FindObjectOfType(typeof(RVOSimulator)) as RVOSimulator).GetSimulator();

        //Define the vertices of our obstacle
        Vector3[] verts = new Vector3[] { new Vector3(1, 0, -1), new Vector3(1, 0, 1), new Vector3(-1, 0, 1), new Vector3(-1, 0, -1) };

        //Add our obstacle to the simulation, we set the height to 2 units
        sim.AddObstacle(verts, 2);
        AstarPath.active.Scan();
    }
}