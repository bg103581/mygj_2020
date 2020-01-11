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
    [SerializeField]
    [Range(1, 20)]
    private int _nbShrink;
    [SerializeField]
    private float _timeToShrink;

    [HideInInspector]
    public bool _isShrinking;

    private float _sizeShrink;
    private float _actualSize;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(_startRadius, transform.localScale.y, _startRadius);
        _sizeShrink = (_startRadius - _endRadius) / _nbShrink;
        _actualSize = _startRadius;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !_isShrinking && _nbShrink != 0) {
            Shrink();
        }
    }

    private void Shrink() {
        _isShrinking = true;
        DOTween.To(
            ()=>transform.localScale, 
            x=> transform.localScale = x, 
            new Vector3(_actualSize - _sizeShrink, transform.localScale.y, _actualSize - _sizeShrink), 
            _timeToShrink)
            .SetEase(Ease.Linear)
            .OnComplete(()=> UpdateShrink());
    }

    private void UpdateShrink() {
        _isShrinking = false;
        _nbShrink--;
        _actualSize -= _sizeShrink;
    }
}
