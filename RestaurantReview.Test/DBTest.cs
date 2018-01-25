using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RestaurantReview.Test
{
    [TestClass()]
    public class DBTest : SqlDatabaseTestClass
    {

        public DBTest()
        {
            InitializeComponent();
        }

        [TestInitialize()]
        public void TestInitialize()
        {
            base.InitializeTest();
        }
        [TestCleanup()]
        public void TestCleanup()
        {
            base.CleanupTest();
        }

        #region Designer support code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_AddCityTest_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBTest));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition3;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_AddRestaurantTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_RestaurantsGetByCityTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_ReviewsGetByUserTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition2;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_ReviewsGetByUserTest_PretestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_AddRestaurantTest_PretestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_AddRestaurantTestDuplicates_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition2;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_AddRestaurantTestDuplicates_PretestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_RestaurantsGetByCityTest_PretestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_AddCityTestDuplicates_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition4;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_AddCityTest_PretestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_AddCityTestDuplicates_PretestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_AddReviewTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition5;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_AddReviewTestDuplicates_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition6;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_AddReviewTest_PretestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_AddReviewTestDuplicates_PretestAction;
            this.dbo_AddCityTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_AddRestaurantTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_RestaurantsGetByCityTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_ReviewsGetByUserTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_AddRestaurantTestDuplicatesData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_AddCityTestDuplicatesData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_AddReviewTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_AddReviewTestDuplicatesData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            dbo_AddCityTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            scalarValueCondition3 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            dbo_AddRestaurantTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            scalarValueCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            dbo_RestaurantsGetByCityTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            rowCountCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            dbo_ReviewsGetByUserTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            rowCountCondition2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            dbo_ReviewsGetByUserTest_PretestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            dbo_AddRestaurantTest_PretestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            dbo_AddRestaurantTestDuplicates_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            scalarValueCondition2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            dbo_AddRestaurantTestDuplicates_PretestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            dbo_RestaurantsGetByCityTest_PretestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            dbo_AddCityTestDuplicates_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            scalarValueCondition4 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            dbo_AddCityTest_PretestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            dbo_AddCityTestDuplicates_PretestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            dbo_AddReviewTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            scalarValueCondition5 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            dbo_AddReviewTestDuplicates_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            scalarValueCondition6 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            dbo_AddReviewTest_PretestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            dbo_AddReviewTestDuplicates_PretestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            // 
            // dbo_AddCityTest_TestAction
            // 
            dbo_AddCityTest_TestAction.Conditions.Add(scalarValueCondition3);
            resources.ApplyResources(dbo_AddCityTest_TestAction, "dbo_AddCityTest_TestAction");
            // 
            // scalarValueCondition3
            // 
            scalarValueCondition3.ColumnNumber = 1;
            scalarValueCondition3.Enabled = true;
            scalarValueCondition3.ExpectedValue = "0";
            scalarValueCondition3.Name = "scalarValueCondition3";
            scalarValueCondition3.NullExpected = false;
            scalarValueCondition3.ResultSet = 1;
            scalarValueCondition3.RowNumber = 1;
            // 
            // dbo_AddRestaurantTest_TestAction
            // 
            dbo_AddRestaurantTest_TestAction.Conditions.Add(scalarValueCondition1);
            resources.ApplyResources(dbo_AddRestaurantTest_TestAction, "dbo_AddRestaurantTest_TestAction");
            // 
            // scalarValueCondition1
            // 
            scalarValueCondition1.ColumnNumber = 1;
            scalarValueCondition1.Enabled = true;
            scalarValueCondition1.ExpectedValue = "0";
            scalarValueCondition1.Name = "scalarValueCondition1";
            scalarValueCondition1.NullExpected = false;
            scalarValueCondition1.ResultSet = 1;
            scalarValueCondition1.RowNumber = 1;
            // 
            // dbo_RestaurantsGetByCityTest_TestAction
            // 
            dbo_RestaurantsGetByCityTest_TestAction.Conditions.Add(rowCountCondition1);
            resources.ApplyResources(dbo_RestaurantsGetByCityTest_TestAction, "dbo_RestaurantsGetByCityTest_TestAction");
            // 
            // rowCountCondition1
            // 
            rowCountCondition1.Enabled = true;
            rowCountCondition1.Name = "rowCountCondition1";
            rowCountCondition1.ResultSet = 1;
            rowCountCondition1.RowCount = 2;
            // 
            // dbo_ReviewsGetByUserTest_TestAction
            // 
            dbo_ReviewsGetByUserTest_TestAction.Conditions.Add(rowCountCondition2);
            resources.ApplyResources(dbo_ReviewsGetByUserTest_TestAction, "dbo_ReviewsGetByUserTest_TestAction");
            // 
            // rowCountCondition2
            // 
            rowCountCondition2.Enabled = true;
            rowCountCondition2.Name = "rowCountCondition2";
            rowCountCondition2.ResultSet = 1;
            rowCountCondition2.RowCount = 2;
            // 
            // dbo_ReviewsGetByUserTest_PretestAction
            // 
            resources.ApplyResources(dbo_ReviewsGetByUserTest_PretestAction, "dbo_ReviewsGetByUserTest_PretestAction");
            // 
            // dbo_AddRestaurantTest_PretestAction
            // 
            resources.ApplyResources(dbo_AddRestaurantTest_PretestAction, "dbo_AddRestaurantTest_PretestAction");
            // 
            // dbo_AddRestaurantTestDuplicates_TestAction
            // 
            dbo_AddRestaurantTestDuplicates_TestAction.Conditions.Add(scalarValueCondition2);
            resources.ApplyResources(dbo_AddRestaurantTestDuplicates_TestAction, "dbo_AddRestaurantTestDuplicates_TestAction");
            // 
            // scalarValueCondition2
            // 
            scalarValueCondition2.ColumnNumber = 1;
            scalarValueCondition2.Enabled = true;
            scalarValueCondition2.ExpectedValue = "-1";
            scalarValueCondition2.Name = "scalarValueCondition2";
            scalarValueCondition2.NullExpected = false;
            scalarValueCondition2.ResultSet = 2;
            scalarValueCondition2.RowNumber = 1;
            // 
            // dbo_AddRestaurantTestDuplicates_PretestAction
            // 
            resources.ApplyResources(dbo_AddRestaurantTestDuplicates_PretestAction, "dbo_AddRestaurantTestDuplicates_PretestAction");
            // 
            // dbo_RestaurantsGetByCityTest_PretestAction
            // 
            resources.ApplyResources(dbo_RestaurantsGetByCityTest_PretestAction, "dbo_RestaurantsGetByCityTest_PretestAction");
            // 
            // dbo_AddCityTestDuplicates_TestAction
            // 
            dbo_AddCityTestDuplicates_TestAction.Conditions.Add(scalarValueCondition4);
            resources.ApplyResources(dbo_AddCityTestDuplicates_TestAction, "dbo_AddCityTestDuplicates_TestAction");
            // 
            // scalarValueCondition4
            // 
            scalarValueCondition4.ColumnNumber = 1;
            scalarValueCondition4.Enabled = true;
            scalarValueCondition4.ExpectedValue = "-1";
            scalarValueCondition4.Name = "scalarValueCondition4";
            scalarValueCondition4.NullExpected = false;
            scalarValueCondition4.ResultSet = 3;
            scalarValueCondition4.RowNumber = 1;
            // 
            // dbo_AddCityTest_PretestAction
            // 
            resources.ApplyResources(dbo_AddCityTest_PretestAction, "dbo_AddCityTest_PretestAction");
            // 
            // dbo_AddCityTestDuplicates_PretestAction
            // 
            resources.ApplyResources(dbo_AddCityTestDuplicates_PretestAction, "dbo_AddCityTestDuplicates_PretestAction");
            // 
            // dbo_AddReviewTest_TestAction
            // 
            dbo_AddReviewTest_TestAction.Conditions.Add(scalarValueCondition5);
            resources.ApplyResources(dbo_AddReviewTest_TestAction, "dbo_AddReviewTest_TestAction");
            // 
            // scalarValueCondition5
            // 
            scalarValueCondition5.ColumnNumber = 1;
            scalarValueCondition5.Enabled = true;
            scalarValueCondition5.ExpectedValue = "0";
            scalarValueCondition5.Name = "scalarValueCondition5";
            scalarValueCondition5.NullExpected = false;
            scalarValueCondition5.ResultSet = 1;
            scalarValueCondition5.RowNumber = 1;
            // 
            // dbo_AddReviewTestDuplicates_TestAction
            // 
            dbo_AddReviewTestDuplicates_TestAction.Conditions.Add(scalarValueCondition6);
            resources.ApplyResources(dbo_AddReviewTestDuplicates_TestAction, "dbo_AddReviewTestDuplicates_TestAction");
            // 
            // scalarValueCondition6
            // 
            scalarValueCondition6.ColumnNumber = 1;
            scalarValueCondition6.Enabled = true;
            scalarValueCondition6.ExpectedValue = "-1";
            scalarValueCondition6.Name = "scalarValueCondition6";
            scalarValueCondition6.NullExpected = false;
            scalarValueCondition6.ResultSet = 2;
            scalarValueCondition6.RowNumber = 1;
            // 
            // dbo_AddReviewTest_PretestAction
            // 
            resources.ApplyResources(dbo_AddReviewTest_PretestAction, "dbo_AddReviewTest_PretestAction");
            // 
            // dbo_AddReviewTestDuplicates_PretestAction
            // 
            resources.ApplyResources(dbo_AddReviewTestDuplicates_PretestAction, "dbo_AddReviewTestDuplicates_PretestAction");
            // 
            // dbo_AddCityTestData
            // 
            this.dbo_AddCityTestData.PosttestAction = null;
            this.dbo_AddCityTestData.PretestAction = dbo_AddCityTest_PretestAction;
            this.dbo_AddCityTestData.TestAction = dbo_AddCityTest_TestAction;
            // 
            // dbo_AddRestaurantTestData
            // 
            this.dbo_AddRestaurantTestData.PosttestAction = null;
            this.dbo_AddRestaurantTestData.PretestAction = dbo_AddRestaurantTest_PretestAction;
            this.dbo_AddRestaurantTestData.TestAction = dbo_AddRestaurantTest_TestAction;
            // 
            // dbo_RestaurantsGetByCityTestData
            // 
            this.dbo_RestaurantsGetByCityTestData.PosttestAction = null;
            this.dbo_RestaurantsGetByCityTestData.PretestAction = dbo_RestaurantsGetByCityTest_PretestAction;
            this.dbo_RestaurantsGetByCityTestData.TestAction = dbo_RestaurantsGetByCityTest_TestAction;
            // 
            // dbo_ReviewsGetByUserTestData
            // 
            this.dbo_ReviewsGetByUserTestData.PosttestAction = null;
            this.dbo_ReviewsGetByUserTestData.PretestAction = dbo_ReviewsGetByUserTest_PretestAction;
            this.dbo_ReviewsGetByUserTestData.TestAction = dbo_ReviewsGetByUserTest_TestAction;
            // 
            // dbo_AddRestaurantTestDuplicatesData
            // 
            this.dbo_AddRestaurantTestDuplicatesData.PosttestAction = null;
            this.dbo_AddRestaurantTestDuplicatesData.PretestAction = dbo_AddRestaurantTestDuplicates_PretestAction;
            this.dbo_AddRestaurantTestDuplicatesData.TestAction = dbo_AddRestaurantTestDuplicates_TestAction;
            // 
            // dbo_AddCityTestDuplicatesData
            // 
            this.dbo_AddCityTestDuplicatesData.PosttestAction = null;
            this.dbo_AddCityTestDuplicatesData.PretestAction = dbo_AddCityTestDuplicates_PretestAction;
            this.dbo_AddCityTestDuplicatesData.TestAction = dbo_AddCityTestDuplicates_TestAction;
            // 
            // dbo_AddReviewTestData
            // 
            this.dbo_AddReviewTestData.PosttestAction = null;
            this.dbo_AddReviewTestData.PretestAction = dbo_AddReviewTest_PretestAction;
            this.dbo_AddReviewTestData.TestAction = dbo_AddReviewTest_TestAction;
            // 
            // dbo_AddReviewTestDuplicatesData
            // 
            this.dbo_AddReviewTestDuplicatesData.PosttestAction = null;
            this.dbo_AddReviewTestDuplicatesData.PretestAction = dbo_AddReviewTestDuplicates_PretestAction;
            this.dbo_AddReviewTestDuplicatesData.TestAction = dbo_AddReviewTestDuplicates_TestAction;
        }

        #endregion


        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        #endregion

        [TestMethod()]
        public void dbo_AddCityTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_AddCityTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_AddRestaurantTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_AddRestaurantTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_RestaurantsGetByCityTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_RestaurantsGetByCityTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_ReviewsGetByUserTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_ReviewsGetByUserTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }
        [TestMethod()]
        public void dbo_AddRestaurantTestDuplicates()
        {
            SqlDatabaseTestActions testActions = this.dbo_AddRestaurantTestDuplicatesData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }
        [TestMethod()]
        public void dbo_AddCityTestDuplicates()
        {
            SqlDatabaseTestActions testActions = this.dbo_AddCityTestDuplicatesData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }
        [TestMethod()]
        public void dbo_AddReviewTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_AddReviewTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }
        [TestMethod()]
        public void dbo_AddReviewTestDuplicates()
        {
            SqlDatabaseTestActions testActions = this.dbo_AddReviewTestDuplicatesData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }





        private SqlDatabaseTestActions dbo_AddCityTestData;
        private SqlDatabaseTestActions dbo_AddRestaurantTestData;
        private SqlDatabaseTestActions dbo_RestaurantsGetByCityTestData;
        private SqlDatabaseTestActions dbo_ReviewsGetByUserTestData;
        private SqlDatabaseTestActions dbo_AddRestaurantTestDuplicatesData;
        private SqlDatabaseTestActions dbo_AddCityTestDuplicatesData;
        private SqlDatabaseTestActions dbo_AddReviewTestData;
        private SqlDatabaseTestActions dbo_AddReviewTestDuplicatesData;
    }
}
