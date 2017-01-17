<%@ WebHandler Language="C#" Class="Search_CS" %>

using System;
using System.Web;

using System.Configuration;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data;

public class Search_CS : IHttpHandler {
   
    public void ProcessRequest (HttpContext context) {
        string prefixText = context.Request.QueryString["q"];
        //using (SqlConnection conn = new SqlConnection())
        //{
            //conn.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            //using (SqlCommand cmd = new SqlCommand())
            //{
        //MySqlCommand cmd = new MySqlCommand("Insert into contactus (Name,Email,Country,ContactNo,Subject,Message)values (@name,@email,@country,@contact,@subject,@message)", con);
        //cmd.Parameters.AddWithValue("@name", txtname.Text);
        //cmd.Parameters.AddWithValue("@email", txtEmail.Text);
        //cmd.Parameters.AddWithValue("@country", ddlCountry.SelectedValue);
        //cmd.Parameters.AddWithValue("@contact", txtContact.Text);
        //cmd.Parameters.AddWithValue("@subject", txtSubject.Text);
        //cmd.Parameters.AddWithValue("@message", txtMessage.Text);
        //con.Open();
        //cmd.ExecuteNonQuery();
        //con.Close();
                //MySqlCommand cmd = new MySqlCommand("select gardenname from garden where gardenname like @SearchText + '%'",conn);
                //cmd.Parameters.AddWithValue("@SearchText", prefixText);
                //cmd.Connection = conn;
                //StringBuilder sb = new StringBuilder(); 
                //conn.Open();
                //System.Data.DataSet ds= cmd.ExecuteNonQuery();
                //{
                //    while (sdr.Read())
                //    {
                //        sb.Append(sdr["gardenname"])
                //            .Append(Environment.NewLine);
                //    }
                //}
                //conn.Close();
        BranchData cg = new BranchData();
        System.Data.DataSet ds = cg.getBranch("select gardenname from garden where gardenname like '%"+prefixText+"%'");
        StringBuilder sb = new StringBuilder();
        for(int i=0;i<ds.Tables[0].Rows.Count;i++)
        {
            sb.Append(ds.Tables[0].Rows[i]["gardenname"].ToString())
            .Append(Environment.NewLine);
        }
         context.Response.Write(sb.ToString()); 
            }
    //    }
    //}

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}