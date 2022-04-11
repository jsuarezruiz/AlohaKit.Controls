using AlohaKit.Gallery.Models;
using AlohaKit.Gallery.ViewModels.Base;

namespace AlohaKit.Gallery.ViewModels
{
    public class MainViewModel : BaseGalleryViewModel
    {
        protected override IEnumerable<SectionModel> CreateItems() => new[]
        { 
            new SectionModel(typeof(LoadingView), "BusyIndicator",
                "It can be used to indicate busy status during app loading, data processing, etc."),
                 
            new SectionModel(typeof(ButtonView), "Button",
                "The Button responds to a tap or click."),

            new SectionModel(typeof(ProgressBarView), "ProgressBar",
                "The ProgressRadial is a control that indicates the progress of a task."),

            new SectionModel(typeof(ProgressRadialView), "ProgressRadial",
                "The ProgressRadial is a control that indicates the progress of a task."),
            
            new SectionModel(typeof(ToggleSwitchView), "ToggleSwitch",
                "The ToggleSwitchView is a horizontal toggle button that can be manipulated by the user to toggle between on and off states."),
        };
    }
}