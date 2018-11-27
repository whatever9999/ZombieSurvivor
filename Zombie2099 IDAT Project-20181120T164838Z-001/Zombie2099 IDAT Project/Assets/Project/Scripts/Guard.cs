using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour {

    public static event System.Action OnGuardHasSpottedPlayer;

    public float speed;
    public float turnSpeed = 110;
    public float timeToSpotPlayer = 0.5f;

    public Light spotLight;
    public float viewDistance;
    public float presenceRange;
    public LayerMask viewMask;
    public bool isClose;

    float viewAngle;
    float playerVisibleTimer;

    public Transform pathHolder;
    Transform player;
    Color originalSpotlightColour;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        viewAngle = spotLight.spotAngle;
        originalSpotlightColour = spotLight.color;

        Vector3[] waypoints = new Vector3[pathHolder.childCount];

        for(int i = 0; i < waypoints.Length; i++) {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }

        StartCoroutine(FollowPath(waypoints));
    }

    private void Update()
    {
        if (CanSeePlayer())
        {
            playerVisibleTimer += Time.deltaTime;
            FindObjectOfType<Heart>().secondsBetweenBeats -= Time.deltaTime;
        }
        else
        {
            playerVisibleTimer -= Time.deltaTime;
            FindObjectOfType<Heart>().secondsBetweenBeats += Time.deltaTime;
        }
        playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, timeToSpotPlayer);
        spotLight.color = Color.Lerp(originalSpotlightColour, Color.red, playerVisibleTimer / timeToSpotPlayer);

        if (playerVisibleTimer >= timeToSpotPlayer)
        {
            if (OnGuardHasSpottedPlayer != null)
            {
                OnGuardHasSpottedPlayer();
            }
        }

        StartCoroutine(CheckIfClose());
    }

    bool CanSeePlayer() {
        if(Vector3.Distance(transform.position, player.position) < viewDistance) {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);

            if(angleBetweenGuardAndPlayer < viewAngle / 2f) {
                if(!Physics.Linecast(transform.position, player.position, viewMask)) {
                    return true;
                }
            }
        }
        return false;
    }

    bool CloseToPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < presenceRange)
        {
            return true;
        }
        return false;
    }

    IEnumerator CheckIfClose()
    {
        if (CloseToPlayer())
        {
            FindObjectOfType<Heart>().secondsBetweenBeats -= Time.deltaTime * 5.2f;
            if (FindObjectOfType<Heart>().secondsBetweenBeats < 0.5f) FindObjectOfType<Heart>().secondsBetweenBeats = 0.5f;
            isClose = true;
        }
        else
        {
            isClose = false;
        }
        yield return new WaitForSeconds(5);
    }

    IEnumerator FollowPath(Vector3[] waypoints) {
        transform.position = waypoints[0];

        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
        transform.LookAt(targetWaypoint);

        while(true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
            
            if(transform.position == targetWaypoint) {
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];

                yield return StartCoroutine(TurnToFace(targetWaypoint));
            }
                yield return null;
        }
    }

    IEnumerator TurnToFace(Vector3 lookTarget) {
        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f) {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }

    void OnDrawGizmos() {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;

        foreach (Transform waypoint in pathHolder) {
            Gizmos.DrawSphere(waypoint.position, .3f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }
}
