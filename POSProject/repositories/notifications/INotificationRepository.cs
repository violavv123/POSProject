using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSProject.models;

namespace POSProject.repositories.notifications
{
    public interface INotificationRepository
    {
        List<NotificationModel> GetNotifications(bool onlyUnread, int daysBack = 30, int maxRows = 200);
        void MarkAsRead(int id);
        int GetUnreadCount(int daysBack = 30);
        void Create(string type, string severity, string title, string message, string relatedEntity = null, int? relatedEntityId = null, int? createdBy = null);
    }
}
