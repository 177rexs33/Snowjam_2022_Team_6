using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ22
{
    public class ActionButton : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] ActionParameters actionProperties;

        [SerializeField] EventSO actionDisabledEvent;
        [SerializeField] EventSO actionReadyEvent;
        [SerializeField] EventSO actionActiveEvent;
        [SerializeField] EventSO actionRechargingEvent;

        void Reset()
        {
            animator = GetComponentInChildren<Animator>();
        }

        void Awake()
        {
            animator.SetFloat("recharge_speed", 1 / actionProperties.RechargeDuration);
            actionReadyEvent.Event += OnActionReady;
            actionActiveEvent.Event += OnActionActive;
            actionRechargingEvent.Event += OnActionRecharging;
            actionDisabledEvent.Event += OnActionDisabled;
        }

        void OnDestroy()
        {
            actionReadyEvent.Event -= OnActionReady;
            actionActiveEvent.Event -= OnActionActive;
            actionRechargingEvent.Event -= OnActionRecharging;
            actionDisabledEvent.Event -= OnActionDisabled;
        }

        void OnActionReady()
        {
            animator.SetTrigger("ready");
        }

        void OnActionActive()
        {
            animator.SetTrigger("active");
        }

        void OnActionRecharging()
        {
            animator.SetTrigger("recharging");
        }

        void OnActionDisabled()
        {
            animator.SetTrigger("disabled");
        }
    }
}
