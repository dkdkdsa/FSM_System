using System;
using System.Collections.Generic;
using UnityEngine;

namespace FSM_System
{

    public delegate void FSM_StateChanged<T>(T oldState, T newState) where T : Enum;

    public class FSM_Controller<T> : MonoBehaviour where T : Enum
    {

        private Dictionary<T, FSM_State<T>> stateContainerByEnum = new Dictionary<T, FSM_State<T>>();

        private FSM_State<T> currentStateObject => stateContainerByEnum[currentState];

        protected T currentState;

        public event FSM_StateChanged<T> OnStateChanged;

        public T CurrentState => currentState;

        protected virtual void Awake()
        {

            currentState = default(T);

        }

        protected virtual void Update()
        {

            currentStateObject.Update();



        }

        protected void AddState<TState>(TState stateObject, T enumStateType) where TState : FSM_State<T>
        {

            stateContainerByEnum.Add(enumStateType, stateObject);

        }

        public void ChangeState(T state)
        {

            var oldState = state;

            currentStateObject.Exit();
            currentState = state;
            currentStateObject.Enter();

            OnStateChanged?.Invoke(oldState, state);

        }

        public FSM_State<T> GetState(T type) 
        {

            return stateContainerByEnum[type];

        }

    }

}
