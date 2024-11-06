using DataAccess.Repositories;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace DataAccess.Wrapper
{
    public class RepositoryWrapper : Domain.Interfaces.IRepositoryWrapper
    {
        private MarketpalceContext _repoContext;
        private IUserRepository _user;
        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }
                return _user;
            }
        }
        private IUserDiscountRepository _userdiscount;
        public IUserDiscountRepository UserDiscount
        {
            get
            {
                if (_userdiscount == null)
                {
                    _userdiscount = new UserDiscountRepository(_repoContext);
                }
                return _userdiscount;
            }
        }
        private IUserFileRepository _userfile;
        public IUserFileRepository UserFile
        {
            get
            {
                if (_userfile == null)
                {
                    _userfile = new UserFileRepository(_repoContext);
                }
                return _userfile;
            }
        }
        private IAdressRepository _adress;
        public IAdressRepository Adress
        {
            get
            {
                if (_adress == null)
                {
                    _adress = new AdressRepository(_repoContext);
                }
                return _adress;
            }
        }
        private IChatRepository _chat;
        public IChatRepository Chat
        {
            get
            {
                if (_chat == null)
                {
                    _chat = new ChatRepository(_repoContext);
                }
                return _chat;
            }
        }
        private IChatParticipantRepository _chatparticipant;
        public IChatParticipantRepository ChatParticipant
        {
            get
            {
                if (_chatparticipant == null)
                {
                    _chatparticipant = new ChatParticipantRepository(_repoContext);
                }
                return _chatparticipant;
            }
        }
        private IDiscountRepository _discount;
        public IDiscountRepository Discount
        {
            get
            {
                if (_discount == null)
                {
                    _discount = new DiscountRepository(_repoContext);
                }
                return _discount;
            }
        }
        private IFilePermissionRepository _filepermission;
        public IFilePermissionRepository FilePermission
        {
            get
            {
                if (_filepermission == null)
                {
                    _filepermission = new FilePermissionRepository(_repoContext);
                }
                return _filepermission;
            }
        }
        private IFileRepository _file;
        public IFileRepository File
        {
            get
            {
                if (_file == null)
                {
                    _file = new FileRepository(_repoContext);
                }
                return _file;
            }
        }
        private IMessageRepository _message;
        public IMessageRepository Message
        {
            get
            {
                if (_message == null)
                {
                    _message = new MessageRepository(_repoContext);
                }
                return _message;
            }
        }
        private IImageRepository _image;
        public IImageRepository Image
        {
            get
            {
                if (_image == null)
                {
                    _image = new ImageRepository(_repoContext);
                }
                return _image;
            }
        }
        private INotificationRepository _notification;
        public INotificationRepository Notification
        {
            get
            {
                if (_notification == null)
                {
                    _notification = new NotificationRepository(_repoContext);
                }
                return _notification;
            }
        }
        private IOrderItemRepository _orderitem;
        public IOrderItemRepository OrderItem
        {
            get
            {
                if (_orderitem == null)
                {
                    _orderitem = new OrderItemRepository(_repoContext);
                }
                return _orderitem;
            }
        }
        private IOrderRepository _order;
        public IOrderRepository Order
        {
            get
            {
                if (_order == null)
                {
                    _order = new OrderRepository(_repoContext);
                }
                return _order;
            }
        }
        private IPaymentRepository _payment;
        public IPaymentRepository Payment
        {
            get
            {
                if (_payment == null)
                {
                    _payment = new PaymentRepository(_repoContext);
                }
                return _payment;
            }
        }
        private IPaymentUserRepository _paymentuser;
        public IPaymentUserRepository PaymentUser
        {
            get
            {
                if (_paymentuser == null)
                {
                    _paymentuser = new PaymentUserRepository(_repoContext);
                }
                return _paymentuser;
            }
        }
        private IPriceHistoryRepository _pricehistory;
        public IPriceHistoryRepository PriceHistory
        {
            get
            {
                if (_pricehistory == null)
                {
                    _pricehistory = new PriceHistoryRepository(_repoContext);
                }
                return _pricehistory;
            }
        }
        private ISearchHistoryRepository _searchhistory;
        public ISearchHistoryRepository SearchHistory
        {
            get
            {
                if (_searchhistory == null)
                {
                    _searchhistory = new SearchHistoryRepository(_repoContext);
                }
                return _searchhistory;
            }
        }
        private IProductRepository _product;
        public IProductRepository Product
        {
            get
            {
                if (_product == null)
                {
                    _product = new ProductRepository(_repoContext);
                }
                return _product;
            }
        }
        private IReviewRepository _review;
        public IReviewRepository Review
        {
            get
            {
                if (_review == null)
                {
                    _review = new ReviewRepository(_repoContext);
                }
                return _review;
            }
        }
        public RepositoryWrapper(MarketpalceContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public async Task Save()
        {
            await _repoContext.SaveChangesAsync();
        }
    }
}