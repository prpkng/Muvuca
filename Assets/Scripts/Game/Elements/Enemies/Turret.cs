using System;
using System.Collections;
using CartoonFX;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Muvuca.Core;
using Muvuca.Effects;
using Muvuca.Game.Common;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Muvuca.Game.Elements.Enemies
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private float rotateDuration;
        [SerializeField] private Transform rotateObject;
        [SerializeField] private Transform muzzleOrigin;

        [SerializeField] private UnityEvent activated;
        [SerializeField] private UnityEvent deactivated;
        [SerializeField] private UnityEvent fired;
        
        [SerializeField] private UnityEvent firstWarning;
        [SerializeField] private UnityEvent secondWarning;
        [SerializeField] private UnityEvent thirdWarning;
        [SerializeField] private UnityEvent resetWarnings;
        
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float fireStartDelay;
        [SerializeField] private float fireRate;
        [SerializeField] private int burstFireCount;
        [SerializeField] private float burstDelay;
        [SerializeField] private float shootingErrorAngle = 10;
        [SerializeField] private float bulletSpeed;

        [Header("Effects")]
        [SerializeField] private ShakeData shotShake;
        [SerializeField] private ShakeData shotShakeAngle;

        private IEnumerator lastActiveCoroutine;
        
        public void Activate()
        {
            activated.Invoke();
            lastActiveCoroutine = ActivateCoroutine();
            if (!enabled) return;
            StartCoroutine(lastActiveCoroutine);
        }

        public void Stop()
        {
            resetWarnings.Invoke();
            deactivated.Invoke();
            StopCoroutine(lastActiveCoroutine);
            lastActiveCoroutine = null;
        }
        
        public IEnumerator ActivateCoroutine()
        {
            var dir = ((Vector2)PlayerController.Instance.col.bounds.center - (Vector2)muzzleOrigin.position).normalized;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            firstWarning.Invoke();
            yield return rotateObject.DORotate(Vector3.forward * (angle - 90), rotateDuration).SetEase(Ease.OutCubic).WaitForCompletion();
            secondWarning.Invoke();
            yield return new WaitForSeconds(fireStartDelay);
            thirdWarning.Invoke();

            int counter = 0;
            while (true)
            {
                dir = ((Vector2)PlayerController.Instance.col.bounds.center - (Vector2)muzzleOrigin.position).normalized;
                rotateObject.transform.up = dir; 
                CameraShaker.TriggerShake(shotShake, shotShakeAngle);
                var a = Util.AngleFromVectorDegrees(dir);  
                a += Random.Range(-shootingErrorAngle, shootingErrorAngle);
                a *= Mathf.Deg2Rad;
                dir = Util.VectorFromAngle(a);
                var bullet = Instantiate(bulletPrefab).GetComponent<MoveIntoDir>();
                bullet.transform.position = muzzleOrigin.position;
                bullet.direction = dir; 
                bullet.speed = bulletSpeed;
                counter++;
                fired.Invoke();
                if (counter > burstFireCount)
                {
                    counter = 0;
                    yield return new WaitForSeconds(burstDelay);
                }
                yield return new WaitForSeconds(fireRate);
            }
        }
        
        
    }
}