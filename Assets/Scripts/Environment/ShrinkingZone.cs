using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShrinkingZone : MonoBehaviour
{
    [SerializeField]
    private float _startRadius = 100f;
    [SerializeField]
    private float _endRadius = 10f;
    //[SerializeField]
    //[Range(1, 20)]
    public int nbShrink;
    [SerializeField]
    private float _timeToShrink;

    [HideInInspector]
    public bool _isShrinking;

    private float _sizeShrink;
    private float _actualSize;

    private bool _playerIsOutOfZone;

    [SerializeField]
    private int _damageZone = 5;
    [SerializeField]
    private float _attackRate = 1f;
    private float _nextAttackTime = 0f;
    [SerializeField]
    private PlayerLife _playerLife;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(_startRadius, transform.localScale.y, _startRadius);
        _sizeShrink = (_startRadius - _endRadius) / nbShrink;
        _actualSize = _startRadius;
    }

    //Update is called once per frame
    void Update() {
        if (Time.time >= _nextAttackTime) {
            if (_playerIsOutOfZone) {
                DamagePlayer();
                _nextAttackTime = Time.time + 1f / _attackRate;
            }
        }
    }

    public void Shrink() {
        if (!_isShrinking && nbShrink != 0) {
            _isShrinking = true;

            DOTween.To(
                () => transform.localScale,
                x => transform.localScale = x,
                new Vector3(_actualSize - _sizeShrink, transform.localScale.y, _actualSize - _sizeShrink),
                _timeToShrink)
                .SetEase(Ease.Linear)
                .OnComplete(() => UpdateShrink());
        }
    }

    private void UpdateShrink() {
        _isShrinking = false;
        nbShrink--;
        _actualSize -= _sizeShrink;
    }

    private void DamagePlayer() {
        _playerLife.TakeDamage(_damageZone);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            Debug.Log("in zone");
            _playerIsOutOfZone = false;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {
            Debug.Log("in zone");
            _playerIsOutOfZone = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            Debug.Log("out of zone");
            _playerIsOutOfZone = true;
        }
    }
}
