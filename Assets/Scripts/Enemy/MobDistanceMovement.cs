using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobDistanceMovement : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent;
    private MobDistance _mob;

    [SerializeField]
    private GameObject _enemyBullet;
    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    private float _maxRange = 10f;
    [SerializeField]
    private float _fieldOfVision = 20f;
    [SerializeField]
    private int _playerMask;
    
    private bool _isAtPlayerRange;
    private bool _hasPlayerLineVision;
    
    private bool isVisionBlocked;
    private bool playerInFOV;

    // Use this for initialization
    void Start() {
        //Initializes the agent with the very same script that is attached to the game object.
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
        agent = GetComponent<NavMeshAgent>();
        _mob = GetComponent<MobDistance>();
    }

    // Update is called once per frame
    void Update() {
        if (_isAtPlayerRange && _hasPlayerLineVision) {
            Shoot();
        } else {
            move();
        }

        _isAtPlayerRange = Vector3.Distance(transform.position, target.transform.position) <= _maxRange;
        _hasPlayerLineVision = CheckPlayerInFOV();
    }

    private void move() {
        if (target != null) { //Clean coding
            agent.isStopped = false;
            Debug.DrawLine(transform.position, target.transform.position, Color.blue);
            agent.SetDestination(target.transform.position);
        }
    }

    private void Shoot() {
        agent.isStopped = true;
        agent.transform.LookAt(target);

        if (Time.time >= _mob.nextAttackTimeDistance) {
            //launch projectile
            Debug.Log("shoot");
            LaunchBullet();
            _mob.nextAttackTimeDistance = Time.time + 1f / _mob.attackRateDistance;
        }
    }

    private void LaunchBullet() {
        GameObject bullet = Instantiate(_enemyBullet, _firePoint.position, _firePoint.rotation);
        Rigidbody rbBullet = bullet.GetComponent<Rigidbody>();
        rbBullet.AddForce(_firePoint.forward * _mob.bulletSpeed, ForceMode.Impulse);
    }

    private bool CheckPlayerInFOV() {
        Vector3 targetDirection = target.transform.position - transform.position;
        playerInFOV = Vector3.Angle(transform.TransformDirection(Vector3.forward), targetDirection) <= _fieldOfVision;

        //isVisionBlocked;

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << _playerMask;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(_firePoint.position, targetDirection, out hit, Mathf.Infinity, _playerMask)) {
            isVisionBlocked = true;
            //Debug.DrawRay(_firePoint.position, targetDirection * hit.distance, Color.red);
        } else {
            isVisionBlocked = false;
            //Debug.DrawRay(_firePoint.position, targetDirection * 1000, Color.white);
        }
        return playerInFOV && !isVisionBlocked;
    }
}
