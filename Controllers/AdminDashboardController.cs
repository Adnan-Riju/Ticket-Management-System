using CompanyManagement.ViewModel;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CompanyManagement.Controllers
{
    public class AdminDashboardController : Controller
    {
        public IActionResult Index()
        {
            var vmuser = HttpContext.Session.GetComplexData<VmUser>("UserInformation");

            if (vmuser == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (vmuser.UserRoleId != 0 && vmuser.UserRoleId != 1)
            {
                // Optionally, redirect to an unauthorized page or return a forbidden response
                return RedirectToAction("Login", "Login"); // Adjust according to your routing
            }
            var sql = @"
        SELECT 
            SUM(CASE WHEN ActiveStatus = 1 THEN 1 ELSE 0 END) AS ActiveCount, 
            SUM(CASE WHEN ActiveStatus = 2 THEN 1 ELSE 0 END) AS ProcessingCount,
            SUM(CASE WHEN ActiveStatus = 3 THEN 1 ELSE 0 END) AS DelegateCount,
            SUM(CASE WHEN ActiveStatus = 4 THEN 1 ELSE 0 END) AS DiscardCount,
            COUNT(*) AS TotalCount
        FROM Ticket
    ";

            using (var con = new SqlConnection(AppSetting.ConnectionString))
            {
                var result = con.QuerySingle(sql);

                var ticketSummaries = new List<TicketSummary>
        {
            new TicketSummary { StatusCount = result.ActiveCount, ActiveStatusString = "Active" },
            new TicketSummary { StatusCount = result.ProcessingCount, ActiveStatusString = "Processing" },
            new TicketSummary { StatusCount = result.DelegateCount, ActiveStatusString = "Closed" },
            new TicketSummary { StatusCount = result.DiscardCount, ActiveStatusString = "Discard" },
            new TicketSummary { StatusCount = result.TotalCount, ActiveStatusString = "Total Tickets" }  // Adding total count
        };

                ViewBag.TicketSummaries = ticketSummaries;
                return View(ticketSummaries);
            }
        }
    }
}
