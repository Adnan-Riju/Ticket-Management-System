using CompanyManagement.ModelHelper;
using CompanyManagement.Models;
using CompanyManagement.ViewModel;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Net.Sockets;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace TicketManagementSystem.Controllers
{
    public class SupportEmployeeDashboardController : Controller
    {
        public CMScontext _context;

        public SupportEmployeeDashboardController(CMScontext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> EmployeeActiveTickets()
        {
            var vmuser = HttpContext.Session.GetComplexData<VmUser>("UserInformation");
            if (vmuser == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var userId = vmuser.Id;
            var tickets = await _context.Ticket
                .Where(t => t.ActiveStatus == 1 && t.Assigned == userId) // Assuming 1 is the code for Active status
                .Select(t => new TicketVm
                {
                    TicketId = t.Id,
                    TicketNo = t.TicketNo,
                    IssuedBy = t.IssuedBy,
                    IssuedDate = t.IssuedDate,
                    ProductId = t.ProductId,
                    ProductName = _context.Product
                                    .Where(p => p.Id == t.ProductId)
                                    .Select(p => p.ProductName)
                                    .FirstOrDefault(),
                    ServiceTypeId = t.ServiceTypeId,
                    ServiceTypeName = _context.ServiceType
                                        .Where(s => s.Id == t.ServiceTypeId)
                                        .Select(s => s.ServiceTypeName)
                                        .FirstOrDefault(),
                    TicketTypeId = t.TicketTypeId,
                    TicketTypeName = _context.TicketType
                                            .Where(tt => tt.Id == t.TicketTypeId)
                                            .Select(tt => tt.TicketTypeName)
                                            .FirstOrDefault(),
                    TicketSubject = t.TicketSubject,
                    TicketDetails = t.TicketDetails,
                    CompleteBy = t.CompleteBy,
                    CompleteDate = t.CompleteDate,
                    ManagementTicketStatus = t.ManagementTicketStatus,
                    CustomerStatus = t.CustomerStatus,
                    ActiveStatus = t.ActiveStatus,
                    IssuedByName = _context.User
                                        .Where(u => u.Id == t.IssuedBy)
                                        .Select(u => u.UserName)
                                        .FirstOrDefault(),
                    
                    
                    AssignedName = t.Assigned.HasValue
                                    ? _context.User
                                        .Where(u => u.Id == t.Assigned.Value)
                                        .Select(u => u.UserName)
                                        .FirstOrDefault()
                                    : "Not Assigned",
                    CompleteByName = t.CompleteBy.HasValue
                                    ? _context.User
                                        .Where(u => u.Id == t.CompleteBy.Value)
                                        .Select(u => u.UserName)
                                        .FirstOrDefault()
                                    : null
                })
                .ToListAsync();

            return View(tickets);
        }


        public async Task<IActionResult> EmployeeProcessingTickets()
        {
            var vmuser = HttpContext.Session.GetComplexData<VmUser>("UserInformation");
            if (vmuser == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var userId = vmuser.Id;
            var tickets = await _context.Ticket
                .Where(t => t.ActiveStatus == 2 && t.Assigned == userId) // Assuming 1 is the code for Active status
                .Select(t => new TicketVm
                {
                    TicketId = t.Id,
                    TicketNo = t.TicketNo,
                    IssuedBy = t.IssuedBy,
                    IssuedDate = t.IssuedDate,
                    ProductId = t.ProductId,
                    ProductName = _context.Product
                                    .Where(p => p.Id == t.ProductId)
                                    .Select(p => p.ProductName)
                                    .FirstOrDefault(),
                    ServiceTypeId = t.ServiceTypeId,
                    ServiceTypeName = _context.ServiceType
                                        .Where(s => s.Id == t.ServiceTypeId)
                                        .Select(s => s.ServiceTypeName)
                                        .FirstOrDefault(),
                    TicketTypeId = t.TicketTypeId,
                    TicketTypeName = _context.TicketType
                                            .Where(tt => tt.Id == t.TicketTypeId)
                                            .Select(tt => tt.TicketTypeName)
                                            .FirstOrDefault(),
                    TicketSubject = t.TicketSubject,
                    TicketDetails = t.TicketDetails,
                    CompleteBy = t.CompleteBy,
                    CompleteDate = t.CompleteDate,
                    ManagementTicketStatus = t.ManagementTicketStatus,
                    CustomerStatus = t.CustomerStatus,
                    ActiveStatus = t.ActiveStatus,
                    IssuedByName = _context.User
                                        .Where(u => u.Id == t.IssuedBy)
                                        .Select(u => u.UserName)
                                        .FirstOrDefault(),


                    AssignedName = t.Assigned.HasValue
                                    ? _context.User
                                        .Where(u => u.Id == t.Assigned.Value)
                                        .Select(u => u.UserName)
                                        .FirstOrDefault()
                                    : "Not Assigned",
                    CompleteByName = t.CompleteBy.HasValue
                                    ? _context.User
                                        .Where(u => u.Id == t.CompleteBy.Value)
                                        .Select(u => u.UserName)
                                        .FirstOrDefault()
                                    : null
                })
                .ToListAsync();

            return View(tickets);
        }

        public async Task<IActionResult> EmployeeClosedTickets()
        {
            var vmuser = HttpContext.Session.GetComplexData<VmUser>("UserInformation");
            if (vmuser == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var userId = vmuser.Id;
            var tickets = await _context.Ticket
                .Where(t => t.ActiveStatus == 3 && t.Assigned == userId) // Assuming 3 is the code for Closed status
                .Select(t => new TicketVm
                {
                    TicketId = t.Id,
                    TicketNo = t.TicketNo,
                    IssuedBy = t.IssuedBy,
                    IssuedDate = t.IssuedDate,
                    ProductId = t.ProductId,
                    ProductName = _context.Product.Where(p => p.Id == t.ProductId).Select(p => p.ProductName).FirstOrDefault(),
                    ServiceTypeId = t.ServiceTypeId,
                    ServiceTypeName = _context.ServiceType.Where(s => s.Id == t.ServiceTypeId).Select(s => s.ServiceTypeName).FirstOrDefault(),
                    TicketTypeId = t.TicketTypeId,
                    TicketTypeName = _context.TicketType.Where(tt => tt.Id == t.TicketTypeId).Select(tt => tt.TicketTypeName).FirstOrDefault(),
                    TicketSubject = t.TicketSubject,
                    TicketDetails = t.TicketDetails,
                    CompleteBy = t.CompleteBy,
                    CompleteDate = t.CompleteDate,
                    ManagementTicketStatus = t.ManagementTicketStatus,
                    CustomerStatus = t.CustomerStatus,
                    ActiveStatus = t.ActiveStatus,
                    IssuedByName = _context.User.Where(u => u.Id == t.IssuedBy).Select(u => u.UserName).FirstOrDefault(),
                   
                    AssignedName = t.Assigned.HasValue ? _context.User.Where(u => u.Id == t.Assigned.Value).Select(u => u.UserName).FirstOrDefault() : "Not Assigned",
                    CompleteByName = t.CompleteBy.HasValue ? _context.User.Where(u => u.Id == t.CompleteBy.Value).Select(u => u.UserName).FirstOrDefault() : null
                })
                .ToListAsync();

            return View(tickets);
        }

        
        public async Task<IActionResult> EmployeeTotalTickets()
        {
            var vmuser = HttpContext.Session.GetComplexData<VmUser>("UserInformation");
            if (vmuser == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var userId = vmuser.Id;
            // Fetch tickets with all statuses 1, 2, 3, and 4
            var tickets = await _context.Ticket
                .Where(t => t.ActiveStatus >= 1 && t.ActiveStatus <= 4 && (t.Assigned == userId || t.Assigned == userId))
                .Select(t => new TicketVm
                {
                    TicketId = t.Id,
                    TicketNo = t.TicketNo,
                    IssuedBy = t.IssuedBy,
                    IssuedDate = t.IssuedDate,
                    ProductId = t.ProductId,
                    ProductName = _context.Product.Where(p => p.Id == t.ProductId).Select(p => p.ProductName).FirstOrDefault(),
                    ServiceTypeId = t.ServiceTypeId,
                    ServiceTypeName = _context.ServiceType.Where(s => s.Id == t.ServiceTypeId).Select(s => s.ServiceTypeName).FirstOrDefault(),
                    TicketTypeId = t.TicketTypeId,
                    TicketTypeName = _context.TicketType.Where(tt => tt.Id == t.TicketTypeId).Select(tt => tt.TicketTypeName).FirstOrDefault(),
                    TicketSubject = t.TicketSubject,
                    TicketDetails = t.TicketDetails,
                    CompleteBy = t.CompleteBy,
                    CompleteDate = t.CompleteDate,
                    ManagementTicketStatus = t.ManagementTicketStatus,
                    CustomerStatus = t.CustomerStatus,
                    ActiveStatus = t.ActiveStatus,
                    IssuedByName = _context.User.Where(u => u.Id == t.IssuedBy).Select(u => u.UserName).FirstOrDefault(),
                    
                    AssignedName = t.Assigned.HasValue ? _context.User.Where(u => u.Id == t.Assigned.Value).Select(u => u.UserName).FirstOrDefault() : "Not Assigned",
                    CompleteByName = t.CompleteBy.HasValue ? _context.User.Where(u => u.Id == t.CompleteBy.Value).Select(u => u.UserName).FirstOrDefault() : null
                })
                .ToListAsync();

            return View(tickets);
        }
    }
}
