using System;
using System.Collections;
using CartoonFX;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Muvuca.Core;
using Muvuca.Effects;
using Muvuca.Game.Common;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Muvuca.Game.Elements.Enemies
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private float rotateDuration;
        [SerializeField] private Transform target;

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
            lastActiveCoroutine = ActivateCoroutine();
            if (!enabled) return;
            StartCoroutine(lastActiveCoroutine);
        }

        public void Stop()
        {
            StopCoroutine(lastActiveCoroutine);
            lastActiveCoroutine = null;
        }
        
        public IEnumerator ActivateCoroutine()
        {
            var dir = ((Vector2)target.position - (Vector2)transform.position).normalized;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            yield return transform.DORotate(Vector3.forward * (angle - 90), rotateDuration).SetEase(Ease.OutCubic).WaitForCompletion();
            yield return new WaitForSeconds(fireStartDelay);

            int counter = 0;
            while (true)
            {
                CameraShaker.TriggerShake(shotShake, shotShakeAngle);
                var a = angle;  
                a += Random.Range(-shootingErrorAngle, shootingErrorAngle);
                a *= Mathf.Deg2Rad;
                dir = Util.VectorFromAngle(a);
                var bullet = Instantiate(bulletPrefab).GetComponent<MoveIntoDir>();
                bullet.transform.position = transform.position;
                bullet.direction = dir; 
                bullet.speed = bulletSpeed;
                counter++;
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