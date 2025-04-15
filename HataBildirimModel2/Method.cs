using Microsoft.Data.SqlClient;
using System.Data;

namespace HataBildirimModel2
{
    public class Method
    {
        // sql baglantısı

        public SqlConnection baglan()
        {
            SqlConnection baglanti = new SqlConnection("Data Source=ZIYABURAKYAYLA\\SQLEXPRESS;Initial Catalog=HataBildirim;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            baglanti.Open();
            return (baglanti);
        }

        public DataRow GetDataRow(string sql)
        {
            DataTable table = GetDataTable(sql);
            if (table.Rows.Count == 0) return null;
            return table.Rows[0];
        }

        public DataTable GetDataTable(string sql)
        {
            SqlConnection baglanti = this.baglan();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, baglanti);
            DataTable dt = new DataTable();
            try
            {
                adapter.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message + " (" + sql + ")");
            }
            adapter.Dispose();
            baglanti.Close();
            baglanti.Dispose();
            return dt;
        }


    }
}
