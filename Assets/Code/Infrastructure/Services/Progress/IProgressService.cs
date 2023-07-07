namespace Code.Infrastructure.Services.Progress
{
    public interface IProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}