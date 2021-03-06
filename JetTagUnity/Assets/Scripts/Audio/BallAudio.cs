﻿using UnityEngine;
using System.Collections;

public class BallAudio : MonoBehaviour
{
    private Chara ball;
    public WorldSound bump_sound_prefab;
    public WorldSound burst_sound, pickup_sound;
    //public int bump_sound_pool_buffer = 2;

    public void Awake()
    {
        //if (bump_sound_prefab != null) ObjectPool.Instance.RequestObjects(bump_sound_prefab, bump_sound_pool_buffer, true);
        //if (possess_sound_prefab != null) ObjectPool.Instance.RequestObjects(possess_sound_prefab, 1, true);

        ball = GetComponent<Chara>();
        ball.on_bump_wall += PlayBumpSound;
        ball.on_tag += OnTag;
        ball.on_pickup += OnPickup;
    }

    private void OnTag(Chara tagger, Chara target)
    {
        if (tagger == ball)
        {
            burst_sound.SetPitchOffset(Random.Range(-0.1f, 0.05f));
            burst_sound.Play();
        }
    }
    private void OnPickup(Pickup pu)
    {
        pickup_sound.SetPitchOffset(Random.Range(-0.05f, 0.05f));
        pickup_sound.Play();
    }
    private void PlayBumpSound(float force)
    {
        if (bump_sound_prefab == null) return;
        //WorldSound s = ObjectPool.Instance.GetObject(bump_sound_prefab, false);
        WorldSound s = Instantiate(bump_sound_prefab);

        s.transform.position = transform.position;
        s.base_volume = Mathf.Pow(force, 2);
        s.SetPitchOffset(Random.Range(-0.15f, 0.15f));
        s.Play();
    }
}
