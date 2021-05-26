using System;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;
using Xamarin.Forms.Internals;

namespace XamProjectTemplate.Helpers.MediaHelper
{
    [Preserve(AllMembers = true)]
    public interface IMediaHelper
    {
        Task<MediaFile> PickPhotoAsync(PickMediaOptions options = null);
        Task<MediaFile> TakePhotoAsync(StoreCameraMediaOptions options);
        Task<MediaFile> PickVideoAsync();
        Task<MediaFile> TakeVideoAsync(StoreVideoOptions options);
    }
}
