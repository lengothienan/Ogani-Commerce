using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using System.Configuration;
using System.Net;
using System.Net.Mail;
namespace ActionDB
{
    public class Code
    {
        Model1 db = new Model1();
        public Admin GetAdmin(String AID)
        {
            return db.Admins.Where(a => a.AID == AID).FirstOrDefault();
        }
        public List<PayOrder> GetPayOrder(String UID)
        {
            return db.PayOrders.Where(c=>c.UID==UID).ToList();
        }
        public PayOrder GetPayOrderOne(String IDP)
        {
            return db.PayOrders.Where(c => c.IDP == IDP).FirstOrDefault();
        }
        public List<InforOrder> GetInforOrders(String IDP)
        {
            return db.InforOrders.Where(c => c.IDP == IDP).ToList();
        }
        public User GetUser(string uid)
        {
            return db.Users.Where(c => c.UID == uid).FirstOrDefault();
        }
        public User GetUserID(string uid)
        {
            return db.Users.Where(c => c.ID == uid).FirstOrDefault();
        }
        public Product GetProduct(string idsp)
        {
            return db.Products.Where(c=> c.IDSP == idsp).FirstOrDefault();
        }
        public List<Product> GetProducts()
        {
            return db.Products.ToList();
        }
        public List<Product> GetProductSearch(string Name)
        {
            string sql = "SELECT * from Product where Name like '%"+Name+"%'";
            return db.Database.SqlQuery<Product>(sql).ToList();
        }
        public void AddObject<T>(T obj)
        {
            db.Set(obj.GetType()).Add(obj);
        }
        public void DeleteObject<T>(T obj)
        {
            db.Set(obj.GetType()).Remove(obj);
        }
        public void Save()
        {
            db.SaveChanges();
        }
        public void DeleteList<T>(List<T> objs)
        {
            db.Set(objs[0].GetType()).RemoveRange(objs);
        }
        public List<ProductType> productTypes()
        {
            return db.ProductTypes.ToList();
        }
        public List<ProductOfType> GetProductOfTypes(string idsp)
        {
            return db.ProductOfTypes.Where(m=>m.IDSP == idsp).ToList();
        }
        public List<ProductOfType> GetProductOfTypess(string idlsp)
        {
            return db.ProductOfTypes.Where(m => m.IDLSP == idlsp).ToList();
        }
        public Sale GetSale(string id) {
            return db.Sales.Where(m => m.IDKM == id).FirstOrDefault();
        }
        public List<Sale> GetSales()
        {
            return db.Sales.ToList();
        }
        public LoveProduct GetLoveProduct(string uid,string idsp)
        {
            return db.LoveProducts.Where(m => m.UID == uid && m.IDSP == idsp).FirstOrDefault();
        }
        public List<LoveProduct> GetLoveProducts(string uid)
        {
            return db.LoveProducts.Where(m => m.UID == uid).ToList();
        }
        public Comment GetComment(string id)
        {
            return db.Comments.Where(m => m.IDCM == id).FirstOrDefault();
        }
        public List<Comment> GetComments()
        {
            return db.Comments.ToList();
        }
        public List<Chat> GetChats()
        {
            return db.Chats.ToList();
        }
        public void sendmail(string ToEmailAddress, string subject, string content)
        {
            var FromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var FromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
            var FromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var SMTPHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var SMTPPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();
            bool EnabledSSL = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());
            string body = content;
            MailMessage mail = new MailMessage(new MailAddress(FromEmailAddress, FromEmailDisplayName), new MailAddress(ToEmailAddress));
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;
            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(FromEmailAddress, FromEmailPassword);
            client.Host = SMTPHost;
            client.EnableSsl = EnabledSSL;
            client.Port = !string.IsNullOrEmpty(SMTPPort) ? Convert.ToInt32(SMTPPort) : 0;
            client.Send(mail);
        }
        public List<AddressOrder> GetAddressOrders()
        {
            return db.AddressOrders.ToList();
        }
        public FeeShip GetFeeShip(string ID)
        {
            return db.FeeShips.Where(m => m.ID == ID).FirstOrDefault();
        }
        public List<Event> GetEvents()
        {
            return db.Events.ToList();
        }
        public List<ProductOfEvent> GetProductOfEvents(string id)
        {
            return db.ProductOfEvents.Where(m => m.IDE == id).ToList();
        }
    }
}
