using CompanyManagement.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CompanyManagement.ViewModel
{

    public class TicketChatViewModel
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string UserTitle { get; set; }
        public string Product { get; set; }
        public string Status { get; set; }
        public string AssignedBy { get; set; }
        public string? ChatMessage { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TicketSubject { get; set; }
        public string TicketDetails { get; set; }
        public string? FileUploadPath { get; set; } 
        public IFormFile? FileUpload { get; set; } 
        public List<TicketChatMessageViewModel> PreviousMessages { get; set; } = new List<TicketChatMessageViewModel>();
       
    }

    public class TicketChatMessageViewModel
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string TicketSubject { get; set; }
        public int UserId { get; set; }
        public string UserTitle { get; set; }
        public string TicketDetails { get; set; }
        public string ChatMessage { get; set; }
        public string Status { get; set; }
        public string AssignedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Product { get; set; }
        public string? FileUploadPath { get; set; }
        public string Username { get; set; } // Add username to this model

        // New properties for enhanced message tracking
        public string MessageType { get; set; } // E.g., Text, Image, File
    }

}
