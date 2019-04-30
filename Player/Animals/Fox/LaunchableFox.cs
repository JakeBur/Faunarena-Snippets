using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchableFox : LaunchableAnimal
{
    public class FoxHitInfo
    {
        public Vector3 direction;
        public bool hit;
        public FoxHitInfo(Vector3 direction)
        {
            this.direction = direction;
            this.hit = true;
        }

        public void SetNotHit() { hit = false; }
    }
    public FoxHitInfo info;

    public override void Launch(float force, Vector3 direction)
    {
        if (!HitStop.HitStopActive) HitStop.Instance.ToggleHitStop();
        storedForce = direction.normalized * force;

        //Rumble the player when they get hit
        InputManager iM = this.GetComponent<InputManager>();
        iM.Rumble();

        // the fox was launched
        info = new FoxHitInfo(direction);

        CameraShake.Instance.ShakeScreen(50, .2f);
    }
}
