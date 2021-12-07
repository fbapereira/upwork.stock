using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace upwork.stock.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public TransactionsStatement newTransactionsStatement { get; set; }
        public List<Report> reportList;
        public bool isSucessful;
        public string status = "menu";
        public Report targetReport;
        private readonly Dao _dao = new Dao();

        public IndexModel(ILogger<IndexModel> logger)
        {
            reportList = _dao.GetReportList();
        }

        public void OnPostLoadReport(string id)
        {
            int reportId = Convert.ToInt32(id);
            targetReport = reportList.Find((x) => x.id == reportId);
            targetReport.dataset = _dao.GetReportData(targetReport.name);
            status = "report";
        }

        public void OnPostChangeStatus(string newStatus) { 
            isSucessful = false;
            status = newStatus;
        }

        public void OnPostAddTransaction()
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            _dao.AddTransactionStatement(newTransactionsStatement);
            status = "menu";
            isSucessful = true;
        }
    }
}