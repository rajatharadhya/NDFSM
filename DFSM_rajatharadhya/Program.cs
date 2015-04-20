/**
 * Author- Rajath Aradhya Mysore Shekar
 * Course - CS5800
 * Assignment 6
 * Date - 3/22/2015
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace DFSM_rajatharadhya
{
    /*! \brief Driving Program */
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader DFSMReader = new StreamReader("69.txt");
            /*! \brief Reading DFSM from a text file.
             *          
             *   text files location is in the project folder under bin\debug\DFSM.txt or input.txt
             *   streamreader is used to read from a text file. 
             *   first line defines DFSM 
             *   each variable is seperated using string.split with a delimeter.
             *   and everything is read into its respective Lists.
             *   After user enters the input, the input is checked for acceptance
             *   */
            int file = 69;
            Console.WriteLine(" Example on page 69");
            actualMain(DFSMReader, file);
            DFSMReader.Close();
            Console.WriteLine();
            Console.WriteLine(" Example on page 71");
            file = 71;
            StreamReader DFSMReader2 = new StreamReader("71.txt");
            actualMain(DFSMReader2, file);
            DFSMReader2.Close();
            Console.WriteLine();
            Console.WriteLine(" Example on page 73");
            file = 73;
            StreamReader DFSMReader3 = new StreamReader("73.txt");
            actualMain(DFSMReader3, file);
            DFSMReader.Close();
            Console.ReadLine();
        }
        static void actualMain(StreamReader DFSMReader, int file)
        {
            string readingline = DFSMReader.ReadLine();
            string[] line = readingline.Split('/');
            List<string> K = new List<string>();    /*!< finite set of states */
            List<char> sigma = new List<char>();    /*!< input alphabets  */

            List<Delta> delta = new List<Delta>();  /*!< trasition function */
            string s;                               /*!< start state */
            List<string> A = new List<string>();    /*!< accepting states */
            K = line[0].Split(',').ToList<string>();

            int j = 0;
            foreach (string str in line[1].Split(','))
            {
                sigma.Add(Convert.ToChar(str));
                j++;
            }


            s = line[2];

            A = line[3].Split(',').ToList<string>();

            while ((readingline = DFSMReader.ReadLine()) != null)
            {
                line = readingline.Split('/');
                delta.Add(new Delta(line[0], Convert.ToChar(line[1]), line[2]));
            }
            DFSMReader.Close();
            nDFSM dfsm = new nDFSM(K, sigma, delta, s, A);
            string inputs;
            StreamReader inputReader;
            if(file == 69)
            inputReader = new StreamReader("input69.txt");
            else if(file == 71)
            {
                 inputReader = new StreamReader("input71.txt");
            }
            else
                 inputReader = new StreamReader("input73.txt");
            while ((inputs = inputReader.ReadLine()) != null)
            {
                Console.WriteLine("Input --> " + inputs);
                dfsm.InputCheckAcceptance(inputs);
            }
        }
    }
}
