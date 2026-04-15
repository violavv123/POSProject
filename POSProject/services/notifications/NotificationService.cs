using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSProject.repositories.notifications;
using POSProject.models;

namespace POSProject.services.notifications
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notifRepo;

        public NotificationService(INotificationRepository notifRepo)
        {
            _notifRepo = notifRepo;
        }

        public List<NotificationModel> GetNotifications(bool onlyUnread, int daysBack = 30, int maxRows = 200)
        {
            return _notifRepo.GetNotifications(onlyUnread, daysBack, maxRows);
        }

        public void MarkAsRead(int id)
        {
            _notifRepo.MarkAsRead(id);
        }

        public int GetUnreadCount(int daysBack = 30)
        {
            return _notifRepo.GetUnreadCount(daysBack);
        }

        public void Create(string type, string severity, string title, string message, string relatedEntity = null, int? relatedEntityId = null, int? createdBy = null)
        {
            _notifRepo.Create(type, severity, title, message, relatedEntity, relatedEntityId, createdBy);
        }
        
    }
}
