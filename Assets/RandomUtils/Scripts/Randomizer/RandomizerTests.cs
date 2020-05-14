using System.Collections.Generic;

namespace GG.Infrastructure.Utils
{
    public static class RandomSelectorTests
    {
        private const string OVERALL_RESULT = "Random selector tests result. No-repeat errors: {0}. Distribution result: {1}";
        private const string NO_REPEAT_RESULT = "repeats: {0}, resolution: {1}";
        private const string DISTRIBUTION_RESULT = "min occurance {0}, max occurance {1}";
        private const string REPEAT_AT_STEP = " total {0} at steps: {1}";
        private const string RESOLUTION_PASSED = "Passed";
        private const string RESOLUTION_FAILED = "Failed";
        private const string REPEATED_STEP = " {0} ";

        /// <summary>
        /// Performs internal testing:
        ///  - Select method - selection with no repeat
        ///  - SelectFlatDistributed - selection is random, but every item selected in same proportion.
        /// </summary>
        /// <param name="testingAmount">amount of items to test</param>
        /// <param name="noRepeatIterations">how many iterations perform to test Select method</param>
        /// <param name="distributionIterations">how many iterations perform to test SelectFlatDistributed method</param>
        /// <returns>test report with errors and resolution</returns>
        public static string Test(int testingAmount = 10, int noRepeatIterations = 100, int distributionIterations = 100)
        {
            Randomizer testSelector = PrepareTestingSelector(testingAmount);

            string repeatSteps, repeatResolution;

            TestNoRepeatFunctionality(noRepeatIterations, testSelector, out repeatSteps, out repeatResolution);

            int minOccurance, maxOccurance;

            TestFlatDistributionFunctionality(distributionIterations, testSelector, out minOccurance, out maxOccurance);

            return string.Format(OVERALL_RESULT,
                string.Format(NO_REPEAT_RESULT, repeatSteps, repeatResolution),
                string.Format(DISTRIBUTION_RESULT, minOccurance, maxOccurance));
        }

        private static Randomizer PrepareTestingSelector(int testingAmount)
        {
            var testList = new List<int>();

            for (int i = 0; i < testingAmount; ++i)
            {
                testList.Add(i);
            }

            Randomizer testSelector = new Randomizer(testList.Count);
            return testSelector;
        }

        private static void TestFlatDistributionFunctionality(int distributionIterations, Randomizer testSelector, out int minOccurance, out int maxOccurance)
        {
            Dictionary<int, int> distributionTest = new Dictionary<int, int>();

            int currentSelection = testSelector.SelectFlatDistributed();

            for (int i = 0; i < distributionIterations; ++i)
            {
                currentSelection = FlatDistributionTestIteration(testSelector, distributionTest);
            }

            FlatDistributionTestResultsPreparation(out minOccurance, out maxOccurance, distributionTest);
        }

        private static int FlatDistributionTestIteration(Randomizer testSelector, Dictionary<int, int> distributionTest)
        {
            int currentSelection = testSelector.SelectFlatDistributed();

            if (distributionTest.ContainsKey(currentSelection))
            {
                distributionTest[currentSelection] = distributionTest[currentSelection] + 1;
            }
            else
            {
                distributionTest.Add(currentSelection, 1);
            }

            return currentSelection;
        }

        private static void FlatDistributionTestResultsPreparation(out int minOccurance, out int maxOccurance, Dictionary<int, int> distributionTest)
        {
            minOccurance = int.MaxValue;
            maxOccurance = 0;
            for (int i = 0; i < distributionTest.Count; ++i)
            {
                if (distributionTest[i] < minOccurance)
                {
                    minOccurance = distributionTest[i];
                }

                if (distributionTest[i] > maxOccurance)
                {
                    maxOccurance = distributionTest[i];
                }
            }
        }

        private static void TestNoRepeatFunctionality(int noRepeatIterations, Randomizer testSelector, out string repeatSteps, out string repeatResolution)
        {
            int previousValue = testSelector.SelectNoRepeat();

            repeatSteps = string.Empty;
            int totalRepeats = 0;

            for (int i = 0; i < noRepeatIterations; ++i)
            {
                NoRepeatTestIteration(testSelector, ref repeatSteps, ref previousValue, ref totalRepeats, i);
            }

            repeatResolution = NoRepeatTestResultsPreparation(ref repeatSteps, totalRepeats);
        }

        private static void NoRepeatTestIteration(Randomizer testSelector, ref string repeatSteps, ref int previousValue, ref int totalRepeats, int i)
        {
            var testValue = testSelector.SelectNoRepeat();
            if (previousValue == testValue)
            {
                repeatSteps = repeatSteps + string.Format(REPEATED_STEP, i);
                totalRepeats++;
            }
            previousValue = testValue;
        }

        private static string NoRepeatTestResultsPreparation(ref string repeatSteps, int totalRepeats)
        {
            string repeatResolution = RESOLUTION_PASSED;
            if (totalRepeats > 0)
            {
                repeatResolution = RESOLUTION_FAILED;
                repeatSteps = string.Format(REPEAT_AT_STEP, totalRepeats, repeatSteps);
            }
            else
            {
                repeatSteps = 0.ToString();
            }

            return repeatResolution;
        }
    }
}
