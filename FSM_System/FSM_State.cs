using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FSM_System
{
    public class FSM_State<T> where T : Enum
    {

        private HashSet<FSM_Transition<T>> transitionContainer = new HashSet<FSM_Transition<T>>();

        protected Transform transform;
        protected GameObject gameObject;
        protected FSM_Controller<T> controller;
        protected bool isControllRelesed;

        public FSM_State(FSM_Controller<T> controller)
        {

            this.controller = controller;
            gameObject = controller.gameObject;
            transform = controller.transform;

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

            UpdateState();

        }

        protected virtual void EnterState() { }
        protected virtual void ExitState() { }
        protected virtual void UpdateState() { }

    }

}
