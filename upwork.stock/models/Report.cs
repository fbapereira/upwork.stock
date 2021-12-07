using System.Data;

namespace upwork.stock
{
    /// <summary>
    /// UI Table report
    /// </summary>
    public class Report
    {
        /// <summary>
        /// General id
        /// </summary>
        public int id;

        /// <summary>
        /// Name that will be researched
        /// </summary>
        public string? name;

        /// <summary>
        /// Name that will go to UI
        /// </summary>
        public string? text;

        /// <summary>
        /// Description that will go to UI
        /// </summary>
        public string? description;

        /// <summary>
        /// Data
        /// </summary>
        public DataSet? dataset;
    }
}
