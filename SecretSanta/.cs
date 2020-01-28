using System;

public class SampleData
{
    static public class SampleData
    {
        public const string Inigo = "Inigo";
        public const string Montoya = "Montoya";
        public const string InigoMontoyaEmail = "inigo@montoya.me";

        public const string Princess = "Princess";
        public const string Buttercup = "Buttercup";
        public const string PrincessButtercupEmail = "inigo@montoya.me";

        static public User CreateUser() => new Author(Inigo, Montoya, InigoMontoyaEmail);
        static public Gift CreateGift() => new Gift(Princess, Buttercup, PrincessButtercupEmail);
    }
}
