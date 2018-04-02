using ElmSharp;
using WearableUIGallery.Extensions;
using WearableUIGallery.Tizen.Wearable.DependencyService;
using Xamarin.Forms;

[assembly: Dependency(typeof(GlobalScrollConfigService))]
namespace WearableUIGallery.Tizen.Wearable.DependencyService
{
    public class GlobalScrollConfigService : IGlobalScrollConfig
    {
        public double BringInScrollFriction
        {
            get
            {
                return ElmScrollConfig.BringInScrollFriction;
            }
            set
            {
                ElmScrollConfig.BringInScrollFriction = value;
            }
        }
    }
}
