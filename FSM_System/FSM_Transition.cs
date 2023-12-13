using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FSM_System
{

    public abstract class FSM_Transition<T> where T : Enum
    {

        protected Transform transform;
        protected GameObject gameObject;
        protected FSM_Controller<T> controller;
        protected T nextState;

        public FSM_Transition(FSM_Controller<T> controller, T nextState) 
        {
            
            this.controller = controller;

            gameObject = controller.gameObject;
            transform = controller.transform;
            
            this.nextState = nextState;

        }

        protected abstract bool CheckTransition();

        public void RunTransition()
        {

            if(CheckTransition())
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
