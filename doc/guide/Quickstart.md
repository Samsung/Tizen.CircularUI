# Quickstart

This guide explains how to create an application that represents `Toast` and `CircleSlider`. If you press the button, `Toast` pops up and it automatically disappears after timeout (in seconds).
If you rotate the bezel of Tizen wearable device, `CircleSlider` bar is moves forward or backward direction.

This document requires `Visual Studio` and the `Visual Studio tools for Tizen`. If you have not installed it, please [install it first.](https://developer.tizen.org/development/visual-studio-tools-tizen/installing-visual-studio-tools-tizen)

## 1. Set up development environment and create project
- Create a Tizen Xamarin Forms application. For more information, see [Tizen.org guide](https://developer.tizen.org/development/training/.net-application/creating-your-first-tizen-.net-application).

  In this guide, project name is `SampleCircleApp`

### Tizen Wearable App Template
#### 1) Create project
- Select 'Tizen Wearable App' template on 'New project'.
    ![tizen_project_wizard_capture_template](data/tizen_project_wizard_capture_template.png)

- You can use APIs of Xamarin.Forms and Tizen.Wearable.CircularUI now.
    ![tizen_project_wizard_capture_template2](data/tizen_project_wizard_capture_template2.png)

 *Remark : If you want to choose 'Tizen XAML App template', for more information, see [Guide of Tizen XAML App template](Quickstart_tizenXAMLAppTemplate.md).*

#### 2) Insert CircularUI control code
- In App.cs file, add the following code. This code defines the user interface for the page:

  - `CirclePage` is derive from `Xamarin.Forms.Page`. This Page content area has `Label` and `Button`.
     For more information, see [CirclePage guide](CirclePage.md).
  - `circlePage.CircleSurfaceItems.Add()` : `CircleSliderSurfaceItem` is attached for `CircleSurfaceItem` of  `CirclePage`.
  - `RotaryFocusObject` is set `circleSlider`. `CircleSliderSurfaceItem` has rotary focus. It can receive a Rotary Event from the wearable device's bezel interaction.
  - `OnButtonClicked` is an event handler of `Button` `Clicked` event. The following code simply displays Toast popup during three seconds:

**App.cs file**
```cs
using System;

using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;

namespace SampleCircleApp
{
    public class App : Application
    {
        public App()
        {
            Button btn = new Button { Text = "show toast" };
            btn.Clicked += OnButtonClicked;

            CircleSliderSurfaceItem circleSlider = new CircleSliderSurfaceItem() {
                Increment = 0.5,
                IsVisible = true,
                Maximum = 15,
                Minimum = 0,
                Value = 3,
            };

            // The root page of your application
            CirclePage circlePage = new CirclePage() {
                Content = new StackLayout {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Orientation = StackOrientation.Vertical,
                    Children = {
                        new Label {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = "Welcome to Xamarin Forms!"
                        },
                        btn
                    }
                },
            };
            circlePage.CircleSurfaceItems.Add(circleSlider);
            circlePage.RotaryFocusObject = circleSlider;
            MainPage = circlePage;
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            Toast.DisplayText("Toast popup", 3000);
        }
        ...
    }
}

```

## 2. Build and launch your application.
- Build the solution 
    - In the Visual Studio menu, select Build > Build Solution.
    - In the Solution Explorer view, right-click the solution name and select Build.

- Launch Tizen Emulator
    - Click `Launch Tizen Emulator` button, as displayed in the following image:

    ![launch_emulator1](data/launch_emulator1.png)
    ![launch_emulator2](data/launch_emulator2.png)

- For Windows OS, copy the application tpk file from the project binary path to sdb tool path.
    - `SampleCircleApp` tpk:
            The file is located in `ProjectPath\SampleCircleApp\SampleCircleApp\bin\Debug\tizen40\org.tizen.example.SampleCircleApp-1.0.0.tpk`.
    - Project path: To locate the project path, in the Solution Explorer view, right-click the solution name and click `open folder in file explorer`.
    - sdb tool: This is located in `c:\tizen\tools\sdb.exe`

- For Windows OS, launch Tizen Sdb Command Prompt (Tool > Tizen > Tizen Sdb Command Prompt).
  For Linux, you can use sdb command directly in your project path.

- Install your app with sdb command
    ```
    sdb install org.tizen.example.SampleCircleApp-1.0.0.tpk
    ```
- Launch wearable emulator to verify the application<br>
  ![launchApp](data/launch_app.png)<br>

  ![appCapture1](data/app_capture1.png)<br>
