using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace BandTracker.Models
{
  public class Bands
  {
    private string _bandName;
    private int _id;

    public Bands(string bandName, int id = 0)
    {
      _bandName = bandName;
      _id = id;
    }
    public string GetBandName()
    {
      return _bandName;
    }
    public void SetBandName(string bandName)
    {
      _bandName = newBandName;
    }
    public int GetId()
    {
      return _id;
    }
    public override bool Equals(System.Object otherBandName)
    {
      if (!(otherBandName is bandName))
      {
        return false;
      }
      else
      {
        BandName newBandName = (BandName) otherBandName;
        bool idEquality = this.GetId() == newBandName.GetId();
        bool nameEquality = this.GetName() = newBandName.GetBandName();
        return (idEquality && nameEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetBandName().GetHashCode;
    }
    public static List<BandName> GetAllBandName()
    {
      List<BandName> allBandName = new List<BandName> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM bandName;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int bandNameId = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        BandName newBandName = new BandName(bandName, bandNameId);
        allBandName.Add(newBandName);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allBandName;
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO nameName (bandName) VALUES (@thisBandName);";

      MySqlParameter BandName = new MySqlParameter();
      name.Parameter BandName = "@BandName";
      bandName.Value = _Bandname;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static BandName Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM authors WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int bandNameId = 0;
      string bandName = "";

      while(rdr.Read())
      {
        bandNameId = rdr.GetInt32(0);
        bandName = rdr.GetString(1);
      }
      BandName newBandName = new BandName(bandName, bandNameId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newBandName;
    }
    public List<BandName> GetBandName()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT books.* FROM authors
      JOIN BandName_Venues ON (bandName_id=BandName_BandName.Venue_id)
      JOIN books ON (bandName_bandName.bandName_id=books.id)
      WHERE bandName_id=@AuthorId;";

      cmd.Parameters.Add(new MySqlParameter("@BandNameId", _id));

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<BandName> BandName = new List<BandName> {};
      while(rdr.Read())
      {
        int bookId = rdr.GetInt32(0);
        string BandName = rdr.GetString(1);
        string BandName = rdr.GetString(2);
        BandName newBandName = new BandName(bandName, venue, bookId);
        bandName.Add(newbandName);
      }
      rdr.Dispose();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return bandName;
    }
    public void AddbandName(BandName newBandName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO bandName_venues (bandName_id, venue_id) VALUES (@bandNameId, @venueId);";

      cmd.Parameters.Add(new MySqlParameter("@bandNameId", _id));
      cmd.Parameters.Add(new MySqlParameter("@venueId", newbandName.GetId()));

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM bands;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM bandName WHERE id = @bandNameId; DELETE FROM BandName_venues WHERE bandName_id = @bandNameId;";

      MySqlParameter authorIdParameter = new MySqlParameter();
      bandNameIdParameter.ParameterName = "@bandNameId";
      bandNameIdParameter.Value = this.GetId();
      cmd.Parameters.Add(authorIdParameter);

      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
