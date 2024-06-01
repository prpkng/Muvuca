using System;
using System.Collections;
using CartoonFX;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Muvuca.Core;
using Muvuca.Effects;
using Muvuca.Game.Common;
using Muvuca.Game.Elements.Platform;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Muvuca.Game.Elements.Enemies
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private Transform targetPlatform;
        [SerializeField] private float rotateDuration;
        [SerializeField] private Transform rotateObject;
        [SerializeField] private Transform muzzleOrigin;

        [SerializeField] private Transform lazerTransform;

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
        [SerializeField] private float minShakeDistance;


        [Header("Effects")]
        [SerializeField] private ShakeData shotShake;
        [SerializeField] private ShakeData shotShakeAngle;


        private LaunchPlatform plat;

        private void Awake()
        {
            plat = GetComponent<LaunchPlatform>();
            StartCoroutine(ShootingCoroutine());
        }

        public IEnumerator ShootingCoroutine()
        {
            var counter = 0;
            while (true)
            {
                var dir = ((Vector2)targetPlatform.position - (Vector2)rotateObject.position).normalized;
                rotateObject.transform.up = dir;
                if (Vector2.Distance(PlayerController.Instance.transform.position, transform.position) < minShakeDistance)
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
                yield return new WaitUntil(() => !plat.hasPlayer);
                yield return new WaitForSeconds(fireRate);
            }
        }

        [SerializeField] private LayerMask playerLayer;

        private void LateUpdate()
        {
            rotateObject.up = (targetPlatform.position - rotateObject.position).normalized;
            lazerTransform.up = (targetPlatform.position - lazerTransform.position).normalized;
            lazerTransform.localScale = new Vector3(1f, Vector2.Distance(lazerTransform.position, targetPlatform.position), 1f);
        }

    }
}