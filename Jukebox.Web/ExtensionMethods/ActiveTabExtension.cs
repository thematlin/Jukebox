using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jukebox.Web.ExtensionMethods
{
    public static class ActiveTabExtension
    {
        public static string SelectedTab(this HtmlHelper helper, string activeController, string[] activeActions, string cssClass)
        {
            var currentAction = helper.ViewContext.Controller.ValueProvider.GetValue("action").RawValue.ToString();
            var currentController = helper.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString();

            var cssClassToUse = currentController.Equals(activeController) && activeActions.Contains(currentAction) ? cssClass : String.Empty;

            return cssClassToUse;
        }
    }
}