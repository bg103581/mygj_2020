using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    [SerializeField]
    private bool _isTouchingPlayer;

    private MobDistance _mob;
    // Start is called before the first frame update
    void Start()
    {
        _mob = transform.parent.GetComponent<MobDistance>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= _mob.nextAttackTime) {
            if (_isTouchingPlayer) {
                Attack();
                _mob.nextAttackTime = Time.time + 1f / _mob.attackRate;
            }
        }
    }

    private void Attack() {
        //Debug.Log("hurt player");
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            _isTouchingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            _isTouchingPlayer = false;
        }
    }
}
