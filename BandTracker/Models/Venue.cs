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
