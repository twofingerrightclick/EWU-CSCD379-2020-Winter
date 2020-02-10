
using SecretSanta.Business.Dto;

public static class ApiSampleData
{
    // Properties
    public const string _SAMPLE_FIRSTNAME1 = "sample1_fname";
    public const string _SAMPLE_LASTNAME1 = "sample1_lname";
    public const string _SAMPLE_EMAIL1 = "sample1@email.com";

    public const string _SAMPLE_FIRSTNAME2 = "sample2_fname";
    public const string _SAMPLE_LASTNAME2 = "sample2_lname";
    public const string _SAMPLE_EMAIL2 = "sample2@email.com";


    public const string _SAMPLE_TITLE1 = "sample1_title";
    public const string _SAMPLE_URL1 = "sample1_url";
    public const string _SAMPLE_DESCRIPTION1 = "sample1_description";

    public const string _SAMPLE_TITLE2 = "sample2_title";
    public const string _SAMPLE_URL2 = "sample2_url";
    public const string _SAMPLE_DESCRIPTION2 = "sample2_description";



    static public User CreateUser1() => new User() { FirstName = _SAMPLE_FIRSTNAME1, LastName = _SAMPLE_LASTNAME1 };
    static public User CreateUser2() => new User() { FirstName = _SAMPLE_FIRSTNAME2, LastName = _SAMPLE_LASTNAME2 };

    static public UserInput CreateDtoInputUser1() => new UserInput(){ FirstName = _SAMPLE_FIRSTNAME1, LastName = _SAMPLE_LASTNAME1 };
    static public UserInput CreateDtoInputUser2() => new UserInput() { FirstName = _SAMPLE_FIRSTNAME2, LastName = _SAMPLE_LASTNAME2 };
    static public GiftInput CreateGift() => new GiftInput() { Title=_SAMPLE_TITLE1, Description=_SAMPLE_DESCRIPTION1, Url=_SAMPLE_URL1, UserId= 1 };
    static public GroupInput CreateGroup1()
    {
        return new GroupInput() { Title = _SAMPLE_TITLE1 };
    }
    static public GroupInput CreateGroup2()
    {
        return new GroupInput() { Title = _SAMPLE_TITLE2 };
    }
}