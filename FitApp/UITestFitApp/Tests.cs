using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using UITestFitApp.Pages;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITestFitApp
{
    public class Tests : BaseTest
    {
        public Tests(Platform platform) : base(platform)
        {

        }

        [SetUp]
        public void BeforeEachTest()
        {
            base.BeforeEachTest();
            //SignUpPage.WaitForPageToLoad();
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First Screen");
        }

        //[Test]
        //public void SignUpPageIsVisible()
        //{
        //    Assert.IsTrue(SignUpPage.isPageVisible);
        //}

        [Test]
        public void CompletedFormDisplayHomePage()
        {
            //SignUpPage.EnterName("Luke");
            //SignUpPage.EnterEmail("Luke@gmail.com");
            //SignUpPage.EnterPassword("Luke@123");
            //SignUpPage.EnterWeight("80");
            //SignUpPage.EnterHeight("180");
        }

    }
}
