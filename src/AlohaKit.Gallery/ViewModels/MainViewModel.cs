using AlohaKit.Gallery.Models;
using AlohaKit.Gallery.ViewModels.Base;

namespace AlohaKit.Gallery.ViewModels
{
    public class MainViewModel : BaseGalleryViewModel
    {
        protected override IEnumerable<SectionModel> CreateItems() => new[]
        {  
            new SectionModel(typeof(AvatarView), "Avatar", 
                "The Avatar control displays the initials of a person, entity, or group on top of a colored circular background."),

            new SectionModel(typeof(LoadingView), "BusyIndicator",
                "It can be used to indicate busy status during app loading, data processing, etc."),
                 
            new SectionModel(typeof(ButtonView), "Button",
                "The Button responds to a tap or click."),
           
            new SectionModel(typeof(CheckBoxView), "CheckBox",
                "CheckBox is a type of button that can either be checked or empty."),
				
			new SectionModel(typeof(LinearGaugeView), "LinearGauge",
				"LinearGauge displays simple value within a specific range."),

			new SectionModel(typeof(NumericUpDownView), "NumericUpDown",
                " NumericUpDown control provides up and down repeat buttons to increase and decrease values."),
				
			new SectionModel(typeof(PieChartView), "PieChart",
				"The PieChart visualizes its data points using radial coordinate system."),

			new SectionModel(typeof(PulseIconView), "PulseIcon",
                "PulseIcon generates pulsation relative to your icon."),

            new SectionModel(typeof(ProgressBarView), "ProgressBar",
                "The ProgressBar is a control that indicates the progress of a task."),

            new SectionModel(typeof(ProgressRadialView), "ProgressRadial",
                "The ProgressRadial is a control that indicates the progress of a task."),
			 
			new SectionModel(typeof(SegmentedControlView), "SegmentedControl",
				"The SegmentedControl provides a simple way to choose from a linear set of two or more segments."),

			new SectionModel(typeof(RatingView), "Rating",
                "Rating is a control that allows users to rate by selecting number of items from a predefined number of items."),
             
            new SectionModel(typeof(SliderView), "Slider",
                "The Slider is a horizontal bar that can be manipulated by the user to select a double value from a continuous range."),

            new SectionModel(typeof(ToggleSwitchView), "ToggleSwitch",
                "The ToggleSwitch is a horizontal toggle button that can be manipulated by the user to toggle between on and off states."),
        };
    }
}