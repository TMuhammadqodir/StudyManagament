namespace StudyManagement.Service.Helpers;

public static class TelNumberChecker
{
    public static bool CheckNumber(string telNumber)
    {
        if(telNumber.Length != 13) 
            return false;

        if (telNumber[..5].Equals("+9989"))
            return true;

        return false;
    }
}
