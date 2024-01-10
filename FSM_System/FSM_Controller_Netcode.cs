using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;

namespace FSM_System.Netcode
{
    public class FSM_Controller_Netcode<T> : NetworkBehaviour where T : Enum
    {

        private Dictionary<T, List<FSM_State_Netcode<T>>> stateContainerByEnum = new Dictionary<T, List<FSM_State_Netcode<T>>>();

        private List<FSM_State_Netcode<T>> currentStateObjects => stateContainerByEnum[currentState];

        protected T currentState;

        public event FSM_StateChanged<T> OnStateChanged;

        public T CurrentState => currentState;

        protected virtual void Awake()
        {

            currentState = default(T);

        }

        protected virtual void Update()
        {

            foreach (var state in currentStateObjects)
            {

                state.Update();

            }


        }

        public void AddState<TState>(TState stateObject, T enumStateType) where TState : FSM_State_Netcode<T>
        {

            if (stateContainerByEnum.ContainsKey(enumStateType))
            {

                stateContainerByEnum[enumStateType].Add(stateObject);

            }
            else
            {

                stateContainerByEnum.Add(enumStateType, new List<FSM_State_Netcode<T>> { stateObject });

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

        public List<FSM_State_Netcode<T>> GetState(T type)
        {

            return stateContainerByEnum[type];

        }

    }

}
