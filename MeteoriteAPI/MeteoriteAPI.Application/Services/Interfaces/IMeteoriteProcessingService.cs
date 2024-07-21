namespace MeteoriteAPI.Application.Services.Interfaces
{
    public interface IMeteoriteProcessingService
    {
        Task PrepareDataAsync();

        Task ProcessDataAsync();
    }
}
