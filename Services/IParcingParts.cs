namespace ParcingYamaha.Services
{
    internal interface IParcingParts
    {
        Task GetParts(string desiredModel);
    }
}