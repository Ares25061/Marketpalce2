using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Wrapper
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IProductRepository Product { get; }
        IFileRepository File { get; }
        IAdressRepository Adress { get; }
        IChatRepository Chat { get; }
        IChatParticipantRepository ChatParticipant { get; }
        IDiscountRepository Discount { get; }
        IFilePermissionRepository FilePermission { get; }
        IImageRepository Image { get; }
        IMessageRepository Message { get; }
        INotificationRepository Notification { get; }
        IOrderItemRepository OrderItem { get; }
        IOrderRepository Order { get; }
        IPaymentRepository Payment { get; }
        IPaymentUserRepository PaymentUser { get; }
        IPriceHistoryRepository PriceHistory { get; }
        IReviewRepository Review { get; }
        ISearchHistoryRepository SearchHistory { get; }
        IUserDiscountRepository UserDiscount { get; }
        IUserFileRepository UserFile { get; }
        void Save();
    }
}
