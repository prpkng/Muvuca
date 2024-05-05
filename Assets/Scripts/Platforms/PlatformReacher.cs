using Muvuca.Player;
using Muvuca.Systems;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Scripting;

namespace Muvuca.Elements
{
    public class PlatformReacher : MonoBehaviour
    {
        private DistanceChecker distanceChecker;
        private void Start()
        {
            distanceChecker = GetComponent<DistanceChecker>();
            distanceChecker.target = PlayerController.Instance.transform;
            distanceChecker.entered += Entered;
        }

        private void OnDestroy()
        {
            distanceChecker.entered -= Entered;
        }

        private void Entered()
        {
            PlayerController.Instance.collidedWithPlatform?.Invoke(transform);
        }

        const float minDistance = 1;




    }
}