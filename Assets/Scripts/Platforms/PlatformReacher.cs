using Muvuca.Player;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Muvuca.Elements
{
    public class PlatformReacher : MonoBehaviour
    {

        const float minDistance = 1;


        private IEnumerator Start()
        {
            while (true)
            {
                if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position)
                    < minDistance)
                {
                    PlayerController.Instance.collidedWithPlatform?.Invoke(transform);
                    Debug.DrawLine(transform.position, PlayerController.Instance.transform.position,
                        Color.green, .09f);
                    Destroy(this);
                    break;
                }
                Debug.DrawLine(transform.position,
                    PlayerController.Instance.transform.position, Color.red, .09f);
                yield return new WaitForSeconds(0.1f);
            }
        }

    }
}