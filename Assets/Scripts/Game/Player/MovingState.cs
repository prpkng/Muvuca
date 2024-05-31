using System.Linq;
using Muvuca.Core;
using Muvuca.Game.Elements.Platform;
using UnityEngine;

namespace Muvuca.Game.Player
{
    public class MovingState : PlayerState
    {

        private float counter;
        public Vector3 direction;

        public override void Enter(string[] data = null)
        {
            direction = data is { Length: <= 0 } ? Vector3.zero : Util.DeserializeVector3Array(data[0])[0];

            player.enteredPlatform += EnteredPlatform;

            InputManager.AttackPressed += AttackPressed;
            counter = 0;
        }

        private void AttackPressed()
        {
            player.PlayAnimation("attack");
        }

        public override void Exit()
        {
            player.enteredPlatform -= EnteredPlatform;
            InputManager.AttackPressed -= AttackPressed;
        }

        private void EnteredPlatform(Transform platform)
        {
            Debug.Log("Entered platform!");
            player.platform = platform;
            player.transform.position = platform.position;
            if (platform.TryGetComponent(out LaunchPlatform plat))
                plat.hasPlayer = true;
            else if (platform.GetChild(0).TryGetComponent(out plat))
            {
                plat.hasPlayer = true;
                player.platform = platform.GetChild(0);
            }

            machine.ChangeState("idle", null);
        }

        public override void Update()
        {
            counter += Time.deltaTime;
            var speed = player.distanceSpeedCurve.Evaluate(counter) * player.movingSpeed;
            if (speed <= 0)
            {
                var plat = LaunchPlatform.availablePlatforms.OrderBy(p =>
                    Vector2.Distance(p.transform.position, player.transform.position)).ElementAt(0).transform;
                if (Vector2.Distance(plat.position, player.transform.position) < player.minimumReturnDistance)
                    player.platform = plat;
                machine.ChangeState("return");
                return;
            }
            player.transform.position += speed * Time.deltaTime * direction;
        }
    }
}