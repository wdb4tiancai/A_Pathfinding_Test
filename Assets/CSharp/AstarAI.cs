using UnityEngine;
using System.Collections;
using Pathfinding;

public class AstarAI : MonoBehaviour
{
    //Ŀ��λ��;  
    Vector3 targetPosition;

    Seeker seeker;
    CharacterController characterController;

    //���������·��;  
    Path path;

    //�ƶ��ٶ�;  
    float playerMoveSpeed = 10f;

    //��ǰ��  
    int currentWayPoint = 0;

    bool stopMove = true;

    //Player���ĵ�;  
    float playerCenterY = 1.0f;


    // Use this for initialization  
    void Start()
    {
        seeker = GetComponent<Seeker>();

        playerCenterY = transform.localPosition.y;
    } 
  
    //Ѱ·����;  
    public void OnPathComplete(Path p)
    {  
        Debug.Log("OnPathComplete error = "+p.error);  
  
        if (!p.error)  
        {  
            currentWayPoint = 0;  
            path = p;  
            stopMove = false;  
        }  
  
        for (int index = 0; index<path.vectorPath.Count; index++)  
        {  
            Debug.Log("path.vectorPath["+index+"]="+path.vectorPath[index]);  
        }  
    }  
      
    // Update is called once per frame  
    void Update()
    {  
        if (Input.GetMouseButtonDown(0))  
        {  
            RaycastHit hit;  
            if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))  
            {  
                return;  
            }  
            if (!hit.transform)  
            {  
                return;  
            }  
            targetPosition = hit.point;// new Vector3(hit.point.x, transform.localPosition.y, hit.point.z);  
  
            Debug.Log("targetPosition=" + targetPosition);  
  
            seeker.StartPath(transform.position, targetPosition, OnPathComplete);  
        }  
    }  
  
    void FixedUpdate()
    {  
        if (path == null || stopMove)  
        {  
            return;  
        }  
  
        //����Player��ǰλ�ú� ��һ��Ѱ·���λ�ã����㷽��;  
        Vector3 currentWayPointV = new Vector3(path.vectorPath[currentWayPoint].x, path.vectorPath[currentWayPoint].y + playerCenterY, path.vectorPath[currentWayPoint].z);
    Vector3 dir = (currentWayPointV - transform.position).normalized;

    //������һ֡Ҫ���� dir���� �ƶ����پ���;  
    dir *= playerMoveSpeed* Time.fixedDeltaTime;

    //���������һ֡��λ�ƣ��ǲ��ǻᳬ����һ���ڵ�;  
    float offset = Vector3.Distance(transform.localPosition, currentWayPointV);  
  
        if (offset< 0.1f)  
        {  
            transform.localPosition = currentWayPointV;  
  
            currentWayPoint++;  
  
            if (currentWayPoint == path.vectorPath.Count)  
            {  
                stopMove = true;  
  
                currentWayPoint = 0;  
                path = null;  
            }  
        }  
        else  
        {  
            if (dir.magnitude > offset)  
            {  
                Vector3 tmpV3 = dir * (offset / dir.magnitude);
                dir = tmpV3;  
  
                currentWayPoint++;  
  
                if (currentWayPoint == path.vectorPath.Count)  
                {  
                    stopMove = true;  
  
                    currentWayPoint = 0;  
                    path = null;  
                }  
            }  
            transform.localPosition += dir;  
        }  
    }  
}