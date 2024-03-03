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
    public abstract class FSM_Transition_Netcode<T> where T : Enum
    {

        protected Transform transform;
        protected GameObject gameObject;
        protected FSM_Controller_Netcode<T> controller;
        protected T nextState;
        protected NetworkObject NetworkObject => controller.NetworkObject;
        protected ulong OwnerClientId => controller.OwnerClientId;
        protected bool IsClient => controller.IsClient;
        protected bool IsServer => controller.IsServer;
        protected bool IsOwner => controller.IsOwner;
        protected bool IsHost => controller.IsHost;

        public FSM_Transition_Netcode(FSM_Controller_Netcode<T> controller, T nextState)
        {

            this.controller = controller;

            gameObject = controller.gameObject;
            transform = controller.transform;

            this.nextState = nextState;

        }

        protected abstract bool CheckTransition();

        public void RunTransition()
        {

            if (CheckTransition())
            {

                controller.ChangeState(nextState);

            }

        }

        public TCompo GetComponent<TCompo>()
        {

            return controller.GetComponent<TCompo>();

        }

        public Coroutine StartCoroutine(IEnumerator coroutine)
        {

            return controller.StartCoroutine(coroutine);

        }

    }
}
