using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace BandTracker.Models
{
  public class Venue
  {
    public string VenueName {get; private set;}
    public int Id {get; private set;}

    public Venue(string name, int id = 0)
    {
      VenueName = name;
      Id = id;
    }

    public override bool Equals(System.Object otherVenue)
    {
      if (!(otherVenue is Venue))
      {
        return false;
      }
      else
      {
        Venue newVenue = (Venue) otherVenue;
        bool idEquality = (this.Id == newVenue.Id);
        bool nameEquality = (this.VenueName == newVenue.VenueName);
        return (idEquality && nameEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.Id.GetHashCode();
    }

    public bool HasSamePropertiesAs(Venue other)
    {
      return (
        this.Id == other.Id &&
        this.VenueName == other.VenueName);
    }

    // CREATE
    public void Save()
       {
         MySqlConnection conn = DB.Connection();
         conn.Open();

         var cmd = conn.CreateCommand() as MySqlCommand;
         cmd.CommandText = @"INSERT INTO venues (venue_name) VALUES (@venue_name);";

         MySqlParameter name = new MySqlParameter();
         name.ParameterName = "@venue_name";
         name.Value = this.VenueName;
         cmd.Parameters.Add(name);

         cmd.ExecuteNonQuery();
         Id = (int) cmd.LastInsertedId;

         conn.Close();
         if (conn != null)
         {
           conn.Dispose();
         }
       }

    // READ
    public static Venue FindById(int searchId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM venues WHERE id = @VenueId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@VenueId";
      thisId.Value = searchId;
      cmd.Parameters.Add(thisId);

      int venueId = 0;
      string venueName = "";

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        venueId = rdr.GetInt32(0);
        venueName = rdr.GetString(1);
      }
      Venue output = new Venue(venueName, venueId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return output;
    }

    public static List<Venue> GetAll()
    {
      List<Venue> allVenues = new List<Venue>();

      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"SELECT * FROM venues;";
      MySqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);

        Venue newVenue = new Venue(venueName, venueId);
        allVenues.Add(newVenue);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allVenues;
    }

    // UPDATE
    public void Update(Venue newVenue)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE venues SET venue_name = @NewName WHERE id = @VenueId;";

      MySqlParameter newName = new MySqlParameter();
      newName.ParameterName = "@NewName";
      newName.Value = newVenue.VenueName;
      cmd.Parameters.Add(newName);

      MySqlParameter venueId = new MySqlParameter();
      venueId.ParameterName = "@VenueId";
      venueId.Value = this.Id;
      cmd.Parameters.Add(venueId);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    // DESTROY
    public static void ClearAll()
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
  }
}
