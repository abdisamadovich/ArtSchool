namespace ArtSchools.Auth;

public interface IRng
{
    string Generate(int length = 50, bool removeSpecialChars = false);
}