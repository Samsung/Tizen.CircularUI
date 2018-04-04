# Tizen CircularUI

[![License](https://img.shields.io/badge/licence-Apache%202.0-brightgreen.svg?style=flat)](LICENSE)


- [Introduction](#introduction)
- [Controls](#controls)
- [Prerequisite](#prerequisite)
- [How to use CircularUI](#how-to-use-circularui)

## Introduction
The Tizen CircularUI project is a set of helpful extensions of the Xamarin Forms framework.<br>
The aim of the Tizen CircularUI project is to develop an open source software motivate software developer to creating Tizen Wearable Xamarin Forms app more easily and efficiently.<br> 
The binaries are available via NuGet (package name CircularUI)<br>

_Xamarin Forms provides cross-platform APIs, but this project are only worked on the Samsung Gear device that support Tizen .NET._ 


## Controls
The Tizen CircularUI project provides you the following UI controls:

- CirclePage : A subclass of Xamarin.Forms.Page that can show circular ProgressBar and circular Slider and Button on bottom of screen. it can also show MenuItems on the circular menu.(MoreOption)
- CircleListView : A subclass of Xamarin.Forms.ListView control fits in the circular screen and have circular scroll bar.
- CircleProgressBarSurfaceItem : A control  fits in the circular screen. it can be shown in CirclePage only.
- CircleSliderSurfaceItem : A Slider control that responds to the bezel action and fits in the circular screen.
- CircleScrollView : A subclass of Xamarin.Forms.ScrollView that can be scrolled by the bezel action.
- CircleDateTimeSelector : A control to select date or time fits in the circular screen.
- CircleStepper : A control to select a number of steps fits in the circular screen.
- CircleStackLayout : A container to layout children linear in the circular area.
- IndexPage : A subclass of multiple page that can slide child page horizontal and has dots on top of the screen for the number of child pages.
- Check : A subclass of Xamarin.Forms.Switch control supports tizen specific style.
- RotationReceiver : A event receiver for using the bezel action.
- Radio : A radio control.
- Toast : A popup for simple feedback.
- TwoButtonPage : A subclass of Xamarin.Forms.Page has two button in left and right side of the circular screen.
- ContextPopup : A context popup fits in the circular screen.
- ConfirmationPopup : A popup has two button in left and right side of the circular screen.
- ConfirmPopupEffect : the effect to show a small popup has one or two button sticky with any control.
- InformationPopup : A popup has a control to show progress and one button in bottom side of circular screen.

![widgets](doc/design/data/widgets.png)

## Prerequisite
 - Visual Studio 2017
 - Visual Studio Tools for Tizen
     - [How to install Visual Studio Tools for Tizen](https://developer.tizen.org/development/visual-studio-tools-tizen/installing-visual-studio-tools-tizen)
 - Installing Tizen Wearable emulator image (WEARABLE-4.0-Emulator or WEARABLE-5.0-Emulator)

## How to use CircularUI
- [How to make Tizen Wearable Xamarin Forms App with CircularUI](doc/guide/HOW_TO_MAKE_TIZEN_XAMARIN_FORMS_APP_WITH_CIRCULARUI.md)

- The [API Reference](https://github.sec.samsung.net/pages/dotnet/apidoc/XamarinFormsCircularUI/api/index.html) is available on the web to browse.