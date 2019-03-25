using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using RelevantCodes.ExtentReports;
using SpecflowPages;
using System;
using System.Collections.Generic;
using System.Threading;
using TechTalk.SpecFlow;
using static SpecflowPages.CommonMethods;

namespace SpecflowTests.AcceptanceTest
{
    [Binding]
    public class SpecFlowFeature1Steps : Utils.Start
    {
       
        
        [Given(@"I clicked on the Language tab under Profile page")]
        public void GivenIClickedOnTheLanguageTabUnderProfilePage()
        {
            //Wait
            Thread.Sleep(1500);
       
            // Click on Profile tab
            Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[1]/a[1]")).Click();
            
        }

        [When(@"I add different languages '(.*)'  with levels '(.*)'")]
        public void WhenIAddDifferentLanguagesWithLevels(string p0, string p1)
        {
           int level = 0;
            switch (p1)
            {
                case "Basic":
                    level = 1;
                    break;
                case "Conversational":
                    level = 2;
                    break;
                case "Fluent":
                    level = 3;
                    break;
                case "Native":
                    level = 4;
                    break;
                default:
                    Console.WriteLine("please select a level");
                    break;
            }

            //Click on Add New button
            Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table/thead/tr/th[3]/div")).Click();

            //Add Language
            Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/div/div[1]/input")).SendKeys(p0);

            //Click on Language Level
            Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/div/div[2]/select")).Click();

            //Choose the language level
            IWebElement Lang = Driver.driver.FindElements(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/div/div[2]/select/option"))[level];
            Lang.Click();

            //Click on Add button
            Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/div/div[3]/input[1]")).Click();

        }

        [Then(@"Added languages '(.*)' should be displayed on my listing")]
        public void ThenAddedLanguagesShouldBeDisplayedOnMyListing(string p0)
        {
            try
            {
               
                bool result = false;
                //Start the Reports
                CommonMethods.ExtentReports();
                Thread.Sleep(2000);
                CommonMethods.test = CommonMethods.extent.StartTest("Add a Language");

                Thread.Sleep(2000);
                int row = Driver.driver.FindElements(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table/tbody")).Count;
                for (int i = 1; i <= row; i++)
                {
                    string ActualValue = Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table/tbody"+ "["+ i + "]"+ "/tr/td[1]")).Text;
                    if (p0.Equals(ActualValue))
                    {
                        result = true;
                        break;
                    }

                    else
                    {
                        result = false;
                    }
                }
                    Thread.Sleep(500);
                if (result == true)
                {
                    CommonMethods.test.Log(LogStatus.Pass, $"Test Passed, Added {p0} Language Successfully");
                    SaveScreenShotClass.SaveScreenshot(Driver.driver, "LanguageAdded");
                }

                else
                    CommonMethods.test.Log(LogStatus.Fail, "Test Failed");

            }
            catch (Exception e)
            {
                CommonMethods.test.Log(LogStatus.Fail, "Test Failed", e.Message);
            }

        }



        [When(@"I delete ""(.*)"" language")]
        public void WhenIDeleteLanguage(string p0)
        {
            string actualValue;
            int row = Driver.driver.FindElements(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table/tbody")).Count;
            IWebElement data;

            for (int i = 1; i <= row; i++)
            {
                data = Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table/tbody" + "[" + i + "]" + "/tr/td[1]"));
                actualValue = data.Text;
                Console.WriteLine(data.Text);

                if (p0.Equals(actualValue))
                {
                    Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table/tbody" + "[" + i + "]" + "/tr/td[3]/span[2]/i")).Click();
                    break;
                }
            }

        }

        [Then(@"""(.*)"" shouldnot be displayed on my listings")]
        public void ThenShouldnotBeDisplayedOnMyListings(string p0)
        {
            string actualValue;
            int row;
            IWebElement data;
            bool result = true;
            try
            {
                //Start the Reports
                CommonMethods.ExtentReports();
                Thread.Sleep(1000);
                CommonMethods.test = CommonMethods.extent.StartTest("Delete a Language");

                Thread.Sleep(2000);
                //   string ActualValue = Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table/tbody/tr/td[1]")).Text;
                row = Driver.driver.FindElements(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table/tbody")).Count;
                Console.WriteLine(row);
                Thread.Sleep(500);
                for (int i = 1; i <= row; i++)
                {
                    data = Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table/tbody" + "[" + i + "]" + "/tr/td[1]"));
                    actualValue = data.Text;
                    if (p0 != actualValue)
                    {
                        result = true;
                    }

                    else
                    {
                        result = false;
                        break;
                    }

                }
                if (result == true)
                {
                    CommonMethods.test.Log(LogStatus.Pass, "Test Passed, Deleted a Language Successfully");
                    SaveScreenShotClass.SaveScreenshot(Driver.driver, "LanguageDeleted");
                }
                else
                {
                    CommonMethods.test.Log(LogStatus.Fail, "Test Failed");
                }

            }
            catch (Exception e)
            {
                CommonMethods.test.Log(LogStatus.Fail, "Test Failed", e.Message);
            }
        }

        [Given(@"I clicked on the Education tab under Profile page")]
        public void GivenIClickedOnTheEducationTabUnderProfilePage()
        {
            //Wait
            Thread.Sleep(1500);

            // Click on Profile tab
            Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[1]/a[3]")).Click();
        }

        [When(@"I add university ""(.*)"" Degree ""(.*)""")]
        public void WhenIAddUniversityDegree(string p0, string p1)
        {
            //Click on Add New button
            Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/thead/tr/th[6]/div")).Click();

            //Add University
            Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/div/div[1]/div[1]/input")).SendKeys(p0);

            //Click on Country of College
            Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/div/div[1]/div[2]/select")).Click();

            //Choose the Country
            IWebElement country = Driver.driver.FindElements(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/div/div[1]/div[2]/select/option"))[10];
            country.Click();

            //Click on Title
            Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/div/div[2]/div[1]/select")).Click();

            //Choose Title
            IWebElement title = Driver.driver.FindElements(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/div/div[2]/div[1]/select/option"))[6];
            title.Click();

            //Add Degree
            Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/div/div[2]/div[2]/input")).SendKeys(p1);

            //Click on Year of Graduation
            Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/div/div[2]/div[3]/select")).Click();

            //Choose Year of Graduation
            IWebElement year = Driver.driver.FindElements(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/div/div[2]/div[3]/select/option"))[4];
            year.Click();

            //Click on Add Button
            Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/div/div[3]/div/input[1]")).Click();

        }

        [Then(@"Education with Degree ""(.*)"" should be displayed on my listings")]
        public void ThenEducationWithDegreeShouldBeDisplayedOnMyListings(string p0)
        {
            try
            {
                //Start the Reports
                CommonMethods.ExtentReports();
                Thread.Sleep(1000);
                CommonMethods.test = CommonMethods.extent.StartTest("Add Education");

                Thread.Sleep(1000);
                string ActualValue = Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody/tr/td[4]")).Text;
                Thread.Sleep(500);
                if (p0.Equals(ActualValue))
                {
                    CommonMethods.test.Log(LogStatus.Pass, "Test Passed, Added Education Successfully");
                    SaveScreenShotClass.SaveScreenshot(Driver.driver, "EducationAdded");
                }

                else
                    CommonMethods.test.Log(LogStatus.Fail, "Test Failed");

            }
            catch (Exception e)
            {
                CommonMethods.test.Log(LogStatus.Fail, "Test Failed", e.Message);
            }

        }

        [When(@"I edited university ""(.*)"" to ""(.*)""")]
        public void WhenIEditedUniversityTo(string p0, string p1)
        {
            string actualValue;
            int row = Driver.driver.FindElements(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody")).Count;
            IWebElement data;

            for (int i = 1; i <= row; i++)
            {
                data = Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody" + "[" + i + "]" + "/tr/td[2]"));
                actualValue = data.Text;
                Console.WriteLine(data.Text);

                if (p0.Equals(actualValue))
                {
                    //Click on Edit 
                    Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody" + "[" + i + "]" + "/tr/td[6]/span[1]/i")).Click();

                    //Clear  University
                    Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody" + "[" + i + "]" + "/tr/td/div[1]/div[1]/input")).Clear();

                    //Update  University
                    Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody" + "[" + i + "]" + "/tr/td/div[1]/div[1]/input")).SendKeys(p1);
                    Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody" + "[" + i + "]" + "/tr/td/div[3]/input[1]")).Click();
                    break;

                }
            }

        }

        [Then(@"Updated Education with University ""(.*)"" should be displayed on my listings")]
        public void ThenUpdatedEducationWithUniversityShouldBeDisplayedOnMyListings(string p0)
        {
            string actualValue;
            int row;
            IWebElement data;
            try
            {
                //Start the Reports
                CommonMethods.ExtentReports();
                Thread.Sleep(1000);
                CommonMethods.test = CommonMethods.extent.StartTest("Update Education");

                Thread.Sleep(2000);
                //   string ActualValue = Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[2]/div/div[2]/div/table/tbody/tr/td[1]")).Text;
                row = Driver.driver.FindElements(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody")).Count;
                Console.WriteLine(row);
                Thread.Sleep(500);
                for (int i = 1; i <= row; i++)
                {
                    data = Driver.driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[4]/div/div[2]/div/table/tbody" + "[" + i + "]" + "/tr/td[2]"));
                    actualValue = data.Text;
                    if (p0.Equals(actualValue))
                    {
                        CommonMethods.test.Log(LogStatus.Pass, "Test Passed, Updated Education Successfully");
                        SaveScreenShotClass.SaveScreenshot(Driver.driver, "UpdatedEducation");
                    }

                    else
                    {
                        CommonMethods.test.Log(LogStatus.Fail, "Test Failed");
                    }

                }

            }
            catch (Exception e)
            {
                CommonMethods.test.Log(LogStatus.Fail, "Test Failed", e.Message);
            }

        }

    }
}
