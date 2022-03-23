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
             
            new SectionModel(typeof(ProgressBarView), "ProgressBar",
                "The ProgressRadial is a control that indicates the progress of a task."),

            new SectionModel(typeof(ProgressRadialView), "ProgressRadial",
                "The ProgressRadial is a control that indicates the progress of a task."),   
        };
    }
}