﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine
{
    abstract class AStateTransducer<T>
    {
        //definition says : ordered 6 objects
        protected List<object> orderedList = new List<object>();

        public IEnumerable<T> OutputAlphabet { get => (IEnumerable<T>)orderedList[2]; }
        public AStateTransducer(List<AState<T>> allStates, List<T> finiteInputSymbols ,IEnumerable<T> outputAlphabet, Sigma<T> changeStateFunction, AState<T> startState, List<FinalState<T>> finalStates)
        {
            orderedList.Add(allStates);
            orderedList.Add(finiteInputSymbols);

            orderedList.Add(outputAlphabet);
            
            orderedList.Add(changeStateFunction);
            orderedList.Add(startState);
            orderedList.Add(finalStates);
        }
    }
}