using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace BandTracker.Models
{
  public class Band
  {
    // public int _id {get; private set;}
    // public string _bandName {get; private set;}

    private int _id;
    private string _bandName;


    public Band(string BandName, int Id = 0)
    {
      _id = Id;
      _bandName = BandName;
    }

    public string GetBandName()
    {
      return _bandName;
    }

    public int GetBandId()
    {
      return _id;
    }

    public static List<Band> GetAll()
    {
      List<Band> allBands = new List<Band> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM bands;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int bandId = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        Band newBand = new Band(bandName, bandId);
        allBands.Add(newBand);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allBands;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `bands` (`name`) VALUES (@BandName);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@BandName";
      name.Value = this._bandName;
      cmd.Parameters.Add(name);

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

    //  public override bool Equals(System.Object otherBand)
    //  {
    //    Band newBand = (Band) otherBand;
    //    return this.GetBandName().Equals(newBand.GetBandName());
    //  }

   public override bool Equals(System.Object otherBand)
    {
      if (!(otherBand is Band))
      {
        return false;
      }
      else
      {
        Band newBand = (Band) otherBand;
        bool idEquality = (this.GetBandId() == newBand.GetBandId());
        bool nameEquality = (this.GetBandName() == newBand.GetBandName());
        return (idEquality && nameEquality);
      }
    }

    public override int GetHashCode()
    {
         return this.GetBandName().GetHashCode();
    }

    public static Band Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `bands` WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int bandId = 0;
      string bandName = "";

      while (rdr.Read())
      {
        bandId = rdr.GetInt32(0);
        bandName = rdr.GetString(1);
      }

      Band foundBand= new Band(bandName, bandId);  // This line is new!

       conn.Close();
       if (conn != null)
       {
        conn.Dispose();
       }

       return foundBand;  // This line is new!
     }

    public void UpdateName(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE bands SET name = @newName WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _bandName = newName;
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public void AddVenue(Venue newVenue)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO bands_venues (venue_id, band_id) VALUES (@VenueId, @BandId);";

      MySqlParameter venue_id = new MySqlParameter();
      venue_id.ParameterName = "@VenueId";
      venue_id.Value = newVenue.GetVenueId();
      cmd.Parameters.Add(venue_id);

      MySqlParameter band_id = new MySqlParameter();
      band_id.ParameterName = "@BandId";
      band_id.Value = _id;
      cmd.Parameters.Add(band_id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public List<Venue> GetVenues()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT venue_id FROM bands_venues WHERE band_id = @bandId;";

      MySqlParameter bandIdParameter = new MySqlParameter();
      bandIdParameter.ParameterName = "@bandId";
      bandIdParameter.Value = _id;
      cmd.Parameters.Add(bandIdParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      List<int> venueIds = new List<int> {};
      while(rdr.Read())
      {
          int venueId = rdr.GetInt32(0);
          venueIds.Add(venueId);
      }
      rdr.Dispose();

      List<Venue> venues = new List<Venue> {};
      foreach (int venueId in venueIds)
      {
          var venueQuery = conn.CreateCommand() as MySqlCommand;
          venueQuery.CommandText = @"SELECT * FROM venues WHERE id = @VenueId;";

          MySqlParameter venueIdParameter = new MySqlParameter();
          venueIdParameter.ParameterName = "@VenueId";
          venueIdParameter.Value = venueId;
          venueQuery.Parameters.Add(venueIdParameter);

          var venueQueryRdr = venueQuery.ExecuteReader() as MySqlDataReader;
          while(venueQueryRdr.Read())
          {
              int thisVenueId = venueQueryRdr.GetInt32(0);
              string venueName = venueQueryRdr.GetString(1);
              Venue foundVenue = new Venue(venueName, thisVenueId);
              venues.Add(foundVenue);
          }
          venueQueryRdr.Dispose();
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return venues;
    }



    // public List<Venue> GetVenues()
    // {
    //   List<Venue> venues = new List<Venue> {};
    //   return venues;
    // }
  }
}
