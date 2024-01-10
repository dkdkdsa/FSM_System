using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace FSM_System.Netcode
{
    public class FSM_State_Netcode<T> where T : Enum
    {


        private HashSet<FSM_Transition<T>> transitionContainer = new HashSet<FSM_Transition<T>>();

        protected Transform transform;
        protected GameObject gameObject;
        protected FSM_Controller_Netcode<T> controller;
        protected bool isControllRelesed;
        protected NetworkObject NetworkObject => controller.NetworkObject;
        protected ulong OwnerClientId => controller.OwnerClientId;
        protected bool IsClient => controller.IsClient;
        protected bool IsServer => controller.IsServer;
        protected bool IsOwner => controller.IsOwner;
        protected bool IsHost => controller.IsHost;

        public FSM_State_Netcode(FSM_Controller_Netcode<T> controller)
        {

            this.controller = controller;
            gameObject = controller.gameObject;
            transform = controller.transform;

        }

        public TCompo GetComponent<TCompo>()
        {

            return controller.GetComponent<TCompo>();

        }

        public Coroutine StartCoroutine(IEnumerator coroutine)
        {

            return controller.StartCoroutine(coroutine);

        }

        public void StopCoroutine(Coroutine coroutine)
        {

            controller.StopCoroutine(coroutine);

        }

        public void Enter()
        {

            isControllRelesed = false;
            EnterState();

        }

        public void Exit()
        {

            isControllRelesed = true;
            ExitState();

        }

        public void Update()
        {

            foreach (var trans in transitionContainer)
            {

                trans.RunTransition();

            }

            if (!isControllRelesed)
            {

                UpdateState();

            }

        }

        protected virtual void EnterState() { }
        protected virtual void ExitState() { }
        protected virtual void UpdateState() { }

        public void AddTransition(FSM_Transition<T> transition)
        {

            transitionContainer.Add(transition);

        }


    }
}
