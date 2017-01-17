using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// Summary description for BranchData
/// </summary>
public class BranchData
{
	 private int _Id;
     private int _College_id;
     private int _Deparment_id;
     private string _Branch_name;
     private string _Duration;

	public BranchData()
	{
		//
		// TODO: Add constructor logic here
		//
	}
     public BranchData(int Id)
    {
        Connection connect = new Connection();
        List<MySqlParameter> param = new List<MySqlParameter>();
        param.Add(new MySqlParameter("@int_Id", Id));
        using (DataSet ds = connect.GetDataset("SELECT * FROM branch WHERE id=@int_Id", param))
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                HasValue = true;
                _Id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                _College_id =int.Parse( ds.Tables[0].Rows[0]["college_id"].ToString());
                _Deparment_id = int.Parse( ds.Tables[0].Rows[0]["branch_id"].ToString());
                _Branch_name = ds.Tables[0].Rows[0]["branch_name"].ToString();
                _Duration = ds.Tables[0].Rows[0]["duration"].ToString();
            }
            else
            {
                HasValue = false;
            }
        }
        connect.Dispose();
        connect = null;
    }
     public BranchData(string area)
    {
        Connection connect = new Connection();
        List<MySqlParameter> param = new List<MySqlParameter>();
        param.Add(new MySqlParameter("@unicollagename", area));
        using (DataSet ds = connect.GetDataset("SELECT * FROM universitycollage WHERE unicollagename=@unicollagename", param))
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                HasValue = true;
             
                _Id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                _College_id =int.Parse( ds.Tables[0].Rows[0]["college_id"].ToString());
                _Deparment_id = int.Parse(ds.Tables[0].Rows[0]["branch_id"].ToString());
                _Branch_name = ds.Tables[0].Rows[0]["branch_name"].ToString();
                _Duration = ds.Tables[0].Rows[0]["duration"].ToString();

            }
            else
            {
                HasValue = false;
            }
        }
        connect.Dispose();
        connect = null;
    }
    public void Save()
    {
        List<MySqlParameter> param = new List<MySqlParameter>();
        param.Add(new MySqlParameter("@college_id", _College_id));
        param.Add(new MySqlParameter("@branch_id", _Deparment_id));
        param.Add(new MySqlParameter("@branch_name", _Branch_name));
        param.Add(new MySqlParameter("@duration", _Duration));
        Connection connect = new Connection();
        connect.ExecStatement("INSERT INTO branch(college_id,branch_id,branch_name,duration) VALUES(@college_id,@branch_id,@branch_name,@duration)", param);
        connect.Dispose();
        connect = null;
    }

    public void Update(int id)
    {
        List<MySqlParameter> param = new List<MySqlParameter>();
        param.Add(new MySqlParameter("@id", id));
        param.Add(new MySqlParameter("@college_id", _College_id));
        param.Add(new MySqlParameter("@branch_id", _Deparment_id));
        param.Add(new MySqlParameter("@branch_name", _Branch_name));
        param.Add(new MySqlParameter("@duration", _Duration));
        Connection connect = new Connection();
        connect.ExecStatement("UPDATE college SET college_name=@college_name,college_address=@college_address,college_details=@college_details WHERE id=@id", param);
        connect.Dispose();
        connect = null;
    }
    public DataSet getBranch(String query)
    {
        Connection connect = new Connection();
        List<MySqlParameter> param = new List<MySqlParameter>();

        DataSet ds = connect.GetDataset(query);
        return ds;
    }
    public DataSet getFullUniversity()
    {
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["mySQLconn"].ConnectionString;
        MySqlConnection cnn = new MySqlConnection(conn);
        MySqlDataAdapter cmd1 = new MySqlDataAdapter("select * from college where typs='University'", cnn);

        //Create and fill the DataSet.
        DataSet ds = new DataSet();
        cmd1.Fill(ds, "universitycollage");

        //Create a second DataAdapter for the Titles table.
        MySqlDataAdapter cmd2 = new MySqlDataAdapter("select * from department where typs='University'", cnn);
        cmd2.Fill(ds, "department");
        //Create the relation bewtween the Category and Sub-Category tables.
        ds.Relations.Add("myrelation",
        ds.Tables["universitycollage"].Columns["id"],
        ds.Tables["department"].Columns["collageid"]);

        return ds;
        cnn.Close();
    }
    public DataSet getFullCollege()
    {
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["mySQLconn"].ConnectionString;
        MySqlConnection cnn = new MySqlConnection(conn);
        MySqlDataAdapter cmd1 = new MySqlDataAdapter("select * from universitycollage where typs='college'", cnn);

        //Create and fill the DataSet.
        DataSet ds = new DataSet();
        cmd1.Fill(ds, "universitycollage");

        //Create a second DataAdapter for the Titles table.
        MySqlDataAdapter cmd2 = new MySqlDataAdapter("select * from department where typs='college'", cnn);
        cmd2.Fill(ds, "department");

        //Create the relation bewtween the Category and Sub-Category tables.
        ds.Relations.Add("myrelations",
        ds.Tables["universitycollage"].Columns["id"],
        ds.Tables["department"].Columns["collageid"]);

        return ds;
        cnn.Close();
    }
  
    public void Delete(string query)
    {
        Connection connect = new Connection();
        connect.ExecStatement(query);
        connect.Dispose();
        connect = null;
    }
    public int Id
    {
        get { return _Id; }
        set { _Id = value; }
    }

    public int College_id
    {
        get { return _College_id; }
        set { _College_id = value; }
    }
    public int Deparment_id 
    {
        get { return _Deparment_id; }
        set { _Deparment_id = value; }
    }
    public string Branch_name
    {
        get { return _Branch_name; }
        set { _Branch_name = value; }
    }
    public string Duration
    {
        get { return _Duration; }
        set { _Duration = value; }
    }

    public bool HasValue
    {
        get;
        set;
    }
}
