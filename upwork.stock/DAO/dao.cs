using System.Data;
using System.Data.SqlClient;

namespace upwork.stock
{
    /// <summary>
    /// Data access object, resposible to connect with the data base
    /// </summary>
    public class Dao
    {
        private string connetionString;

        public Dao()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json")
               .Build();
            connetionString = configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Get the list of the report from the data base
        /// </summary>
        /// <returns> The available list of reports </returns>
        public List<Report> GetReportList()
        {
            List<Report> lstReport;

            using (DataSet reportListDataSet = new DataSet())
            {
                using (SqlDataAdapter reportListDataAdapter = new SqlDataAdapter())
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connetionString))
                    {
                        using (SqlCommand objCMD = new SqlCommand("sp_getReports", sqlConnection)) {
                            objCMD.CommandType = CommandType.StoredProcedure;
                            reportListDataAdapter.SelectCommand = objCMD;
                            reportListDataAdapter.Fill(reportListDataSet);
                            lstReport = reportListDataSet.Tables[0].AsEnumerable().Select(dataRow =>
                               new Report
                               {
                                   id = dataRow.Field<int>("id"),
                                   name = dataRow.Field<string>("name"),
                                   text = dataRow.Field<string>("text"),
                                   description = dataRow.Field<string>("description")
                               }).ToList();
                        }
                    }
                }
            }
            return lstReport;
        }

        /// <summary>
        /// Return the data coming from the report data
        /// </summary>
        /// <param name="viewName">view name coming from the new Dao().GetReportList()</param>
        /// <returns>data available in the view</returns>
        public DataSet GetReportData(string viewName)
        {
            DataSet reportDataSet = new DataSet();
            using (SqlDataAdapter reportDataAdapter = new SqlDataAdapter())
            {
                using (SqlConnection sqlConnection = new SqlConnection(connetionString))
                {
                    string query = "SELECT * FROM " + viewName;
                    SqlCommand objCMD = new SqlCommand(query, sqlConnection);
                    reportDataAdapter.SelectCommand = objCMD;
                    reportDataAdapter.Fill(reportDataSet);
                }
            }
            return reportDataSet;
        }

        /// <summary>
        /// Add a new transaction to the data base
        /// </summary>
        /// <param name="transactionsStatement">new transaction</param>
        public void AddTransactionStatement(TransactionsStatement transactionsStatement)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connetionString))
            {
                SqlCommand cmd = new SqlCommand("sp_addTransactionsStatement", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@scheme", transactionsStatement.scheme));
                cmd.Parameters.Add(new SqlParameter("@scrip", transactionsStatement.scrip));
                cmd.Parameters.Add(new SqlParameter("@date", transactionsStatement.statementDate));
                cmd.Parameters.Add(new SqlParameter("@isBuy", transactionsStatement.isBuy));
                cmd.Parameters.Add(new SqlParameter("@isAvailable", transactionsStatement.isAvailable));
                cmd.Parameters.Add(new SqlParameter("@netRate", transactionsStatement.netRate));
                cmd.Parameters.Add(new SqlParameter("@quantity", transactionsStatement.quantity));
                cmd.Parameters.Add(new SqlParameter("@amount", transactionsStatement.amount));
                cmd.Parameters.Add(new SqlParameter("@realisedGain", transactionsStatement.realisedGain));
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }
    }
}

