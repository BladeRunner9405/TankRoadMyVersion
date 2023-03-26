using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.StickyNote;

public class Coin : MonoBehaviour
{
    [SerializeField] private Animator _coinAnimator;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private Color _coinColorStart;
    [SerializeField] private Color _coinColorEnd;
    [SerializeField] private float _animationTime;
    [SerializeField] private Renderer _rend;

    private bool _dissapear;

    private void Awake()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Wallet>(out Wallet _wallet))
        {
            _coinAnimator.SetTrigger("Collect");
            _audio.Play();
            _dissapear = true;
            _rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            _wallet.CollectCoint(1);
        }
    }

    public void Update()
    {
        if (_dissapear)
        {
            float lerp = Mathf.PingPong(Time.time, _animationTime) / _animationTime;
            _rend.material.color = Color.Lerp(_coinColorStart, _coinColorEnd, lerp);
        }
    }
}
