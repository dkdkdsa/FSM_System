using System;
using System.Collections.Generic;
using UnityEngine;

namespace FSM_System
{

    public delegate void FSM_StateChanged<T>(T oldState, T newState) where T : Enum;

    public class FSM_Controller<T> : MonoBehaviour where T : Enum
    {

        private Dictionary<T, List<FSM_State<T>>> stateContainerByEnum = new Dictionary<T, List<FSM_State<T>>>();

        private List<FSM_State<T>> currentStateObjects => stateContainerByEnum[currentState];

        protected T currentState;

        public event FSM_StateChanged<T> OnStateChanged;

        public T CurrentState => currentState;

        protected virtual void Awake()
        {

            currentState = default(T);

        }

        protected virtual void Update()
        {

            foreach(var state in currentStateObjects)
            {

                state.Update();

            }


        }

        public void AddState<TState>(TState stateObject, T enumStateType) where TState : FSM_State<T>
        {

            if (stateContainerByEnum.ContainsKey(enumStateType))
            {

                stateContainerByEnum[enumStateType].Add(stateObject);

            }
            else
            {

                stateContainerByEnum.Add(enumStateType, new List<FSM_State<T>>{stateObject});

            }


        }

        public void ChangeState(T state)
        {

            var oldState = state;

            foreach (var stateObj in currentStateObjects)
            {

                stateObj.Exit();

            }

            currentState = state;

            foreach (var stateObj in currentStateObjects)
            {

                stateObj.Enter();

            }

            OnStateChanged?.Invoke(oldState, state);

        }

        public List<FSM_State<T>> GetState(T type) 
        {

            return stateContainerByEnum[type];

        }

    }

}
