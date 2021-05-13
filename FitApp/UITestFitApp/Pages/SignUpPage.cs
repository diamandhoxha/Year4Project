using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace UITestFitApp.Pages
{
    class SignUpPage : BasePage
    {
        readonly Query entName, entEmail, entPassword, entWeight, entHeight, imgSignup;

        public SignUpPage(IApp app, Platform platform) : base(app, platform, "SignUp Page")
        {
            if (OnAndroid)
            {
                entName = x => x.Class("FormsEditText").Index(0);
                entEmail = x => x.Class("FormsEditText").Index(0);
                entPassword = x => x.Class("FormsEditText").Index(0);
                entWeight = x => x.Class("FormsEditText").Index(0);
                entHeight = x => x.Class("FormsEditText").Index(0);
            }
            else
            {
                entName = x => x.Class("FormsEditText").Index(0);
                entEmail = x => x.Class("FormsEditText").Index(0);
                entPassword = x => x.Class("FormsEditText").Index(0);
                entWeight = x => x.Class("FormsEditText").Index(0);
                entHeight = x => x.Class("FormsEditText").Index(0);
            }

            imgSignup = x => x.Marked("ImgSignup");
        }

        public void EnterText(Query textBoxQuery, string text)
        {
            app.ClearText(textBoxQuery);
            app.EnterText(textBoxQuery, text);
            app.DismissKeyboard();
        }

        public void EnterName(string text)
        {
            EnterText(entName, text);

            app.Screenshot("Entered Name");
        }

        public void EnterEmail(string text)
        {
            EnterText(entEmail, text);

            app.Screenshot("Entered Email");
        }

        public void EnterPassword(string text)
        {
            EnterText(entPassword, text);

            app.Screenshot("Entered Password");
        }

        public void EnterWeight(string text)
        {
            EnterText(entWeight, text);

            app.Screenshot("Entered Weight");
        }

        public void EnterHeight(string text)
        {
            EnterText(entHeight, text);

            app.Screenshot("Entered Height");
        }

        public void TapSignUp(string text)
        {
            app.Tap(imgSignup);

            app.Screenshot("SignUp Image Tapped");
        }

    }
}
