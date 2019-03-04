using System;
using System.Collections.Generic;
namespace assembly.Logic
{
    public class ArgumentModeler
    {
        public static char SEPERATOR;
        public static string[] RemoveFirst(string[] args)
        {
            string[] newArgs = new string[args.Length];
            newArgs[0] = SEPERATOR.ToString();
            for (int i = 1; i < args.Length; i++)
            {
                newArgs[i] = args[i];
            }
            return newArgs;
        }
        public static string[] RemoveFirstAndAdd(string[] args, string extra)
        {
            string[] newArgs = new string[args.Length + 1];
            newArgs[0] = SEPERATOR.ToString();
            for (int i = 1; i < args.Length; i++)
            {
                newArgs[i] = args[i];
            }
            newArgs[args.Length] = extra;
            return newArgs;
        }
        public static string[] RemoveFirstAndAdd(string[] args, string extra1, string extra2)
        {
            string[] newArgs = new string[args.Length + 2];
            newArgs[0] = SEPERATOR.ToString();
            for (int i = 1; i < args.Length; i++)
            {
                newArgs[i] = args[i];
            }
            newArgs[args.Length] = extra1;
            newArgs[args.Length + 1] = extra2;
            return newArgs;
        }
        public static string[] RemovePairs(string[] args)
        {
            string[] newArgs = new string[args.Length / 2 + 1];
            newArgs[0] = SEPERATOR.ToString();
            for (int i = 1, j = 1; i < args.Length; i += 2, j++)
            {
                newArgs[j] = args[i];
            }
            return newArgs;
        }
        public static string[] RemovePairsAndAdd(string[] args, string extra)
        {
            string[] newArgs = new string[args.Length / 2 + 2];
            newArgs[0] = SEPERATOR.ToString();
            for (int i = 1, j = 1; i < args.Length; i += 2, j++)
            {
                newArgs[j] = args[i];
            }
            newArgs[args.Length / 2 + 1] = extra;
            return newArgs;
        }
        public static string[] RemovePairsAndAdd(string[] args, string extra1, string extra2)
        {
            string[] newArgs = new string[args.Length / 2 + 3];
            newArgs[0] = SEPERATOR.ToString();
            for (int i = 1, j = 1; i < args.Length; i += 2, j++)
            {
                newArgs[j] = args[i];
            }
            newArgs[args.Length / 2 + 1] = extra1;
            newArgs[args.Length / 2 + 2] = extra2;
            return newArgs;
        }
        public static string[] RemovePairsAndAdd(string[] args, string extra1, string extra2, string extra3)
        {
            string[] newArgs = new string[args.Length / 2 + 4];
            newArgs[0] = SEPERATOR.ToString();
            for (int i = 1, j = 1; i < args.Length; i += 2, j++)
            {
                newArgs[j] = args[i]; 
            }
            newArgs[args.Length / 2 + 1] = extra1;
            newArgs[args.Length / 2 + 2] = extra2;
            newArgs[args.Length / 2 + 3] = extra3;
            return newArgs;
        }

        public static string[] RemoveOddsAndAdd(string[] args, string extra)
        {
            string[] newArgs = new string[args.Length / 2 + 3];
            newArgs[0] = SEPERATOR.ToString();
            for (int i = 0, j = 1; i < args.Length; i += 2, j++)
            {
                newArgs[j] = args[i];
            }
            newArgs[args.Length / 2 + 2] = extra;
            return newArgs;
        }
        public static string[] cutArguments(string[] args, int indxIni, int indxEnd) {
            string[] cutArgs = new string[indxEnd - indxIni + 1];
            for (int i = indxIni, count = 0; i <= indxEnd; i ++, count ++) {
                cutArgs[count] = args[i];
            }
            return cutArgs;
        }
    }
}
