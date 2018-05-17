using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace BandTracker.Models
{
  public class Venues
  {
    private string _venuesName;
    private string _location;
    private int _id;

    public Venues(string venuesName, string location, int id = 0)
    {
      _venuesName = venuesName;
      _location = location;
      _id = id;
    }
    public string GetVenuesName()
    {
      return _venuesName;
    }
    public void SetVenuesName(string newVenuesName)
    {
      _venuesName = newVenuesName;
    }
    public int GetId()
    {
      return _id;
    }
    public string Getlocation()
    {
      return _location;
    }
    public void Setlocation(string newlocation)
    {
      _location = newLocation;
    }
    public override bool Equals(System.Object otherVenues)
    {
      if (!(otherVenues is Venues))
      {
        return false;
      }
      else
      {
        Venues newVenues = (Venues) otherVenues;
        bool idEquality = this.GetId() == newVenues.GetId();
        bool venuesNameEquality = this.GetVenuesName() == newVenues.GetVenuesName();
        bool locationEquality = this.GetLocation() == newVenues.GetLocation();
        return (idEquality && venuesNameEquality && locationEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetVenuesName().GetHashCode();
    }

    public static List<Venues> GetAllVenuess()
    {
      List<Venues> allVenues = new List<Venues> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM Venues;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int venuesId = rdr.GetInt32(0);
        string venuesName = rdr.GetString(1);
        string venuesLocation = rdr.GetString(2);
        Venues newVenues = new Venues(venuesName, venuesLocation, VenuesId);
        allVenues.Add(newVenues);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allVenues;
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO venues (venuesName, location) VALUES (@thisVenuesName, @thisVenueslocation);";

      cmd.Parameters.Add(new MySqlParameter("@thisVenuesVenuesName", _VenuesName));
      cmd.Parameters.Add(new MySqlParameter("@thisVenueslocation", _location));

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

    }
    public static Venues Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM Venuess WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterVenuesName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int venuesId = 0;
      string venuesName = "";
      string venueslocation = "";

      while(rdr.Read())
      {
        venuesId = rdr.GetInt32(0);
        venuesVenuesName = rdr.GetString(1);
        venueslocation = rdr.GetString(2);
      }
      Venues newVenues = new Venues(venuesName, venueslocation, venuesId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newVenues;
    }
    public void AddVenues(Venues newVenues)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO venuess_bands (venues_id, band_id) VALUES (@venuesId, @bandId);";

      cmd.Parameters.Add(new MySqlParameter("@venuesId", _id));
      cmd.Parameters.Add(new MySqlParameter("@bandId", newBand.GetId()));

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public List<Venues> GetVenues()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT venues.* FROM bands
      JOIN venuess_bands ON (venues.id=venuess_bands.Venues_id)
      JOIN bands ON (venues_bands.venues_id=bands.id)
      WHERE venues.id=@VenuesId;";

      cmd.Parameters.Add(new MySqlParameter("@VenuesId", _id));

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Venues> venues = new List<Venues> {};
      while(rdr.Read())
      {
        int venuesId = rdr.GetInt32(0);
        string venuesName = rdr.GetString(1);
        Venues newVenues = new Venues(venuesName, venuesId);
        venues.Add(newVenues);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return venues;
    }
    public void UpdateVenuesName(string newVenuesName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE venues SET venuesName = @newVenuesName WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterVenuesName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter VenuesName = new MySqlParameter();
      VenuesName.ParameterVenuesName = "@newVenuesName";
      VenuesName.Value = newVenuesName;
      cmd.Parameters.Add(VenuesName);

      cmd.ExecuteNonQuery();
      _venuesName = newVenuesName;
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
      cmd.CommandText = @"DELETE FROM venues;";
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
      cmd.CommandText = @"DELETE FROM venuess WHERE id = @venuesId; DELETE FROM venuess_bands WHERE venues_id = @venuesId;";

      MySqlParameter venuesIdParameter = new MySqlParameter();
      venuesIdParameter.ParameterVenuesName = "@venuesId";
      venuesIdParameter.Value = this.GetId();
      cmd.Parameters.Add(venuesIdParameter);

      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
