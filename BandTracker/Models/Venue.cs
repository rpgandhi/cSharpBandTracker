using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace BandTracker.Models
{
  public class Venue
  {
    private int _id;
    private string _venueName;


  public Venue(string VenueName, int Id = 0)
  {
    _id = Id;
    _venueName = VenueName;
  }

  public string GetVenueName()
  {
    return _venueName;
  }

  public int GetVenueId()
  {
    return _id;
  }

  public static List<Venue> GetAll()
  {
    List<Venue> allVenues = new List<Venue> {};
    MySqlConnection conn = DB.Connection();
    conn.Open();
    MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"SELECT * FROM venues;";
    MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
    while(rdr.Read())
    {
      int venueId = rdr.GetInt32(0);
      string venueName = rdr.GetString(1);
      Venue newVenue = new Venue(venueName, venueId);
      allVenues.Add(newVenue);
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
    cmd.CommandText = @"INSERT INTO `venues` (`name`) VALUES (@VenueName);";

    MySqlParameter name = new MySqlParameter();
    name.ParameterName = "@VenueName";
    name.Value = this._venueName;
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
     cmd.CommandText = @"DELETE FROM venues;";

     cmd.ExecuteNonQuery();

     conn.Close();
     if (conn != null)
     {
         conn.Dispose();
     }
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
        bool idEquality = (this.GetVenueId() == newVenue.GetVenueId());
        bool nameEquality = (this.GetVenueName() == newVenue.GetVenueName());
        return (idEquality && nameEquality);
      }
    }

    public override int GetHashCode()
    {
         return this.GetVenueName().GetHashCode();
    }

    public static Venue Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `venues` WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int venueId = 0;
      string venueName = "";

      while (rdr.Read())
      {
          venueId = rdr.GetInt32(0);
          venueName = rdr.GetString(1);
      }

      Venue foundVenue= new Venue(venueName, venueId);  // This line is new!

       conn.Close();
       if (conn != null)
       {
           conn.Dispose();
       }

      return foundVenue;
    }

    public void UpdateName(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE venues SET name = @newName WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _venueName = newName;
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public List<Band> GetBands()
    {
      //Task = Band
      //Category = Venue
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var bandQuery = conn.CreateCommand() as MySqlCommand;
      bandQuery.CommandText = @"SELECT bands.* FROM venues
          JOIN bands_venues ON (venues.id = bands_venues.venue_id)
          JOIN bands ON (bands_venues.band_id = bands.id)
          WHERE venues.id = @VenueId;";

      MySqlParameter venueIdParameter = new MySqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = _id;
      bandQuery.Parameters.Add(venueIdParameter);

      var rdr = bandQuery.ExecuteReader() as MySqlDataReader;
      List<Band> bands = new List<Band>{};

      while(rdr.Read())
      {
        int bandId = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        Band newBand = new Band(bandName, bandId);
        bands.Add(newBand);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return bands;
    }

    public void Delete()
    {
      //Task = Venue
      //Category = Band
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM venues WHERE id = @VenueId; DELETE FROM bands_venues WHERE venue_id = @VenueId;";

      MySqlParameter venueIdParameter = new MySqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = this.GetVenueId();
      cmd.Parameters.Add(venueIdParameter);

      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }

    public void AddBand(Band newBand)
    {
      //Category = Venue
      //Task = Band
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO bands_venues (venue_id, band_id) VALUES (@VenueId, @BandId);";

      MySqlParameter venue_id = new MySqlParameter();
      venue_id.ParameterName = "@VenueId";
      venue_id.Value = _id;
      cmd.Parameters.Add(venue_id);

      MySqlParameter band_id = new MySqlParameter();
      band_id.ParameterName = "@BandId";
      band_id.Value = newBand.GetBandId();
      cmd.Parameters.Add(band_id);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }
  }
}
