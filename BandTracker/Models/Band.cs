using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BandTracker.Models
{
  public class Band
  {
    public int Id {get; private set;}
    public string Name {get; private set;}
    public int VenueId {get; private set;}

    public Band(string name, int venueId = 0, int id = 0)
    {
      Name = name;
      VenueId = venueId;
      Id = id;
    }

    // See all Band's belonging to a Venue
    public static List<Band> GetAll()
    {
      List<Band> output = new List<Band> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM bands;";

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int venueId = rdr.GetInt32(2);
        Band newBand = new Band(name, venueId, id);
        output.Add(newBand);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return output;
    }

    public List<Venue> GetVenues()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT venues.* FROM bands JOIN bands_venues ON (band_id = bands_venues.venue_id) JOIN venues ON (bands_venues.venue_id = venue_id) WHERE band_id = @BandId;";

      MySqlParameter categoryIdParameter = new MySqlParameter();
      categoryIdParameter.ParameterName = "@BandId";
      categoryIdParameter.Value = Id;
      cmd.Parameters.Add(categoryIdParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Venue> venues = new List<Venue>{};

      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueDescription = rdr.GetString(1);
        Venue newVenue = new Venue(venueDescription, venueId);
        venues.Add(newVenue);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return venues;
    }

    public static void ClearAll()
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

    public static Band Find(int searchId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM bands WHERE id = @BandId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@BandId";
      thisId.Value = searchId;
      cmd.Parameters.Add(thisId);

      int bandId = 0;
      string bandName = "";
      int venueId = 0;

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        bandId = rdr.GetInt32(0);
        bandName = rdr.GetString(1);
        venueId = rdr.GetInt32(2);
      }
      Band output = new Band(bandName, venueId, bandId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return output;
    }

    // Adds new band to a specific Venue. Cannot add a Band if no Venue Id is entered.
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO bands (band_name, venue_id) VALUES (@Name, @venue_id);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@Name";
      name.Value = this.Name;
      cmd.Parameters.Add(name);

      MySqlParameter venueId = new MySqlParameter();
      venueId.ParameterName = "@venue_id";
      venueId.Value = this.VenueId;
      cmd.Parameters.Add(venueId);

      cmd.ExecuteNonQuery();
      this.Id = (int)cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public bool HasSamePropertiesAs(Band other)
    {
      return (
        this.Id == other.Id &&
        this.Name == other.Name &&
        this.VenueId == other.VenueId);
    }

    public override bool Equals(System.Object otherBand)
    {
      if (!(otherBand is Band))
      {
        return false;
      }
      else
      {
        Band newBand = (Band) otherBand;
        bool idEquality = (this.Id == newBand.Id);
        bool nameEquality = (this.Name == newBand.Name);
        return (idEquality && nameEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.Id.GetHashCode();
    }
  }
}
