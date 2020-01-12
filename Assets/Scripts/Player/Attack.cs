using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Attack : MonoBehaviour {
    public Weapon weaponInHand;

    [SerializeField]
    private Transform _pivotWeapon;

    private void Update() {
        if (Time.time >= weaponInHand.nextAttackTime) {
            LaunchAttack();
        }
    }

    private void LaunchAttack() {
        if (weaponInHand.isCac) {
            if (Input.GetMouseButtonDown(0)) {
                Debug.Log("nik");
                Swing();
                weaponInHand.nextAttackTime = Time.time + 1f / weaponInHand.attackRate;
            }
        } else {
            if (Input.GetMouseButton(0)) {
                Shoot();
                weaponInHand.nextAttackTime = Time.time + 1f / weaponInHand.attackRate;
            }
        }
    }

    private void Swing() {
        Sequence swingSequence = DOTween.Sequence();

        float duration = 1f / weaponInHand.attackRate;

        swingSequence.Append(_pivotWeapon.DOLocalRotate(new Vector3(0f, 25f, 0f), duration * 0.1f)).SetEase(Ease.Linear);
        swingSequence.AppendCallback(() => weaponInHand.col.enabled = true);
        swingSequence.Append(_pivotWeapon.DOLocalRotate(new Vector3(0f, -40f, 0f), duration * 0.8f)).SetEase(Ease.Linear);
        swingSequence.AppendCallback(() => weaponInHand.col.enabled = false);
        swingSequence.Append(_pivotWeapon.DOLocalRotate(Vector3.zero, duration * 0.1f)).SetEase(Ease.Linear);
    }

    private void Shoot() {
        GameObject bullet = Instantiate(weaponInHand.bullet, weaponInHand.firePoint.position, weaponInHand.firePoint.rotation);
        Rigidbody rbBullet = bullet.GetComponent<Rigidbody>();
        rbBullet.AddForce(weaponInHand.transform.forward * weaponInHand.bulletSpeed, ForceMode.Impulse);
    }
}
