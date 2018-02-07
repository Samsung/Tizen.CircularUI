using Tizen.Applications;
using ElmSharp;
using Xamarin.Forms.CircularUI.Tizen.Watchface;
using System;

namespace SimpleTextWatchface
{
    class App : FormsWatchface
    {
        ClockViewModel _viewModel;
        protected override void OnCreate()
        {
            base.OnCreate();
            var app = new WatchfaceApplication();
            _viewModel = new ClockViewModel();
            app.BindingContext = _viewModel;
            LoadWatchface(app);
        }

        protected override void OnTick(TimeEventArgs time)
        {
            base.OnTick(time);
            if (_viewModel != null)
            {
                _viewModel.Time = time.Time.UtcTimestamp;
            }
        }

        static void Main(string[] args)
        {
            App app = new App();
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            Xamarin.Forms.CircularUI.Tizen.CircularUI.Init();
            app.Run(args);
        }
    }
}
