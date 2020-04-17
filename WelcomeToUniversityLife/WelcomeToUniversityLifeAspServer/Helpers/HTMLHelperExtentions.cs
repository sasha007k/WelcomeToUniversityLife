using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace WelcomeToUniversityLifeAspServer.Helpers
{
    public static class HTMLHelperExtentions
    {
        public static string IsSelected(this IHtmlHelper htmlHelper, string controllers, string actions,
            string cssClass = "selected")
        {
            var currentAction = htmlHelper.ViewContext.RouteData.Values["action"] as string;
            var currentController = htmlHelper.ViewContext.RouteData.Values["controller"] as string;

            if (!string.IsNullOrWhiteSpace(controllers) && 
                !string.IsNullOrWhiteSpace(actions) && 
                !string.IsNullOrWhiteSpace(currentAction) &&
                !string.IsNullOrWhiteSpace(currentController))
            {
                IEnumerable<string> acceptedActions = (actions ?? currentAction).Split(',');
                IEnumerable<string> acceptedControllers = (controllers ?? currentController).Split(',');

                return acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController)
                    ? cssClass
                    : string.Empty;
            }

            return string.Empty;
        }
    }
}