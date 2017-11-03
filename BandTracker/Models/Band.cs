using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace BandTracker.Models
{
  public class Band
  {
    public string Name {get; private set;}
    public int Id {get; private set;}

    public Band(string name, int id = 0)
    {
      Name = name;
      Id = id;
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

    public bool HasSamePropertiesAs(Band other)
    {
      return (
        this.Id == other.Id &&
        this.Name == other.Name);
    }

    // CREATE

    public void Save()
     {
       MySqlConnection conn = DB.Connection();
       conn.Open();

       var cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText = @"INSERT INTO bands (band_name) VALUES (@band_name);";

       MySqlParameter name = new MySqlParameter();
       name.ParameterName = "@band_name";
       name.Value = this.Name;
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

    public static List<Band> GetAll()
    {
      List<Band> allBands = new List<Band>();

      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"SELECT * FROM bands;";
      MySqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int bandId = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);

        Band newBand = new Band(bandName, bandId);
        allBands.Add(newBand);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allBands;
    }

    // DESTROY

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
  }
}
