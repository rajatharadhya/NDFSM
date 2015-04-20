/**
 * Author- Rajath Aradhya Mysore Shekar
 * Course - CS5800
 * Assignment 6
 * Date - 3/22/2015
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFSM_rajatharadhya
{
    /*! \brief Constructor */
    public class nDFSM
    {
        List<string> K = new List<string>();    /*!< finite set of states */
        List<char> Sigma = new List<char>();    /*!< input alphabets  */
        List<Delta> deltA = new List<Delta>();  /*!< trasition function */
        string S;                               /*!< start state */
        List<string> A = new List<string>();    /*!< accepting states */
        List<string> ST = new List<string>();
        List<string> epsilon = new List<string>();
        /*! \brief Constructor */
        public nDFSM(List<string> k, List<char> sigma, List<Delta> delta, string s, List<string> a)
        {
            K = k.ToList();
            Sigma = sigma.ToList();
            AddDelta(delta);
            AddInitialState(s);
            Acceptingstates(a);
        }

        private void addToST()
        {
            ST.Add(S);
            foreach (Delta delt in deltA)
            {
                if (delt.StartState == S && delt.InputSymbol == 'E')
                {
                    ST.Add(delt.EndState);
                }
            }
            List<string> checkST = new List<string>();
            checkST = ST;
            foreach (String start in checkST.ToList())
            {
                recursiveAddToST(start);
            }
        }
        private void recursiveAddToST(string start)
        {
            Delta del = deltA.Find(t => t.StartState == start && t.InputSymbol == 'E');
            if (del != null && !ST.Contains(del.EndState) && del.StartState != S)
            {
                ST.Add(del.EndState);
                recursiveAddToST(del.EndState);
            }
        }
        private bool ValidDelta(Delta delt)
        {
            return K.Contains(delt.StartState) && K.Contains(delt.EndState) &&
                   Sigma.Contains(delt.InputSymbol); /*!< check if start and end state is in delta and also check if thsymbol is valid */
        }

        private void AddDelta(List<Delta> delta) /*!< Setting all transition states */
        {
            foreach (Delta del in delta.Where(ValidDelta))
            {
                deltA.Add(del);
            }
        }

        private void AddInitialState(string s)
        {
            if (s != null && K.Contains(s))
            {
                S = s;/*!< setting intial state */
            }
        }



        private void Acceptingstates(List<string> acceptingstates)
        {
            foreach (string acceptState in acceptingstates.Where(finalState => K.Contains(finalState)))
            {
                A.Add(acceptState); /*!< setting accepting state */
            }
        }

        /*! InputCheckAcceptance is used to check if the input entered by user is acceppted or rejected  */


        public void InputCheckAcceptance(string input) /*!< input from the file */
        {
            epsilon.Clear();
            foreach (Delta de in deltA)
            {
                if (de.InputSymbol == 'E')
                {
                    if(!epsilon.Contains(de.StartState))
                    {
                        epsilon.Add(de.StartState);
                    }
                }
            }
            ST.Clear();
            addToST();
            List<string> ST1 = new List<string>();
            bool checkAcceptance = false;
            if (InputValidate(input) && ValidateDFSM())
            {
                return;
            }
           
            foreach (char inputSym in input.ToCharArray())
            {
                ST1.Clear();
                foreach(string q in ST)
                {
                    foreach(Delta d in deltA)
                    {
                        if (d.StartState == q && d.InputSymbol == inputSym && !ST1.Contains(d.EndState))
                        {
                            ST1.Add(d.EndState);
                            if(epsilon.Contains(d.EndState))
                            {
                                recursiveAddToST1(d.EndState,ST1);
                            }
                        }
                    }
                }
                ST.Clear();
                foreach (string stg in ST1)
                    ST.Add(stg);
                
                
                //currentState = del.EndState;
                //trace.Append(del + "\n");
            }
            foreach (string q in ST)
            {
                if(A.Contains(q))
                    checkAcceptance = true;
            }
            if (checkAcceptance)
            {
                Console.WriteLine("Input Accepted\n " );
                return;
            }
            Console.WriteLine("Input Rejected\n" );
            //Console.WriteLine(trace.ToString());
            /*!< returns if it is acccepted or rejected */
        }

        private List<string> recursiveAddToST1(string start, List<string> ST1)
        {
            Delta del = deltA.Find(t => t.StartState == start && t.InputSymbol == 'E');
            if (del != null && !ST1.Contains(del.EndState) )
            {
                ST1.Add(del.EndState);
                recursiveAddToST1(del.EndState,ST1);
            }
            return ST1;
        }
        /*!
          To check if the input entered by user is valid
        */
        private bool InputValidate(string inputs) /*!< input from the file  */
        {
            foreach (char input in inputs.ToCharArray().Where(input => !Sigma.Contains(input)))
            {
                Console.WriteLine(input + " --> wrong input \n");
                return true;
            }
            return false; /*!< true if input is valid */
        }

        /*!
        To check if DFSM has valid initial state and accepting state
      */
        private bool ValidateDFSM()
        {
            if (CheckIntialState())
            {
                Console.WriteLine("Missing intial state");
                return true;
            }
            if (checkAcceptingState())
            {
                Console.WriteLine("Missing final state");
                return true;
            }
            return false; /*!< return true if DFSM is valid */
        }



        private bool CheckIntialState()
        {
            return string.IsNullOrEmpty(S);
        }

        private bool checkAcceptingState()
        {
            return A.Count == 0;
        }

    }
}
