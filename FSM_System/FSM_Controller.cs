using System;
using System.Collections.Generic;
using UnityEngine;

namespace FSM_System
{

    public delegate void StateChanged<T>(T oldState, T newState) where T : Enum;

    public class FSM_Controller<T> : MonoBehaviour where T : Enum
    {

        private Dictionary<T, FSM_State<T>> stateContainerByEnum = new Dictionary<T, FSM_State<T>>();
        private Dictionary<Type, FSM_State<T>> stateContainerByType = new Dictionary<Type, FSM_State<T>>();

        private FSM_State<T> currentStateObject => stateContainerByEnum[currentState];

        protected T currentState;

        public event StateChanged<T> OnStateChanged;

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

            var stateType = typeof(TState);

            if (stateContainerByEnum.ContainsKey(enumStateType))
            {

                Debug.LogError("키가 중복됨");

            }
            else
            {

                stateContainerByEnum.Add(enumStateType, stateObject);

            }

            if (stateContainerByType.ContainsKey(stateType))
            {

                Debug.LogError("키가 중복됨");

            }
            else
            {

                stateContainerByType.Add(stateType, stateObject);

            }


        }

        public void ChangeState(T state)
        {

            var oldState = state;

            currentStateObject.Exit();
            currentState = state;
            currentStateObject.Enter();

            OnStateChanged?.Invoke(oldState, state);

        }

    }

}
