using Microsoft.AspNetCore.Mvc;
using School_Core.ViewModels;

namespace School_Core.ViewComponents
{
    public class CounterViewComponent : ViewComponent
    {
        private readonly CounterTableViewModel.IProvider _counterTableProvider;

        public CounterViewComponent(CounterTableViewModel.IProvider counterTableProvider)
        {
            _counterTableProvider = counterTableProvider;
        }

        public IViewComponentResult Invoke(string color, bool shouldAddTeachers = true)
        {
            return View(_counterTableProvider.GetViewModel(color, shouldAddTeachers));
        }
    }
}