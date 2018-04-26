# How to make Tizen Xamarin Forms App with CircularUI
This guide show how to create an application that represents `Toast` and `CircleSlider`. If user presses the button, `Toast` pops up and then it automatically disappear after timeout seconds.
 And If user rotates bezel of Tizen wearable device. `CircleSlider` bar is moving forward or backward direction.

## 1. Set up the development enviroment and Create a project
- Create Tizen Xamarin Forms App following to [Tizen.org guide](https://developer.tizen.org/development/training/.net-application/creating-your-first-tizen-.net-application).<br>
  In this guide project name is `SampleCircleApp`

- Select only wearable device at tizen project wizard.

    ![tizen_project_wizard_capture](data/tizen_project_wizard_capture.png)

- Search Tizen.Wearable.CircularUI nuget pakage at Nuget package manager. Package source is nuget.org.

    ![nuget_package_manager_capture](data/nuget_package_manager_capture.png)

- Install Tizen.Wearable.CircularUI nuget at portable class library(PCL) project.

    ![after_Install_nuget_package](data/after_Install_nuget_package.png)


## 2. Insert CircularUI Control code
- remove SampleCircleApp.cs that is generated automatically at PCL. and then add App.xaml and App.xaml.cs using add item.

- In App.xaml file, remove all of the template code and replace it with the following code. This code defines the user interface for the page.
  
  - `xmlns:w=clr-namespace:Tizen.Wearable.CircularUI.Forms` : `w` prefix means `Tizen.Wearable.CircularUI.Forms` namespace.
  - `<w:CirclePage>` : `CirclePage` derive from `Xamarin.Forms.Page`. This Page's content area has `Label` and `Button`.
  - `<w:CirclePage.CircleSurfaceItems>` : `CircleSliderSurfaceItem` attached for `CircleSurfaceItem` of  `CirclePage`.
  - `RotaryFocusTargetName` is set `slider` name. `CircleSliderSurfaceItem` has rotary focus. `CircleSliderSurfaceItem` can receive rotary event from the wearable device's bezel interaction.

   For more information . Please refer to [CirclePage guide](CirclePage.md)

**App.xaml file**
```xml
<?xml version="1.0" encoding="utf-8" ?>
<Application
    x:Class="SampleCircleApp.App"
    xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:local="clr-namespace:SimpleCircleApp"
    xmlns:w="clr-namespace:Tizen.Wearable.CircularUI.Forms;assembly=Tizen.Wearable.CircularUI.Forms">
    <Application.MainPage>
        <NavigationPage x:Name="MainNavigation">
            <x:Arguments>
                <w:CirclePage
                    x:Name="page"
                    NavigationPage.HasNavigationBar="False"
                    RotaryFocusTargetName="slider">
                    <w:CirclePage.Content>
                        <StackLayout
                            HorizontalOptions="Center"
                            Orientation="Vertical"
                            VerticalOptions="Center">
                            <Label HorizontalTextAlignment="Center" Text="Welcome to Xamarin Forms!" />
                            <Button Clicked="OnButtonClicked" Text="show toast" />
                        </StackLayout>
                    </w:CirclePage.Content>
                    <w:CirclePage.CircleSurfaceItems>
                        <w:CircleSliderSurfaceItem
                            x:Name="slider"
                            Increment="0.5"
                            IsVisible="True"
                            Maximum="15"
                            Minimum="0"
                            Value="3" />
                    </w:CirclePage.CircleSurfaceItems>
                </w:CirclePage>
            </x:Arguments>
        </NavigationPage>
    </Application.MainPage>
</Application>
```

<br><br>
- In App.xaml.cs, remove all of the template code and replace it with the following code.
    - `OnButtonClicked` is event handler of `Button` `Clicked` event. below code simply display Toast popup during 3 second.
    
**App.xaml.cs file**
```cs
using Tizen.Wearable.CircularUI.Forms;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleCircleApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        public void OnButtonClicked(object sender, EventArgs e)
        {
            Toast.DisplayText("Toast popup", 3000);
        }
    }
}
```

<br><br>
- For initialize CircularUI instance. Please insert `FormsCircularUI.Init()` code in Main method at `SampleCircleApp.Tizen.Wearable.cs`.
You should import `Tizen.Wearable.CircularUI.Forms.Renderer` with `using` directives.

**SampleCircleApp.Tizen.Wearable.cs file**
```cs
using Tizen.Wearable.CircularUI.Forms.Renderer;


        static void Main(string[] args)
        {
            var app = new Program();
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            FormsCircularUI.Init();  //must insert this initialize code
            app.Run(args);
        }
```

<br><br>
## 3. Build and then launch your application.
- Build the solution.  
    - In the Visual Studio menu, select Build > Build Solution.
    - In the Solution Explorer view, right-click the solution name and select Build.

- Launch Tizen Emulator
    - Press `Launch Tizen Emulator` button, refer to below image

    ![launch_emulator1](data/launch_emulator1.png)
    ![launch_emulator2](data/launch_emulator2.png)



- In Windows OS. copy application tpk from project binary path to sdb tool path.\
    - `SampleCircleApp` tpk locate in `ProjectPath\SampleCircleApp.Tizen.Wearable\bin\Debug\tizen40\org.tizen.example.SampleCircleApp.Tizen.Wearable-1.0.0.tpk`.\
    - You can verify your project path. In the Solution Explorer view, right-click the solution name and press `open folder in file explorer` menu.
    - sdb tool loacted in `c:\tizen\tools\sdb.exe`)

- If your OS is window, launch Tizen Sdb Command Prompt(Tool > Tizen > Tizen Sdb Command Prompt).
  If you use Linux. you can use sdb command directly in your project path. 

- Install your app with sdb command

```
sdb install org.tizen.example.SampleCircleApp.Tizen.Wearable-1.0.0.tpk
```

- Check your App at Wearable emulator<br>
  ![launchApp](data/launch_app.png)<br>

  ![appCapture1](data/app_capture1.png)<br>