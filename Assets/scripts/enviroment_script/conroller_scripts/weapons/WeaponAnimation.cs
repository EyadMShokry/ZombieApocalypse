using UnityEngine;
using System.Collections;

public class WeaponAnimation : MonoBehaviour
{
    public Animation anim;
    public AnimationClip fire;
    public AnimationClip reload;
    public AnimationClip draw;
    public KeyCode reloadKey = KeyCode.R;
    public KeyCode fireKey = KeyCode.Mouse0;
    public KeyCode drawKey = KeyCode.D;

    void Awake()
    {
        if (fire == null) Debug.LogError("Please assign a fire aimation in the inspector!");
        if (reload == null) Debug.LogError("Please assign a reload animation in the inspector!");
        if (draw == null) Debug.LogError("Please assign a draw animation in the inspector!");
    }

    // Update is called once per frame
	/*IEnumerator Wait()
	{
		print(Time.time);
		yield return new WaitForSeconds(1);
		print(Time.time);
	}
    void Update()
    {
		/*if (Input.GetKeyDown(reloadKey) && !anim.IsPlaying(fire.name)&&!anim.IsPlaying(draw.name))
        {
            if (reload != null) anim.Play(reload.name);
        }

		else if (Input.GetMouseButton(0) && !anim.IsPlaying(reload.name)&&!anim.IsPlaying(draw.name))
        {
            if (fire != null) anim.Play(fire.name);
        }
		else if (Input.GetKeyDown(drawKey) &&!anim.IsPlaying(fire.name)&&!anim.IsPlaying(reload.name))
        {
            if (draw != null) anim.Play(draw.name);
        }
    }*/
}
