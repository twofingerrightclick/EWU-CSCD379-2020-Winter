
using SecretSanta.Data;


public static class SampleData
{
    // Properties
#pragma warning disable CA1707 // Identifiers should not contain underscores
    public const string _SAMPLE_FIRSTNAME1 = "sample1_fname";
    public const string _SAMPLE_LASTNAME1 = "sample1_lname";
    public const string _SAMPLE_EMAIL1 = "sample1@email.com";

    public const string _SAMPLE_FIRSTNAME2 = "sample2_fname";
    public const string _SAMPLE_LASTNAME2 = "sample2_lname";
    public const string _SAMPLE_EMAIL2 = "sample2@email.com";


    public const string _SAMPLE_TITLE1 = "sample1_title";
    public const string _SAMPLE_URL1 = "https://docs.microsoft.com/sampleurl1";
    public const string _SAMPLE_DESCRIPTION1 = "sample1_description";

    public const string _SAMPLE_TITLE2 = "sample2_title";
    public const string _SAMPLE_URL2 = "https://docs.microsoft.com/sampleurl2";
    public const string _SAMPLE_DESCRIPTION2 = "sample2_description";

#pragma warning restore CA1707 // Identifiers should not contain underscores


    static public User CreateDataUser1() => new User(_SAMPLE_FIRSTNAME1, _SAMPLE_LASTNAME1);
    static public User CreateDataUser2() => new User(_SAMPLE_FIRSTNAME2, _SAMPLE_LASTNAME2);

   
    static public Gift CreateGift() => new Gift(_SAMPLE_TITLE1, _SAMPLE_DESCRIPTION1, _SAMPLE_URL1, CreateDataUser1());
    static public Gift CreateGift2() => new Gift(_SAMPLE_TITLE2, _SAMPLE_DESCRIPTION2, _SAMPLE_URL2, CreateDataUser1());
    static public Group CreateGroup1()
    {
        return new Group(_SAMPLE_TITLE1);
    }
    static public Group CreateGroup2()
    {
        return new Group(_SAMPLE_TITLE2);
    }
}