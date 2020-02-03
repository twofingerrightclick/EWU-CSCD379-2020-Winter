using SecretSanta.Data;
using SecretSanta.Business.Services;


public static class SampleData
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



    static public User CreateUser1() => new User(_SAMPLE_FIRSTNAME1, _SAMPLE_LASTNAME1);
    static public User CreateUser2() => new User(_SAMPLE_FIRSTNAME2, _SAMPLE_LASTNAME2);
    static public Gift CreateGift() => new Gift(_SAMPLE_TITLE1, _SAMPLE_DESCRIPTION1, _SAMPLE_URL1, CreateUser1());
    static public Group CreateGroup1() {
        return new Group(_SAMPLE_TITLE1);
    }
    static public Group CreateGroup2() {
        return new Group(_SAMPLE_TITLE2);
    }
}
