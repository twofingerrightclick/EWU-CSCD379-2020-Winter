using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Data.Tests
{
    static public class SampleData
    {
        public const string Princess= "Princess";
        public const string Buttercup = "Buttercup";
        public const string Montoya = "Montoya";
        public const string Inigo = "Inigo";


        static public Author CreateInigoMontoya() =>
            new Author(Inigo, Montoya, "apple@microsoft.com");

        static public Author CreatePrincess() =>
            new Author(Buttercup, Princess, "apple@microsoft.com");

        







    }
}
