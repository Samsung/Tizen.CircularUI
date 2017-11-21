namespace Xamarin.Forms.CircularUI.Renderer
{
    public static class FormsCircularUIExtension
    {
        public static bool IsInitialized { get; private set; }

        public static void Init()
        {
            if (IsInitialized)
                return;

            IsInitialized = true;
        }
    }
}
