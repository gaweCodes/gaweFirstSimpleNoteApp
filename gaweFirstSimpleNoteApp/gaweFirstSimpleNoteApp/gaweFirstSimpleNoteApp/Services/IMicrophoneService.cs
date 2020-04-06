using System.Threading.Tasks;

namespace gaweFirstSimpleNoteApp.Services
{
    public interface IMicrophoneService
    {
        Task<bool> GetPermissionAsync();
        void OnRequestPermissionResult(bool isGranted);
    }
}
