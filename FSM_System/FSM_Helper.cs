using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSM_System
{
    public static class FSM_Helper
    {

        public static FSM_State<T> AddTransition<T>(this FSM_State<T> state, FSM_Transition<T> transition) where T : Enum
        {

            state.AddTransition(transition);
            return state;

        }

    }
}
