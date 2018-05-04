# Tizen Wearable CircularUI

- [Introduction](#introduction)
- [Controls](#controls)
- [Prerequisite](#prerequisite)
- [How to use CircularUI](#how-to-use-circularui)

## Introduction
The Tizen Wearable CircularUI project is a set of helpful extensions of the Xamarin Forms framework.<br>
The aim of the Tizen Wearable CircularUI project is to develop an open source software motivate software developer to creating Tizen Wearable Xamarin Forms app more easily and efficiently.<br> 
The binaries are available via NuGet (package name **Tizen.Wearable.CircularUI**)<br>

_Xamarin Forms provides cross-platform APIs, but this project are only worked on the Samsung Gear device that support Tizen .NET._ 


## Controls
The Tizen Wearable CircularUI project provides you the following UI controls:

- Check : A subclass of Xamarin.Forms.Switch control supports Tizen specific style.
- CircleDateTimeSelector : A control to select date or time fits in the circular screen.
- CircleListView : A subclass of Xamarin.Forms.ListView control fits in the circular screen and has the circular scrollbar.
- CirclePage : A subclass of Xamarin.Forms.Page that can show Circular ProgressBar and Circular Slider and the button on the bottom of the screen. it can also show MenuItems on the circular menu.(MoreOption)
- CircleProgressBarSurfaceItem : A control fits in the circular screen. it can be shown in CirclePage only.
- CircleSliderSurfaceItem : A Slider control that responds to the bezel action and fits in the circular screen.
- CircleScrollView : A subclass of Xamarin.Forms.ScrollView that can be scrolled by the bezel action.
- CircleStackLayout : A container to layout children linear in the circular area.
- CircleStepper : A control to select a number of steps fits in the circular screen.
- CircleSurfaceEffectBehavior : The CircleSurfaceEffectBehavior is an effect which allows you to insert views that require CircleSurface.
- ConfirmationPopup : A popup has two buttons on the left and right side of the circular screen.
- ConfirmPopupEffect : the effect to show a small popup has one or two button sticky with any control.
- ContextPopup : A context popup fits in the circular screen.
- IndexPage : A subclass of multiple pages that can slide child page horizontal and has dots on top of the screen for the number of child pages.
- InformationPopup : A popup has a control to show progress and one button on the bottom side of the circular screen.
- IRotationReceiver : A event receiver for using the bezel action.
- Radio : A radio control.
- PopupEntry : The PopupEntry is a class that extends Xamarin.Forms.Entry. It makes a new layer when editing text on the entry.
- Toast : A popup for simple feedback.
- TwoButtonPage : A subclass of Xamarin.Forms.Page has two buttons on the left and right side of the circular screen.

![widgets](doc/design/data/widgets.png)

## Prerequisite
 - Visual Studio 2017
 - Visual Studio Tools for Tizen
     - [How to install Visual Studio Tools for Tizen](https://developer.tizen.org/development/visual-studio-tools-tizen/installing-visual-studio-tools-tizen)
 - Installing Tizen Wearable emulator image (WEARABLE-4.0-Emulator or WEARABLE-5.0-Emulator)

## How to use CircularUI
- [QuickStart](doc/guide/Quickstart.md)

- The [API Reference](https://samsung.github.io/Tizen.CircularUI/index.html) is available on the web to browse.