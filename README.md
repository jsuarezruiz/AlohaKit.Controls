# AlohaKit

<div align="center">
   <a href="https://www.nuget.org/packages/AlohaKit"><img src="https://img.shields.io/nuget/v/AlohaKit?color=blue&style=flat-square&logo=nuget"></a>
   <a href="https://www.nuget.org/packages/AlohaKit"><img src="https://img.shields.io/nuget/dt/AlohaKit.svg?style=flat-square"></a>
   <a href="./LICENSE"><img src="https://img.shields.io/github/license/jsuarezruiz/AlohaKit"></a>
</div>

![AlohaKit](https://raw.githubusercontent.com/jsuarezruiz/AlohaKit/main/images/alohakit-promo.png)

A set of .NET MAUI drawn controls.

![AlohaKit Gallery](https://raw.githubusercontent.com/jsuarezruiz/AlohaKit/main/images/alohakit-gallery.png)

_NOTE: There is an experimental project in .NET called [Microsoft.Maui.Graphics.Controls](https://github.com/dotnet/Microsoft.Maui.Graphics.Controls) (GraphicsControls) with drawn controls. The main goal of GraphicsControls is to validate if there are interest in .NET MAUI drawn controls, creating the controls that were available in [Xamarin.Forms Visual](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/visual/material-visual) with Cupertino, Fluent and Material Design. GraphicsControls controls implement existing controls in .NET MAUI in a drawn way with the same properties, events, etc. 
On the other hand, AlohaKit is a personal project also with drawn controls but without strictly following the .NET MAUI control interfaces. That is, we can have in AlohaKit a Rating control for example that was not part of Xamarin.Forms Visual or the Button control can have a Stroke property of type Brush while in .NET MAUI it is a Color. AlohaKit main goal is to offer more options to developers with more controls showing another way to create controls but is not an alternative or replacement for GraphicsControls, are not the same library and do not have the same goals._

## Usage

**Step 1**: Include the AlohaKit package reference in your project. 

**Step 2**: Enjoy coding!.

#### Controls

The **controls** available are:

### Avatar

The Avatar control displays the initials of a person, entity, or group on top of a colored circular background.

![Avatar](https://raw.githubusercontent.com/jsuarezruiz/AlohaKit/main/images/alohakit-avatar.png)

Features:
- Can choose from several predefined sizes.
- Allow to customize the background with a gradient.
- Allow to customize the fill with a gradient.

### BarChart 

The BarChart is a control that provides a way of showing data represented as vertical bars.

![BarChart](https://raw.githubusercontent.com/jsuarezruiz/AlohaKit/main/images/alohakit-barchart.png)

### Button

The Button responds to a tap or click.

![Button](https://raw.githubusercontent.com/jsuarezruiz/AlohaKit/main/images/alohakit-button.png)

Features:
- Allow to customize the background with a gradient.
- Allow to customize the border with a gradient.
- Define the border thickness.
- Support content alignment.
- Provides option to draw a shadow around the control.

### BusyIndicator

It can be used to indicate busy status during app loading, data processing, etc.

![BusyIndicator](https://raw.githubusercontent.com/jsuarezruiz/AlohaKit/main/images/alohakit-busyindicator.png)

Features:
- Supports to show/hide busy indicator.
- All the colors can be customized.
- Provides option to draw a shadow around the control.

### CheckBox

CheckBox is a type of button that can either be checked or empty.

![CheckBox](https://raw.githubusercontent.com/jsuarezruiz/AlohaKit/main/images/alohakit-checkbox.png)

Features:
- Checked/Unchecked state when IsChecked is true/false respectively.
- Customize the width of the lines with which the Checkbox element is drawn.
- Defines the Brush applied to the control when it is checked.
- Defines the Brush applied to the control when it is unchecked.
- Exposes Commands that allows you to manage actions when Checkbox state changes.

### LinearGauge

LinearGauge displays simple value within a specific range.

![LinearGauge](https://raw.githubusercontent.com/jsuarezruiz/AlohaKit/main/images/alohakit-lineargauge.png)

### LineChart

The LineChart is a control that provides a way of showing data represented as a line.

![LineChart](https://raw.githubusercontent.com/jsuarezruiz/AlohaKit/main/images/alohakit-linechart.png)

### MultiBarChart

The MultiBarChart is a control that provides a way of showing groups of data represented as vertical bars.

![MultiBarChart](https://raw.githubusercontent.com/jsuarezruiz/AlohaKit/main/images/alohakit-multibar.png)

### MultiLineChart

The MultiLineChart is a control that provides a way of showing groups of data represented as multiple lines.

![MultiLineChart](https://raw.githubusercontent.com/jsuarezruiz/AlohaKit/main/images/alohakit-multiline.png)

### PulseIcon

PulseIcon generates pulsation relative to your icon.

![PulseIcon](https://raw.githubusercontent.com/jsuarezruiz/AlohaKit/main/images/alohakit-pulseicon.png)

Features:
- All the colors can be customized supporting gradients.
- Can easily manage the animation.

### ProgressBar

The ProgressBar visually represents progress as a horizontal or vertical bar that is filled to a percentage represented by a float value.

![ProgressBar](https://raw.githubusercontent.com/jsuarezruiz/AlohaKit/main/images/alohakit-progressbar.png)

Features:
- Can use horizontal or vertical bar.
- All the colors can be customized supporting gradients.
- Can easily animate the progress.

### ProgressRadial

The ProgressRadial is a control that indicates the progress of a task.

![ProgressRadial](https://raw.githubusercontent.com/jsuarezruiz/AlohaKit/main/images/alohakit-progressradial.png)

Features:
- Provide elegance in representing progression.
- All the colors can be customized.

### Rating

Rating is a control that allows users to rate by selecting number of items (stars) from a predefined number of items.

![Rating](https://raw.githubusercontent.com/jsuarezruiz/AlohaKit/main/images/alohakit-rating.png)

Features:
- Exposes a Value property that is used to set and read the number of the selected items.
- Supports read-only mode.
- You can control the visual appearance of the shapes.

### Slider

Slider is a horizontal bar that can be manipulated by the user to select a double value from a continuous range.

![Slider](https://raw.githubusercontent.com/jsuarezruiz/AlohaKit/main/images/alohakit-slider.png)

Features:
- All the colors can be customized supporting gradients.

### ToggleSwitch

The ToggleSwitch is a horizontal toggle button that can be manipulated by the user to toggle between on and off states.

![ToggleSwitch](https://raw.githubusercontent.com/jsuarezruiz/AlohaKit/main/images/alohakit-toggleswitch.png)

Features:
- Allow to customize the track with a gradient.
- Allow to customize the thumb with a gradient.

## Contribute

Do you want to contribute?.

**Found a Bug?**

If you find a bug, you can help me by submitting an [issue](https://github.com/jsuarezruiz/AlohaKit/issues). Even better, you can submit a [Pull Request](https://github.com/jsuarezruiz/AlohaKit/pulls) with a fix.

**Submitting a pull request**

For every contribution, you must:
- Test your code.
- target main branch (or an appropriate release branch if appropriate for a bug fix).

## Feedback or Requests

Use GitHub [Issues](https://github.com/jsuarezruiz/AlohaKit/issues) for bug reports and feature requests.

## Principles

* Principle #1: Kept AlohaKit simple.
* Principle #2: Any control added must be a drawn control and allow to extend and customize everything.

## Copyright and license

Code released under the [MIT license](https://opensource.org/licenses/MIT).