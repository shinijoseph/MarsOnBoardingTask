using System;
using TechTalk.SpecFlow;

namespace SpecflowTests
{
    [Binding]
    public class SpecFlowFeatureSteps
    {
        [When(@"I add different languages English  with levels Fluent")]
        public void WhenIAddDifferentLanguagesEnglishWithLevelsFluent()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"English should be displayed on my listing")]
        public void ThenEnglishShouldBeDisplayedOnMyListing()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
