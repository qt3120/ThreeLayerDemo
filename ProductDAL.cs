using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThreeLayerDemo2.Core.Entity;
namespace ThreeLayerDemo2.Core.DAL
{
    class ProductDAL
    {
        private DBConnection conn = null;
        public ProductDAL() {
            this.conn = new DBConnection("localhost", "Products");
        }
        public bool InsertProduct(Product p) {
            string cmd = "INSERT INTO Product(ProductID, ProductName, ProductDescriptions, CategoryID) " +
                " VALUES(@id, @name, @description, @catid)";
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@id", SqlDbType.Int);
            sqlParams[0].Value = p.ProductID;
            sqlParams[1] = new SqlParameter("@name", SqlDbType.NVarChar);
            sqlParams[1].Value = p.ProductName;
            sqlParams[2] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParams[2].Value = p.ProductDescriptions;
            sqlParams[3] = new SqlParameter("@catid", SqlDbType.Int);
            sqlParams[3].Value = p.CategoryID;
            return this.conn.executeInsertQuery(cmd, sqlParams);
        }
        public bool UpdateProduct(Product p) {
            string cmd = "UPDATE Product SET ProductName=@name" + 
                ", ProductDescriptions=@description, CategoryID=@catid " +
                " WHERE ProductID=@id";
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@name", SqlDbType.NVarChar);
            sqlParams[0].Value = p.ProductName;
            sqlParams[1] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParams[1].Value = p.ProductDescriptions;
            sqlParams[2] = new SqlParameter("@catid", SqlDbType.Int);
            sqlParams[2].Value = p.CategoryID;
            sqlParams[3] = new SqlParameter("@id", SqlDbType.Int);
            sqlParams[3].Value = p.ProductID;
            return this.conn.executeUpdateQuery(cmd, sqlParams);
        }
        public List<Product> GetProducts() {
            string cmd = "Select *from Product";
            DataTable table = this.conn.executeSelectQuery(cmd, null);
            List<Product> list = new List<Product>();
            foreach (DataRow dr in table.Rows)
            {
                Product p = new Product();
                p.ProductID = int.Parse(dr["ProductID"].ToString());
                p.ProductName = dr["ProductName"].ToString();
                p.ProductDescriptions = dr["ProductDescriptions"].ToString();
                p.CategoryID = int.Parse(dr["CategoryID"].ToString());
                list.Add(p);
            }
            return list;
        }
        public Product Find(int id) {
            List<Product> list = this.GetProducts();
            return list.Find(p => p.ProductID == id);
        }
        public List<Product> FindByName(string name) {
            List<Product> list = this.GetProducts();
            return list.FindAll(p => p.ProductName.Contains(name));
        }
        public bool DeleteProduct(int id)
        {
            String cmd = "Delete from Product WHERE ProductID = @id";
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@id", SqlDbType.Int);
            sqlParams[0].Value = id;
            return this.conn.executeInsertQuery(cmd, sqlParams);
        }
    }
}
