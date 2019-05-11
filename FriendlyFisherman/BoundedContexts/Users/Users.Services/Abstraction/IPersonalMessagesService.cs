using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Users.Services.Request.PersonalMessage;
using Users.Services.Response.PersonalMessage;

namespace Users.Services.Abstraction
{
    public interface IPersonalMessagesService
    {
        Task<GetAllMessagesResponse> GetAllMessagesBySenderIdAsync(GetAllMessagesRequest request);       
        Task<GetAllMessagesResponse> GetAllMessagesByReceiverIdAsync(GetAllMessagesRequest request);   
        Task<GetAllMessagesResponse> GetMessageThreadAsync(GetMessagesRequest request);   
        Task<GetMessageResponse> GetMessageByIdAsync(GetMessagesRequest request);                
        Task<EditMessageResponse> SaveMessageAsync(EditMessageRequest request);
        Task<EditMessageResponse> MarkMessageAsReadAsync(EditMessageRequest request);
        Task<GetMessageResponse> DeleteMessageAsync(GetMessagesRequest request); 
    }
}
