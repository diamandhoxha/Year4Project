using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;

namespace UITestFitApp.Pages
{
    public abstract class BasePage
    {
         public readonly string pageTitle;

         public readonly IApp app;
         public readonly bool OnAndroid;
         public readonly bool OnIos;

        protected BasePage(IApp app, Platform platform, string pageTitle)
        {
            this.app = app;

            OnAndroid = platform == Platform.Android;
            OnIos = platform == Platform.iOS;

            this.pageTitle = pageTitle;
        }

        public bool isPageVisible => app.Query(pageTitle).Length > 0;

        public void WaitForPageToLoad()
        {
            app.WaitForElement(pageTitle);
        }

    }
}
