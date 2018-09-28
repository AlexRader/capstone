using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FOV : MonoBehaviour
{
    public float viewRadius;

    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask, obsticleMask;

    [HideInInspector]
    public List<Transform> visTarg = new List<Transform>();

    public float meshRes;
    public int edgeIter;
    public float edgeDistThreshold = .1f;

    public float maskCutawayDist;

    public MeshFilter viewMF;
    Mesh viewM;
    private void Start()
    {
        viewM = new Mesh();
        viewM.name = "View Mesh";
        viewMF.mesh = viewM;

        StartCoroutine("FindTargetsWithDelay", 0.2f);
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
    void LateUpdate()
    {
        DrawFOV();    
    }

    void FindVisibleTargets()
    {
        visTarg.Clear();
        Collider[] targetsInVR = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for(int i = 0; i < targetsInVR.Length; ++i )
        {
            Transform target = targetsInVR[i].transform;
            Vector3 dir2Target = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dir2Target) < viewAngle / 2)
            {
                float dist2Target = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dir2Target, dist2Target, obsticleMask))
                {
                    visTarg.Add(target);
                }
            }
        }
    }

    void DrawFOV()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshRes);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();

        ViewCastInfo oldViewCast = new ViewCastInfo();

        for (int i = 0; i < stepCount; ++i)
        {
            float angle = transform.eulerAngles.z - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newVC = ViewCast(angle);

            if(i > 0)
            {
                bool edgeDistThreshExceed = Mathf.Abs(oldViewCast.mDist - newVC.mDist) > edgeDistThreshold;
                if(oldViewCast.mHit != newVC.mHit || (oldViewCast.mHit && newVC.mHit && edgeDistThreshExceed))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newVC);
                    if(edge.mPointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.mPointA);
                    }
                    if (edge.mPointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.mPointB);
                    }
                }
            }

            viewPoints.Add(newVC.mPoint);
            oldViewCast = newVC;
        }

        int vertexCount = viewPoints.Count + 1;

        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for(int i = 0; i < vertexCount-1; ++i)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]) + Vector3.forward * maskCutawayDist;

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewM.Clear();
        viewM.vertices = vertices;
        viewM.triangles = triangles;
        viewM.RecalculateNormals();
    }

    EdgeInfo FindEdge(ViewCastInfo minVC, ViewCastInfo maxVC)
    {
        float minAngle = minVC.mAngle;
        float maxAngle = maxVC.mAngle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for(int i = 0; i < edgeIter; ++i)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newVC = ViewCast(angle);

            bool edgeDistThreshExceed = Mathf.Abs(minVC.mDist - newVC.mDist) > edgeDistThreshold;

            if (newVC.mHit == minVC.mHit && !edgeDistThreshExceed)
            {
                minAngle = angle;
                minPoint = newVC.mPoint;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newVC.mPoint;
            }
        }
        return new EdgeInfo(minPoint, maxPoint);
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obsticleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public struct ViewCastInfo
    {
        public bool mHit;
        public Vector3 mPoint;
        public float mDist, mAngle;

        public ViewCastInfo(bool hit, Vector3 point, float dist, float angle)
        {
            mHit = hit;
            mPoint = point;
            mDist = dist;
            mAngle = angle;
        }
    }

    public struct EdgeInfo
    {
        public Vector3 mPointA, mPointB;

        public EdgeInfo(Vector3 pointA, Vector3 pointB)
        {
            mPointA = pointA;
            mPointB = pointB;
        }
    }

}
