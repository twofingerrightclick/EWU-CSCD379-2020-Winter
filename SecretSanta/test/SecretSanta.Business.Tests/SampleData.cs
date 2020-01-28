using SecretSanta.Data;
using System;

public static class SampleData
{
    
        
            public const string Inigo = "Inigo";
            public const string Montoya = "Montoya";
            public const string InigoMontoyaEmail = "inigo@montoya.me";

            public const string Princess = "Princess";
            public const string Buttercup = "Buttercup";
            public const string PrincessButtercupEmail = "inigo@montoya.me";


            public const string Title = "Ring Doorbell";
            public const string Url = "www.ring.com";
            public const string Description = "The doorbell that saw too much";
           

            static public User CreateUser1() => new User(Inigo, Montoya);
            static public User CreateUser2() => new User(Princess, Buttercup);
            static public Gift CreateGift() => new Gift(Title,Description,Url,CreateUser1());
        
    
}
