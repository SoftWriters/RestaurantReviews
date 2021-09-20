using System.Data;

namespace RestaurantReviews.DataAccess
{
    public static class TestManager
    {
        public static void ClearTestData()
        {
            DBCaller.IsTest = true;
            string script = "TRUNCATE TABLE tbl_Review; TRUNCATE TABLE tbl_Restaurant; TRUNCATE TABLE tbl_User;";
            DBCaller.Call(script, DBCaller.CreateParameterList(), CommandType.Text);
        }
    }
}
